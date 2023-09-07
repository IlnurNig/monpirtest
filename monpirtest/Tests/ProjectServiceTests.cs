using Microsoft.EntityFrameworkCore;
using Xunit;
using monpirtest.Model;
using monpirtest.Service;
using monpirtest.Storage;

namespace monpirtest.Tests;

public class ProjectServiceTests
{
    private DbContextOptions<PirDbContext> GetDbContextOptions(string databaseName)
    {
        return new DbContextOptionsBuilder<PirDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;
    }

    [Fact]
    public void GetAllProjects_ShouldReturnAllProjects()
    {
        // Arrange
        var options = GetDbContextOptions("GetAllProjects_ShouldReturnAllProjects");
        using (var dbContext = new PirDbContext(options))
        {
            dbContext.Projects.Add(new Project { ProjectId = Guid.NewGuid(), Name = "Project 1", Code = "Code 1" });
            dbContext.Projects.Add(new Project { ProjectId = Guid.NewGuid(), Name = "Project 2", Code = "Code 2" });
            dbContext.SaveChanges();
        }

        using (var dbContext = new PirDbContext(options))
        {
            var projectService = new ProjectService(dbContext);

            // Act
            var projects = projectService.GetAllProjects();

            // Assert
            Assert.Equal(2, projects.Count());
        }
    }

    [Fact]
    public void GetProjectById_ShouldReturnCorrectProject()
    {
        // Arrange
        var options = GetDbContextOptions("GetProjectById_ShouldReturnCorrectProject");
        var projectId = Guid.NewGuid();
        using (var dbContext = new PirDbContext(options))
        {
            dbContext.Projects.Add(new Project { ProjectId = projectId, Name = "Project 1", Code = "Code 1" });
            dbContext.Projects.Add(new Project { ProjectId = Guid.NewGuid(), Name = "Project 2", Code = "Code 2" });
            dbContext.SaveChanges();
        }

        using (var dbContext = new PirDbContext(options))
        {
            var projectService = new ProjectService(dbContext);

            // Act
            var project = projectService.GetProjectById(projectId);

            // Assert
            Assert.NotNull(project);
            Assert.Equal("Project 1", project.Name);
        }
    }

    [Fact]
    public void CreateProject_ShouldAddNewProject()
    {
        // Arrange
        var options = GetDbContextOptions("CreateProject_ShouldAddNewProject");
        using (var dbContext = new PirDbContext(options))
        {
            var projectService = new ProjectService(dbContext);

            // Act
            projectService.CreateProject(
                new Project { ProjectId = Guid.NewGuid(), Name = "Project 1", Code = "Code 1" });

            // Assert
            Assert.Equal(1, dbContext.Projects.Count());
            Assert.Equal("Project 1", dbContext.Projects.First().Name);
        }
    }

    [Fact]
    public void CreateProject_ShouldThrowException_WhenNameIsEmpty()
    {
        // Arrange
        var options = GetDbContextOptions("CreateProject_ShouldThrowException_WhenNameIsEmpty");
        using (var dbContext = new PirDbContext(options))
        {
            var projectService = new ProjectService(dbContext);

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
                projectService.CreateProject(new Project { ProjectId = Guid.NewGuid(), Name = "", Code = "Code 1" }));
        }
    }

    [Fact]
    public void CreateProject_ShouldThrowException_WhenCodeIsEmpty()
    {
        // Arrange
        var options = GetDbContextOptions("CreateProject_ShouldThrowException_WhenCodeIsEmpty");
        using (var dbContext = new PirDbContext(options))
        {
            var projectService = new ProjectService(dbContext);

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
                projectService.CreateProject(new Project
                    { ProjectId = Guid.NewGuid(), Name = "Project 1", Code = "" }));
        }
    }

    [Fact]
    public void UpdateProject_ShouldUpdateExistingProject()
    {
        // Arrange
        var options = GetDbContextOptions("UpdateProject_ShouldUpdateExistingProject");
        var projectId = Guid.NewGuid();
        using (var dbContext = new PirDbContext(options))
        {
            dbContext.Projects.Add(new Project { ProjectId = projectId, Name = "Project 1", Code = "Code 1" });
            dbContext.SaveChanges();
        }

        using (var dbContext = new PirDbContext(options))
        {
            var projectService = new ProjectService(dbContext);

            // Act
            var project = dbContext.Projects.First();
            project.Name = "Updated Project";
            project.Code = "Updated Code";
            projectService.UpdateProject(project);

            // Assert
            Assert.Equal("Updated Project", dbContext.Projects.First().Name);
            Assert.Equal("Updated Code", dbContext.Projects.First().Code);
        }
    }

    [Fact]
    public void UpdateProject_ShouldThrowException_WhenProjectIdDoesNotExist()
    {
        // Arrange
        var options = GetDbContextOptions("UpdateProject_ShouldThrowException_WhenProjectIdDoesNotExist");
        var projectId = Guid.NewGuid();
        using (var dbContext = new PirDbContext(options))
        {
            dbContext.Projects.Add(new Project { ProjectId = projectId, Name = "Project 1", Code = "Code 1" });
            dbContext.SaveChanges();
        }

        using (var dbContext = new PirDbContext(options))
        {
            var projectService = new ProjectService(dbContext);

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
                projectService.UpdateProject(new Project
                    { ProjectId = Guid.NewGuid(), Name = "Updated Project", Code = "Updated Code" }));
        }
    }

    [Fact]
    public void DeleteProject_ShouldRemoveProject()
    {
        // Arrange
        var options = GetDbContextOptions("DeleteProject_ShouldRemoveProject");
        var projectId = Guid.NewGuid();
        using (var dbContext = new PirDbContext(options))
        {
            dbContext.Projects.Add(new Project { ProjectId = projectId, Name = "Project 1", Code = "Code 1" });
            dbContext.SaveChanges();
        }

        using (var dbContext = new PirDbContext(options))
        {
            var projectService = new ProjectService(dbContext);

            // Act
            projectService.DeleteProject(projectId);

            // Assert
            Assert.Empty(dbContext.Projects);
        }
    }

    [Fact]
    public void AddObjectPir_ShouldAddObjectPirToProject()
    {
        // Arrange
        var options = GetDbContextOptions("AddObjectPir_ShouldAddObjectPirToProject");
        var projectId = Guid.NewGuid();
        var objectPirId = Guid.NewGuid();
        using (var dbContext = new PirDbContext(options))
        {
            dbContext.Projects.Add(new Project { ProjectId = projectId, Name = "Project 1", Code = "Code 1" });
            dbContext.ObjectPir.Add(new ObjectPir { Id = objectPirId, Name = "ObjectPir 1", ProjectId = projectId });
            dbContext.SaveChanges();
        }

        using (var dbContext = new PirDbContext(options))
        {
            var projectService = new ProjectService(dbContext);

            // Act
            projectService.AddObjectPir(projectId, dbContext.ObjectPir.First());

            // Assert
            var project = dbContext.Projects.Include(p => p.ObjectPirs).FirstOrDefault(p => p.ProjectId == projectId);
            Assert.NotNull(project);
            Assert.Equal(1, project.ObjectPirs.Count);
            Assert.Equal(objectPirId, project.ObjectPirs.First().Id);
        }
    }

    [Fact]
    public void AddObjectPir_ShouldThrowException_WhenProjectIdDoesNotExist()
    {
        // Arrange
        var options = GetDbContextOptions("AddObjectPir_ShouldThrowException_WhenProjectIdDoesNotExist");
        var projectId = Guid.NewGuid();
        var objectPirId = Guid.NewGuid();
        using (var dbContext = new PirDbContext(options))
        {
            dbContext.ObjectPir.Add(new ObjectPir { Id = objectPirId, Name = "ObjectPir 1", ProjectId = projectId });
            dbContext.SaveChanges();
        }

        using (var dbContext = new PirDbContext(options))
        {
            var projectService = new ProjectService(dbContext);

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
                projectService.AddObjectPir(projectId, dbContext.ObjectPir.First()));
        }
    }

    [Fact]
    public void RemoveObjectPir_ShouldThrowException_WhenProjectIdDoesNotExist()
    {
        // Arrange
        var options = GetDbContextOptions("RemoveObjectPir_ShouldThrowException_WhenProjectIdDoesNotExist");
        var projectId = Guid.NewGuid();
        var objectPirId = Guid.NewGuid();
        using (var dbContext = new PirDbContext(options))
        {
            dbContext.ObjectPir.Add(new ObjectPir { Id = objectPirId, Name = "ObjectPir 1", ProjectId = projectId });
            dbContext.SaveChanges();
        }

        using (var dbContext = new PirDbContext(options))
        {
            var projectService = new ProjectService(dbContext);

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
                projectService.RemoveObjectPir(projectId, objectPirId));
        }
    }

    [Fact]
    public void AddDocumentationPd_ShouldAddDocumentationPdToProject()
    {
        // Arrange
        var options = GetDbContextOptions("AddDocumentationPd_ShouldAddDocumentationPdToProject");
        var projectId = Guid.NewGuid();
        var documentationPdId = Guid.NewGuid();
        using (var dbContext = new PirDbContext(options))
        {
            dbContext.Projects.Add(new Project { ProjectId = projectId, Name = "Project 1", Code = "Code 1" });
            dbContext.DocumentationPd.Add(new DocumentationPd
                { Id = documentationPdId, Number = 1, ProjectId = projectId });
            dbContext.SaveChanges();
        }

        using (var dbContext = new PirDbContext(options))
        {
            var projectService = new ProjectService(dbContext);

            // Act
            projectService.AddDocumentationPd(projectId, documentationPdId);

            // Assert
            var project = dbContext.Projects.Include(p => p.DocumentationsPd)
                .FirstOrDefault(p => p.ProjectId == projectId);
            Assert.NotNull(project);
            Assert.Equal(1, project.DocumentationsPd.Count);
            Assert.Equal(documentationPdId, project.DocumentationsPd.First().Id);
        }
    }

    [Fact]
    public void AddDocumentationPd_ShouldThrowException_WhenProjectIdDoesNotExist()
    {
        // Arrange
        var options = GetDbContextOptions("AddDocumentationPd_ShouldThrowException_WhenProjectIdDoesNotExist");
        var projectId = Guid.NewGuid();
        var documentationPdId = Guid.NewGuid();
        using (var dbContext = new PirDbContext(options))
        {
            dbContext.DocumentationPd.Add(new DocumentationPd
                { Id = documentationPdId, Number = 1, ProjectId = projectId });
            dbContext.SaveChanges();
        }

        using (var dbContext = new PirDbContext(options))
        {
            var projectService = new ProjectService(dbContext);

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
                projectService.AddDocumentationPd(projectId, documentationPdId));
        }
    }

    [Fact]
    public void RemoveDocumentationPd_ShouldRemoveDocumentationPdFromProject()
    {
        // Arrange
        var options = GetDbContextOptions("RemoveDocumentationPd_ShouldRemoveDocumentationPdFromProject");
        var projectId = Guid.NewGuid();
        var documentationPdId = Guid.NewGuid();
        using (var dbContext = new PirDbContext(options))
        {
            dbContext.Projects.Add(new Project { ProjectId = projectId, Name = "Project 1", Code = "Code 1" });
            dbContext.DocumentationPd.Add(new DocumentationPd
                { Id = documentationPdId, Number = 1, ProjectId = projectId });
            dbContext.SaveChanges();
        }

        using (var dbContext = new PirDbContext(options))
        {
            var projectService = new ProjectService(dbContext);

            // Act
            projectService.RemoveDocumentationPd(projectId, documentationPdId);

            // Assert
            var project = dbContext.Projects.Include(p => p.DocumentationsPd)
                .FirstOrDefault(p => p.ProjectId == projectId);
            Assert.NotNull(project);
            Assert.Empty(project.DocumentationsPd);
        }
    }

    [Fact]
    public void RemoveDocumentationPd_ShouldThrowException_WhenProjectIdDoesNotExist()
    {
        // Arrange
        var options = GetDbContextOptions("RemoveDocumentationPd_ShouldThrowException_WhenProjectIdDoesNotExist");
        var projectId = Guid.NewGuid();
        var documentationPdId = Guid.NewGuid();
        using (var dbContext = new PirDbContext(options))
        {
            dbContext.DocumentationPd.Add(new DocumentationPd
                { Id = documentationPdId, Number = 1, ProjectId = projectId });
            dbContext.SaveChanges();
        }

        using (var dbContext = new PirDbContext(options))
        {
            var projectService = new ProjectService(dbContext);

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
                projectService.RemoveDocumentationPd(projectId, documentationPdId));
        }
    }
}