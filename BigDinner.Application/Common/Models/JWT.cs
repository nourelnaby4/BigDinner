namespace BigDinner.Application.Common.Models
{
    public class JWT
    {
        public const string SectionName= "JWT";
        public string  Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
