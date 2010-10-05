using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace OSWebCore
{
    public sealed class Endereco : BaseEntity
    {
        private readonly string[] siglasUf = new string[] { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" };

        private long idEndereco;
        private long idPessoa;
        private string logradouro;
        private string numero;
        private string complemento;
        private string bairro;
        private string cidade;
        private string estado;

        [EntityProperty(true)]
        public long IdEndereco
        {
            get
            {
                return this.idEndereco;
            }
            set
            {
                this.idEndereco = value;
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
        public string Logradouro
        {
            get
            {
                return this.logradouro;
            }
            set
            {
                this.logradouro = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public string Numero
        {
            get
            {
                return this.numero;
            }
            set
            {
                this.numero = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public string Complemento
        {
            get
            {
                return this.complemento;
            }
            set
            {
                this.complemento = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public string Bairro
        {
            get
            {
                return this.bairro;
            }
            set
            {
                this.bairro = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public string Cidade
        {
            get
            {
                return this.cidade;
            }
            set
            {
                this.cidade = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public string Estado
        {
            get
            {
                return this.estado;
            }
            set
            {
                this.estado = CleanString(value);
            }
        }

        protected override Collection<BaseRule> CreateRules()
        {
            Collection<BaseRule> rules = new Collection<BaseRule>();

            rules.Add(new SimpleRule("IdPessoa", "Campo 'Pessoa' é inválido.", () => { return this.idPessoa > 0; }));
            rules.Add(new SimpleRule("Logradouro", "Campo 'Logradouro' não pode ser branco.", () => { return this.logradouro != null; }));
            rules.Add(new SimpleRule("Logradouro", "Campo 'Logradouro' não pode exceder o limite de 200 caracteres.", () => { return this.logradouro.Length <= 200; }));
            rules.Add(new SimpleRule("Numero", "Campo 'Número' não pode ser branco.", () => { return this.numero != null; }));
            rules.Add(new SimpleRule("Numero", "Campo 'Número' não pode exceder o limite de 15 caracteres.", () => { return this.numero.Length <= 15; }));
            rules.Add(new SimpleRule("Complemento", "Campo 'Complemento' não pode exceder o limite de 60 caracteres.", () => { return this.complemento == null || this.complemento.Length <= 60; }));
            rules.Add(new SimpleRule("Bairro", "Campo 'Bairro' não pode ser branco.", () => { return this.bairro != null; }));
            rules.Add(new SimpleRule("Bairro", "Campo 'Bairro' não pode exceder o limite de 40 caracteres.", () => { return this.bairro.Length <= 40; }));
            rules.Add(new SimpleRule("Cidade", "Campo 'Cidade' não pode ser branco.", () => { return this.cidade != null; }));
            rules.Add(new SimpleRule("Cidade", "Campo 'Cidade' não pode exceder o limite de 40 caracteres.", () => { return this.cidade.Length <= 40; }));
            rules.Add(new SimpleRule("Estado", "Campo 'Estado' não pode ser branco.", () => { return this.estado != null; }));
            rules.Add(new SimpleRule("Estado", "Campo 'Estado' é inválido.", () => { return this.siglasUf.Contains(this.estado.ToUpperInvariant()); }));

            return rules;
        }
    }
}