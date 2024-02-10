namespace BergerDb.Domain.Users.Permissions;

public class Permission
{
    public Permission(long id, string name)
    {
        Id = id;
        Name = name;
    }

    public long Id { get; }

    public string Name { get; }
}
