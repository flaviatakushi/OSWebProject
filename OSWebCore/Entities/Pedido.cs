using System;
using System.Collections.ObjectModel;

namespace OSWebCore
{
    public sealed class Pedido : BaseEntity
    {
        private readonly DateTime minDtValue = new DateTime(1900, 1, 1);

        private long idPedido;
        private long idPedidoTipo;
        private long idPedidoStatus;
        private long idCliente;
        private long? idTecnico;
        private long idDeptoTecnico;
        private string descricaoEquipamento;
        private string descricaoDefeito;
        private string descricaoServico;
        private string diagnosticoTecnico;
        private string numSerieEquipamento;
        private float valorPecas;
        private float valorServico;
        private string descricaoCancelamento;
        private DateTime? dtCancelamento;
        private DateTime? dtPgtoConfirmado;

        [EntityProperty(true)]
        public long IdPedido
        {
            get
            {
                return this.idPedido;
            }
            set
            {
                this.idPedido = value;
            }
        }

        [EntityProperty(true)]
        public long IdPedidoTipo
        {
            get
            {
                return this.idPedidoTipo;
            }
            set
            {
                this.idPedidoTipo = value;
            }
        }

        [EntityProperty(true)]
        public long IdPedidoStatus
        {
            get
            {
                return this.idPedidoStatus;
            }
            set
            {
                this.idPedidoStatus = value;
            }
        }

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

        [EntityProperty(true)]
        public long? IdTecnico
        {
            get
            {
                return this.idTecnico;
            }
            set
            {
                this.idTecnico = value;
            }
        }

        [EntityProperty(true)]
        public long IdDeptoTecnico
        {
            get
            {
                return this.idDeptoTecnico;
            }
            set
            {
                this.idDeptoTecnico = value;
            }
        }

        [EntityProperty(true)]
        public string DescricaoEquipamento
        {
            get
            {
                return this.descricaoEquipamento;
            }
            set
            {
                this.descricaoEquipamento = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public string DescricaoDefeito
        {
            get
            {
                return this.descricaoDefeito;
            }
            set
            {
                this.descricaoDefeito = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public string DescricaoServico
        {
            get
            {
                return this.descricaoServico;
            }
            set
            {
                this.descricaoServico = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public string DiagnosticoTecnico
        {
            get
            {
                return this.diagnosticoTecnico;
            }
            set
            {
                this.diagnosticoTecnico = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public string NumSerieEquipamento
        {
            get
            {
                return this.numSerieEquipamento;
            }
            set
            {
                this.numSerieEquipamento = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public float ValorPecas
        {
            get
            {
                return this.valorPecas;
            }
            set
            {
                this.valorPecas = value;
            }
        }

        [EntityProperty(true)]
        public float ValorServico
        {
            get
            {
                return this.valorServico;
            }
            set
            {
                this.valorServico = value;
            }
        }

        [EntityProperty(true)]
        public string DescricaoCancelamento
        {
            get
            {
                return this.descricaoCancelamento;
            }
            set
            {
                this.descricaoCancelamento = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public DateTime? DtCancelamento
        {
            get
            {
                return this.dtCancelamento;
            }
            set
            {
                this.dtCancelamento = value;
            }
        }

        [EntityProperty(true)]
        public DateTime? DtPgtoConfirmado
        {
            get
            {
                return this.dtPgtoConfirmado;
            }
            set
            {
                this.dtPgtoConfirmado = value;
            }
        }

        protected override Collection<BaseRule> CreateRules()
        {
            Collection<BaseRule> rules = new Collection<BaseRule>();

            rules.Add(new SimpleRule("IdPedidoTipo", "Campo 'Tipo de Pedido' é inválido", () => { return this.idPedidoTipo > 0; }));
            rules.Add(new SimpleRule("IdPedidoStatus", "Campo 'Status do Pedido' é inválido.", () => { return this.idPedidoStatus > 0; }));
            rules.Add(new SimpleRule("IdCliente", "Campo 'Cliente' é inválido.", () => { return this.idCliente > 0; }));
            rules.Add(new SimpleRule("IdDeptoTecnico", "Campo 'Departamento Técnico' é inválido.", () => { return this.idDeptoTecnico > 0; }));
            rules.Add(new SimpleRule("DescricaoEquipamento", "Campo 'Descrição do Equipamento' não pode ser branco.", () => { return this.descricaoEquipamento != null; }));
            rules.Add(new SimpleRule("DescricaoEquipamento", "Campo 'Descrição do Equipamento' não pode exceder o limite de 1000 caracteres.", () => { return this.descricaoEquipamento.Length <= 1000; }));
            rules.Add(new SimpleRule("DescricaoDefeito", "Campo 'Descrição do Defeito' não pode ser branco.", () => { return this.descricaoDefeito != null; }));
            rules.Add(new SimpleRule("DescricaoDefeito", "Campo 'Descrição do Defeito' não pode exceder o limite de 1000 caracteres.", () => { return this.descricaoDefeito.Length <= 1000; }));
            rules.Add(new SimpleRule("DescricaoServico", "Campo 'Descrição do Serviço' não pode ser branco.", () => { return this.descricaoServico != null; }));
            rules.Add(new SimpleRule("DescricaoServico", "Campo 'Descrição do Serviço' não pode exceder o limite de 1000 caracteres.", () => { return this.descricaoServico.Length <= 1000; }));
            rules.Add(new SimpleRule("DiagnosticoTecnico", "Campo 'Diagnóstico Técnico' não pode exceder o limite de 1000 caracteres.", () => { return this.diagnosticoTecnico == null || this.diagnosticoTecnico.Length <= 1000; }));
            rules.Add(new SimpleRule("NumSerieEquipamento", "Campo 'Número de Série do Equipamento' não pode ser branco.", () => { return this.numSerieEquipamento != null; }));
            rules.Add(new SimpleRule("NumSerieEquipamento", "Campo 'Número de Série do Equipamento' não pode exceder o limite de 100 caracteres.", () => { return this.numSerieEquipamento.Length <= 100; }));
            rules.Add(new SimpleRule("ValorPecas", "Campo 'Valor das Peças' não pode ser menor que zero.", () => { return this.valorPecas >= 0; }));
            rules.Add(new SimpleRule("ValorServico", "Campo 'Valor do Serviço' não pode ser menor que zero.", () => { return this.valorServico >= 0; }));
            rules.Add(new SimpleRule("DescricaoCancelamento", "Campo 'Descrição do Cancelamento' não pode exceder o limite de 1000 caracteres.", () => { return this.descricaoCancelamento == null || this.descricaoCancelamento.Length <= 1000; }));
            rules.Add(new SimpleRule("DtCancelamento", "Campo 'Data de Cancelamento' deve possuir uma data igual ou superior a 01/01/1900.", () => { return !this.dtCancelamento.HasValue || this.dtCancelamento.Value >= this.minDtValue; }));
            rules.Add(new SimpleRule("DtPgtoConfirmado", "Campo 'Data de Confirmação de Pagamento' deve possuir uma data igual ou superior a 01/01/1900.", () => { return !this.dtPgtoConfirmado.HasValue || this.dtPgtoConfirmado.Value >= this.minDtValue; }));

            return rules;
        }
    }
}