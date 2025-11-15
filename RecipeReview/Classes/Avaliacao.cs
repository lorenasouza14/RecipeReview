namespace RecipeReview.Classes
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public string Nota { get; set; } // 1 a 5
        public string Comentario { get; set; }

        public int UsuarioId { get; set; }
        public int ReceitaId { get; set; }

        public Avaliacao(string nota, string comentario)
        {
            Nota = nota;
            Comentario = comentario;
        }

    }

    
}
