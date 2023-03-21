using Domain;

namespace Test_Fidele.Services
{
    public class Metier : IMetier
    {
        public Metier() 
        { 
        
        }

        public Categorie charger()
        {
            return new Categorie { Code = 5, Libelle = "Dix neuf"};
        }
    }
}
