using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Mvc;

namespace SiteRBC.Controllers.Support
{
    public class SupportController : Controller
    {
		private readonly IConfiguration _configuration;

		public SupportController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpPost]
		public async Task<IActionResult> SendMessage(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				return BadRequest("Message cannot be empty.");
			}

			try
			{
				// Отримання конфігурації SMTP
				var smtpConfig = _configuration.GetSection("Smtp");
				var emailMessage = new MimeMessage();

				emailMessage.From.Add(new MailboxAddress("Support Chat", smtpConfig["Email"]));
				emailMessage.To.Add(new MailboxAddress("Support", "roshirabuilding@gmail.com"));
				emailMessage.Subject = "Support Chat Message";
				emailMessage.Body = new TextPart("plain")
				{
					Text = message
				};

				using (var client = new SmtpClient())
				{
					await client.ConnectAsync(smtpConfig["Host"], int.Parse(smtpConfig["Port"]), MailKit.Security.SecureSocketOptions.StartTls);
					await client.AuthenticateAsync(smtpConfig["Email"], smtpConfig["Password"]);
					await client.SendAsync(emailMessage);
					await client.DisconnectAsync(true);
				}

				return Ok(new { success = true, message = "Your message has been sent successfully!" });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { success = false, message = ex.Message });
			}
		}
		public IActionResult GeneralSupportPage()
        {
            return View();
        }
    }
}
