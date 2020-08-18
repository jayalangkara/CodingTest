using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoController(ToDoContext context)
        {
            _context = context;

            if (_context.ToDoItems.Count() == 0)
            {
                // Create a new ToDoItem if collection is empty
                _context.ToDoItems.Add(new ToDoItem
                {
                    DateTimeExp = DateTime.Now.AddDays(1),
                    Title = "ToDo Title",
                    Description = "ToDo Description"
                });
                _context.SaveChanges();
            }
        }

        // GET: api/todo
        // Get All Todo’s
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        // GET: api/todo/1
        // Get Specific Todo
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(long id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return toDoItem;
        }

        // POST: api/todo
        // Create Todo
        // Set Todo percent complete
        // Mark Todo as Done
        /* 
         * Test PostTodoItem in Postman
         * Example request body JSON:
         * 
            {
                "dateTimeExp": "2020-08-20 21:00:00",
                "title": "Laundry",
                "description": "Uniforms",
                "percentComplete": 100,
                "isComplete": true
            }
         *
         */
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetToDoItem), new { id = item.Id }, item);
        }

        // PUT: api/todo/1
        // Update Todo
        /* 
         * Test PutToDoItem in Postman
         * Example request body JSON:
         * 
            {
                "id": 1,
                "dateTimeExp": "2020-08-20 22:00:00",
                "title": "Homework",
                "description": "Painting",
                "percentComplete": 90,
                "isComplete": false
            }
         *
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoItem(long id, ToDoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Content("ToDo Updated");
        }

        // DELETE: api/todo/1
        // Delete Todo
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(long id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return Content("ToDo Deleted");
        }
    }
}