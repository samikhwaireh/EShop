using Microsoft.AspNetCore.Identity;

namespace EShop.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        //public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public async Task<IdentityResult> SetPasswordHashAsync(string password, UserManager<ApplicationUser> userManager)
        {
            var hashedPassword = userManager.PasswordHasher.HashPassword(this, password);
            var result = await userManager.UpdateAsync(this);
            if (result.Succeeded)
            {
                PasswordHash = hashedPassword;
                await userManager.UpdateAsync(this);
            }
            return result;
        }

        public bool VerifyPassword(string password, UserManager<ApplicationUser> userManager)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var result = passwordHasher.VerifyHashedPassword(this, PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
