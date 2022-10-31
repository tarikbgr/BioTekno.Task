namespace BioTekno.Task.Models.Entities
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
    }
}