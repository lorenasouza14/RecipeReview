namespace RecipeReview.Classes
{
    public class Bebida : Receita
    {
       public bool Alcoolica { get; set; }
       public int Ml {  get; set; }

        public Bebida(string nome, string descricao, string modoPreparo, string tipo) : base(nome, descricao, modoPreparo, tipo)
        {
            Tipo = "Bebida";
        }
    }
}
