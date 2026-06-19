using Microsoft.AspNetCore.Mvc;

namespace TaskManagerAPI.Controllers
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private static List<TaskItem> tasks = new List<TaskItem>
        {
            new TaskItem { Id = 1, Title = "Buy groceries", IsCompleted = false },
            new TaskItem { Id = 2, Title = "Learn .NET", IsCompleted = true },
            new TaskItem { Id = 3, Title = "Apply for jobs", IsCompleted = false }
        };

        // GET all tasks
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(tasks);
        }

        // GET single task by id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound("Task not found");
            return Ok(task);
        }

        // POST create new task
        [HttpPost]
        public IActionResult Create([FromBody] TaskItem task)
        {
            task.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(task);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        // PUT update existing task
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TaskItem updated)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound("Task not found");
            task.Title = updated.Title;
            task.IsCompleted = updated.IsCompleted;
            return Ok(task);
        }

        // DELETE remove task
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound("Task not found");
            tasks.Remove(task);
            return Ok("Task deleted");
        }
    }
}