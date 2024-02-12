using System.Text.Json.Serialization;

namespace DataAccessLayer.Models;

public class Client : BaseEntity<Guid, Guid>
{
    public string Name { get; set; }

    public Guid CompanyId { get; set; }

    [JsonIgnore]
    public Company Company { get; set; }
}
