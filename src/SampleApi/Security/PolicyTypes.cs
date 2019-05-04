namespace SampleApi.Security
{
    public class PolicyTypes
    {
        public const string IsAdmin = nameof(IsAdmin);
        public const string IsAdminOrPortal = nameof(IsAdminOrPortal);
        public const string IsPortal = nameof(IsPortal);
        public const string IsWorkgroup = nameof(IsWorkgroup);
        public const string All = nameof(All);
    }
}
