using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;

namespace OSWebCore
{
    public class GrupoPermissaoBL : BaseBusinessLayer<GrupoPermissao>
    {
        public GrupoPermissaoBL(string strConnection)
            : base(DataProvider.SqlServer, strConnection)
        {
            ////
        }

        public GrupoPermissao SelectById(long idGrupoPermissao)
        {
            var list = this.SelectData(CommandType.Text, "select id_grupopermissao, id_grupo, id_permissao from tb_grupopermissao where id_permissao = " + idGrupoPermissao);

            return list.Count > 0 ? list[0] : null;
        }

        public Collection<GrupoPermissao> SelectAll()
        {
            return this.SelectData(CommandType.Text, "select id_grupopermissao, id_grupo, id_permissao from tb_grupopermissao");
        }

        public Collection<GrupoPermissao> SelectAllByGrupo(long idGrupo)
        {
            return this.SelectData(CommandType.Text, "select id_grupopermissao, id_grupo, id_permissao from tb_grupopermissao where id_grupo = " + idGrupo);
        }

        public Collection<GrupoPermissao> SelectAllByPermissao(long idPermissao)
        {
            return this.SelectData(CommandType.Text, "select id_grupopermissao, id_grupo, id_permissao from tb_grupopermissao where id_permissao = " + idPermissao);
        }

        public override int InsertData(GrupoPermissao entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("Id_Grupo", DbType.Int64, entity.IdGrupo);
                this.DataAccess.AddParameter("Id_Permissao", DbType.Int64, entity.IdPermissao);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_INS_GRUPOPERMISSAO");

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

        public override int DeleteData(GrupoPermissao entity)
        {
            throw new NotImplementedException();
        }

        public override int UpdateData(GrupoPermissao entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("Id_Grupo", DbType.Int64, entity.IdGrupo);
                this.DataAccess.AddParameter("Id_Permissao", DbType.Int64, entity.IdPermissao);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_UPD_GRUPOPERMISSAO");

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

        protected override GrupoPermissao CreateEntity(IDataRecord record)
        {
            if (record == null)
                throw new ArgumentNullException("record");

            return new GrupoPermissao()
            {
                IdGrupoPermissao = record["Id_GrupoPermissao"].ToLong(CultureInfo.InvariantCulture),
                IdGrupo = record["Id_Grupo"].ToLong(CultureInfo.InvariantCulture),
                IdPermissao = record["Id_Permissao"].ToLong(CultureInfo.InvariantCulture),
            };
        }
    }
}