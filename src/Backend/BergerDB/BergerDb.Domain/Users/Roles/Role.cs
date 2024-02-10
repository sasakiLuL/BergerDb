using BergerDb.Domain.Users;
using BergerDb.Domain.Users.Permissions;
using System.Reflection;

namespace BergerDb.Domain.Users.Roles;

public sealed class Role
{
    public static readonly Role Admin = new(1, "Admin");

    public static readonly Role Visitor = new(2, "Visitor");

    public static IEnumerable<Role> GetValues()
    {
        IEnumerable<Role> roles = typeof(Role)
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static)
            .ToList().ConvertAll(x => (Role)x.GetValue(null)!);

        return roles;
    }

    private Role() { }

    private Role(long id, string name)
    {
        Id = id;
        Name = name;
    }

    public long Id { get; }

    public string Name { get; }

    public IReadOnlyCollection<Permission> Permissions { get; }

    public IReadOnlyCollection<User> Users { get; }
}
