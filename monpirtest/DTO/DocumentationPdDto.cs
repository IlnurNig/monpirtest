namespace monpirtest.DTO;

public class DocumentationPdDto
{
    public Guid Id { get; set; }

    public string Stamp { get; set; }

    public int Number { get; set; }

    public ProjectDto Project { get; set; }
}