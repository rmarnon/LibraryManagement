namespace LibraryManagement.Application.ViewModels
{
    public record BookViewModel
    {
        public string Title { get; private init; }
        public string Author { get; private init; }
        public string Isbn { get; private init; }
        public uint PublicationYear { get; private init; }

        public BookViewModel(string title, string author, string isbn, uint publicationYear)
        {
            Title = title;
            Author = author;
            Isbn = isbn;
            PublicationYear = publicationYear;
        }
    }
}
