using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace monpirtest.Model;

public class DocumentationPd
{
    [Key] 
    public Guid Id { get; set; }

    public StampPd? Stamp { get; set; }

    public int Number { get; set; }

    [Required] 
    public Guid ProjectId { get; set; }

    [ForeignKey(nameof(ProjectId))] 
    public Project Project { get; set; }
}