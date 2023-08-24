using AWS_SQS.Models;

namespace AWS_SQS.Interfaces
{
    public interface ISqsService
    {
        Task<IEnumerable<ToDoItemModel>> GetToDoItemsAsync();
        Task PublishToDoItemAsync(ToDoItemModel item);
    }
}
