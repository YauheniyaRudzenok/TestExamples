using Jane.Todo.Api.Dto;
using Jane.Todo.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jane.Todo.Api.Controllers
{
	[Route("api/todo")]
	public class TodoController : Controller
	{
		private readonly ITodoService todoService;

		public TodoController(ITodoService todoService)
		{
			this.todoService = todoService;
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpPost]
		public IActionResult Create(TodoTaskDto dto)
		{
			if (dto == null) return this.BadRequest();

			var createdDto = this.todoService.Create(dto);

			return this.Ok(createdDto);
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpPut]
		public IActionResult Update(TodoTaskDto dto)
		{
			if (dto == null) return this.BadRequest();

			var updatedDto = this.todoService.Update(dto);

			return this.Ok(updatedDto);
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpDelete]
		[Route("{id:int}")]
		public IActionResult Delete(int id)
		{
			if (id <= 0) return this.BadRequest();

			this.todoService.Delete(id);

			return this.Ok();
		}

		[Route("{id:int}")]
		[HttpGet]
		public IActionResult Get(int id)
		{
			if (id <= 0) return this.BadRequest();

			return this.Ok(this.todoService.GetById(id));
		}

		[HttpGet]
		public IActionResult Get() => this.Ok(this.todoService.GetAll());

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpPut]
		[Route("{id:int}/finished/{finished:bool}")]
		public IActionResult SetFinished(int id, bool finished)
		{
			if (id <= 0) return this.BadRequest();

			this.todoService.SetFinished(id, finished);

			return this.Ok();
		}
	}
}