using _01_EFC.Contexts;
using _01_EFC.Models;
using _01_EFC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace _01_EFC.Services;

internal class TicketService
{
    private static DataContext _context = new DataContext();


    public static async Task SaveAsync(Ticket ticket)
    {
        var _ticketEntity = new TicketEntity
        {
            Title = ticket.Title,
            Description = ticket.Description,
            Created = DateTime.Now
        };

        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == ticket.Email);
        
        if (customer != null)
            _ticketEntity.CustomerId = customer.Id;
        else
            _ticketEntity.Customer = new CustomerEntity
            {
                FirstName = ticket.FirstName,
                LastName = ticket.LastName,
                Email = ticket.Email,
                PhoneNummber = ticket.PhoneNummber
            };



        _ticketEntity.Status = new StatusEntity
        {
            Id = ticket.Id,
            Status = "Ej på Börjad"
        };


        _context.Add(_ticketEntity);
        await _context.SaveChangesAsync();

    }

    
    public static async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        var _tickets = new List<Ticket>();

        foreach (var _ticket in await _context.Tickets.Include(x => x.Customer).Include(x => x.Status).ToListAsync())
            _tickets.Add(new Ticket
            {
               Id = _ticket.Id,
               Title = _ticket.Title,
               Description = _ticket.Description,
               FirstName = _ticket.Customer.FirstName,
               LastName = _ticket.Customer.LastName,
               Email = _ticket.Customer.Email,
               PhoneNummber = _ticket.Customer.PhoneNummber,
               Created = _ticket.Created,
               Status = _ticket.Status.Status
            });

        return _tickets;
    }


    public static async Task<Ticket> GetAsync(int ticketId)
    {
        var _ticket = await _context.Tickets.Include(x => x.Customer).Include(x => x.Status).Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == ticketId);

        if (_ticket != null)
            return new Ticket
            {
                Id = _ticket.Id,
                Title = _ticket.Title,
                Description = _ticket.Description,
                Created = _ticket.Created,
                FirstName = _ticket.Customer.FirstName,
                LastName = _ticket.Customer.LastName,
                Email = _ticket.Customer.Email,
                PhoneNummber = _ticket.Customer.PhoneNummber,
                Status = _ticket.Status.Status,
                Comment = _ticket.Comments
            };
        else
            return null!;
    }




    public static async Task<StatusEntity> GetStatusAsync(int id)
    {
        var _status = await _context.Status.FirstOrDefaultAsync(x => x.Id == id);

        if (_status != null)
            return new StatusEntity
            {
                Id = _status.Id,

            };
        else return null!;
    }


    public static async Task UpdateStatusAsync(StatusEntity status)
    {
        var _status = await _context.Status.FirstOrDefaultAsync(x => x.Id == status.Id);

        if (_status != null)
        {
            _status.Status = status.Status;
        }
        else
            return;

        _context.Update(_status);
        await _context.SaveChangesAsync();

    }





    public static async Task AddCommentToTicket(int ticketId, string commentText)
    {
        using (var dbContext = new DataContext())
        {
            var ticket = await dbContext.Tickets.FirstOrDefaultAsync(x => x.Id == ticketId);

            if (ticket != null)
            {
                var _comment = new CommentEntity
                {
                    TicketId = ticketId,
                    Comment = commentText,
                    Created = DateTime.Now
                };

                dbContext.Add(_comment);
                await dbContext.SaveChangesAsync();
                Console.WriteLine("Comment added successfully");
            }
            else
            {
                Console.WriteLine($"No ticket with id {ticketId} was found ");
            }
        }
    }




    public static async Task<TicketEntity> GetTicketAndComment (int id)
    {
        var _ticket = await _context.Tickets.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);
        if (_ticket != null)
            return new TicketEntity
            {
                Id = _ticket.Id,
                Title = _ticket.Title,
                Description = _ticket.Description,
                Comments = _ticket.Comments,
                Created = _ticket.Created
            };

        else
            return null!;
    }





}

