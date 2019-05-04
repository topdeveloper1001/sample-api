namespace SampleApi.Security
{
    public class Roles
    {
        public const string Admin = nameof(Admin);
        public const string Portal = nameof(Portal);
        public const string Workgroup = nameof(Workgroup);

        public static string[] AllRoles => new[] { Admin, Portal, Workgroup };
    }
}
