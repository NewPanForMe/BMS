using BMS_SMS.Models;
using Ys.Tools.Interface;
using Ys.Tools.MoreTool;

namespace BMS_Db.BLL.Sms;

public class SmsBll:IBll
{
    /// <summary>
    /// 发送验证码
    /// </summary>
    /// <param name="phoneNumber">手机号</param>
    /// <param name="code">验证码</param>
    public async void SendSmsCode(string phoneNumber,string code)
    {
        var param = new Param()
        {
            code = code
        };
        SmsSendConfig.Instance = new SmsSendConfig()
        {
            SignName = "BMS",
            TemplateCode = "SMS_461325435",
            TemplateParam = JsonTools.Serialize(param)
        };
        await BMS_SMS.Base.BaseSms.Instance.Send(phoneNumber);
    }
  
    class Param
    {
        public string code { get; set; } = String.Empty;
    }
}