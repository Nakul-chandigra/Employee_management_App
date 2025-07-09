using System.ComponentModel.DataAnnotations;

namespace practice_1.model
{
    public class Department
    {
        [Key]
        public int D_id { get; set; }
        public string D_name { get; set; }
    }
}
