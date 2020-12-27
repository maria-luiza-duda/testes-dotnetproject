using Polly.Retry;

namespace ONGColab.Service
{
    public interface IPollyService
    {
        AsyncRetryPolicy CriarPoliticaWaitAndRetryPara(string method);
    }
}
