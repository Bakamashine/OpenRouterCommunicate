using System.Text.Json.Serialization;  // Для атрибутов, если нужно (опционально)

namespace OpenRouterCommunicate.Serilization
{
    public class ChatCompletionResponse
    {
        public List<Choice> choices { get; set; } = null!;
        public long created { get; set; }
        public string model { get; set; } = null!;
        public string @object { get; set; } = null!;
        public Usage usage { get; set; } = null!;
    }

    public class Choice
    {
        public Message message { get; set; } = null!;
        public int index { get; set; }
        public string finish_reason { get; set; } = null!;
    }

    public class Message
    {
        public string content { get; set; } = null!;
        public string role { get; set; } = null!;
    }

    public class Usage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
        public int precached_prompt_tokens { get; set; }
    }
}