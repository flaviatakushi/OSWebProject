using System;
using System.Collections.ObjectModel;

namespace OSWebCore
{
    public sealed class ContatoTipo : BaseEntity
    {
        private long idContatoTipo;
        private string descricao;

        [EntityProperty(true)]
        public long IdContatoTipo
        {
            get
            {
                return this.idContatoTipo;
            }
            set
            {
                this.idContatoTipo = value;
            }
        }

        [EntityProperty(true)]
        public string Descricao
        {
            get
            {
                return this.descricao;
            }
            set
            {
                this.descricao = CleanString(value);
            }
        }

        protected override Collection<BaseRule> CreateRules()
        {
            Collection<BaseRule> rules = new Collection<BaseRule>();

            rules.Add(new SimpleRule("Descricao", "Campo 'Descrição' não pode ser branco.", () => { return this.descricao != null; }));
            rules.Add(new SimpleRule("Descricao", "Campo 'Descrição' não pode exceder o limite de 30 caracteres.", () => { return this.descricao.Length <= 30; }));

            return rules;
        }
    }
}