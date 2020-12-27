using System.Collections.Generic;
using System.Threading.Tasks;
using ONGColab.Domain.Entities;

namespace ONGColab.Domain
{
    public interface IVoluntariadoRepository
    {
        Task AdicionarAsync(Voluntariado model);
        Task<IEnumerable<Voluntariado>> RecuperarVoluntariadosAsync(int pageIndex = 0);
    }
}