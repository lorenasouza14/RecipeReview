using System.Text.Json.Serialization;

namespace RecipeReview.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public List<Avaliacao>? Avaliacoes { get; set; }
        [JsonIgnore]
        public List<Receita>? ReceitasCriadas { get; set; }

    }
}

