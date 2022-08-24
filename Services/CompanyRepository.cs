using Dapper;
using SQL_Project.Data;
using SQL_Project.Dto;
using SQL_Project.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SQL_Project.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;

        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "SELECT Id, Name ,Address,Country FROM Companies";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }

        public async Task<Company> GetCompany(int id)
        {
            var query = $"SELECT * FROM Companies WHERE Id={id}";
            using(var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Company>(query, id); 
                return company; 
            }
        }
        public async Task<Company> CreateCompany(CompanyForCreationDto company)
        {
            var query = "INSERT INTO Companies(Name,Address,Country) VALUES (@Name,@Address,@Country)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var id =await connection.QuerySingleAsync<int>(query,parameters);

                var createdCompany = new Company //mapping
                {
                    Id = id,
                    Name = company.Name,
                    Address = company.Address,
                    Country = company.Country,

                };
                return createdCompany;  
            }
        }


        public async Task UpdateCompany(int id, CompanyForUpdateDto company)
        {
            var query = $"UPDATE Companies SET Name=@Name, Address=@Address, Country=@Country WHERE Id={id}";

            var parameters=new DynamicParameters();
            parameters.Add("Id",id,DbType.Int32);
            parameters.Add("Name",company.Name,DbType.String);
            parameters.Add("Address",company.Address,DbType.String);
            parameters.Add("Country",company.Country,DbType.String);

            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,parameters);    
            } 
        }
      
        public async Task DeleteCompany(int id)
        {
            var query = $"DELETE FROM Companies WHERE Id={id}";

            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

     
    }
}
