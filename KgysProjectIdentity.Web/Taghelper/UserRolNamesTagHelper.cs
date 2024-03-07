using KgysProjectIdentity.Repository.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace KgysProjectIdentity.Web.Taghelper
{
    public class UserRolNamesTagHelper : TagHelper
    {
        public string UserId { get; set; } = null!;
        private readonly Microsoft.AspNetCore.Identity.UserManager<AppUser> _userManager;

        public UserRolNamesTagHelper(Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var userroles = await _userManager.GetRolesAsync(user!);
            var stringBuilder = new StringBuilder();
            userroles.ToList().ForEach(x =>
            {
                stringBuilder.Append(@$"<span class='badge bg-secondary mx-1'>{x.ToLower()}</span>");
            });

            output.Content.SetHtmlContent(stringBuilder.ToString());

        }
    }
}
