using Microsoft.AspNetCore.Mvc;
using University_system.Model;
using University_system.Services;

namespace University_system.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IRepositoryService<Student> _repository;
        public StudentController(IRepositoryService<Student> repository)
        {
            _repository = repository;
        }
        [HttpGet("register year")]
        public IActionResult GetByYear(int year)
        {
            var result = _repository.GetByYear(year);

            return Ok(result);
        }
        [HttpGet("id")]
        public IActionResult GetById(Guid id)
        {
            var result = _repository.GetById(id);

            if(result == null)return NotFound();

            return Ok(result);
        }
        [HttpPost]
        [Route("Add student")]
        public IActionResult AddStudent(Student student)
        {
            _repository.Add(student);

            return Ok();
        }
        [HttpPut]
        [Route("Update student data")]
        public IActionResult Update(Student student)
        {
            if (_repository.GetById(student.Id) == null)
                return NotFound();

            var result = _repository.Update(student.Id,student);

            return Ok(result);
        }
        [HttpDelete]
        [Route("Remove student")]
        public IActionResult DeleteById(Guid id)
        {
            var result = _repository.Delete(id);
            
            if(result==null)return NotFound();

            return Ok();
        }
    }
}