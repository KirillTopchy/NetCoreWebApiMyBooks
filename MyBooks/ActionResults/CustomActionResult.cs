using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBooks.Data.ViewModels;
using System.Threading.Tasks;

namespace MyBooks.ActionResults
{
    public class CustomActionResult : IActionResult
    {
        private readonly CustomActionResultVM _customActionResultVM;

        public CustomActionResult(CustomActionResultVM customActionResultVM)
        {
            _customActionResultVM = customActionResultVM;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_customActionResultVM ?? _customActionResultVM.Publisher as object)
            {
                StatusCode = _customActionResultVM.Exception != null ? StatusCodes.Status500InternalServerError :
                StatusCodes.Status200OK
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
