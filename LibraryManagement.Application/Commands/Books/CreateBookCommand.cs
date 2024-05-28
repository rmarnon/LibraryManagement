using MediatR;

namespace LibraryManagement.Application.Commands.Books
{
    public class CreateBookCommand : IRequest
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Isbn { get; private set; }
        public uint PublicationYear { get; private set; }
    }
}
