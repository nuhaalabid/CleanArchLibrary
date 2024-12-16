using Xunit;
using Applikation.Books.Commands.AddBook;
using Applikation.Interfaces.RepositoryInterfaces;
using Microsoft.Extensions.Logging;
using Models;
using FakeItEasy;
using Applikation.Books.Queries.GetById;
using Applikation.Books.Commands.UpdateBook;
using Applikation.Books.Commands.DeleteBook;
using static Applikation.Books.Commands.AddBook.AddBookCommad;
using Applikation.Books.Queries.GetAll;


namespace Integrationstests
{
    public class BookTest
    {
        [Fact]
        public async Task Handle_ShouldAddBook_WhenCommandIsValid()
        {
            // Arrange
            var fakeRepository = A.Fake<IBookRepository>();
            var fakeLogger = A.Fake<ILogger<AddBookCommandHandler>>();
            var handler = new AddBookCommandHandler(fakeRepository, fakeLogger);

            var newBook = new Book
            {
                Title = "Test Book",
                Description = "A description of the test book."
            };
            var addedBook = new Book
            {
                Id = 1,
                Title = "Test Book",
                Description = "A description of the test book."
            };

            // Mocka AddBook-metoden
            A.CallTo(() => fakeRepository.AddBook(A<Book>.Ignored))
                .Returns(Task.FromResult(addedBook));

            var command = new AddBookCommand(newBook);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result); 
            Assert.True(result.IsSuccessful);
            Assert.Equal(addedBook.Title, result.Data.Title); 
            Assert.Equal(addedBook.Description, result.Data.Description); 
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenBookIsUpdated()
        {
            // Arrange
            var fakeRepository = A.Fake<IBookRepository>();
            var fakeLogger = A.Fake<ILogger<UpdateBookByIdCommandHandler>>();
            var handler = new UpdateBookByIdCommandHandler(fakeRepository, fakeLogger);

            var existingBook = new Book
            {
                Id = 1,
                Title = "Old Title",
                Description = "Old Description"
            };

            var updatedBook = new Book
            {
                Id = 1,
                Title = "New Title",
                Description = "New Description"
            };

            A.CallTo(() => fakeRepository.GetBookById(existingBook.Id))
                .Returns(Task.FromResult(existingBook));

            A.CallTo(() => fakeRepository.UpdateBook(A<Book>.Ignored))
                .Returns(Task.FromResult(updatedBook));

            var command = new UpdateBookByIdCommand(updatedBook, existingBook.Id);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.NotNull(result.Data); 
            Assert.Equal(updatedBook.Title, result.Data.Title); 
            Assert.Equal(updatedBook.Description, result.Data.Description); 
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenBookIsDeleted()
        {
            // Arrange
            var fakeRepository = A.Fake<IBookRepository>();
            var fakeLogger = A.Fake<ILogger<DeleteBookCommandHandler>>();
            var handler = new DeleteBookCommandHandler(fakeRepository, fakeLogger);

            var bookId = 1;

            // Mocka GetBookById-metoden för att returnera en befintlig bok
            A.CallTo(() => fakeRepository.GetBookById(bookId))
                .Returns(Task.FromResult(new Book { Id = bookId, Title = "Test Book" }));

            // Mocka DeleteBook-metoden så att den inte gör något
            A.CallTo(() => fakeRepository.DeleteBook(bookId)).DoesNothing();

            var command = new DeleteBookCommand(bookId);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.IsSuccessful); 
            Assert.True(result.Data); 
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenBooksExist()
        {
            // Arrange
            var fakeRepository = A.Fake<IBookRepository>();
            var fakeLogger = A.Fake<ILogger<GetAllBookQueryHandler>>();
            var handler = new GetAllBookQueryHandler(fakeRepository, fakeLogger);

            var books = new List<Book>
        {
             new Book { Id = 1, Title = "Test Book 1", Description = "Description 1" },
             new Book { Id = 2, Title = "Test Book 2", Description = "Description 2" }
        };

            A.CallTo(() => fakeRepository.GetAllBooks()).Returns(Task.FromResult(books));

            var query = new GetAllBookQuery();

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.NotNull(result); 
            Assert.True(result.IsSuccessful); 
            Assert.Equal(books, result.Data); 
        }


        [Fact]
        public async Task Handle_ShouldReturnBook_WhenBookIsFound()
        {
            // Arrange
            var fakeRepository = A.Fake<IBookRepository>();
            var fakeLogger = A.Fake<ILogger<GetBookByIdQueryHandler>>();
            var handler = new GetBookByIdQueryHandler(fakeRepository, fakeLogger);

            var bookId = 1;
            var book = new Book { Id = bookId, Title = "Test Book" };

            A.CallTo(() => fakeRepository.GetBookById(bookId))
                .Returns(Task.FromResult(book));

            var query = new GetBookByIdQuery(bookId);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.True(result.IsSuccessful); 
            Assert.NotNull(result.Data); 
            Assert.Equal(book, result.Data); 
        }

    }









}


