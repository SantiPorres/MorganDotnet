namespace Application.Wrappers
{
    public class JwtTokenResponse
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; }

        public JwtTokenResponse(bool succeeded, string token)
        {
            Succeeded = succeeded;
            Token = token;
        }
    }
}
