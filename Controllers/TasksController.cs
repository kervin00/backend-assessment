using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assessment.Models;
using System;
using System.Linq;

namespace Assessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
         private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = _context.Tasks.ToList();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var task = _context.Tasks.Find(id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

       [HttpPost]
        public async Task<ActionResult<List<TaskManagement>>> AddTask( [FromBody] TaskManagement task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskManagement updatedTask)
        {
            if (id <= 0 || updatedTask == null)
                return BadRequest();

            var existingTask = _context.Tasks.Find(id);

            if (existingTask == null)
                return NotFound();

            // Update properties of the existing task
            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.Status = updatedTask.Status;

            _context.SaveChanges();

            return Ok($"Task updated successfully: {id}");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            if (id <= 0)
                return BadRequest();

            var task = _context.Tasks.Find(id);

            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return Ok($"Task deleted successfully: {id}");
        }
    }
}
