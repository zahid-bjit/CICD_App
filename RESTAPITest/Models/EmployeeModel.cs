using System.ComponentModel.DataAnnotations;

namespace RESTAPITest.Models
{
    public class EmployeeModel
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}