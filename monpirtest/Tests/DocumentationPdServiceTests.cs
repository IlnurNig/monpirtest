using Microsoft.EntityFrameworkCore;
using monpirtest.Model;
using monpirtest.Service;
using monpirtest.Storage;
using Xunit;

namespace monpirtest.Tests;

public class DocumentationPdServiceTests
{
    private readonly DbContextOptions<PirDbContext> _options = new DbContextOptionsBuilder<PirDbContext>()
        .UseInMemoryDatabase(databaseName: "test_database")
        .Options;

    [Fact]
    public void GetDocumentationPdById_ExistingId_ShouldReturnDocumentationPd()
    {
        // Arrange
        using (var context = new PirDbContext(_options))
        {
            var service = new DocumentationPdService(context);
            var documentationPd = new DocumentationPd
                { Id = Guid.NewGuid(), Stamp = new StampPd(), Number = 1, ProjectId = Guid.NewGuid() };
            context.DocumentationPd.Add(documentationPd);
            context.SaveChanges();

            // Act
            var result = service.GetDocumentationPdById(documentationPd.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(documentationPd.Id, result.Id);
            Assert.Equal(documentationPd.Number, result.Number);
        }
    }

    [Fact]
    public void GetDocumentationPdById_NonExistingId_ShouldReturnNull()
    {
        // Arrange
        using (var context = new PirDbContext(_options))
        {
            var service = new DocumentationPdService(context);

            // Act
            var result = service.GetDocumentationPdById(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }
    }

    [Fact]
    public void CreateDocumentationPd_InvalidData_ShouldThrowArgumentException()
    {
        // Arrange
        using (var context = new PirDbContext(_options))
        {
            var service = new DocumentationPdService(context);
            var documentationPd = new DocumentationPd
                { Id = Guid.NewGuid(), Stamp = null, Number = 2, ProjectId = Guid.Empty };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.CreateDocumentationPd(documentationPd));
        }
    }
}