namespace Agendamento.API.Infrastructure.Interfaces
{
    public interface IAgendamentosRepository
    {
        Task<Entities.Agendamento> Criar(Entities.Agendamento usuario);
        Entities.Agendamento Obter(int idCondominio, int idAreaCondominio, DateTime data);
    }
}