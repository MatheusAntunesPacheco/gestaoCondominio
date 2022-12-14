using MediatR;
using Mobile.BFF.API.Entities;

namespace Mobile.BFF.API.Application.Command.LerTokenJwt
{
    /// <summary>
    /// Leitura do paload do token JWT
    /// </summary>
    public class LerPayloadTokenJwtCommand : IRequest<PayloadTokenJwt>
    {
        /// <summary>
        /// Token WT
        /// </summary>
        public string TokenJwt { get; private set; }

        public LerPayloadTokenJwtCommand(string tokenJwt)
        {
            TokenJwt = tokenJwt;
        }
    }
}