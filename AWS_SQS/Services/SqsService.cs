using Amazon.SQS.Model;
using AWS_SQS.Interfaces;
using AWS_SQS.Models;
using System.Text.Json;

namespace AWS_SQS.Services
{
    public class SqsService : ISqsService
    {
        private readonly ISqsClientFactory _sqsClientFactory;

        public SqsService(ISqsClientFactory sqsClientFactory)
        {
            _sqsClientFactory = sqsClientFactory;
        }

        public async Task<IEnumerable<ToDoItemModel>> GetToDoItemsAsync()
        {
            var messages = new List<ToDoItemModel>();

            var request = new ReceiveMessageRequest
            {
                QueueUrl = _sqsClientFactory.GetSqsQueue(),
                MaxNumberOfMessages = 10,
                VisibilityTimeout = 10,
                WaitTimeSeconds = 10,
            };

            var response = await _sqsClientFactory.GetSqsClient().ReceiveMessageAsync(request);

            foreach (var message in response.Messages)
            {
                try
                {
                    var m = JsonSerializer.Deserialize<ToDoItemModel>(message.Body);
                    if (m != null)
                        messages.Add(m);

                    //var delRequest = new DeleteMessageRequest
                    //{
                    //    QueueUrl = _sqsClientFactory.GetSqsQueue(),
                    //    ReceiptHandle = message.ReceiptHandle
                    //};

                    //var delResponse = await _sqsClientFactory.GetSqsClient().DeleteMessageAsync(delRequest);
                }
                catch
                {
                    // Invalid message, ignore
                }
            }

            return messages;
        }

        public async Task PublishToDoItemAsync(ToDoItemModel item)
        {
            var request = new SendMessageRequest
            {
                MessageBody = JsonSerializer.Serialize(item),
                QueueUrl = _sqsClientFactory.GetSqsQueue(),
            };

            var client = _sqsClientFactory.GetSqsClient();
            await client.SendMessageAsync(request);
        }
    }
}
