using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace monpirtest.Model;

public class DocumentationRd
{
    [Key] 
    public Guid Id { get; set; }

    public StampRd? Stamp { get; set; }

    public int Number { get; set; }

    [Required] 
    public Guid ObjectId { get; set; }

    [ForeignKey(nameof(ObjectId))] 
    public ObjectPir ObjectPir { get; set; }
}