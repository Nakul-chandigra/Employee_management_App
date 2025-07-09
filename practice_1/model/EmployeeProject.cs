using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace practice_1.model
{
    public class EmployeeProject
    {
        //EP_id int PRIMARY KEY IDENTITY(1,1),
        //EP_Name VARCHAR(100),
        //EMP_id INT,   
        // P_id INT,

        [Key]
        public int EP_id { get; set; }
        public string EP_Name { get; set; }
        public int EMP_id { get; set; }
        public int P_id { get;set; } 
    }
}
