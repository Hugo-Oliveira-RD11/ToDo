namespace Todo.Application.TodoTasks.Commands.Update.Title;

public class UpdateTodoTaskTitleCommand
{
    public Guid TaskId { get; init; }
    public Guid UserId { get; init; }
    public string NewTitle { get; init; } = string.Empty;

    public UpdateTodoTaskTitleCommand(Guid taskId, Guid userId, string newTitle)
    {
        TaskId = taskId;
        UserId = userId;
        NewTitle = newTitle;
    }
}