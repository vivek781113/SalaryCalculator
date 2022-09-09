using System.ComponentModel.DataAnnotations;

namespace SalaryAPI.Dto
{
    public class UserInputDto
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Annual Salary is required.")]
        public double AnnualSalary { get; set; }

        [Required(ErrorMessage = "Super rate is required.")]
        public double SuperRate { get; set; }
        
        [Required(ErrorMessage = "Pay period is required.")]
        public string PayPeriod { get; set; }
    }
}
