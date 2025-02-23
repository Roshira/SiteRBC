using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Options;
using SiteRBC.Models.AIModels;

namespace SiteRBC.Controllers.ProductCalc
{
    /**
    * @class AccountsController
    * @brief Controller isn't do.
    */
    public class ProductCalcController : Controller
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		public ProductCalcController(HttpClient httpClient, IOptions<OpenAIOptions> options)
		{
			_httpClient = httpClient;
			_apiKey = options.Value.ApiKey; // Отримуємо ключ з конфігурації
		}

		public IActionResult GeneralPageCalc()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> GetResponse(string userInput)
		{
			if (string.IsNullOrWhiteSpace(userInput))
			{
				ModelState.AddModelError("", "Запит не може бути порожнім.");
				return View("GeneralPageCalc", string.Empty);
			}

			var apiUrl = "https://api.openai.com/v1/chat/completions";
			var requestContent = new StringContent(JsonSerializer.Serialize(new
			{
				model = "GPT-4",
				messages = new[]
				{
			new { role = "user", content = userInput }
		}
			}), Encoding.UTF8, "application/json");

			_httpClient.DefaultRequestHeaders.Authorization =
				new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

			try
			{
				var response = await _httpClient.PostAsync(apiUrl, requestContent);
				if (response.IsSuccessStatusCode)
				{
					var responseData = await response.Content.ReadAsStringAsync();
					var result = JsonSerializer.Deserialize<ChatGptResponse>(responseData);

					return View("GeneralPageCalc", result.Choices[0].Message.Content);
				}
				else
				{
					// Логування статусу і вмісту відповіді
					var errorDetails = await response.Content.ReadAsStringAsync();
					ViewData["ErrorMessage"] = $"Помилка API: Статус {response.StatusCode}, Деталі: {errorDetails}";
				}
			}
			catch (Exception ex)
			{
				// Логування винятків
				ViewData["ErrorMessage"] = $"Сталася виняткова ситуація: {ex.Message}";
			}

			return View("GeneralPageCalc", "Сталася помилка під час отримання відповіді.");
		}
	}

	public class ChatGptResponse
	{
		public Choice[] Choices { get; set; }
	}

	public class Choice
	{
		public Message Message { get; set; }
	}

	public class Message
	{
		public string Content { get; set; }
	}
}