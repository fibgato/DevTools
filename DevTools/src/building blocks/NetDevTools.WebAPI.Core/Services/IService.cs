using NetDevTools.Core.Communication;

namespace NetDevTools.WebAPI.Core.Services
{
    public interface IService
    {
        bool ValidarProcessamento();
        ResponseResult RetornarErros();
    }
}
