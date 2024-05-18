namespace BigDinner.Application.Common.Models;

public class RedisSetting
{
    public const string SectionName = "Redis";
    public string ConnectionString { get; set; }
}
