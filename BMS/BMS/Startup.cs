using BMS_Base.Config;
using BMS_Base.Interface;
using BMS_Db.EfContext;
using Consul;
using Microsoft.EntityFrameworkCore;
using NLog.Fluent;
using System.Reflection;
using System.Reflection.Emit;
using NLog.Extensions.Logging;
using Ys.Tools.MiddleWare;
using Ys.Tools.Config;

namespace BMS;

public class Startup
{

    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        this._configuration = configuration;
    }
    // This method gets called by the runtime. Use this method to add services to the container.
    // 该方法由运行时调用，使用该方法向DI容器添加服务
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigin", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();;
            });
        });
        RegisterNLog(services);
        //注入数据库
        RegisterDb(services);
        //自动实现类注入
        RegisterIBll(services);
        //获取consulConfig
        RegisterConsul(services);
        //获取TokenConfig
        RegisterToken(services);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    // 该方法由运行时调用，使用该方法配置HTTP请求管道
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRouting();
        //UseCors 必须放在 之后 UseRouting 和之前 UseAuthorization。
        //这是为了确保 CORS 标头包含在已授权和未经授权的调用的响应中。
        app.UseCors("AllowAllOrigin");
        app.UseAuthorization();
        app.UseAuthentication();
        app.UseMiddleware<ExceptionMiddleWare>();

        app.UseEndpoints(x =>
        {
            x.MapControllers();
            x.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        });
        //配置Consul
        RegisterConsul(appLifetime);
    }
    /// <summary>
    /// 注册consul
    /// </summary>
    /// <param name="appLifetime"></param>
    private static void RegisterConsul(IHostApplicationLifetime appLifetime)
    {
        using var client = new ConsulClient(x => x.Address = new Uri("http://127.0.0.1:8500"));
        var check = new AgentServiceCheck()
        {
            DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务停止后，5s开始接触注册
            HTTP = ConsulConfig.Instance.CheckApi,//健康检查
            Interval = TimeSpan.FromSeconds(10),//每10s轮询一次健康检查
            Timeout = TimeSpan.FromSeconds(5),
        };
        var service = new AgentServiceRegistration()
        {
            Checks = new[] { check },
            ID = Guid.NewGuid().ToString(),
            Name = ConsulConfig.Instance.ServiceName,
            Port = ConsulConfig.Instance.Port,
            Address = ConsulConfig.Instance.Address
        };
        client.Agent.ServiceRegister(service).Wait();
        appLifetime.ApplicationStopped.Register(() =>
        {
            Console.WriteLine("服务停止中");
            using var consulClient = new ConsulClient(x => x.Address = new Uri("http://127.0.0.1:8500"));
            consulClient.Agent.ServiceDeregister(service.ID).Wait();
        });
    }

    /// <summary>
    /// 自动依赖注入
    /// </summary>
    /// <param name="service"></param>
    private static void RegisterIBll(IServiceCollection service)
    {
        //获取程序下所有的程序集
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        assemblies.ForEach(assembly =>
        {
            //获取跟BMS相关的类
            var types = assembly.GetTypes().Where(x=>x is { FullName: { }, IsClass: true } && x.FullName.Contains("BMS")).ToList();
            types.ForEach(type =>
            {
                var list = type.GetInterfaces().Where(x=>x==typeof(IBll)).ToList();
                if(list.Count>0)
                    service.AddScoped(type);
                var staticBll = type.GetInterfaces().Where(x => x == typeof(IStaticBll)).ToList();
                if (staticBll.Count > 0)
                    service.AddSingleton(type);
            });
        });
    }

    /// <summary>
    /// 注入数据库
    /// </summary>
    /// <param name="service"></param>
    private  void RegisterDb(IServiceCollection service)
    {
        service.Configure<DbConfig>(_configuration.GetSection("DbConfig"));
        _configuration.Bind("DbConfig", DbConfig.Instance);
        Console.WriteLine("DbConfig：" + DbConfig.Instance);
        //已经注入，可以直接使用
        service.AddDbContext<BmsV1DbContext>(opt =>
        {
            opt.UseSqlServer(DbConfig.Instance.SqlServer);
        });
    }

    /// <summary>
    /// 获取consulConfig
    /// </summary>
    /// <param name="service"></param>
    private void RegisterConsul(IServiceCollection service)
    {
        //获取配置文件信息
        service.Configure<ConsulConfig>(_configuration.GetSection("ConsulConfig"));
        _configuration.Bind("ConsulConfig", ConsulConfig.Instance);
    }

    /// <summary>
    /// 配置Nlog
    /// </summary>
    /// <param name="service"></param>
    private static void RegisterNLog(IServiceCollection service)
    {
        service.AddLogging(log =>
        {
            log.AddNLog();
        });
    }
    /// <summary>
    /// 注入Token
    /// </summary>
    /// <param name="service"></param>
    private  void RegisterToken(IServiceCollection service)
    {
        service.Configure<TokenConfig>(_configuration.GetSection("TokenConfig"));
        _configuration.Bind("TokenConfig", TokenConfig.Instance);
        Console.WriteLine("TokenConfig：" + TokenConfig.Instance);
   
    }


}