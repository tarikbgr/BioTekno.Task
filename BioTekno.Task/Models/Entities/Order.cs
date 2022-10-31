namespace BioTekno.Task.Models.Entities;

public class Order : BaseModel
{
    public Order()
    {
        OrderDetails = new HashSet<OrderDetail>();
    }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerGSM { get; set; }
    public decimal TotalAmount { get; set; }

    virtual public  ICollection<OrderDetail>? OrderDetails { get; set; }
}