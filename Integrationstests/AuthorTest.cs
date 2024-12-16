using Applikation.Authors.Commands.AddAuthor;
using Applikation.Authors.Commands.DeleteAuthor;
using Applikation.Authors.Commands.UpdateAuthor;
using Applikation.Authors.Queries.GetAll;
using Applikation.Authors.Queries.GetById;
using Applikation.Interfaces.RepositoryInterfaces;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Integrationstests
{
    public class AuthorTest
    {
        [Fact]
        public async Task Handle_AddAuthor_ShouldReturnSuccess_WhenAuthorIsAdded()
        {
            // Arrange
            var fakeRepository = A.Fake<IAuthorRepository>();
            var fakeLogger = A.Fake<ILogger<AddAuthorCommandHandler>>();
            var handler = new AddAuthorCommandHandler(fakeRepository, fakeLogger);

            var newAuthor = new Author { Name = "Test Author" };
            var addedAuthor = new Author { Id = 1, Name = "Test Author" };

            A.CallTo(() => fakeRepository.AddAuthor(A<Author>.Ignored)).Returns(Task.FromResult(addedAuthor));

            var command = new AddAuthorCommand(newAuthor);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.NotNull(result.Data);
            Assert.Equal(addedAuthor.Id, result.Data.Id);
        }
        [Fact]
        public async Task Handle_UpdateAuthor_ShouldReturnSuccess_WhenAuthorExists()
        {
            // Arrange
            var fakeRepository = A.Fake<IAuthorRepository>();
            var fakeLogger = A.Fake<ILogger<UpdateAuthorCommandHandler>>();
            var handler = new UpdateAuthorCommandHandler(fakeRepository, fakeLogger);

            var updatedAuthor = new Author { Id = 1, Name = "Updated Name" };
            var existingAuthor = new Author { Id = 1, Name = "Existing Name" };

            A.CallTo(() => fakeRepository.GetAuthorById(existingAuthor.Id)).Returns(Task.FromResult(existingAuthor));
            A.CallTo(() => fakeRepository.UpdateAuthor(A<Author>.Ignored)).Returns(Task.FromResult(updatedAuthor));

            var command = new UpdateAuthorCommand(existingAuthor.Id, updatedAuthor);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(updatedAuthor.Name, result.Data.Name);
        }

        [Fact]
        public async Task Handle_DeleteAuthor_ShouldReturnSuccess_WhenAuthorExists()
        {
            // Arrange
            var fakeRepository = A.Fake<IAuthorRepository>();
            var fakeLogger = A.Fake<ILogger<DeleteAuthorCommandHandler>>();
            var handler = new DeleteAuthorCommandHandler(fakeRepository, fakeLogger);

            var authorId = 1;
            var existingAuthor = new Author { Id = authorId, Name = "Author to Delete" };

            A.CallTo(() => fakeRepository.GetAuthorById(authorId)).Returns(Task.FromResult(existingAuthor));
            A.CallTo(() => fakeRepository.DeleteAuthor(authorId)).DoesNothing();

            var command = new DeleteAuthorCommand(authorId);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.True(result.Data);
        }

        [Fact]
        public async Task Handle_GetAllAuthors_ShouldReturnAuthors_WhenAuthorsExist()
        {
            // Arrange
            var fakeRepository = A.Fake<IAuthorRepository>();
            var fakeLogger = A.Fake<ILogger<GetAllAuthorQueryHandler>>();
            var handler = new GetAllAuthorQueryHandler(fakeRepository, fakeLogger);

            var authors = new List<Author>
        {
            new Author { Id = 1, Name = "Author 1", },
            new Author { Id = 2, Name = "Author 2",}
        };

            A.CallTo(() => fakeRepository.GetAllAuthor()).Returns(Task.FromResult(authors));

            var query = new GetAllAuthorQuery();

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(authors.Count, result.Data.Count);
        }

        [Fact]
        public async Task Handle_GetAuthorById_ShouldReturnAuthor_WhenAuthorExists()
        {
            // Arrange
            var fakeRepository = A.Fake<IAuthorRepository>();
            var fakeLogger = A.Fake<ILogger<GetAuthorByIdQueryHandler>>();
            var handler = new GetAuthorByIdQueryHandler(fakeRepository, fakeLogger);

            var authorId = 1;
            var author = new Author { Id = authorId, Name = "Test Author" };

            A.CallTo(() => fakeRepository.GetAuthorById(authorId)).Returns(Task.FromResult(author));

            var query = new GetAuthorByIdQuery(authorId);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(author.Name, result.Data.Name);
        }

        }
    }
