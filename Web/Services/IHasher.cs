namespace Web.Services;

public interface IHasher
{
    string Hash(string source);
    bool VerifyHash(string hashed, string expected);
}