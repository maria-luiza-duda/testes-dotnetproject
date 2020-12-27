using System.Threading.Tasks;
using ONGColab.Domain.ViewModels;

namespace ONGColab.Domain
{
    public interface IHomeInfoRepository
    {
        Task<HomeViewModel> RecuperarDadosIniciaisHomeAsync();
    }
}