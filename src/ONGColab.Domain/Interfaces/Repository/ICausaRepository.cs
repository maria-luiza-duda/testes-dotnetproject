using System.Collections.Generic;
using System.Threading.Tasks;
using ONGColab.Domain.Entities;

namespace ONGColab.Domain
{
    public interface ICausaRepository
    {
        Task<Causa> Adicionar(Causa causa);
        Task<IEnumerable<Causa>> RecuperarCausas();
    }
}
