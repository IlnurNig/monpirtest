using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using monpirtest.DTO;
using monpirtest.Model;
using monpirtest.Service;

namespace monpirtest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentationRdController : ControllerBase
{
    private readonly DocumentationRdService _documentationRdService;
    private readonly IMapper _mapper;

    public DocumentationRdController(DocumentationRdService documentationRdService, IMapper mapper)
    {
        _documentationRdService = documentationRdService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllDocumentationRds()
    {
        var documentationRds = _documentationRdService.GetAllDocumentationRds();
        var documentationRdDtos = _mapper.Map<IEnumerable<DocumentationRdDto>>(documentationRds);
        return Ok(documentationRdDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetDocumentationRdById(Guid id)
    {
        var documentationRd = _documentationRdService.GetDocumentationRdById(id);
        if (documentationRd == null)
        {
            return NotFound();
        }

        var documentationRdDto = _mapper.Map<DocumentationRdDto>(documentationRd);
        return Ok(documentationRdDto);
    }

    [HttpPost]
    public IActionResult CreateDocumentationRd(DocumentationRdDto documentationRdDto)
    {
        var documentationRd = _mapper.Map<DocumentationRd>(documentationRdDto);
        _documentationRdService.CreateDocumentationRd(documentationRd);
        var createdDocumentationRdDto = _mapper.Map<DocumentationRdDto>(documentationRd);
        return CreatedAtAction(nameof(GetDocumentationRdById), new { id = createdDocumentationRdDto.Id },
            createdDocumentationRdDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateDocumentationRd(Guid id, DocumentationRdDto documentationRdDto)
    {
        var documentationRd = _mapper.Map<DocumentationRd>(documentationRdDto);
        documentationRd.Id = id;
        _documentationRdService.UpdateDocumentationRd(documentationRd);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteDocumentationRd(Guid id)
    {
        _documentationRdService.DeleteDocumentationRd(id);
        return NoContent();
    }
}