using December.Business.Contacts.Email;

namespace December.Business.Services.Interfaces;

public interface IEmailService
{
    public void Send(MessageVM messagevm);
}
