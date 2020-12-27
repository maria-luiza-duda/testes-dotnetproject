using System.Collections.Generic;
using System.Threading.Tasks;
using ONGColab.Domain.ViewModels;

namespace ONGColab.Domain
{
    public interface IHomeInfoService
    {
        Task<HomeViewModel> RecuperarDadosIniciaisHomeAsync();        
        Task<IEnumerable<CausaViewModel>> RecuperarCausasAsync();
    }
}