using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace monpirtest.Model;

public class Project
{
    [Key] 
    public Guid ProjectId { get; set; }

    [MaxLength(50)] 
    [Required] 
    public string Name { get; set; }

    [MaxLength(50)] 
    public string Code { get; set; }

    [InverseProperty(nameof(ObjectPir.Project))]
    public ICollection<ObjectPir> ObjectPirs { get; set; }
    
    [InverseProperty(nameof(DocumentationPd.Project))] 
    public ICollection<DocumentationPd> DocumentationsPd { get; set; }
}