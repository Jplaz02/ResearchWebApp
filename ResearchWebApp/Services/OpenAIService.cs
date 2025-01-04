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
        _apiKey = "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"; // Replace with your API Key
    }

    public async Task<string> GenerateText(string prompt)
    {
        var request = new
        {
            model = "gpt-4o",  // Use the new model
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

            // Log the response status code and headers
            Console.WriteLine($"Response Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Headers: {response.Headers}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonDocument.Parse(jsonResponse);
                return result.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
            }
            else
            {
                // Read and log the error content for debugging
                var errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error Response: {errorResponse}");
                return $"Error: {response.StatusCode}, {errorResponse}";
            }
        }
        catch (Exception ex)
        {
            // Log the exception for debugging
            Console.WriteLine($"Exception: {ex.Message}");
            return "Error: Unable to generate text due to an exception.";
        }
    }
}
