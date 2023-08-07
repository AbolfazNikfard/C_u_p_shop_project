using System.ComponentModel.DataAnnotations;

namespace C_u_p_Shop_Project.PersianTranslationError
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
    AllowMultiple = false)]
    public class PersianRequiredAttribute : RequiredAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return $"وارد {name} الزامی است";
        }
    }
}
