using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDinner.Application.Features.Authentication
{
    public record TokenModel
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiresOnUtc { get; set; }
        public ApplicationUser User { get; set; } = null!;
    }
}
