namespace BallTalkAPI.Auth.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public UserDto(string id, string userName, string email)
        {
            Id = id;
            UserName = userName;
            Email = email;
        }
    }
}
