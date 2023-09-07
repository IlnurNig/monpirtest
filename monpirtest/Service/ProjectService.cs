using monpirtest.Model;
using monpirtest.Storage;

namespace monpirtest.Service;

public class ProjectService
{
    private readonly PirDbContext _dbContext;

    public ProjectService(PirDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ProjectService()
    {
    }

    public IEnumerable<Project> GetAllProjects()
    {
        return _dbContext.Projects.ToList();
    }

    public Project GetProjectById(Guid id)
    {
        return _dbContext.Projects.FirstOrDefault(p => p.ProjectId == id);
    }

    public void CreateProject(Project project)
    {
        ValidateProject(project);

        _dbContext.Projects.Add(project);
        _dbContext.SaveChanges();
    }

    public void UpdateProject(Project project)
    {
        ValidateProject(project);

        if (!_dbContext.Projects.Any(p => p.ProjectId == project.ProjectId))
        {
            throw new ArgumentException("Project with the specified ProjectId does not exist.",
                nameof(project.ProjectId));
        }

        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();
    }

    private void ValidateProject(Project project)
    {
        if (project == null)
        {
            throw new ArgumentNullException(nameof(project));
        }

        if (string.IsNullOrWhiteSpace(project.Name))
        {
            throw new ArgumentException("Name cannot be empty or whitespace.", nameof(project.Name));
        }

        if (string.IsNullOrWhiteSpace(project.Code))
        {
            throw new ArgumentException("Code cannot be empty or whitespace.", nameof(project.Code));
        }

        if (project.Name.Length > 50)
        {
            throw new ArgumentException("Name length cannot exceed 50 characters.", nameof(project.Name));
        }

        if (_dbContext.Projects.Any(p => p.Name == project.Name && p.ProjectId != project.ProjectId))
        {
            throw new ArgumentException("Project with the same name already exists.", nameof(project.Name));
        }

        if (project.Code.Length > 50)
        {
            throw new ArgumentException("Code length cannot exceed 50 characters.", nameof(project.Code));
        }
    }

    public void DeleteProject(Guid id)
    {
        var project = _dbContext.Projects.FirstOrDefault(p => p.ProjectId == id);
        if (project != null)
        {
            _dbContext.Projects.Remove(project);
            _dbContext.SaveChanges();
        }
    }

    public void AddObjectPir(Guid projectId, ObjectPir objectPir)
    {
        var project = _dbContext.Projects.FirstOrDefault(p => p.ProjectId == projectId);
        if (project == null)
        {
            throw new ArgumentException("Project with the specified ProjectId does not exist.", nameof(projectId));
        }

        if (objectPir == null)
        {
            throw new ArgumentNullException(nameof(objectPir));
        }

        project.ObjectPirs.Add(objectPir);
        _dbContext.SaveChanges();
    }

    public void RemoveObjectPir(Guid projectId, Guid objectPirId)
    {
        var project = _dbContext.Projects.FirstOrDefault(p => p.ProjectId == projectId);
        if (project == null)
        {
            throw new ArgumentException("Project with the specified ProjectId does not exist.", nameof(projectId));
        }

        var objectPir = project.ObjectPirs.FirstOrDefault(op => op.Id == objectPirId);
        if (objectPir == null)
        {
            throw new ArgumentException("ObjectPir with the specified Id does not exist.", nameof(objectPirId));
        }

        project.ObjectPirs.Remove(objectPir);
        _dbContext.SaveChanges();
    }

    public void AddDocumentationPd(Guid projectId, Guid documentationPdId)
    {
        var project = _dbContext.Projects.FirstOrDefault(p => p.ProjectId == projectId);
        if (project == null)
        {
            throw new ArgumentException("Project with the specified ProjectId does not exist.", nameof(projectId));
        }

        var documentationPd = _dbContext.DocumentationPd.FirstOrDefault(dp => dp.Id == documentationPdId);
        if (documentationPd == null)
        {
            throw new ArgumentException("DocumentationPd with the specified Id does not exist.",
                nameof(documentationPdId));
        }

        project.DocumentationsPd.Add(documentationPd);
        _dbContext.SaveChanges();
    }

    public void RemoveDocumentationPd(Guid projectId, Guid documentationPdId)
    {
        var project = _dbContext.Projects.FirstOrDefault(p => p.ProjectId == projectId);
        if (project == null)
        {
            throw new ArgumentException("Project with the specified ProjectId does not exist.", nameof(projectId));
        }

        var documentationPd = _dbContext.DocumentationPd.FirstOrDefault(dp => dp.Id == documentationPdId);
        if (documentationPd == null)
        {
            throw new ArgumentException("DocumentationPd with the specified Id does not exist.",
                nameof(documentationPdId));
        }

        project.DocumentationsPd.Remove(documentationPd);
        _dbContext.SaveChanges();
    }
}