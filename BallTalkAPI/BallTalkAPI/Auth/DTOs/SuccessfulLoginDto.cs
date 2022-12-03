namespace BallTalkAPI.Auth.DTOs
{
    public class SuccessfulLoginDto
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public bool IsAdmin { get; }

        public SuccessfulLoginDto(string accessToken, string userName, string userId, IEnumerable<string> roles)
        {
            AccessToken = accessToken;
            UserName = userName;
            UserId = userId;
            IsAdmin = roles.Where(role => role.ToLower() == "admin").Any();
        }
    }
}
