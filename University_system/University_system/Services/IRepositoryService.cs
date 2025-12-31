using Microsoft.AspNetCore.Mvc;
using University_system.DTO;
using University_system.Model;

namespace University_system.Services
{
    public interface IRepositoryService<T>where T : class
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<Employee>>GetAllEmployeeByJobTitle(string title);
        Task<T> Update(Guid id,T entity);
        Task<IEnumerable<Student>> GetByYear(int year);
        T Add(T entity);
        Task<T> Delete(Guid id);
        Task<Material> GetMaterialByName(string name);
        Task<AuthModel> RegisterAsync_stu(AddStudentDTO model);
        Task<AuthModel> RegisterAsync_emp(AddEmployeeDTO model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
    }
}