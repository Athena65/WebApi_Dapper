using Microsoft.AspNetCore.Mvc;
using SQL_Project.Dto;
using SQL_Project.Services;
using System;
using System.Threading.Tasks;

namespace SQL_Project.Controller
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;

        public CompaniesController(ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _companyRepo.GetCompanies();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpGet("{id}",Name ="CompanyId")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var company= await _companyRepo.GetCompany(id); 
                if(company==null)
                    return NotFound();

                return Ok(company); 

            }catch(Exception ex)
            {
                var response = new ServiceResponse();
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyForCreationDto company)
        {
            try
            {
                var createdCompany = await _companyRepo.CreateCompany(company);
                return CreatedAtRoute(new {id= createdCompany.Id}, createdCompany);
            }
            catch (Exception ex)
            {
                var response = new ServiceResponse();
                response.Success=false; 
                response.Message=ex.Message;
                return BadRequest(response);

            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyForUpdateDto company)
        {
            try
            {
                var dbCompany= await _companyRepo.GetCompany(id);
                if(dbCompany == null)
                    return NotFound();

                await _companyRepo.UpdateCompany(id, company);
                return Ok(dbCompany);
            }
            catch (Exception ex)
            {
                var response = new ServiceResponse();
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);

            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var dbCompany= await _companyRepo.GetCompany(id);
                if (dbCompany == null)
                    return NotFound();

                await _companyRepo.DeleteCompany(id);
                return Ok(dbCompany.Name+ " Deleted");
            }
            catch (Exception ex)
            {
                var response = new ServiceResponse();
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);

            }
        }

        

        

       
    }
}
