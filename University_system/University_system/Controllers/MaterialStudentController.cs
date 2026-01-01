using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University_system.DTO;
using University_system.Model;
using University_system.Services;

namespace University_system.Controllers
{
    [ApiController]
    [Authorize]
    public class MaterialStudentController : ControllerBase
    {
        private readonly IRepositoryService<MaterialStudent> _repository;
        public MaterialStudentController(IRepositoryService<MaterialStudent> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [Route("api/materialstudent/allmaterial")]
        [Authorize(Roles = "Dean,Affairs Employee,Student")]
        public async Task<IActionResult> GetAllMaterial(Guid id)
        {
            var result = await _repository.GetAllMaterial(id);

            return Ok(result);
        }
        [HttpPost]
        [Route("api/materialstudent/downloadmaterial")]
        [Authorize(Roles = "Affairs Employee")]
        public async Task<IActionResult>DownloadMaterial(Guid Studentid,Guid Materialid)
        {
            var result = await _repository.AddMaterial(Studentid, Materialid);

            if (result.fail != "none")
                return BadRequest(result.fail);

            return Ok(result);
        }
        [HttpDelete]
        [Route("api/materialstudent/deletematerial")]
        [Authorize(Roles = "Affairs Employee")]
        public async Task<IActionResult>DeleteMaterial(DownloadMaterialDTO studentmaterial)
        {
            var material = await _repository.GetMaterialByName(studentmaterial.Name);

            var result = await _repository.DeleteMaterial(studentmaterial.Studentid,material.MaterialId);

            if (result.fail == "none")
                return NotFound();

            return Ok();
        }
    }
}
