using System;
using System.Linq;
using System.Security.Cryptography;

namespace Web.Services;

public class Rfc2898Hasher : IHasher
{
    public string Hash(string source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        using Rfc2898DeriveBytes bytes = new(source, 0x10, 0x3e8);
        var salt = bytes.Salt;
        var buffer2 = bytes.GetBytes(0x20);
        
        var dst = new byte[0x31];
        Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
        Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
        return Convert.ToBase64String(dst);
    }
    public bool VerifyHash(string hashed, string expected)
    {
        if (hashed == null)
            throw new ArgumentNullException(nameof(expected));
            
        if (expected == null)
            throw new ArgumentNullException(nameof(expected));
            
        var src = Convert.FromBase64String(hashed);
        if (src.Length != 0x31 || src[0] != 0)
            return false;
        
        var dst = new byte[0x10];
        Buffer.BlockCopy(src, 1, dst, 0, 0x10);
        var buffer3 = new byte[0x20];
        Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
        using var bytes = new Rfc2898DeriveBytes(expected, dst, 0x3e8);
        var buffer4 = bytes.GetBytes(0x20);
        return buffer3.SequenceEqual(buffer4);
    }
}