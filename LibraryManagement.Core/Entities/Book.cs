namespace LibraryManagement.Core.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Isbn { get; private set; }
        public int PublicationYear { get; private set; }
        public virtual List<LoanBook> Loans { get; private set; } = [];

        public Book(string title, string author, string isbn, int publicationYear)
        {
            Title = title.Trim();
            Author = author.Trim();
            Isbn = isbn.Trim();
            PublicationYear = publicationYear;
        }

        public void Update(string title, string author, string isbn, int publicationYear)
        {
            Title = title.Trim();
            Author = author.Trim();
            Isbn = isbn.Trim();
            PublicationYear = publicationYear;
        }
    }
}
