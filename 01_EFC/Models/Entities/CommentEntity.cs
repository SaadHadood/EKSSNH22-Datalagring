namespace _01_EFC.Models.Entities;

internal class CommentEntity
{
    public int Id { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime Created { get; set; }

    public int TicketId { get; set; }
    public TicketEntity Ticket { get; set; } = null!;
}