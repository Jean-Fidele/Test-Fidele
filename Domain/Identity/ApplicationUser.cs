using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Locale { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastLoginOn { get; set; }
        public DateTime LastRefreshedOn { get; set; }
        public Guid SupplierId { get; set; }
    }
}
