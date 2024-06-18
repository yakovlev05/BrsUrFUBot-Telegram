using Microsoft.AspNetCore.Mvc;
using UrfuService.Data.Entities;
using UrfuService.Data.Repositories;
using UrfuService.Models.BrsController;

namespace UrfuService.Controllers;

[ApiController]
[Route("/brs")]
public class BrsController(IRepository<BrsEntity> _repository) : Controller
{
    [HttpPost("add")]
    public async Task<ActionResult<bool>> AddBrs([FromBody] AddBrsRequestModel request)
    {
        await _repository.AddAsync(new BrsEntity()
        {
            RefreshToken = request.RefreshToken
        });
        return Ok(true);
    }
}