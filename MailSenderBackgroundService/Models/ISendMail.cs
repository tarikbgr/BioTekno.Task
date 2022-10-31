namespace SendMail;

public interface ISendMail
{
    int OrderId { get; set; }
    string CustomerEmail { get; set; }
}