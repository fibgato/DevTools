using FluentValidation.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace NetDevTools.Core.Messages
{
    public abstract class Command : Event, IRequest<ValidationResult>
    {
        [JsonIgnore]
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
