using System.ComponentModel.DataAnnotations.Schema;

namespace BioTekno.Task.Models.Entities;

public class OrderDetail : BaseModel
{
    virtual public Order Order { get; set; }
    public int OrderId { get; set; }

    virtual public Product Product { get; set; }
    public int ProductId { get; set; }

    public decimal UnitPrice { get; set; }
}