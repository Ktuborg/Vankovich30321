using Microsoft.AspNetCore.Identity;

namespace Vankovich30321.UI.Models
{
    public class AppUser : IdentityUser
    {
		public byte[] Avatar { get; set; }
		public string MimeType { get; set; } = string.Empty;
	}
}
