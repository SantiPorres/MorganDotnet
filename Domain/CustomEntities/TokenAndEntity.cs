namespace Domain.CustomEntities
{
    public class TokenAndEntity<T>
    {
        public required string Token { get; set; }

        public required T Data { get; set; }
    }
}
