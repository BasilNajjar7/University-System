using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University_system.DTO;
using University_system.Model;
using University_system.Services;

namespace University_system.Controllers
{
    [ApiController]
    [Authorize]
    public class MaterialController : ControllerBase
    {
        private readonly IRepositoryService<Material> _repository;
        public MaterialController(IRepositoryService<Material> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [Route("api/material/all")]
        [Authorize(Roles = "Professor,Dean")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAll();
            
            return Ok(result);
        }
        [HttpGet]
        [Route("api/material/name")]
        [Authorize(Roles = "Professor,Dean")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _repository.GetMaterialByName(name);

            if(result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpPost]
        [Route("api/material/add")]
        [Authorize(Roles = "Admin,Dean")]
        public IActionResult AddNewMaterial(MaterialDTO material)
        {
            Material material1=new Material();

            material1.Name = material.Name;
            material1.Number_of_hour = material.Number_of_hour;
            material1.Completion_requires = material.Completion_requires;
            material1.Subject_professor = material.Subject_professor;

            _repository.Add(material1);
            
            return Ok();
        }
        [HttpPut]
        [Route("api/material/update")]
        [Authorize(Roles = "Dean")]
        public async Task<IActionResult> UpdateMaterial(MaterialDTO material)
        {
            var LastMaterial = await _repository.GetById(material.MaterialId);

            if (LastMaterial == null)
                return NotFound();

            LastMaterial.MaterialId = material.MaterialId;
            LastMaterial.Name = material.Name;
            LastMaterial.Number_of_hour= material.Number_of_hour;
            LastMaterial.Completion_requires = material.Completion_requires;
            LastMaterial.Subject_professor= material.Subject_professor;

            var result = await _repository.Update(material.MaterialId, LastMaterial);

            return Ok(result);
        }
        [HttpDelete]
        [Route("api/material/delete")]
        [Authorize(Roles = "Admin,Dean")]
        public async Task<IActionResult> DeleteMaterial(string MaterialName)
        {
            var result = await _repository.GetMaterialByName(MaterialName);

            if(result == null)
                return NotFound();

            _repository.Delete(result.MaterialId);

            return Ok();
        }
    }
}