using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TUBESPTCAHATAREMBULANSEJATI.Models;

namespace TUBESPTCAHATAREMBULANSEJATI.Services //namespace
{
    public static class ApiClient
    {
        private static readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5176/") };
        public static string? Token { get; private set; }
        public static string? Role { get; private set; }

        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public static void SetAuth(string token, string role)
        {
            Token = token;
            Role = role;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static void ClearAuth()
        {
            Token = null;
            Role = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public static async Task<(bool Success, string Message)> LoginAsync(string username, string password)
        {
            var loginDto = new LoginDto { Username = username, Password = password };
            var content = new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("api/auth/login", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var authData = JsonSerializer.Deserialize<AuthResponseDto>(responseData, _jsonOptions);
                    if (authData != null && !string.IsNullOrEmpty(authData.Token))
                    {
                        SetAuth(authData.Token, authData.Role);
                        return (true, "Login Successful");
                    }
                }
                return (false, "Invalid username or password.");
            }
            catch (Exception ex)
            {
                return (false, $"Error connecting to API: {ex.Message}");
            }
        }

        public static async Task<(bool Success, string Message)> RegisterAsync(string username, string email, string password, string role)
        {
            var registerDto = new RegisterDto
            {
                Username = username,
                Email = email,
                Password = password,
                Roles = new List<string> { role }
            };
            var content = new StringContent(JsonSerializer.Serialize(registerDto), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("api/auth/register", content);
                var responseData = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    return (true, "Registration Successful");
                }
                
                return (false, $"Registration failed: {response.StatusCode} - {responseData}");
            }
            catch (Exception ex)
            {
                return (false, $"Error connecting to API: {ex.Message}");
            }
        }

        public static async Task<List<Paket>> GetPaketsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/pakets");
                if (response.IsSuccessStatusCode)
                {
                    var dataString = await response.Content.ReadAsStringAsync();
                    using var document = JsonDocument.Parse(dataString);
                    var root = document.RootElement;
                    if (root.TryGetProperty("data", out var dataElement))
                    {
                        return JsonSerializer.Deserialize<List<Paket>>(dataElement.GetRawText(), _jsonOptions) ?? new List<Paket>();
                    }
                }
            }
            catch { }
            return new List<Paket>();
        }

        public static async Task<Paket?> GetPaketByResiAsync(string resi)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/pakets/resi/{resi}");
                if (response.IsSuccessStatusCode)
                {
                    var dataString = await response.Content.ReadAsStringAsync();
                    using var document = JsonDocument.Parse(dataString);
                    var root = document.RootElement;
                    if (root.TryGetProperty("data", out var dataElement))
                    {
                        return JsonSerializer.Deserialize<Paket>(dataElement.GetRawText(), _jsonOptions);
                    }
                }
            }
            catch { }
            return null;
        }

        public static async Task<(bool Success, string Message)> CreatePaketAsync(Paket paket)
        {
            var content = new StringContent(JsonSerializer.Serialize(paket), Encoding.UTF8, "application/json");
            try
            {
                var response = await _httpClient.PostAsync("api/pakets", content);
                if (response.IsSuccessStatusCode) return (true, "Paket created successfully");
                return (false, "Failed to create paket");
            }
            catch (Exception ex) { return (false, ex.Message); }
        }

        public static async Task<(bool Success, string Message)> UpdatePaketAsync(int id, Paket paket)
        {
            var content = new StringContent(JsonSerializer.Serialize(paket), Encoding.UTF8, "application/json");
            try
            {
                var response = await _httpClient.PutAsync($"api/pakets/{id}", content);
                if (response.IsSuccessStatusCode) return (true, "Paket updated successfully");
                return (false, "Failed to update paket");
            }
            catch (Exception ex) { return (false, ex.Message); }
        }

        public static async Task<(bool Success, string Message)> DeletePaketAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/pakets/{id}");
                if (response.IsSuccessStatusCode) return (true, "Paket deleted successfully");
                return (false, "Failed to delete paket");
            }
            catch (Exception ex) { return (false, ex.Message); }
        }
    }
}
