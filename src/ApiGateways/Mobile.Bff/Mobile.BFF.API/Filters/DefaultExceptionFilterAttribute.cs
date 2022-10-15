using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace Mobile.BFF.API.Filters
{
    [ExcludeFromCodeCoverage]
    public class DefaultExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private const string DEFAULT_EXCEPTION = "Ocorreu um erro inesperado.";

        public override void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(new { erro = DEFAULT_EXCEPTION })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
