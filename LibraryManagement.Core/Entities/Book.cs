namespace LibraryManagement.Core.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Isbn { get; private set; }
        public uint PublicationYear { get; private set; }
    }
}
