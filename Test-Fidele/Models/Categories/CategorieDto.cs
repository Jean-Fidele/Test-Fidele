namespace Test_Fidele.Models.Categories
{
    public class CategorieReq
    {
        public int Code { get; set; }
    }

    public class CategorieRep
    {
        public int Code { get; set; }
        public string Libelle { get; set; }
    }

    public class CategorieRepList
    {
        public CategorieRepList()
        {
            Categories = new List<CategorieRep>();
        }
        public int totale { get; set; }
        public List<CategorieRep> Categories { get; set; }
    }
}