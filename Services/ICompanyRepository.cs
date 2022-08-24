using SQL_Project.Dto;
using SQL_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQL_Project.Services
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompany(int id);

        public Task<Company> CreateCompany(CompanyForCreationDto company);

        public Task UpdateCompany(int id, CompanyForUpdateDto company);
        public Task DeleteCompany(int id);
    }
}
