namespace BioTekno.Task.Models.Entities
{
    public class Product : BaseModel
    {
        public string Description { get; set; }
        public string Category { get; set; }
        public int Unit { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
