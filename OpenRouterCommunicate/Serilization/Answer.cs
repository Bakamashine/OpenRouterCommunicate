using System.Text.Json.Serialization;

namespace OpenRouterCommunicate.Serilization
{
    public class ChatCompletionResponse
    {
        public string id { set; get; } = null!;
        public string provider { set; get; } = null!;
        public string model { set; get; } = null!;
        public string @object { set; get; } = null!;
        public int created { set; get; } 
        public List<Choices> choices { set; get; } = null!;
    }

    public class Choices
    {
        public string logprobs { set; get; } = string.Empty;
        public string finish_reason { set; get; } = null!;
        public string native_finish_reason { set; get; } = null!;
        public int index { set; get; }
        public Message message { set; get; } = null!;
    }

    public class Message
    {
        public string role { set; get; } = null!;
        public string content { set; get; } = null!;
        public string refusal { set; get; } = string.Empty;
        public string reasoning { set; get; } = string.Empty;
    }

    ///other
    /* Example:
        {"id":"gen-1768598902-c6b7oNEA4LEwLeeN30Jq","provider":"Xiaomi","model":"xiaomi/mimo-v2-flash:free","object":"chat.completion","created":1768598902,"choices":[{"logprobs":null,"finish_reason":"stop","native_finish_reason":"stop","index":0,"message":{"role":"assistant","content":"```cpp\n#include <iostream>\n#include <string>\n\nint main() {\n    std::string s = \"Hello, World!\";\n    std::cout << s << std::endl;\n    return 0;\n}\n```","refusal":null,"reasoning":null}}],"usage":{"prompt_tokens":32,"completion_tokens":45,"total_tokens":77,"cost":0,"is_byok":false,"prompt_tokens_details":{"cached_tokens":4,"audio_tokens":0,"video_tokens":0},"cost_details":{"upstream_inference_cost":null,"upstream_inference_prompt_cost":0,"upstream_inference_completions_cost":0},"completion_tokens_details":{"reasoning_tokens":0,"image_tokens":0}}}
    */
}