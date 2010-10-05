using System;
using System.Collections.ObjectModel;

namespace OSWebCore
{
    public sealed class Permissao : BaseEntity
    {
        private long idPermissao;
        private string url;
        private string descricao;
        private string observacoes;

        [EntityProperty(true)]
        public long IdPermissao
        {
            get
            {
                return this.idPermissao;
            }
            set
            {
                this.idPermissao = value;
            }
        }

        [EntityProperty(true)]
        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = CleanString(value);
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

        [EntityProperty(true)]
        public string Observacoes
        {
            get
            {
                return this.observacoes;
            }
            set
            {
                this.observacoes = CleanString(value);
            }
        }

        protected override Collection<BaseRule> CreateRules()
        {
            Collection<BaseRule> rules = new Collection<BaseRule>();

            rules.Add(new SimpleRule("Url", "Campo 'URL' não pode ser branco.", () => { return this.url != null; }));
            rules.Add(new SimpleRule("Url", "Campo 'URL' não pode exceder o limite de 200 caracteres.", () => { return this.url.Length <= 200; }));
            rules.Add(new SimpleRule("Descricao", "Campo 'Descrição' não pode ser branco.", () => { return this.descricao != null; }));
            rules.Add(new SimpleRule("Descricao", "Campo 'Descrição' não pode exceder o limite de 30 caracteres.", () => { return this.descricao.Length <= 30; }));
            rules.Add(new SimpleRule("Observacoes", "Campo 'Observações' não pode exceder o limite de 1000 caracteres.", () => { return this.observacoes == null || this.observacoes.Length <= 1000; }));

            return rules;
        }
    }
}