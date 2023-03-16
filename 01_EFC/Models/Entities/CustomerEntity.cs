using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_EFC.Models.Entities;

internal class CustomerEntity
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column(TypeName = "char(13)")]
    public string? PhoneNummber { get; set; }

    public ICollection<TicketEntity> TicketEntities = new HashSet<TicketEntity>();

}
