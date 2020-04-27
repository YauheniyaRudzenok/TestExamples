using System;
using Jane.Todo.Api.Entities;
using Jane.Todo.Dto;

namespace Jane.Todo.Api.Services
{
	public static class TodoTaskExtensions
	{
		public static TodoTask ToEntity(this TodoTaskDto dto)
		{
			if (dto == null)
				throw new ArgumentNullException(nameof(dto));

			return new TodoTask
			{
				Id = dto.Id,
				Created = dto.Created,
				DueDate = dto.DueDate,
				Finished = dto.Finished,
				Note = dto.Note,
				Title = dto.Title
			};
		}

		public static TodoTaskDto ToDto(this TodoTask entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			return new TodoTaskDto
			{
				Id = entity.Id,
				Created = entity.Created,
				DueDate = entity.DueDate,
				Finished = entity.Finished,
				Note = entity.Note,
				Title = entity.Title
			};
		}
	}
}