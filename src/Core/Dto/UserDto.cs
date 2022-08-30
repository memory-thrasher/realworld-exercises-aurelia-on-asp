using Realworlddotnet.Core.Entities;

namespace Realworlddotnet.Core.Dto
{

    public record NewUserDto(string Username, string Email, string Password)
    {
        public User ToUser()
        {
            return new User() { Username = Username, Email = Email, Password = Password };
        }
    }

    public record LoginUserDto(string Email, string Password);

    /// <summary>
    ///     At lease one of the items should be not null
    /// </summary>
    public record UpdatedUserDto(string? Username, string? Email, string? Bio, string? Image, string? Password);

    public record UserDto(string Username, string Email, string Token, string Bio, string Image);

}
