namespace December.Business.Options;

public class EmailConfigOptions
{
    public string From { get; set; }
    public string SmptServer { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
