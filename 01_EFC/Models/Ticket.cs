using _01_EFC.Models.Entities;

namespace _01_EFC.Models;

internal class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Created { get; set; }
    public string Status { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNummber { get; set; }

    public ICollection<CommentEntity> Comment { get; set; } = null!;
}
