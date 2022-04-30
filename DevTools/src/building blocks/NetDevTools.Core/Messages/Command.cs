using FluentValidation.Results;
using MediatR;

namespace NetDevTools.Core.Messages
{
    public abstract class Command : Event, IRequest<ValidationResult>
    {
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            TimeStamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
