namespace AWS_SQS.Models
{
    public class ToDoItemModel
    {
        public string ? Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateCompleted { get; set; }
        public string ? Title { get; set; }
        public string ? Description { get; set; }
        public bool Complete { get; set; }
    }
}
