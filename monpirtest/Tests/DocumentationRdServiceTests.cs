using Microsoft.EntityFrameworkCore;
using monpirtest.Model;
using monpirtest.Service;
using monpirtest.Storage;
using Xunit;

namespace monpirtest.Tests;

public class DocumentationRdServiceTests
{
    private readonly DbContextOptions<PirDbContext> _options = new DbContextOptionsBuilder<PirDbContext>()
        .UseInMemoryDatabase(databaseName: "test_database")
        .Options;
    

    [Fact]
    public void GetDocumentationRdById_ExistingId_ShouldReturnDocumentationRd()
    {
        // Arrange
        using (var context = new PirDbContext(_options))
        {
            var service = new DocumentationRdService(context);
            var documentationRd = new DocumentationRd
                { Id = Guid.NewGuid(), Stamp = new StampRd(), Number = 1, ObjectId = Guid.NewGuid() };
            context.DocumentationRd.Add(documentationRd);
            context.SaveChanges();

            // Act
            var result = service.GetDocumentationRdById(documentationRd.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(documentationRd.Id, result.Id);
            Assert.Equal(documentationRd.Number, result.Number);
        }
    }

    [Fact]
    public void GetDocumentationRdById_NonExistingId_ShouldReturnNull()
    {
        // Arrange
        using (var context = new PirDbContext(_options))
        {
            var service = new DocumentationRdService(context);

            // Act
            var result = service.GetDocumentationRdById(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }
    }

    [Fact]
    public void CreateDocumentationRd_InvalidData_ShouldThrowArgumentException()
    {
        // Arrange
        using (var context = new PirDbContext(_options))
        {
            var service = new DocumentationRdService(context);
            var documentationRd = new DocumentationRd
                { Id = Guid.NewGuid(), Stamp = null, Number = 0, ObjectId = Guid.Empty };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.CreateDocumentationRd(documentationRd));
        }
    }
}