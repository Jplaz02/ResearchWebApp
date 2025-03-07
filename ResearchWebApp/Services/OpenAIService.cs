using ResearchWebApp.Models;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;


public class OpenAIService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenAIService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiKey = ""; // Replace with your API Key
    }

    public async Task<string> GenerateText(string prompt)
    {
        var request = new
        {
            model = "gpt-4o",  // You can use a newer version if necessary
            messages = new[] { new { role = "user", content = prompt } },
            max_tokens = 16384,
            temperature = 1.0
        };

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
        requestMessage.Headers.Add("Authorization", $"Bearer {_apiKey}");
        requestMessage.Content = JsonContent.Create(request);

        try
        {
            var response = await _httpClient.SendAsync(requestMessage);

            // Log the response status code and headers for debugging
            Console.WriteLine($"Response Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Headers: {response.Headers}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Full API Response: {jsonResponse}"); // Log the full response for debugging

                try
                {
                    var result = JsonDocument.Parse(jsonResponse);
                    var choicesElement = result.RootElement.GetProperty("choices");

                    if (choicesElement.GetArrayLength() > 0)
                    {
                        var message = choicesElement[0]
                            .GetProperty("message")
                            .GetProperty("content")
                            .GetString();

                        return message;
                    }
                    else
                    {
                        return "No valid choices found in the response.";
                    }
                }
                catch (JsonException jsonEx)
                {
                    // Handle any JSON parsing errors
                    Console.WriteLine($"Error parsing JSON: {jsonEx.Message}");
                    return $"Error parsing JSON response: {jsonEx.Message}";
                }
            }
            else
            {
                // Handle non-success responses
                var errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error Response: {errorResponse}");
                return $"Error: {response.StatusCode}, {errorResponse}";
            }
        }
        catch (Exception ex)
        {
            // Log any other exceptions
            Console.WriteLine($"Exception: {ex.Message}");
            return "Error: Unable to generate text due to an exception.";
        }
    }


}
