using MediatR;

namespace LibraryManagement.Application.Commands.Books
{
    public class CreateBookCommand : IRequest<Unit>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public int PublicationYear { get; set; }
    }
}
