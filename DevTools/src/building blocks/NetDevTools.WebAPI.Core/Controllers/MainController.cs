using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetDevTools.Core.Communication;

namespace NetDevTools.WebAPI.Core.Controllers
{
    [Authorize]
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();

        protected ActionResult CustomResponse(object result = null, int statusCode = 400)
        {
            if (OperacaoValida())
            {
                if (result == null) return NoContent();

                return Ok(result);
            }

            switch (statusCode)
            {
                case 401:
                    return Unauthorized(new ValidationProblemDetails(new Dictionary<string, string[]> { { "Mensagens", Erros.ToArray() } }));
                default:
                    return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]> { { "Mensagens", Erros.ToArray() } }));
            }
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ResponseResult responseResult)
        {
            foreach (var erro in responseResult.Errors.Mensagens)
            {
                AdicionarErroProcessamento(erro);
            }

            return CustomResponse();
        }

        protected bool OperacaoValida()
        {
            return !Erros.Any();
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            Erros.Add(erro);
        }

        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }
    }
}
