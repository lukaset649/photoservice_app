//Model do przechowywania danych przyjętych z żadania logowania
namespace photoservice.Models;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
