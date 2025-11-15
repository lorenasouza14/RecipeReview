namespace RecipeReview.Classes
{
    public class ReceitaDoce : Receita
    {
        public ReceitaDoce(string nome, string descricao, string modoPreparo, string tipo) : base(nome, descricao, modoPreparo, tipo)
        {
            Tipo = "Doce";
        }
    }
}
