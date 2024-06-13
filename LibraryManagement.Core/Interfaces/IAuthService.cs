using LibraryManagement.Core.Enums;

namespace LibraryManagement.Core.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email, Role role);
        string GenerateSha256Hash(string password);
    }
}
