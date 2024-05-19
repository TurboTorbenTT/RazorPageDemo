// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using WebApplication1.Data;

namespace WebApplication1.Areas.Identity.Pages.Account
{
  [AllowAnonymous]
  public class RegisterConfirmationModel : PageModel
  {
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailSender _sender;

    public RegisterConfirmationModel(UserManager<IdentityUser> userManager, IEmailSender sender,
      ApplicationDbContext dbContext)
    {
      _userManager = userManager;
      _sender = sender;
      _dbContext = dbContext;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public bool DisplayConfirmAccountLink { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string EmailConfirmationUrl { get; set; }

    public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
    {
      if (email == null)
      {
        return RedirectToPage("/Index");
      }

      returnUrl = returnUrl ?? Url.Content("~/");


        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
          return NotFound($"Unable to load user with email '{email}'.");
        }

        Email = email;
        // Once you add a real email sender, you should remove this code that lets you confirm the account
        DisplayConfirmAccountLink = true;
        if (DisplayConfirmAccountLink)
        {
          var userId = await _userManager.GetUserIdAsync(user);
          var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
          code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
          EmailConfirmationUrl = Url.Page(
            "/Account/ConfirmEmail",
            pageHandler: null,
            values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
            protocol: Request.Scheme);
        }

        using var client = new HttpClient();
        var userType = (await _userManager.GetClaimsAsync(user)).First(c => c.Type == nameof(UserType));

        var response = await client.PostAsJsonAsync("http://localhost:5124/api/account", new AccountDto());
        if (response.IsSuccessStatusCode is false)
        {
          throw new Exception("Failed to post user");
        }

      return Page();
    }
  }
}