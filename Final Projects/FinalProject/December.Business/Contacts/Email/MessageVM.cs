using MimeKit;
namespace December.Business.Contacts.Email;

public class MessageVM
{
    public List<MailboxAddress> To { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public MessageVM(List<string> to, string subject, string content)
    {
        To = new List<MailboxAddress>();
        To.AddRange(to.Select(x => new MailboxAddress(string.Empty, x)));

        Subject = subject;
        Content = content;
    }

    public MessageVM(string to, string subject, string content)
       : this(new List<string> { to }, subject, content)
    {

    }
}
