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
    public async Task<SmsResult> SendSmsCode(string phoneNumber,string code)
    {
        var param = new Param()
        {
            code = code
        };
        SmsSendConfig.Instance = new SmsSendConfig()
        {
            SignName = "袁赛的博客",
            TemplateCode = "SMS_461325435",
            TemplateParam = JsonTools.Serialize(param)
        };
        var smsResult = await BMS_SMS.Base.BaseSms.Instance.Send(phoneNumber);
        return smsResult;
    }
  
    class Param
    {
        public string code { get; set; } = String.Empty;
    }
}