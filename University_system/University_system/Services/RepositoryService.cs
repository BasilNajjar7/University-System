using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using University_system.Data;
using University_system.DTO;
using University_system.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace University_system.Services
{
    public class RepositoryService<T> : IRepositoryService<T> where T : class
    {
        protected ApplicationDbContext _context;
        private readonly UserManager<User> _usermanager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly JWT _jwt;
        public RepositoryService(ApplicationDbContext context, UserManager<User> usermanager, RoleManager<IdentityRole<Guid>> roleManager, IOptions<JWT> jwt)
        {
            _roleManager= roleManager;
            _context = context;
            _usermanager = usermanager;
            _jwt = jwt.Value;
        }
        public async Task<DownloadMaterialDTO> AddMaterial(Guid Studentid, Guid Materialid)
        {
            var result = await _context.Set<MaterialStudent>().SingleOrDefaultAsync(o => o.StudentId == Studentid && o.MaterialId == Materialid);

            var NewMaterial = new DownloadMaterialDTO();

            var student = await _context.Set<Student>().FindAsync(Studentid);

            var material = await _context.Set<Material>().FindAsync(Materialid);
           
            if (result == null)
            {

                if (material.Number_of_hour * student.CostOfHour < student.Student_Balance && !student.Is_Grant)
                    NewMaterial.fail = "Insufficient balance !!!";
                else if (material.Completion_requires.ToLower() == "none") 
                {
                    NewMaterial.Studentid = Studentid;
                    NewMaterial.Name = _context.Set<Material>().Find(Materialid).Name;
                    NewMaterial.fail = (student.Is_Grant ? "none2" : "none");
                }
                else
                {
                    var lastmaterial = await _context.Set<Material>().SingleOrDefaultAsync(o => o.Completion_requires == material.Completion_requires);

                    var success = await _context.Set<MaterialStudent>().SingleOrDefaultAsync(o => o.StudentId == Studentid && o.MaterialId == Materialid);

                    if (success != null && success.marks >= 60) 
                    {
                        NewMaterial.Studentid = Studentid;
                        NewMaterial.Name = _context.Set<Material>().Find(Materialid).Name;
                        NewMaterial.fail = (student.Is_Grant ? "none2" : "none");
                    }
                    else NewMaterial.fail = "You must complete the requirement first";
                }
            }
            else
            {
                if (result.marks < 60)
                {
                    await DeleteMaterial(Studentid, Materialid);
                    NewMaterial.Studentid = Studentid;
                    NewMaterial.Name = _context.Set<Material>().Find(Materialid).Name;
                    NewMaterial.fail = (student.Is_Grant ? "none2" : "none");
                }
                else NewMaterial.fail = "This material is complete";
            }

            if(NewMaterial.fail == "none" || NewMaterial.fail == "none2")
            {
                var insert = new MaterialStudent();

                insert.StudentId = Studentid;
                insert.MaterialId = Materialid;
                insert.marks = 0;
                if(NewMaterial.fail == "none") student.Student_Balance -= student.CostOfHour * material.Number_of_hour;
              
                _context.Set<MaterialStudent>().Add(insert);
                _context.Set<Student>().Update(student);
                _context.SaveChanges();
            }

            return NewMaterial;
        }
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public async Task<T> Delete(Guid id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            
            if(result!=null)
            {
                _context.Set<T>().Remove(result);
                _context.SaveChanges();
            }

            return result;
        }
        public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();
        public async Task<IEnumerable<Employee>> GetAllEmployeeByJobTitle(string title)
        {
            var result = await _usermanager.GetUsersInRoleAsync(title);

            var emp = result.Cast<Employee>().ToList();
            
            return emp;
        }
        public async Task<T> GetById(Guid id) => await _context.Set<T>().FindAsync(id);
        public async Task<IEnumerable<Student>> GetByYear(int year) => 
            await _context.Set<Student>().Where(b=>b.Year_of_registration.Equals(year)).ToListAsync();
        public async Task<IEnumerable<MaterialStudent>> GetAllMaterial(Guid id)
        {
            var result = await _context.Set<MaterialStudent>()
                .Where(o => o.StudentId == id)
                .Where(o => o.marks >= 60)
                .ToListAsync();
            
            return result;
        }
        public async Task<Material> GetMaterialByName(string name) => await _context.Set<Material>().SingleOrDefaultAsync(b=>b.Name==name);

        public async Task<T> Update(Guid id,T entity)
        {
            var res = await _context.Set<T>().FindAsync(id);

            _context.Entry(res).CurrentValues.SetValues(entity);
       //     res = entity;

            _context.Set<T>().Update(res);
            _context.SaveChanges();

            return res;
        }
        public async Task<DownloadMaterialDTO> DeleteMaterial(Guid Studentid, Guid Materialid)
        {
            var result = await _context.Set<MaterialStudent>().SingleOrDefaultAsync(o => o.StudentId == Studentid && o.MaterialId == Materialid);

            var material = new DownloadMaterialDTO();

            if (result == null)
                material.fail = "none";
            else
            {
                _context.Set<MaterialStudent>().Remove(result);
                _context.SaveChanges();
            }

            return material;
        }
        public async Task<AuthModel> RegisterAsync_stu(AddStudentDTO model)
        {
            if (await _usermanager.FindByEmailAsync(model.Email) != null)
                return new AuthModel { Massage = "Email is already registered!" };

            if (await _usermanager.FindByNameAsync(model.UserName) != null)
                return new AuthModel { Massage = "Username is already registered!" };

            var user = new Student
            {
                First_Name=model.First_Name,
                Last_Name=model.Last_Name,
                Email=model.Email,
                UserName=model.UserName,
                Gender=model.Gender,
                GPA=model. GPA,
                Year_of_registration=model.Year_of_registration,
                Student_Balance=model.Student_Balance,
                PhoneNumber=model.Phone_Number,
                Is_Grant=model.Is_Grant,                
            };
            var result = await _usermanager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                string errors = string.Empty;
                string Last = "^";
                foreach (var error in result.Errors)
                {
                    if (Last != "^") 
                    {
                        errors += Last;
                        errors += ',';
                    }
                    Last = error.Description;
                }
                if (Last != "^") 
                {
                    errors += Last;
                    errors += ".";
                }
                return new AuthModel { Massage = errors };
            }

            await _usermanager.AddToRoleAsync(user,"Student");

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "Student" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }

        public async Task<AuthModel> RegisterAsync_emp(AddEmployeeDTO model)
        {
            if (await _usermanager.FindByEmailAsync(model.Email) != null)
                return new AuthModel { Massage = "Email is already registered!" };

            if (await _usermanager.FindByNameAsync(model.UserName) != null)
                return new AuthModel { Massage = "Username is already registered!" };

            var user = new Employee
            {
                First_Name = model.First_Name,
                Last_Name = model.Last_Name,
                Email = model.Email,
                UserName = model.UserName,
                Gender = model.Gender,
                Salary = model.Salary,
                PhoneNumber=model.Phone_Number
            };
            var result = await _usermanager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                string errors = string.Empty;
                string Last = "^";
                foreach (var error in result.Errors)
                {
                    if (Last != "^")
                    {
                        errors += Last;
                        errors += ',';
                    }
                    Last = error.Description;
                }
                if (Last != "^")
                {
                    errors += Last;
                    errors += ".";
                }
                return new AuthModel { Massage = errors };
            }

            if(await _context.Roles.AnyAsync(r=>r.Name==model.Job_Title) == false)
                return new AuthModel { Massage = "Not found this job title" };

            await _usermanager.AddToRoleAsync(user, model.Job_Title);

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { model.Job_Title },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }

        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();

            var user = await _usermanager.FindByEmailAsync(model.Email);

            if (user == null || !await _usermanager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Massage = "Email or Password is incorrect";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;

            var rolesList = await _usermanager.GetRolesAsync(user);
            authModel.Roles = rolesList.ToList();

            return authModel;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _usermanager.GetClaimsAsync(user);
            var roles = await _usermanager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email)
            }.Union(userClaims)
             .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}