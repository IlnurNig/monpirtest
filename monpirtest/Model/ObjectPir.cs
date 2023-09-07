using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace monpirtest.Model;

public class ObjectPir
{
    [Key] 
    public Guid Id { get; set; }

    [MaxLength(50)] 
    [Required] 
    public string Name { get; set; }

    [Required] 
    public Guid ProjectId { get; set; }

    [ForeignKey(nameof(ProjectId))] 
    public Project Project { get; set; }

    public Guid ParentId { get; set; }

    [ForeignKey(nameof(ParentId))] 
    public ObjectPir Parent { get; set; }

    [InverseProperty(nameof(Parent))] 
    public ICollection<ObjectPir> Children { get; set; }

    [InverseProperty(nameof(DocumentationRd.ObjectPir))]
    public ICollection<DocumentationRd> DocumentationsRd { get; set; }
}