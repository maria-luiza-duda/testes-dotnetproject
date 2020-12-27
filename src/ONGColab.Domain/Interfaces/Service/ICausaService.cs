using System.Collections.Generic;
using System.Threading.Tasks;
using ONGColab.Domain.ViewModels;

namespace ONGColab.Domain
{
    public interface ICausaService
    {
        Task Adicionar(CausaViewModel model);
        Task<IEnumerable<CausaViewModel>> RecuperarCausas();
    }
}