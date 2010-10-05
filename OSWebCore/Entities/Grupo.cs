using System;
using System.Collections.ObjectModel;

namespace OSWebCore
{
    public sealed class Grupo : BaseEntity
    {
        private long idGrupo;
        private string descricao;
        private long idGrupoTipo;

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
        public long IdGrupoTipo
        {
            get
            {
                return this.idGrupoTipo;
            }
            set
            {
                this.idGrupoTipo = value;
            }
        }

        protected override Collection<BaseRule> CreateRules()
        {
            Collection<BaseRule> rules = new Collection<BaseRule>();

            rules.Add(new SimpleRule("Descricao", "Campo 'Descrição' não pode ser branco.", () => { return this.descricao != null; }));
            rules.Add(new SimpleRule("Descricao", "Campo 'Descrição' não pode exceder o limite de 30 caracteres.", () => { return this.descricao.Length <= 30; }));
            rules.Add(new SimpleRule("IdGrupoTipo", "Campo 'Tipo de Grupo' é inválido.", () => { return this.idGrupoTipo > 0; }));

            return rules;
        }
    }
}