using System.ComponentModel.DataAnnotations;

namespace December.Core.Entities;

public class Adress : BaseEntity
{
    [Key]
    public override int Id { get => base.Id; set => base.Id = value; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Adresss { get; set; }
    public string State { get; set; }
    public string Postcode { get; set; }
    public string UserName { get; set; }
}
