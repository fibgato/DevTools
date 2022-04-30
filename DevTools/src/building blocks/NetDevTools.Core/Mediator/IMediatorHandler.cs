using FluentValidation.Results;
using NetDevTools.Core.Messages;

namespace NetDevTools.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T Evento) where T : Event;
        Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
    }
}
