using NetDevTools.Core.Communication;
using NetDevTools.Core.Exceptions;
using System.Text;
using System.Text.Json;

namespace NetDevTools.WebAPI.Core.Services
{
    public abstract class Service : IService
    {
        protected ResponseResult ValidationResult = new ResponseResult();

        protected StringContent ObterConteudo(object dado)
        {
            return new StringContent(
                JsonSerializer.Serialize(dado),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }

        private async Task TratarErro(HttpResponseMessage response)
        {
            try
            {
                ValidationResult = await DeserializarObjetoResponse<ResponseResult>(response);

                if (!ValidationResult.Errors.Mensagens.Any())
                {
                    var modelState = await DeserializarObjetoResponse<ResponseModelResult>(response);

                    AdicionarErro(modelState);
                }
            }
            catch
            {
                ValidationResult.Errors.Mensagens.Add("Erro ao processar sua requisição. Estamos trabalhando para corrigir o problema");
            }
        }

        private void AdicionarErro(ResponseModelResult modelState)
        {
            foreach (var row in modelState.Errors.Select(kvp => string.Format("{0}: {1}", kvp.Key, string.Join(". ", kvp.Value))))
            {
                ValidationResult.Errors.Mensagens.Add(row);
            }
        }

        protected async Task<bool> TratarErrosResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 400:
                case 412:
                case 424:
                case 428:
                    await TratarErro(response);
                    return false;
                case 401:
                    throw new CustomAuthorizeException("Usuário não autenticado");
                case 404:
                    throw new CustomNotFoundException();
                case 403:
                    throw new CustomForbiddenException("Acesso negado");
                case 500:
                    await TratarErro(response);
                    throw new CustomBadRequestException(ValidarProcessamento() ? "Erro ao processar sua requisição" : ValidationResult.Errors.Mensagens.First());

            }

            response.EnsureSuccessStatusCode();

            return true;
        }

        protected ResponseResult RetornoOk()
        {
            return new ResponseResult();
        }

        public bool ValidarProcessamento()
        {
            return !ValidationResult.Errors.Mensagens.Any();
        }

        public ResponseResult RetornarErros()
        {
            return ValidationResult;
        }
    }
}
