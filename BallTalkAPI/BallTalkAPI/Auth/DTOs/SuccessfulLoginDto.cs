namespace BallTalkAPI.Auth.DTOs
{
    public class SuccessfulLoginDto
    {
        public string AccessToken { get; set; }

        public SuccessfulLoginDto(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
