using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;

namespace OSWebCore
{
    public class GrupoBL : BaseBusinessLayer<Grupo>
    {
        public GrupoBL(string strConnection)
            : base(DataProvider.SqlServer, strConnection)
        {
            ////
        }

        public Grupo SelectById(long idGrupo)
        {
            var list = this.SelectData(CommandType.Text, "select id_grupo, descricao, id_grupotipo from tb_grupo where id_grupo = " + idGrupo);

            return list.Count > 0 ? list[0] : null;
        }

        public Grupo SelectByDescricao(string descricao)
        {
            if (descricao == null)
                throw new ArgumentNullException("descricao");

            var list = this.SelectData(CommandType.Text, "select id_grupo, descricao, id_grupotipo from tb_grupo where descricao like '" + descricao + "%'");

            return list.Count > 0 ? list[0] : null;
        }

        public Collection<Grupo> SelectAll()
        {
            return this.SelectData(CommandType.Text, "select id_grupo, descricao, id_grupotipo from tb_grupo");
        }

        public Collection<Grupo> SelectAllByTipo(long idGrupoTipo)
        {
            return this.SelectData(CommandType.Text, "select id_grupo, descricao, id_grupotipo from tb_grupo where id_grupotipo = " + idGrupoTipo);
        }

        public override int InsertData(Grupo entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("Descricao", DbType.String, entity.Descricao);
                this.DataAccess.AddParameter("Id_GrupoTipo", DbType.Int64, entity.IdGrupoTipo);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_INS_GRUPO");

                this.DataAccess.CommitTransaction();
            }
            catch (Exception)
            {
                this.DataAccess.RollbackTransaction();
                throw;
            }
            finally
            {
                this.DataAccess.CloseConnection();
            }

            return result;
        }

        public override int DeleteData(Grupo entity)
        {
            throw new NotImplementedException();
        }

        public override int UpdateData(Grupo entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("Descricao", DbType.String, entity.Descricao);
                this.DataAccess.AddParameter("Id_GrupoTipo", DbType.Int64, entity.IdGrupoTipo);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_UPD_GRUPO");

                this.DataAccess.CommitTransaction();
            }
            catch (Exception)
            {
                this.DataAccess.RollbackTransaction();
                throw;
            }
            finally
            {
                this.DataAccess.CloseConnection();
            }

            return result;
        }

        protected override Grupo CreateEntity(IDataRecord record)
        {
            if (record == null)
                throw new ArgumentNullException("record");

            return new Grupo()
            {
                IdGrupo = record["Id_Grupo"].ToLong(CultureInfo.InvariantCulture),
                Descricao = record["Descricao"].ToString(CultureInfo.InvariantCulture),
                IdGrupoTipo = record["Id_GrupoTipo"].ToLong(CultureInfo.InvariantCulture)
            };
        }
    }
}