using Ys.Tools.Config;

namespace BMS_Base.Config;

public record SystemConfig
{
    public static SystemConfig Instance { get; set; } = new SystemConfig();

    /// <summary>
    /// 账户锁定次数
    /// </summary>
    public int ErrorCount { get; set; }


}