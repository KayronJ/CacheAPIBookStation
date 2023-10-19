using BookStation.Models;

namespace BookStation.Services
{
    public static class BookService
    {
        static List<Book> Books { get; }
        static int nextId = 3;
        static BookService()
        {
            Books = new List<Book>
            {
                new Book {Id = 1, Name = "Lord of the Rings", IsHardCover = false},
                new Book {Id = 2, Name = "Harry Potter and the Philosopher's Stone", IsHardCover = true}
            };
        }

        public static List<Book> GetAll() => Books;

        public static Book? Get(int id) => Books.FirstOrDefault(b => b.Id == id);

        public static void Add(Book book)
        {
            book.Id = nextId++;
            Books.Add(book);
        }

        public static void Delete(int id) 
        {
            var book = Get(id);
            if (book is null)
                return;

            Books.Remove(book);
        }

        public static void Update (Book book)
        {
            var index = Books.FindIndex(b => b.Id == book.Id);
            if(index == -1) 
                return;

            Books[index] = book;
        }
    }
}
