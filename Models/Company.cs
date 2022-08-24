using System.Collections.Generic;

namespace SQL_Project.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        
        public List<Employee> Employees { get; set; }  
    }
}
