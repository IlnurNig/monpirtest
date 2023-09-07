using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using monpirtest.DTO;
using monpirtest.Model;
using monpirtest.Service;

namespace monpirtest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ObjectPirController : ControllerBase
{
    private readonly ObjectPirService _objectPirService;
    private readonly IMapper _mapper;

    public ObjectPirController(ObjectPirService objectPirService, IMapper mapper)
    {
        _objectPirService = objectPirService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllObjectPirs()
    {
        var objectPirs = _objectPirService.GetAllObjectPirs();
        var objectPirDtos = _mapper.Map<IEnumerable<ObjectPirDto>>(objectPirs);
        return Ok(objectPirDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetObjectPirById(Guid id)
    {
        var objectPir = _objectPirService.GetObjectPirById(id);
        if (objectPir == null)
        {
            return NotFound();
        }

        var objectPirDto = _mapper.Map<ObjectPirDto>(objectPir);
        return Ok(objectPirDto);
    }

    [HttpPost]
    public IActionResult CreateObjectPir(ObjectPirDto objectPirDto)
    {
        var objectPir = _mapper.Map<ObjectPir>(objectPirDto);
        _objectPirService.CreateObjectPir(objectPir);
        var createdObjectPirDto = _mapper.Map<ObjectPirDto>(objectPir);
        return CreatedAtAction(nameof(GetObjectPirById), new { id = createdObjectPirDto.Id }, createdObjectPirDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateObjectPir(Guid id, ObjectPirDto objectPirDto)
    {
        var objectPir = _mapper.Map<ObjectPir>(objectPirDto);
        objectPir.Id = id;
        _objectPirService.UpdateObjectPir(objectPir);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteObjectPir(Guid id)
    {
        _objectPirService.DeleteObjectPir(id);
        return NoContent();
    }

    [HttpPost("{objectId}/documentationsRd/{documentationRdId}")]
    public IActionResult AddDocumentationRd(Guid objectId, Guid documentationRdId)
    {
        try
        {
            _objectPirService.AddDocumentationRd(objectId, documentationRdId);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{objectId}/documentationsRd/{documentationRdId}")]
    public IActionResult RemoveDocumentationRd(Guid objectId, Guid documentationRdId)
    {
        try
        {
            _objectPirService.RemoveDocumentationRd(objectId, documentationRdId);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}