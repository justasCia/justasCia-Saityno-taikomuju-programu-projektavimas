namespace BallTalkAPI.Auth.DTOs
{
    public class SuccessfulLoginDto
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; }

        public SuccessfulLoginDto(string accessToken, string userName, IEnumerable<string> roles)
        {
            AccessToken = accessToken;
            UserName = userName;
            IsAdmin = roles.Where(role => role.ToLower() == "admin").Any();
        }
    }
}
