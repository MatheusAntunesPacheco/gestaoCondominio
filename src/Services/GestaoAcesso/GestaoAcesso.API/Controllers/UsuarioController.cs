using GestaoAcesso.API.Application.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestaoAcesso.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IMediator _mediator;

        public UsuarioController(ILogger<UsuarioController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario(CadastrarUsuarioCommand cadastrarUsuarioCommand)
        {
            _logger.LogInformation($"Cadastrando usuário {cadastrarUsuarioCommand.Nome}");
            var resultado = await _mediator.Send(cadastrarUsuarioCommand);

            return Ok(resultado);
        }
    }
}