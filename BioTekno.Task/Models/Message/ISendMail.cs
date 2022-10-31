namespace SendMail;

public interface ISendMail
{
    int OrderId { get; set; }
    string CustomerEmail { get; set; }
}
public class SendMails :ISendMail
{
    public int OrderId { get; set; }
    public string CustomerEmail { get; set; }
}