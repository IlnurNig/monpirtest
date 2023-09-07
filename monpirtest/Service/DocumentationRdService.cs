using monpirtest.Model;
using monpirtest.Storage;

namespace monpirtest.Service;

public class DocumentationRdService
{
    private readonly PirDbContext _dbContext;

    public DocumentationRdService(PirDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<DocumentationRd> GetAllDocumentationRds()
    {
        return _dbContext.DocumentationRd.ToList();
    }

    public DocumentationRd GetDocumentationRdById(Guid id)
    {
        return _dbContext.DocumentationRd.FirstOrDefault(d => d.Id == id);
    }

    public void CreateDocumentationRd(DocumentationRd documentationRd)
    {
        ValidateDocumentationRd(documentationRd);

        _dbContext.DocumentationRd.Add(documentationRd);
        _dbContext.SaveChanges();
    }

    public void UpdateDocumentationRd(DocumentationRd documentationRd)
    {
        ValidateDocumentationRd(documentationRd);

        var existingDocumentationRd = _dbContext.DocumentationRd.FirstOrDefault(d => d.Id == documentationRd.Id);
        if (existingDocumentationRd == null)
        {
            throw new ArgumentException("DocumentationRd not found");
        }

        existingDocumentationRd.Stamp = documentationRd.Stamp;
        existingDocumentationRd.Number = documentationRd.Number;
        existingDocumentationRd.ObjectId = documentationRd.ObjectId;

        _dbContext.SaveChanges();
    }

    public void DeleteDocumentationRd(Guid id)
    {
        var documentationRd = _dbContext.DocumentationRd.FirstOrDefault(d => d.Id == id);
        if (documentationRd == null)
        {
            throw new ArgumentException("DocumentationRd not found");
        }

        _dbContext.DocumentationRd.Remove(documentationRd);
        _dbContext.SaveChanges();
    }

    private void ValidateDocumentationRd(DocumentationRd documentationRd)
    {
        if (documentationRd.Stamp == null)
        {
            throw new ArgumentException("Stamp is required");
        }

        if (documentationRd.Number == 0)
        {
            throw new ArgumentException("Number is required");
        }

        if (documentationRd.ObjectId == Guid.Empty)
        {
            throw new ArgumentException("ObjectId is required");
        }

        var objectPir = _dbContext.ObjectPir.FirstOrDefault(o => o.Id == documentationRd.ObjectId);
        if (objectPir == null)
        {
            throw new ArgumentException("ObjectId does not exist");
        }
    }
}