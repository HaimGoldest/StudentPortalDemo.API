using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentPortalDemo.API.DomainModels;
using StudentPortalDemo.API.Repositories;

namespace StudentPortalDemo.API.Controllers;

[ApiController]
public class GendersController : Controller
{
    private readonly IStudentsRepo _studentsRepo;
    private readonly IMapper _mapper;

    public GendersController(IStudentsRepo studentsRepo, IMapper mapper)
    {
        _studentsRepo = studentsRepo;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("[controller]")]
    public async Task<IActionResult> GetAllGenders()
    {
        var genderList = await _studentsRepo.GetGendersAsync();

        if (genderList == null || !genderList.Any())
        {
            return NotFound();
        }

        return Ok(_mapper.Map<List<Gender>>(genderList));
    }
    
}