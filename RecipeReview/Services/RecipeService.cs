using System.Net.Http;
using System.Text.Json;
using static RecipeReview.Models.MealModel;

public class RecipeService
{
    private readonly HttpClient _http;

    public RecipeService(HttpClient http)
    {
        _http = http;
    }

    private readonly string baseUrl = "https://www.themealdb.com/api/json/v1/1/";

    // 🔎 Buscar receitas por ingrediente
    public async Task<List<MealSummary>> BuscarPorIngrediente(string ingrediente)
    {
        var response = await _http.GetAsync($"{baseUrl}filter.php?i={ingrediente}");
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<MealList>(json);

        return result?.meals ?? new List<MealSummary>();
    }

    // 📌 Buscar detalhes completos por ID
    public async Task<MealDetails?> BuscarDetalhes(string id)
    {
        var response = await _http.GetAsync($"{baseUrl}lookup.php?i={id}");
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<MealDetailsList>(json);

        return result?.meals?.FirstOrDefault();
    }
}
