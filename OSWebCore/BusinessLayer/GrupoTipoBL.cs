using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;

namespace OSWebCore
{
    public class GrupoTipoBL : BaseBusinessLayer<GrupoTipo>
    {
        public GrupoTipoBL(string strConnection)
            : base(DataProvider.SqlServer, strConnection)
        {
            ////
        }

        public GrupoTipo SelectById(long id)
        {
            var list = this.SelectData(CommandType.Text, "select id_grupotipo, descricao from tb_grupotipo where id_permissao = " + id);

            return list.Count > 0 ? list[0] : null;
        }

        public GrupoTipo SelectByDescricao(string descricao)
        {
            if (descricao == null)
                throw new ArgumentNullException("descricao");

            var list = this.SelectData(CommandType.Text, "select id_grupotipo, descricao from tb_grupotipo where descricao like '" + descricao + "%'");

            return list.Count > 0 ? list[0] : null;
        }

        public Collection<GrupoTipo> SelectAll()
        {
            return this.SelectData(CommandType.Text, "select id_grupotipo, descricao from tb_grupotipo");
        }

        public override int InsertData(GrupoTipo entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("Descricao", DbType.String, entity.Descricao);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_INS_GRUPOTIPO");

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

        public override int DeleteData(GrupoTipo entity)
        {
            throw new NotImplementedException();
        }

        public override int UpdateData(GrupoTipo entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("Descricao", DbType.String, entity.Descricao);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_UPD_GRUPOTIPO");

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

        protected override GrupoTipo CreateEntity(IDataRecord record)
        {
            if (record == null)
                throw new ArgumentNullException("record");

            return new GrupoTipo()
            {
                IdGrupoTipo = record["Id_GrupoTipo"].ToLong(CultureInfo.InvariantCulture),
                Descricao = record["Descricao"].ToString(CultureInfo.CurrentCulture),
            };
        }
    }
}