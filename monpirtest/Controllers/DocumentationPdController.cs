using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using monpirtest.DTO;
using monpirtest.Model;
using monpirtest.Service;

namespace monpirtest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentationPdController : ControllerBase
{
    private readonly DocumentationPdService _documentationPdService;
    private readonly IMapper _mapper;

    public DocumentationPdController(DocumentationPdService documentationPdService, IMapper mapper)
    {
        _documentationPdService = documentationPdService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllDocumentationPds()
    {
        var documentationPds = _documentationPdService.GetAllDocumentationPds();
        var documentationPdDtos = _mapper.Map<IEnumerable<DocumentationPdDto>>(documentationPds);
        return Ok(documentationPdDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetDocumentationPdById(Guid id)
    {
        var documentationPd = _documentationPdService.GetDocumentationPdById(id);
        if (documentationPd == null)
        {
            return NotFound();
        }

        var documentationPdDto = _mapper.Map<DocumentationPdDto>(documentationPd);
        return Ok(documentationPdDto);
    }

    [HttpPost]
    public IActionResult CreateDocumentationPd(DocumentationPdDto documentationPdDto)
    {
        var documentationPd = _mapper.Map<DocumentationPd>(documentationPdDto);
        _documentationPdService.CreateDocumentationPd(documentationPd);
        var createdDocumentationPdDto = _mapper.Map<DocumentationPdDto>(documentationPd);
        return CreatedAtAction(nameof(GetDocumentationPdById), new { id = createdDocumentationPdDto.Id },
            createdDocumentationPdDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateDocumentationPd(Guid id, DocumentationPdDto documentationPdDto)
    {
        var documentationPd = _mapper.Map<DocumentationPd>(documentationPdDto);
        documentationPd.Id = id;
        _documentationPdService.UpdateDocumentationPd(documentationPd);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteDocumentationPd(Guid id)
    {
        _documentationPdService.DeleteDocumentationPd(id);
        return NoContent();
    }
}