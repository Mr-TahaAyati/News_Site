using CoreLayer.DTOs.Users;
using CoreLayer.Services.Users;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

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
            var result = _userService.LoginUser(new LoginUserDto()
            {
                Password = Password,
                UserName = UserName
            });
            if(result.Status == OperationResultStatus.NotFound)
            {
                ModelState.AddModelError("UserName", "کاربر نامعتبر است");
                return Page();
            }
            return RedirectToPage("../Index");
        }
    }
}
