using RecipeReview.Models;

namespace RecipeReview.Classes
{
    public class Receita
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string ModoPreparao { get; set; }

        public bool ContemLactose { get; set; }
        public bool ContemGluten { get; set; }
        public bool Vegetariana { get; set; }

        public string Tipo { get; protected set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }


        public List<ReceitaIngrediente> Ingredientes { get; set; }
        

    }
}
