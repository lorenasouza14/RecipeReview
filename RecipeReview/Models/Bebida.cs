namespace RecipeReview.Classes
{
    public class Bebida : Receita
    {
       public bool Alcoolica { get; set; }
       public int Ml {  get; set; }

        public Bebida() 
        {
            Tipo = "Bebida";
        }
    }
}
