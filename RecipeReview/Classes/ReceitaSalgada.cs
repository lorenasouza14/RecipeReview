namespace RecipeReview.Classes
{
    public class ReceitaSalgada : Receita
    {
        

        public ReceitaSalgada(string nome, string descricao, string modoPreparo, string tipo) : base(nome, descricao, modoPreparo, tipo)
        {
            Tipo = "Salgada";
        }
    }
}
