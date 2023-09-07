using Microsoft.EntityFrameworkCore;
using monpirtest.Model;
using monpirtest.Service;
using monpirtest.Storage;
using Xunit;

namespace monpirtest.Tests;

public class ObjectPirServiceTests
{
    private readonly DbContextOptions<PirDbContext> _options = new DbContextOptionsBuilder<PirDbContext>()
        .UseInMemoryDatabase(databaseName: "test_database")
        .Options;

    [Fact]
    public void GetObjectPirById_ExistingId_ShouldReturnObjectPir()
    {
        // Arrange
        using (var context = new PirDbContext(_options))
        {
            var service = new ObjectPirService(context);
            var objectPir = new ObjectPir { Id = Guid.NewGuid(), Name = "Object 1" };
            context.ObjectPir.Add(objectPir);
            context.SaveChanges();

            // Act
            var result = service.GetObjectPirById(objectPir.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(objectPir.Id, result.Id);
            Assert.Equal(objectPir.Name, result.Name);
        }
    }

    [Fact]
    public void GetObjectPirById_NonExistingId_ShouldReturnNull()
    {
        // Arrange
        using (var context = new PirDbContext(_options))
        {
            var service = new ObjectPirService(context);

            // Act
            var result = service.GetObjectPirById(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }
    }

    [Fact]
    public void AddDocumentationRd_InvalidObjectPirId_ShouldThrowArgumentException()
    {
        // Arrange
        using (var context = new PirDbContext(_options))
        {
            var service = new ObjectPirService(context);
            var documentationRd = new DocumentationRd { Id = Guid.NewGuid(), Number = 1 };
            context.DocumentationRd.Add(documentationRd);
            context.SaveChanges();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.AddDocumentationRd(Guid.NewGuid(), documentationRd.Id));
        }
    }

    [Fact]
    public void AddDocumentationRd_InvalidDocumentationRdId_ShouldThrowArgumentException()
    {
        // Arrange
        using (var context = new PirDbContext(_options))
        {
            var service = new ObjectPirService(context);
            var objectPir = new ObjectPir { Id = Guid.NewGuid(), Name = "Object 1" };
            context.ObjectPir.Add(objectPir);
            context.SaveChanges();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.AddDocumentationRd(objectPir.Id, Guid.NewGuid()));
        }
    }
}