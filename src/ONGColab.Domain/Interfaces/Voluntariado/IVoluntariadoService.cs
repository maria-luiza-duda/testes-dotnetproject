using System.Collections.Generic;
using System.Threading.Tasks;
using ONGColab.Domain.ViewModels;

namespace ONGColab.Domain
{
    public interface IPaymentService
    {
        Task<IEnumerable<CausaViewModel>> RecuperarInstituicoesAsync(int page = 0);
        Task AdicionaVoluntariadoAsync(VoluntariadoViewModel voluntariado);
    }
}