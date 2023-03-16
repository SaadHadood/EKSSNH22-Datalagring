namespace _01_EFC.Models.Entities;

internal class StatusEntity
{
    public int Id { get; set; }
    public string Status { get; set; } = null!;

    public virtual ICollection<TicketEntity> Tickets { get; set; } = null!;
}
