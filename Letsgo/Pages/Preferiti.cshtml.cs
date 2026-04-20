using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Letsgo.Pages
{
    [Authorize]
    public class PreferitiModel : PageModel
    {
        
        public void OnGet()
        {
        }
    }
}
