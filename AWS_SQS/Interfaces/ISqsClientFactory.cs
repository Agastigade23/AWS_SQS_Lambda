using Amazon.SQS;

namespace AWS_SQS.Interfaces
{
    public interface ISqsClientFactory
    {
        IAmazonSQS GetSqsClient();
        string GetSqsQueue();
    }
}
