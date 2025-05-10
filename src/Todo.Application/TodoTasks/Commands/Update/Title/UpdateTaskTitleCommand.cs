namespace Todo.Application.TodoTasks.Commands.Update.Title;

public class UpdateTaskTitleCommand
{
    public Guid TaskId { get; init; }
    public Guid UserId { get; init; }
    public string NewTitle { get; init; } = string.Empty;

    public UpdateTaskTitleCommand(Guid taskId, Guid userId, string newTitle)
    {
        TaskId = taskId;
        UserId = userId;
        NewTitle = newTitle;
    }
}