using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.CustomValidation
{
    public class CustomPasswordValidator:IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                if (!user.Email.Contains(user.UserName))
                {
                    errors.Add(new IdentityError() { Code = "PassworContainsUserName", Description = "Şifre alanı kullanıcı adı içeremez" });
                }
            }
            if (password.ToLower().Contains("1234"))
            {
                errors.Add(new IdentityError() { Code = "PassworContains1234", Description = "Şifre alanı ardışık sayı içeremez" });
            }

            if (password.ToLower().Contains(user.Email.ToLower()))
            {
                errors.Add(new IdentityError() { Code = "PassworContainsEmail", Description = "Şifre alanı Email içeremez" });
            }

            if (errors.Count == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
        }
    }
}
