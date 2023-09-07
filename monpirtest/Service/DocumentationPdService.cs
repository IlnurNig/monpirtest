using monpirtest.Model;
using monpirtest.Storage;

namespace monpirtest.Service;

public class DocumentationPdService
{
    private readonly PirDbContext _dbContext;

    public DocumentationPdService(PirDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<DocumentationPd> GetAllDocumentationPds()
    {
        return _dbContext.DocumentationPd.ToList();
    }

    public DocumentationPd GetDocumentationPdById(Guid id)
    {
        return _dbContext.DocumentationPd.FirstOrDefault(d => d.Id == id);
    }

    public void CreateDocumentationPd(DocumentationPd documentationPd)
    {
        ValidateDocumentationPd(documentationPd);

        _dbContext.DocumentationPd.Add(documentationPd);
        _dbContext.SaveChanges();
    }

    public void UpdateDocumentationPd(DocumentationPd documentationPd)
    {
        ValidateDocumentationPd(documentationPd);

        var existingDocumentationPd = _dbContext.DocumentationPd.FirstOrDefault(d => d.Id == documentationPd.Id);
        if (existingDocumentationPd == null)
        {
            throw new ArgumentException("DocumentationPd not found");
        }

        existingDocumentationPd.Stamp = documentationPd.Stamp;
        existingDocumentationPd.Number = documentationPd.Number;
        existingDocumentationPd.ProjectId = documentationPd.ProjectId;

        _dbContext.SaveChanges();
    }

    public void DeleteDocumentationPd(Guid id)
    {
        var documentationPd = _dbContext.DocumentationPd.FirstOrDefault(d => d.Id == id);
        if (documentationPd == null)
        {
            throw new ArgumentException("DocumentationPd not found");
        }

        _dbContext.DocumentationPd.Remove(documentationPd);
        _dbContext.SaveChanges();
    }

    private void ValidateDocumentationPd(DocumentationPd documentationPd)
    {
        if (documentationPd.Stamp == null)
        {
            throw new ArgumentException("Stamp is required");
        }

        if (documentationPd.Number == 0)
        {
            throw new ArgumentException("Number is required");
        }

        if (documentationPd.ProjectId == Guid.Empty)
        {
            throw new ArgumentException("ProjectId is required");
        }

        var project = _dbContext.Projects.FirstOrDefault(p => p.ProjectId == documentationPd.ProjectId);
        if (project == null)
        {
            throw new ArgumentException("ProjectId does not exist");
        }
    }
}