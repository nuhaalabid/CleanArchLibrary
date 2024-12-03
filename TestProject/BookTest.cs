using Infrastructure.Database;
using Applikation;
using Applikation.Books.Commands.AddBook;
using static Applikation.Books.Commands.AddBook.AddBookCommad;
using Models;
using Applikation.Books.Commands.DeleteBook;
using Applikation.Books.Commands.UpdateBook;
using System.Reflection.Metadata;
using NUnit;
using Applikation.Books.Queries.GetAll;
using Applikation.Books.Queries.GetById;

namespace TestProject
{
    public class Tests
    {
        private FakeDatabase _fakeDatabase;
        private GetAllBookQueryHandler _getAllHandler;
        private GetBookByIdQueryHandler _getByIdHandler;

        [SetUp]
        public void Setup()
        {
            _fakeDatabase = new FakeDatabase();
            _getAllHandler = new GetAllBookQueryHandler(_fakeDatabase);
            _getByIdHandler = new GetBookByIdQueryHandler(_fakeDatabase);

        }

        [Test]
        public async Task Handle_ShouldAddBook_WhenValidBookProvided()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var handler = new AddBookCommandHandler(fakeDatabase);

            var newBook = new Book(0, "New Book", "New Description", new Author(1, "Existing Author"));

            // Act
            var result = await handler.Handle(new AddBookCommand(newBook), CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("New Book", result.Title);
            Assert.IsTrue(fakeDatabase.Books.Exists(b => b.Title == "New Book"));
        }

        [Test]
        public async Task Handle_ShouldDeleteBook_WhenValidIdProvided()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var handler = new DeleteBookCommandHandler(fakeDatabase);
            var existingBookId = 1;

            // Act
            var result = await handler.Handle(new DeleteBookCommand(existingBookId), CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(fakeDatabase.Books.Exists(b => b.Id == existingBookId)); // Kontrollera att boken är borta
        }

        [Test]
        public async Task Handle_ShouldReturnNull_WhenInvalidUpdateBookByIdProvided()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var handler = new UpdateBookByIdCommandHandler(fakeDatabase);

            var nonExistingBookId = 12;
            var updatedBook = new Book(nonExistingBookId, "Updated Title", "Updated Description", new Author(2, "Updated Author"));

            var command = new UpdateBookByIdCommand(nonExistingBookId, updatedBook);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllBooks_ShouldReturnAllBooks_WhenBooksExist()
        {
            // Arrange
            var query = new GetAllBookQuery();

            // Act
            var result = await _getAllHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result); 
            Assert.AreEqual(5, result.Count);  
        }

        [Test]
        public async Task GetBookById_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            var query = new GetBookByIdQuery(1);  

            // Act
            var result = await _getByIdHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);  
            Assert.AreEqual(1, result.Id);  
            Assert.AreEqual("Sky", result.Title);  
        }

    }
}
    
    

    
