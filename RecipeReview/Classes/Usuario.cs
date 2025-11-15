namespace RecipeReview.Classes
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<Avaliacao> Avaliacoes { get; set; }
        public List<Receita> ReceitasCriadas { get; set; }

        public Usuario(string nome, string email)
        {
            Nome = nome;
            Email = email;
            Avaliacoes = new List<Avaliacao>();
            ReceitasCriadas = new List<Receita>();
        }
    }
}

