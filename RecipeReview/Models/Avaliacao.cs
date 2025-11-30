namespace RecipeReview.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public string Comentario { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public int ReceitaId { get; set; }
        public Receita? Receita { get; set; }



    }

    
}
