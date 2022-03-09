using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        
        private readonly DataContext _context;

        public TodoListController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoList>>> Get() 
        {
            return Ok(await _context.todolist.ToListAsync());
        }

        [HttpGet("theo id")]
        public async Task<ActionResult<TodoList>> Get(int id) 
        {
            var getId = await _context.todolist.FindAsync(id);
            if (getId == null)
                return BadRequest("Task not found.");
            return Ok(getId);
        }

        [HttpGet("theo status")]
        public async Task<ActionResult<TodoList>> Get(string status) 
        {
            var getStatus = await _context.todolist.Where(j => j.Status == status).ToListAsync();
            return Ok(getStatus);
        }

        [HttpPut]
        public async Task<ActionResult<List<TodoList>>> UpdateTask(TodoList update) 
        {
            var updateitem = await _context.todolist.FindAsync(update.Id);
            if (updateitem == null)
                return BadRequest("Task not found.");

            updateitem.Title = update.Title;
            updateitem.Status = update.Status;

            await _context.SaveChangesAsync();
            return Ok(await _context.todolist.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<TodoList>>> AddItem(TodoList todoAdd)
        {
            _context.todolist.Add(todoAdd);
            await _context.SaveChangesAsync();
            return Ok(await _context.todolist.ToListAsync());
        }

        [HttpDelete("theo id")]
        public async Task<ActionResult<List<TodoList>>> deleteId(int id)
        {
            var getId = await _context.todolist.FindAsync(id);
            if (getId == null)
                return BadRequest("Task not found.");

            _context.todolist.Remove(getId);
            await _context.SaveChangesAsync();

            return Ok(await _context.todolist.ToListAsync());
        }
    }
}
