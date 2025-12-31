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
        Task<IEnumerable<Student>> GetByYear(int year);
        Task<Material> GetMaterialByName(string name);
        Task<IEnumerable<MaterialStudent>> GetAllMaterial(Guid id);
        T Add(T entity);
        Task<DownloadMaterialDTO>AddMaterial(Guid Studentid,Guid Materialid);
        Task<T> Update(Guid id,T entity);
        Task<T> Delete(Guid id);
        Task<DownloadMaterialDTO> DeleteMaterial(Guid Studentid,Guid Materialid);
        Task<AuthModel> RegisterAsync_stu(AddStudentDTO model);
        Task<AuthModel> RegisterAsync_emp(AddEmployeeDTO model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
    }
}