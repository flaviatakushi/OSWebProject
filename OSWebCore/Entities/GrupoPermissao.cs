using System;
using System.Collections.ObjectModel;

namespace OSWebCore
{
    public sealed class GrupoPermissao : BaseEntity
    {
        private long idGrupoPermissao;
        private long idGrupo;
        private long idPermissao;

        [EntityProperty(true)]
        public long IdGrupoPermissao
        {
            get
            {
                return this.idGrupoPermissao;
            }
            set
            {
                this.idGrupoPermissao = value;
            }
        }

        [EntityProperty(true)]
        public long IdGrupo
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

        protected override Collection<BaseRule> CreateRules()
        {
            Collection<BaseRule> rules = new Collection<BaseRule>();

            rules.Add(new SimpleRule("IdGrupo", "Campo 'Grupo' é inválido.", () => { return this.idGrupo > 0; }));
            rules.Add(new SimpleRule("IdPermissao", "Campo 'Permissão é inválido.", () => { return this.idPermissao > 0; }));

            return rules;
        }
    }
}