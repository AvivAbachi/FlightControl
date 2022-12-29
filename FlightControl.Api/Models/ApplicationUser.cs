using Microsoft.AspNetCore.Identity;

namespace FlightControl.Api.Models
{
    public enum Role { Default, Admin };
    public class ApplicationUser : IdentityUser
    {
        public Role Role { get; set; }
    }
}
