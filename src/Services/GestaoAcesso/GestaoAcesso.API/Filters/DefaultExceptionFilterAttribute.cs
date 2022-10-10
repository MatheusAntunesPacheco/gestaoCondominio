using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace GestaoAcesso.API.Filters
{
    [ExcludeFromCodeCoverage]
    public class DefaultExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private const string DEFAULT_EXCEPTION = "Ocorreu um erro inesperado.";

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is FluentValidation.ValidationException)
            {
                context.Result = new ObjectResult(new { erro = context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
                return;
            }

            context.Result = new ObjectResult(new { erro = DEFAULT_EXCEPTION })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
