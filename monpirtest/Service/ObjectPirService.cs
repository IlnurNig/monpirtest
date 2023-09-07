using monpirtest.Model;
using monpirtest.Storage;

namespace monpirtest.Service;

public class ObjectPirService
{
    private readonly PirDbContext _dbContext;

    public ObjectPirService(PirDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<ObjectPir> GetAllObjectPirs()
    {
        return _dbContext.ObjectPir.ToList();
    }

    public ObjectPir GetObjectPirById(Guid id)
    {
        return _dbContext.ObjectPir.FirstOrDefault(o => o.Id == id);
    }

    public void CreateObjectPir(ObjectPir objectPir)
    {
        ValidateObjectPir(objectPir);

        _dbContext.ObjectPir.Add(objectPir);
        _dbContext.SaveChanges();
    }

    public void UpdateObjectPir(ObjectPir objectPir)
    {
        ValidateObjectPir(objectPir);

        var existingObjectPir = _dbContext.ObjectPir.FirstOrDefault(o => o.Id == objectPir.Id);
        if (existingObjectPir == null)
        {
            throw new ArgumentException("ObjectPir not found");
        }

        existingObjectPir.Name = objectPir.Name;
        existingObjectPir.ProjectId = objectPir.ProjectId;
        existingObjectPir.ParentId = objectPir.ParentId;

        _dbContext.SaveChanges();
    }

    public void DeleteObjectPir(Guid id)
    {
        var objectPir = _dbContext.ObjectPir.FirstOrDefault(o => o.Id == id);
        if (objectPir == null)
        {
            throw new ArgumentException("ObjectPir not found");
        }

        _dbContext.ObjectPir.Remove(objectPir);
        _dbContext.SaveChanges();
    }

    private void ValidateObjectPir(ObjectPir objectPir)
    {
        if (string.IsNullOrEmpty(objectPir.Name))
        {
            throw new ArgumentException("Name is required");
        }

        if (objectPir.Name.Length > 50)
        {
            throw new ArgumentException("Name cannot exceed 50 characters");
        }

        if (objectPir.ProjectId == Guid.Empty)
        {
            throw new ArgumentException("ProjectId is required");
        }

        var project = _dbContext.Projects.FirstOrDefault(p => p.ProjectId == objectPir.ProjectId);
        if (project == null)
        {
            throw new ArgumentException("ProjectId does not exist");
        }

        if (objectPir.ParentId != null)
        {
            var parent = _dbContext.ObjectPir.FirstOrDefault(o => o.Id == objectPir.ParentId);
            if (parent == null)
            {
                throw new ArgumentException("ParentId does not exist");
            }
        }
    }

    public void AddDocumentationRd(Guid objectId, Guid documentationRdId)
    {
        var objectPir = _dbContext.ObjectPir.FirstOrDefault(o => o.Id == objectId);
        if (objectPir == null)
        {
            throw new ArgumentException("ObjectPir not found");
        }

        var documentationRd = _dbContext.DocumentationRd.FirstOrDefault(d => d.Id == documentationRdId);
        if (documentationRd == null)
        {
            throw new ArgumentException("DocumentationRd not found");
        }

        objectPir.DocumentationsRd.Add(documentationRd);
        _dbContext.SaveChanges();
    }

    public void RemoveDocumentationRd(Guid objectId, Guid documentationRdId)
    {
        var objectPir = _dbContext.ObjectPir.FirstOrDefault(o => o.Id == objectId);
        if (objectPir == null)
        {
            throw new ArgumentException("ObjectPir not found");
        }

        var documentationRd = objectPir.DocumentationsRd.FirstOrDefault(d => d.Id == documentationRdId);
        if (documentationRd == null)
        {
            throw new ArgumentException("DocumentationRd not found");
        }

        objectPir.DocumentationsRd.Remove(documentationRd);
        _dbContext.SaveChanges();
    }
}