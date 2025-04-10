using CoreLayer.DTOs.Users;
using CoreLayer.Services.Users;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Weblog_New.Pages.Auth
{
    [ValidateAntiForgeryToken]
    [BindProperties]
    public class LoginModel : PageModel
    {
       private readonly IUserService _userService;
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }
        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        [BindProperty]
        public string UserName { get; set; }

        [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
        [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 5 کاراکتر باشد")]
        [BindProperty]
        public string Password { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            var user = _userService.LoginUser(new LoginUserDto()
            {
                Password = Password,
                UserName = UserName
            });
            if(user==null)
            {
                ModelState.AddModelError("UserName", "کاربر نامعتبر است");
                return Page();
            }
            List<Claim> claims = new List<Claim>()
            {
                new Claim("test","test"),
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.FullName),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            HttpContext.SignInAsync(claimPrincipal, properties);
            return RedirectToPage("../Index");
        }
    }
}
