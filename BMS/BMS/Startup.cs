using BMS_Base.Config;
using Consul;

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
                builder
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        services.Configure<ConsulConfig>(_configuration.GetSection("ConsulConfig"));
        _configuration.Bind("ConsulConfig", ConsulConfig.Instance);
        Console.WriteLine( ConsulConfig.Instance);
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
        app.UseAuthorization();
        app.UseEndpoints(x =>
        {
            x.MapControllers();
            x.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        });
        app.UseCors("AllowAllOrigin");
        //配置Consul
        RegisterConsul(appLifetime);
    }

    private void RegisterConsul(IHostApplicationLifetime appLifetime)
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
}