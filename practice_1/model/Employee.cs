using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practice_1.model
{
    public class Employee
    {
        [Key]
        public int EMP_id { get; set; }
        public string EMP_Name { get; set; }
        public string EMP_MO_Number { get; set; }
        public int? LeaveBalance { get; set; }
        public DateTime JoiningDate { get; set; }

        [ForeignKey("Department")]
        public int? D_id { get; set; }
    }
}
