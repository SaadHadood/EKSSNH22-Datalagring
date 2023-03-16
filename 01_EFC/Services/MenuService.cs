using _01_EFC.Models;
using _01_EFC.Models.Entities;

namespace _01_EFC.Services;

internal class MenuService
{

    public async Task MainMenu()
    {
        Console.Clear();
        Console.WriteLine("Välkommen!");
        Console.WriteLine("");
        Console.WriteLine("Vad vill du göra?");
        Console.WriteLine("");
        Console.WriteLine("1. Skapa nytt ärende");
        Console.WriteLine("");
        Console.WriteLine("2. Visa alla ärenden");
        Console.WriteLine("");
        Console.WriteLine("3. Visa specifikt ärende med alla detaljer (Status och kommenterar)");
        Console.WriteLine("");
        Console.WriteLine("4. Byt status på ärende");
        Console.WriteLine("");
        Console.WriteLine("5. Skriv kommentar till ärende");
        Console.WriteLine("");
        Console.WriteLine("6. Visa ärende och dess kommentarer");
        Console.WriteLine("");
        Console.WriteLine("7. Avsluta programmet");
        Console.WriteLine("");
        Console.Write("Välj ett av alternativen ovan (1-7): ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Console.Clear();
                await CreateNewTicketAsync();
                break;

            case "2":
                Console.Clear();
                await ListAllTicketsAsync();
                break;


            case "3":
                Console.Clear();
                await ListSpecificTicketAsync();
                break;

            case "4":
                Console.Clear();
                await UpdateTicketStatus();
                break;

            case "5":
                Console.Clear();
                await AddCommentTicketAsync();
                break;

            case "6":
                Console.Clear();
                await ListSpecificTicketAndCommentAsync();
                break;

            case "7":
                Environment.Exit(0);
                return;
            default:
                Console.Clear();
                Console.WriteLine("Felaktig input. Försök igen.");
                break;
                
        }

        Console.WriteLine("\nTryck på valfri knapp för att fortsätta...");
        Console.ReadKey();

    }


    public static async Task CreateNewTicketAsync()
    {
        var ticket = new Ticket();

        Console.Write("Title: ");
        ticket.Title = Console.ReadLine() ?? "";

        Console.Write("Description: ");
        ticket.Description = Console.ReadLine() ?? "";

        Console.WriteLine("");
        Console.WriteLine("Fyll i dina uppgifter");
        Console.WriteLine("");

        Console.Write("FirstName: ");
        ticket.FirstName = Console.ReadLine() ?? "";

        Console.Write("LastName: ");
        ticket.LastName = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        ticket.Email = Console.ReadLine() ?? "";

        Console.Write("PhoneNummber: ");
        ticket.PhoneNummber = Console.ReadLine() ?? "";

        await TicketService.SaveAsync(ticket);
    }





    public static async Task ListAllTicketsAsync()
    {

        var tickets = await TicketService.GetAllAsync();

        if (tickets.Any())
        {
            foreach (Ticket ticket in tickets)
            {
                Console.WriteLine($"Ticket Id: {ticket.Id}");
                Console.WriteLine($"Title: {ticket.Title}");
                Console.WriteLine($"Description: {ticket.Description}");
                Console.WriteLine($"Ticket created: {ticket.Created}");
                Console.WriteLine($"Name: {ticket.FirstName} {ticket.LastName}");
                Console.WriteLine($"Email: {ticket.Email}");
                Console.WriteLine($"Phonenumber: {ticket.PhoneNummber}");
                Console.WriteLine($"Status: {ticket.Status}");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("Inga kunder finns i databasen.");
            Console.WriteLine("");
        }

    }



    public static async Task ListSpecificTicketAsync()
    {
        Console.Write("Enter Ticket Id: ");
        var ticketIdStr = Console.ReadLine();

        if (!int.TryParse(ticketIdStr, out int ticketId))
        {
            Console.WriteLine("Invalid input. Please enter a valid Ticket Id.");
            Console.WriteLine("");
            return;
        }

        var ticket = await TicketService.GetAsync(ticketId);

        if (ticket != null)
        {
            Console.Clear();
            Console.WriteLine($"Ticket Id: {ticket.Id}");
            Console.WriteLine($"Title: {ticket.Title}");
            Console.WriteLine($"Description: {ticket.Description}");
            Console.WriteLine($"Ticket created: {ticket.Created}");
            Console.WriteLine("");
            Console.WriteLine("Ärendet skapat av: ");
            Console.WriteLine($"Name: {ticket.FirstName} {ticket.LastName}");
            Console.WriteLine($"Email: {ticket.Email}");
            Console.WriteLine($"Phonenumber: {ticket.PhoneNummber}");
            Console.WriteLine("");
            Console.WriteLine($"Status: {ticket.Status}");
            Console.WriteLine("");
            Console.WriteLine("kommentarer till ärendet: ");
            Console.WriteLine("");
            foreach (var commentt in ticket.Comment)
            {
                Console.WriteLine($"Comment: {commentt.Comment}");
                Console.WriteLine($"Comment created: {commentt.Created}");
            }
            Console.WriteLine("");
        }
        else
        {
            Console.Clear();
            Console.WriteLine($"No ticket with Id <{ticketId}> was found.");
            Console.WriteLine("");
        }
    }






    public static async Task UpdateTicketStatus()
    {

        Console.Write("Enter status ID: ");
        int id = int.Parse(Console.ReadLine()!);

        if (id > 0)
        {
            var status = await TicketService.GetStatusAsync(id);

            if (status != null)
            {
                Console.Write("Status: ");
                status.Status = Console.ReadLine() ?? null!;

                await TicketService.UpdateStatusAsync(status);
            }
            else
            {
                Console.WriteLine($"Ticket Id <{id}> not found.");
                Console.WriteLine("");
            }
        }

        else
        {
            Console.WriteLine($"No Id.");
            Console.WriteLine("");
        }

    }




    public static async Task AddCommentTicketAsync()
    {

        var comment = new CommentEntity();

        Console.Write("Enter ticket id: ");
        int ticketId = int.Parse(Console.ReadLine()!);

        if (ticketId > 0)
        {
            comment.TicketId = ticketId;
        }

        Console.Write("Enter comment text: ");
        string commentText = Console.ReadLine()!;

        if (commentText != null)
        {
            comment.Comment = commentText;
        }

        await TicketService.AddCommentToTicket(ticketId, commentText!);
        
    }





    public static async Task ListSpecificTicketAndCommentAsync()
    {
        Console.Write("Enter Ticket ID: ");
        string input = Console.ReadLine()!;
        Console.Clear();

        if (int.TryParse(input, out int id))
        {
            var comment = await TicketService.GetTicketAndComment(id);

            if (comment != null)
            {
                Console.WriteLine($"Ticket Id: {comment.Id}");
                Console.WriteLine($"Title: {comment.Title}");
                Console.WriteLine($"Description: {comment.Description}");
                Console.WriteLine("");

                foreach (var commentt in comment.Comments)
                {
                    Console.WriteLine($"Comment: {commentt.Comment} ----- Created: {commentt.Created}");
                }
            }
            else
            {
                Console.WriteLine($"No ticket belonging to this id <{id}> was found.");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine($"Invalid input: '{input}' is not a valid ID.");
            Console.WriteLine("");
        }
    }



}
