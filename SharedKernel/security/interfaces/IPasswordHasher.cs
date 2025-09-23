namespace WebAPI_Template_Starter.Features.AuthAPI.Utils;

public interface IPasswordHasher
{
    String hashPassword(String username, String password);
    Boolean verifyPassword(String username,String hashedPassword, String providedPassword);
}