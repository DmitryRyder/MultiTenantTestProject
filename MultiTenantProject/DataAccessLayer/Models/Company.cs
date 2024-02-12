using System.Text.Json.Serialization;

namespace DataAccessLayer.Models;

public class Company : BaseEntity<Guid, Guid>
{
    public string Name { get; set; }

    public string Address { get; set; }

    [JsonIgnore]
    public ICollection<Client> Clients { get; set; }
}
