using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;

namespace OSWebCore
{
    public class PedidoTipoBL : BaseBusinessLayer<PedidoTipo>
    {
        public PedidoTipoBL(string strConnection)
            : base(DataProvider.SqlServer, strConnection)
        {
            ////
        }

        public PedidoTipo SelectById(long id)
        {
            var list = this.SelectData(CommandType.Text, "select * from tb_pedidotipo where id_permissao = " + id);

            return list.Count > 0 ? list[0] : null;
        }

        public PedidoTipo SelectByDescricao(string descricao)
        {
            if (descricao == null)
                throw new ArgumentNullException("descricao");

            var list = this.SelectData(CommandType.Text, "select id_pedidotipo, descricao from tb_pedidotipo where descricao = '" + descricao + "'");

            return list.Count > 0 ? list[0] : null;
        }

        public Collection<PedidoTipo> SelectAll()
        {
            return this.SelectData(CommandType.Text, "select id_pedidotipo, descricao from tb_pedidotipo");
        }

        public override int InsertData(PedidoTipo entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("Descricao", DbType.String, entity.Descricao);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_INS_PEDIDOTIPO");

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

        public override int DeleteData(PedidoTipo entity)
        {
            throw new NotImplementedException();
        }

        public override int UpdateData(PedidoTipo entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("Descricao", DbType.String, entity.Descricao);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_UPD_PEDIDOTIPO");

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

        protected override PedidoTipo CreateEntity(IDataRecord record)
        {
            if (record == null)
                throw new ArgumentNullException("record");

            return new PedidoTipo()
            {
                IdPedidoTipo = record["Id_PedidoTipo"].ToLong(CultureInfo.InvariantCulture),
                Descricao = record["Descricao"].ToString(CultureInfo.CurrentCulture),
            };
        }
    }
}