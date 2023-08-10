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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<Todo>))] // ProduceResponseType OK durumunda bize typeof Todo'yu list olarak döndürecek.
        public IActionResult List()
        {
            return Ok(_db.Todos.ToList());
        }

        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(TodoResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(TodoResponse))]
        public IActionResult Create([FromBody] TodoCreateModel model)
        {
            try
            {
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
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }



        }
    }
}
