using System.Collections.Generic;

namespace Web.Models;

public static class RoleCodes
{
    public const string Admin = "admin";

    public static readonly List<string> AllRoleCodes = new()
    { Admin };
}