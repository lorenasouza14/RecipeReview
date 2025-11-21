namespace RecipeReview.Models
{
    public class Ingrediente
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public List<ReceitaIngrediente> Receitas { get; set; }

    }
}
