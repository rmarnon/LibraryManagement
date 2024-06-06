﻿using MediatR;

namespace LibraryManagement.Application.Commands.Books
{
    public class UpdateBookCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public int PublicationYear { get; set; }
    }
}
