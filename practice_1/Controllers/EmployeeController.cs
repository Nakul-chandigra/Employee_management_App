using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practice_1.model;

namespace practice_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataDbcontext _context;

        public EmployeeController(DataDbcontext context)
        {
            _context = context;
        }

        // === Employee CRUD ===

        [HttpPost("addEmployee")]
        public IActionResult AddEmployee([FromBody] Employee emp)
        {
            if (ModelState.IsValid)
            {
                emp.LeaveBalance = 10;
                _context.Employee.Add(emp); 
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpGet("GetEmployeeList")]
        public IActionResult GetEmployees()
        {
            try
            {
                var rs = _context.Employee.ToList();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error : {ex.InnerException?.Message}");
            }
        }


        [HttpGet("getEmployeeById/{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _context.Employee.Find(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateEmployee(int id, Employee updatedEmployee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 

            var employee = _context.Employee.Find(id);
            if (employee == null) return NotFound();

            employee.EMP_Name = updatedEmployee.EMP_Name;
            employee.EMP_MO_Number = updatedEmployee.EMP_MO_Number;
            employee.D_id = updatedEmployee.D_id;
            employee.LeaveBalance = updatedEmployee.LeaveBalance;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("DeleteEmployee")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employee.Find(id);
            if (employee == null)
            {
                return NotFound(new { message = "Employee not found." });
            }

            try
            {
                // Delete related LeaveRequests
                var leaveRequests = _context.LeaveRequest
                                            .Where(lr => lr.EMP_id == id)
                                            .ToList();
                if (leaveRequests.Any())
                {
                    _context.LeaveRequest.RemoveRange(leaveRequests);
                    _context.SaveChanges();
                }

                // Delete related EmployeeProject entries
                var assignedProjects = _context.EmployeeProject
                                               .Where(ep => ep.EMP_id == id)
                                               .ToList();
                if (assignedProjects.Any())
                {
                    _context.EmployeeProject.RemoveRange(assignedProjects);
                    _context.SaveChanges();
                }

                // Delete the employee
                _context.Employee.Remove(employee);

                // Save all changes at once
                _context.SaveChanges();

                return Ok(new { message = "Employee and related data deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the employee.", error = ex.Message });
            }
        }


        [HttpGet("GetDepartmentList")]
        public IActionResult GetDepartments()
        {
            var result = _context.Department.ToList();
            return Ok(result);
        }

        [HttpGet("GetAllLeaveRequests")]
        public  IActionResult GetAllLeaveRequests()
        {
            var leaveRequests = _context.LeaveRequest.OrderByDescending(lr=> lr.LeaveRequestId).ToList();
            return Ok(leaveRequests);
        }

        [HttpPost("AddLeaveRequest")]
        public IActionResult AddLeaveRequest([FromBody] LeaveRequest request)
        {
            if (request == null)
                return BadRequest("Invalid leave request");

            _context.LeaveRequest.Add(request);
            _context.SaveChanges();

            return Ok(request); 
        }


        [HttpPut("ApprovedLeave/{id}")]
        public IActionResult ApprovedLeave(int id)
        {
 
            var leave = _context.LeaveRequest.Find(id);
            if (leave == null)
                return NotFound();

            var employee = _context.Employee.Find(leave.EMP_id);
            if (employee == null)
                return NotFound();

            var days = (leave.EndDate - leave.StartDate).TotalDays + 1;
            employee.LeaveBalance -= (int)days;
            leave.Status = 1;
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("RejectLeave/{id}")]
        public IActionResult RejectLeave(int id)
        {
            var leave = _context.LeaveRequest.Find(id);
            if (leave == null)
                return NotFound("Leave request not found.");

            leave.Status = 2;

            _context.SaveChanges();

            return Ok();
        }
    }
}
    