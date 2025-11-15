namespace RecipeReview.Classes
{
    public abstract class Receita
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<string> Ingredientes { get; set; }
        public string ModoPreparo { get; set; }
        public string Tipo { get; protected set; }

        //Pega as informações nutricionais da receita
        public InformacoesNutricionais InfoNutricional { get; set; } 


        public Receita(string nome, string descricao, string modoPreparo, string tipo)
        {
            Nome = nome;
            Descricao = descricao;
            ModoPreparo = modoPreparo;
            Tipo = tipo;
            Ingredientes = new List<string>();
        }
    }
}
