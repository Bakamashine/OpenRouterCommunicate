namespace OpenRouterCommunicate.Serilization
{
    class Token
    {
        public string access_token { get; set; } = null!;
        public long expires_at { get; set; }
    }
}