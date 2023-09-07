namespace monpirtest.DTO;

public class ProjectDto
{
    public Guid ProjectId { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }

    public ICollection<ObjectPirDto> ObjectPirs { get; set; }

    public ICollection<DocumentationPdDto> DocumentationsPd { get; set; }
}