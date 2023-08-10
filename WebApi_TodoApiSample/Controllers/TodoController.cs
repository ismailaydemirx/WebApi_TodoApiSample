using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebApi_TodoApiSample.Entities;
using static System.Net.Mime.MediaTypeNames;
using static WebApi_TodoApiSample.Controllers.TodoController;
using WebApi_TodoApiSample.Models;

namespace WebApi_TodoApiSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private DatabaseContext _db;

        // Dependecy injection
        public TodoController(DatabaseContext context)
        {
            _db = context;
        }


        [HttpGet("generate-fakedata")]
        public IActionResult GenerateFakeData()
        {
            if (_db.Todos.Any())
            {
                return Ok("Veri tabanındaki todos tablosunda zaten örnek veri mevcuttur");
            }
            for (int i = 0; i < 50; i++)
            {
                Todo todo = new Todo
                {
                    Text = MFramework.Services.FakeData.TextData.GetSentence(),
                    IsCompleted = MFramework.Services.FakeData.BooleanData.GetBoolean(),
                    Description = MFramework.Services.FakeData.TextData.GetSentences(2),
                };
                _db.Todos.Add(todo);
            }

            _db.SaveChanges();
            return Ok("Ok");
        }

        [HttpGet("list")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<TodoResponse>))] // ProduceResponseType OK durumunda bize typeof Todo'yu list olarak döndürecek.
        public IActionResult List()
        {
            //List<Todo> todos = _db.Todos.ToList();
            //List<TodoResponse> result = new List<TodoResponse>();

            //foreach (Todo todo in todos)
            //{
            //    result.Add(new TodoResponse
            //    {
            //        Id = todo.Id,
            //        Text = todo.Text,
            //        Description = todo.Description,
            //        IsCompleted = todo.IsCompleted,
            //    });
            //}

            List<TodoResponse> result = _db.Todos.Select(x => new TodoResponse
                {
                    Id = x.Id,
                    Text = x.Text,
                    Description = x.Description,
                }).ToList();

            return Ok(result);
        }

        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(TodoResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        public IActionResult Create([FromBody] TodoCreateModel model)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                Todo todo = new Todo
                {
                    Text = model.Text,
                    Description = model.Description,
                };

                _db.Todos.Add(todo);
                int effected = _db.SaveChanges();

                if (effected > 0)
                {
                    TodoResponse result = new TodoResponse
                    {
                        Id = todo.Id,
                        Text = todo.Text,
                        Description = todo.Description,
                    };

                    // Created - 201
                    return Created(string.Empty, todo);
                }
                else
                {
                    return BadRequest("Kayıt alınamadı");
                }
                //}
                //catch (Exception ex)
                //{

                //    return BadRequest(ex.Message);
                //}
            }

            return BadRequest(ModelState);



        }

        // Bir kayıdın değişebilecek tüm alanlarını değiştirebilecek bir işlem yapıyorsak Put işlemini kullanmalıyız.

        [HttpPut("edit/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TodoResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public IActionResult Update([FromRoute] int id, [FromBody] TodoUpdateModel model)
        {
            Todo todo = _db.Todos.Find(id);

            if (todo == null)
                return NotFound("Kayıt bulunamadı.");

            todo.Text = model.Text;
            todo.Description = model.Description;
            todo.Description2 = model.Description2;
            todo.IsCompleted = model.IsCompleted;

            int affected = _db.SaveChanges();

            if (affected > 0)
            {
                TodoResponse result = new TodoResponse
                {
                    Id = todo.Id,
                    Text = todo.Text,
                    Description = todo.Description,
                    IsCompleted = todo.IsCompleted,
                };

                return Ok(result);
            }
            else
            {
                return BadRequest("Güncelleme yapılamadı.");
            }
        }

        // Patch istekleri 1 değişkeni güncellemek için kullanılır. Put da yapabiliriz ama Patch bunun için mevcut zaten.

        [HttpPatch("changestate/{id}/{iscompleted}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TodoResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        public IActionResult ChangeState([FromRoute] int id, [FromRoute] bool iscompleted)
        {
            Todo todo = _db.Todos.Find(id);

            if (todo == null)
                return NotFound("Kayıt bulunamadı.");

            todo.IsCompleted = iscompleted;

            int affected = _db.SaveChanges();

            if (affected > 0)
            {
                TodoResponse result = new TodoResponse
                {
                    Id = todo.Id,
                    Text = todo.Text,
                    Description = todo.Description,
                    IsCompleted = todo.IsCompleted,
                };

                return Ok(result);
            }
            else
            {
                return BadRequest("Durum değiştirildi   .");
            }
        }
    }
}
