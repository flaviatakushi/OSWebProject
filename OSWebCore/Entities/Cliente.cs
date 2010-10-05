using System;

namespace OSWebCore
{
    public sealed class Cliente : Pessoa
    {
        private long idCliente;

        [EntityProperty(true)]
        public long IdCliente
        {
            get
            {
                return this.idCliente;
            }
            set
            {
                this.idCliente = value;
            }
        }
    }
}