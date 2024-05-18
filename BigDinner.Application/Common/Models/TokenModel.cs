using BigDinner.Domain.Models.Base;

namespace BigDinner.Application.Common.Models
{
    public record TokenModel
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiresOnUtc { get; set; }
        public ApplicationUser User { get; set; } = null!;
    }
}
