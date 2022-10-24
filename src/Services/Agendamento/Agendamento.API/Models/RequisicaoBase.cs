using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Agendamento.API.Models
{
    public abstract class RequisicaoBase
    {
        /// <summary>
        /// Indicador que verifica se requisição é valida
        /// </summary>
        [BindNever]
        public bool Valido
        {
            get
            {
                ErrosValidacao = new List<string>();
                Validar();
                return !ErrosValidacao.Any();
            }
        }

        /// <summary>
        /// Metodo para retornar erros anotados na validação
        /// </summary>
        [BindNever]
        public List<string> Erros => ErrosValidacao;

        /// <summary>
        /// Lista de erros indicados na validação
        /// </summary>
        private List<string> ErrosValidacao { get; set; } = new List<string>();

        /// <summary>
        /// Metodo utilizado para adicionar erro na lista de validação
        /// </summary>
        /// <param name="erro"></param>
        protected void AdicionarErro(string erro)
        {
            ErrosValidacao.Add(erro);
        }

        /// <summary>
        /// Metodo utilizado para validar as requisições.
        /// Será reescrito em cada classe que herda da RequisicaoBase
        /// </summary>
        protected virtual void Validar()
        {
        }
    }
}
