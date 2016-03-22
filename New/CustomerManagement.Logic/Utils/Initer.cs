using NullGuard;

[assembly: NullGuard(ValidationFlags.All)]

namespace CustomerManagement.Logic.Utils
{
    public static class Initer
    {
        public static void Init(string connectionString)
        {
            SessionFactory.Init(connectionString);
        }
    }
}
