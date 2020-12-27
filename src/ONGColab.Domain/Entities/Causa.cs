using System;
using ONGColab.Domain.Base;

namespace ONGColab.Domain.Entities
{
    public class Causa : Entity
    {
        private Causa() { }

        public Causa(Guid id, string ong, string cidade, string estado)
        {
            Id = id;
            ONG = ong;
            Cidade = cidade;
            Estado = estado;
        }

        public string ONG { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        public override bool Valido()
        {
            return true;
        }
    }
}
