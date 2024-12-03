using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using Applikation.Authors.Commands.AddAuthor;
using Infrastructure.Database;
using Applikation.Authors.Commands.UpdateAuthor;
using Applikation.Authors.Commands.DeleteAuthor;
using Applikation.Authors.Queries.GetAll;
using Applikation.Authors.Queries.GetById;

namespace TestProject
{
    public class AuthorTest
    {
        [Test]
        public async Task Handle_ShouldAddAuthor_WhenValidAuthorProvided()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var handler = new AddAuthorCommandHandler(fakeDatabase);
            var newAuthor = new Author(0, "John Doe");

            var command = new AddAuthorCommand(newAuthor);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("John Doe", result.Name);
            Assert.AreEqual(4, fakeDatabase.Authors.Count);
            Assert.AreEqual("John Doe", fakeDatabase.Authors[3].Name);
        }

        [Test]
        public async Task Handle_ShouldReturnNull_WhenInvalidIdProvided()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var handler = new UpdateAuthorCommandHandler(fakeDatabase);

            var nonExistingAuthorId = 10;
            var updatedAuthor = new Author(nonExistingAuthorId, "Updated Author Name");

            var command = new UpdateAuthorCommand(nonExistingAuthorId, updatedAuthor);

            // Act 
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Handle_ShouldDeleteAuthor_WhenValidIdProvided()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var handler = new DeleteAuthorCommandHandler(fakeDatabase);

            var existingAuthorId = 1;

            // Act
            var result = await handler.Handle(new DeleteAuthorCommand(existingAuthorId), CancellationToken.None);

            // Assert
            Assert.IsTrue(result); 
            Assert.IsFalse(fakeDatabase.Authors.Exists(a => a.Id == existingAuthorId)); 
        }

        [Test]
        public async Task Handle_ShouldReturnAllAuthors_WhenCalled()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase(); 
            var handler = new GetAllAuthorQueryHandler(fakeDatabase); 

            // Act
            var result = await handler.Handle(new GetAllAuthorQuery(), CancellationToken.None);

            // Assert
            Assert.IsNotNull(result); 
            Assert.IsInstanceOf<List<Author>>(result); 
            Assert.AreEqual(fakeDatabase.Authors.Count, result.Count); 
        }
        [Test ]
        public async Task GetAuthorById_ValidId_ReturnsAuthor()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase(); 
            var handler = new GetAuthorByIdQueryHandler(fakeDatabase);
            var existingAuthorId = 1; 

            var query = new GetAuthorByIdQuery(existingAuthorId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result); 
            Assert.AreEqual(existingAuthorId, result.Id); 
            Assert.AreEqual("Anna", result.Name); 
        }


    }

}


    

   

    

