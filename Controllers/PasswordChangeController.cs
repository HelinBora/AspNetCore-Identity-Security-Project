using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using NotikaIdentityEmail.Entities;
using NotikaIdentityEmail.Models.ForgetPasswordModels;

namespace NotikaIdentityEmail.Controllers
{
    public class PasswordChangeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public PasswordChangeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if (string.IsNullOrEmpty(forgetPasswordViewModel.Email))
            {
                ModelState.AddModelError("", "Email alanı boş olamaz.");
                return View();
            }

            var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Email);

            if (user == null)
            {
                // Debug için
                ViewBag.Error = $"Kullanıcı bulunamadı: {forgetPasswordViewModel.Email}";
                return View();
            }

            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var passwordResetTokenLink = Url.Action("ResetPassword", "PasswordChange", new
            {
                userId = user.Id,
                token = passwordResetToken
            }, HttpContext.Request.Scheme);

            // Mail gönderme kodu...

            ViewBag.Message = "Şifre sıfırlama bağlantısı gönderildi.";
            return View();
        }



        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            TempData["userId"] = userId; 
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var userid = TempData["userId"];
            var token = TempData["token"];

            if(userid == null || token == null)
            {
                ViewBag.v = "Hata Oluştu";
            }
            var user = await _userManager.FindByIdAsync(userid.ToString());
            var result = await _userManager.ResetPasswordAsync(user,token.ToString(), resetPasswordViewModel.Password);
            if(result.Succeeded)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            return View();
        }

    }
}
