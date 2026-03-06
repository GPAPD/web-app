namespace web_app.Models
{
    public class MessageRequest
    {
        public string user_input { get; set; }
        public List<object> chat_history { get; set; }
    }
    public class ChatMessage
    {
        public string user { get; set; }
        public string assistant { get; set; }
    }
}
