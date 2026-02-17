using مشروع_قبل_الشغل.Data;

namespace مشروع_قبل_الشغل.Authorizetion
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CheckPermissionAtrribute : Attribute
    {
        public CheckPermissionAtrribute(Permission permission)
        {
            Permission = permission;
        }

        public Permission Permission { get; }
    }
}
