using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NetDevTools.Core.Data;

namespace NetDevTools.Core.Messages
{
    public abstract class CommandHandler
    {

        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AdicionarErro(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected async Task<ValidationResult> SalvarBanco(IUnitOfWork uow)
        {
            try
            {
                if (!await uow.Commit())
                {
                    AdicionarErro("Houve um erro ao persistir os dados!");
                }
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ops! Algo deu errado ao processar seu pedido. Estamos trabalhando para corrigir o problema");
            }
            catch
            {
                throw new Exception($"Ops! Algo deu errado ao processar sua requisição. Tente novamente mais tarde");
            }

            return ValidationResult;
        }
    }
}
