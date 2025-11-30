namespace RecipeReview.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<Avaliacao>? Avaliacoes { get; set; }
        public List<Receita>? ReceitasCriadas { get; set; }

    }
}

