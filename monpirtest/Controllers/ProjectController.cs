using Microsoft.AspNetCore.Mvc;
using monpirtest.DTO;
using monpirtest.Model;
using monpirtest.Service;
using AutoMapper;

namespace monpirtest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ProjectService _projectService;
    private readonly IMapper _mapper;

    public ProjectsController(ProjectService projectService, IMapper mapper)
    {
        _projectService = projectService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllProjects()
    {
        var projects = _projectService.GetAllProjects();
        var projectDtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
        return Ok(projectDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetProjectById(Guid id)
    {
        var project = _projectService.GetProjectById(id);
        if (project == null)
        {
            return NotFound();
        }

        var projectDto = _mapper.Map<ProjectDto>(project);
        return Ok(projectDto);
    }

    [HttpPost]
    public IActionResult CreateProject(ProjectDto projectDto)
    {
        var project = new Project
        {
            ProjectId = projectDto.ProjectId,
            Name = projectDto.Name,
            Code = projectDto.Code
        };

        _projectService.CreateProject(project);

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProject(Guid id, ProjectDto projectDto)
    {
        var project = new Project
        {
            ProjectId = id,
            Name = projectDto.Name,
            Code = projectDto.Code
        };

        try
        {
            _projectService.UpdateProject(project);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProject(Guid id)
    {
        try
        {
            _projectService.DeleteProject(id);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{projectId}/objectpirs")]
    public IActionResult AddObjectPir(Guid projectId, ObjectPirDto objectPirDto)
    {
        var objectPir = new ObjectPir
        {
            Id = objectPirDto.Id,
            Name = objectPirDto.Name
        };

        try
        {
            _projectService.AddObjectPir(projectId, objectPir);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{projectId}/objectpirs/{objectPirId}")]
    public IActionResult RemoveObjectPir(Guid projectId, Guid objectPirId)
    {
        try
        {
            _projectService.RemoveObjectPir(projectId, objectPirId);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{projectId}/documentationspd/{documentationPdId}")]
    public IActionResult AddDocumentationPd(Guid projectId, Guid documentationPdId)
    {
        try
        {
            _projectService.AddDocumentationPd(projectId, documentationPdId);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{projectId}/documentationspd/{documentationPdId}")]
    public IActionResult RemoveDocumentationPd(Guid projectId, Guid documentationPdId)
    {
        try
        {
            _projectService.RemoveDocumentationPd(projectId, documentationPdId);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}