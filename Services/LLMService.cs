using LLM_Interaction.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LLM_Interaction.Services
{
    public static class LLMService
    {
        public static async Task<string> ExecutePromptAsync(Provider provider, Model model, string promptText)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", provider.ApiKey);

                var requestBody = new
                {
                    model = model.Name,
                    prompt = promptText,
                    max_tokens = 150
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(provider.ApiUrl, content);
                //var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                var responseString = await response.Content.ReadAsStringAsync();

                return responseString; // You can parse and format this as needed
            }
            //if (provider.Name == "OpenAI")
            //{
            //    return await ExecuteOpenAIPromptAsync(provider, model, promptText);
            //}
            //else if (provider.Name == "Microsoft Copilot")
            //{
            //    return await ExecuteGeminiPromptAsync(provider, model, promptText);
            //}
            //else
            //{
            //    throw new ArgumentException("Unsupported AI Provider Type");
            //}
        }
        private static async Task<string> ExecuteOpenAIPromptAsync(Provider provider, Model model, string promptText)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", provider.ApiKey);

                var requestBody = new
                {
                    model = model.Name, // e.g., "gpt-3.5-turbo-0125"
                    messages = new[] { new { role = "user", content = promptText } },
                    max_tokens = 150
                };
                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(provider.ApiUrl, content); // provider.ApiUrl for OpenAI should be "https://api.openai.com/v1/chat/completions"
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        private static async Task<string> ExecuteGeminiPromptAsync(Provider provider, Model model, string promptText)
        {
            using (var client = new HttpClient())
            {
                // API Key in URL query parameter
                string requestUrl = $"{provider.ApiUrl}?key={provider.ApiKey}"; // provider.ApiUrl for Gemini should be "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent"

                // Gemini's request body structure
                var requestBody = new
                {
                    contents = new[]
                    {
                new
                {
                    parts = new[]
                    {
                        new { text = promptText }
                    }
                }
            }
                    // Add generationConfig, safetySettings as needed for Gemini
                    // e.g., generationConfig = new { maxOutputTokens = 150 }
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(requestUrl, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}