namespace monpirtest.DTO;

public class ObjectPirDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ProjectDto Project { get; set; }

    public ObjectPirDto Parent { get; set; }

    public ICollection<ObjectPirDto> Children { get; set; }

    public ICollection<DocumentationRdDto> DocumentationsRd { get; set; }
}