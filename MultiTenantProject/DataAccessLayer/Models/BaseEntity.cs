using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models;

public class BaseEntity<TKey, EKey>
{
    [Key]
    public TKey Id { get; set; }

    [Required]
    public EKey TenantId { get; set; }
}