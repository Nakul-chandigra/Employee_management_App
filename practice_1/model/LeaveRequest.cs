using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practice_1.model
{
    public class LeaveRequest
    {
        [Key]
        public int LeaveRequestId { get; set; }

        [ForeignKey("Employee")]
        public int EMP_id { get; set; }
        public int LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public int Status { get; set; }


    }
}
