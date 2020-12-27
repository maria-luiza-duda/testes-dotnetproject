using System.Collections.Generic;
using System.Threading.Tasks;
using ONGColab.Domain.ViewModels;

namespace ONGColab.Domain
{
    public interface IVoluntariadoService
    {
        Task RealizarDoacaoAsync(VoluntariadoViewModel model);
        Task<IEnumerable<VoluntarixViewModel>> RecuperarVoluntarixsAsync(int pageIndex = 0);
    }
}