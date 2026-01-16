using System.ComponentModel.DataAnnotations;

namespace OpenRouterCommunicate.Request
{
    public class TextRequest
    {
        [Required]
        public string text { set; get; }
    }
}
