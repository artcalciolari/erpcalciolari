using System.ComponentModel.DataAnnotations;

namespace ErpCalciolari.DTOs.Update
{
    public class EmployeeUpdateDto
    {
        public string? Name { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
