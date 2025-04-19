using Todo.Domain.Entities;
using Todo.Infrastructure.Data.Model;

namespace Todo.Infrastructure.Mapping
{
    public static class TaskMapping
    {
        public static TaskModel ToTaskModel(Task task)
        {
            return new TaskModel
            {
                Id = task.Id,
                UserId = task.UserId,
                Goal = task.Goal,
                Notes = task.Notes,
                Category = task.Category,
                Done = task.Done,
                ADayToComplet = task.ADayToComplet,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static Task ToTask(TaskModel taskModel)
        {
            var task = new Task(
                taskModel.UserId,
                taskModel.Goal,
                taskModel.Notes,
                taskModel.Category,
                taskModel.ADayToComplet);

            task.UpdateDayToComplete(taskModel.ADayToComplet);
            return task;
        }
    }
}