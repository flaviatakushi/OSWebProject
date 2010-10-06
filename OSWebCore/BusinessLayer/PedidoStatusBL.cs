using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;

namespace OSWebCore
{
    public class PedidoStatusBL : BaseBusinessLayer<PedidoStatus>
    {
        public PedidoStatusBL(string strConnection)
            : base(DataProvider.SqlServer, strConnection)
        {
            ////
        }

        public PedidoStatus SelectById(long id)
        {
            var list = this.SelectData(CommandType.Text, "select * from tb_pedidostatus where id_permissao = " + id);

            return list.Count > 0 ? list[0] : null;
        }

        public PedidoStatus SelectByDescricao(string descricao)
        {
            if (descricao == null)
                throw new ArgumentNullException("descricao");

            var list = this.SelectData(CommandType.Text, "select * from tb_pedidostatus where descricao = '" + descricao + "'");

            return list.Count > 0 ? list[0] : null;
        }

        public Collection<PedidoStatus> SelectAll()
        {
            return this.SelectData(CommandType.Text, "select * from tb_pedidostatus");
        }

        public override int InsertData(PedidoStatus entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("Descricao", DbType.String, entity.Descricao);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_INS_PEDIDOSTATUS");

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

        public override int DeleteData(PedidoStatus entity)
        {
            throw new NotImplementedException();
        }

        public override int UpdateData(PedidoStatus entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("Descricao", DbType.String, entity.Descricao);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_UPD_PEDIDOSTATUS");

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

        protected override PedidoStatus CreateEntity(IDataRecord record)
        {
            if (record == null)
                throw new ArgumentNullException("record");

            return new PedidoStatus()
            {
                IdPedidoStatus = record["Id_PedidoStatus"].ToLong(CultureInfo.InvariantCulture),
                Descricao = record["Descricao"].ToString(CultureInfo.CurrentCulture),
            };
        }
    }
}