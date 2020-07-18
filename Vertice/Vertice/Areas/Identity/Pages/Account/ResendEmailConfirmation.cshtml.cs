// <copyright file="ResendEmailConfirmation.cshtml.cs" company="Thomas Castonguay-Gagnon">
// Copyright (c) Thomas Castonguay-Gagnon. All rights reserved.
// Licensed under the GPL3 license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Vertice.Areas.Identity.Data;

namespace Vertice.Areas.Identity.Pages.Account
{
    /// <summary>
    /// Email resend page
    /// </summary>
    [AllowAnonymous]
    public abstract class ResendEmailConfirmationModel : PageModel
    {
        private readonly UserManager<VerticeUser> _userManager;
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResendEmailConfirmationModel"/> class.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="emailSender"></param>
        public ResendEmailConfirmationModel(UserManager<VerticeUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                Input.Email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
            return Page();
        }

        /// <summary>
        /// User input model class
        /// </summary>
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
    }
}
