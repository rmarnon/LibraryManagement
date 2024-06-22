namespace LibraryManagement.Application.ViewModels
{
    public record BookViewModel
    {
        public string Title { get; private init; }
        public string Author { get; private init; }
        public string Isbn { get; private init; }
        public ushort PublicationYear { get; private init; }

        public BookViewModel(string title, string author, string isbn, ushort publicationYear)
        {
            Title = title;
            Author = author;
            Isbn = isbn;
            PublicationYear = publicationYear;
        }
    }
}
