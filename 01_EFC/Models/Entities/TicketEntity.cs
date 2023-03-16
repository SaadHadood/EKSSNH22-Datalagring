using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace _01_EFC.Models.Entities;

internal class TicketEntity
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Created { get; set; }

    [Required]
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;

    [Required]
    public int StatusId { get; set; }
    public StatusEntity Status { get; set; } = null!;

    public virtual ICollection<CommentEntity> Comments { get; set; } = null!;
}
