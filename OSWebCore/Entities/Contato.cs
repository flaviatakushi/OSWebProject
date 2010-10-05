using System;
using System.Collections.ObjectModel;

namespace OSWebCore
{
    public sealed class Contato : BaseEntity
    {
        private long idContato;
        private long idContatoTipo;
        private long idPessoa;
        private byte? ddd;
        private string telefone;
        private string email;

        [EntityProperty(true)]
        public long IdContato
        {
            get
            {
                return this.idContato;
            }
            set
            {
                this.idContato = value;
            }
        }

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
        public long IdPessoa
        {
            get
            {
                return this.idPessoa;
            }
            set
            {
                this.idPessoa = value;
            }
        }

        [EntityProperty(true)]
        public byte? Ddd
        {
            get
            {
                return this.ddd;
            }
            set
            {
                this.ddd = value;
            }
        }

        [EntityProperty(true)]
        public string Telefone
        {
            get
            {
                return this.telefone;
            }
            set
            {
                this.telefone = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = CleanString(value);
            }
        }

        protected override Collection<BaseRule> CreateRules()
        {
            Collection<BaseRule> rules = new Collection<BaseRule>();

            rules.Add(new SimpleRule("IdContatoTipo", "Campo 'Tipo de Contato' é inválido", () => { return this.idContatoTipo > 0; }));
            rules.Add(new SimpleRule("IdPessoa", "Campo 'Pessoa' é inválido.", () => { return this.idPessoa > 0; }));
            rules.Add(new SimpleRule("Telefone", "Campo 'Telefone' não pode exceder o limite de 15 caracteres.", () => { return this.telefone == null || this.telefone.Length <= 15; }));
            rules.Add(new SimpleRule("Email", "Campo 'E-mail' não pode exceder o limite de 100 caracteres.", () => { return this.email == null || this.email.Length <= 100; }));

            return rules;
        }
    }
}