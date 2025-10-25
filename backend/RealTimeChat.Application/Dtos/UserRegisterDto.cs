namespace RealTimeChat.Application.Dtos;

public class UserRegisterDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}
