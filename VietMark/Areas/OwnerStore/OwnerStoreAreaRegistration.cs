using System.Web.Mvc;

namespace VietMark.Areas.OwnerStore
{
    public class OwnerStoreAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OwnerStore";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OwnerStore_default",
                "OwnerStore/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}