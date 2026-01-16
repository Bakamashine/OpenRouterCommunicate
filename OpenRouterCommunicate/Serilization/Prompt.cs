using dotenv.net.Utilities;

namespace OpenRouterCommunicate.Serilization
{
    struct Rule
    {
        public required string role { set; get; }
        public required string content { set; get; }
    }

    class Prompt
    {
        public string model { set; get; }

        public List<Rule> messages
        {
            get => _message;
            set
            {
                if (value.Count == 0)
                {
                    throw new NullReferenceException("Value is null!");
                }

                _message.AddRange(value);
            }
        }

        private List<Rule> _message = [
            new Rule {role = "system", content = EnvReader.GetStringValue("prompt")}
        ];

        public Prompt(string model)
        {
            this.model = model;
        }
    }
}