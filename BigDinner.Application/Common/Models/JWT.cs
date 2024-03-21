using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
