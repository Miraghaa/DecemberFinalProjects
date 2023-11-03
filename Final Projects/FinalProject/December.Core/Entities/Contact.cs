namespace December.Core.Entities;

public class Contact : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Phone { get; set; }
    public string Message { get; set; }

}
