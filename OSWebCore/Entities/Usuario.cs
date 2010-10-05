using System;
using System.Collections.ObjectModel;

namespace OSWebCore
{
    public sealed class Usuario : Pessoa
    {
        private long idUsuario;
        private string login;
        private string senha;
        private long? idGrupo;

        [EntityProperty(true)]
        public long IdUsuario
        {
            get
            {
                return this.idUsuario;
            }
            set
            {
                this.idUsuario = value;
            }
        }

        [EntityProperty(true)]
        public string Login
        {
            get
            {
                return this.login;
            }
            set
            {
                this.login = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public string Senha
        {
            get
            {
                return this.senha;
            }
            set
            {
                this.senha = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public long? IdGrupo
        {
            get
            {
                return this.idGrupo;
            }
            set
            {
                this.idGrupo = value;
            }
        }

        protected override Collection<BaseRule> CreateRules()
        {
            Collection<BaseRule> rules = base.CreateRules();

            rules.Add(new SimpleRule("Login", "Campo 'Login' não pode ser branco.", () => { return this.login != null; }));
            rules.Add(new SimpleRule("Login", "Campo 'Login' não pode exceder o limite de 25 caracteres.", () => { return this.login.Length <= 25; }));
            rules.Add(new SimpleRule("Senha", "Campo 'Senha' não pode ser branco.", () => { return this.senha != null; }));
            rules.Add(new SimpleRule("Senha", "Campo 'Senha' não pode exceder o limite de 32 caracteres.", () => { return this.senha.Length <= 32; }));
            rules.Add(new SimpleRule("IdGrupo", "Campo 'Grupo' é inválido.", () => { return !this.idGrupo.HasValue || this.idGrupo.Value > 0; }));

            return rules;
        }
    }
}