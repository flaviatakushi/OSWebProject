using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;

namespace OSWebCore
{
    public class PermissaoBL : BaseBusinessLayer<Permissao>
    {
        public PermissaoBL(string strConnection)
            : base(DataProvider.SqlServer, strConnection)
        {
            ////
        }

        public Permissao SelectById(long id)
        {
            var list = this.SelectData(CommandType.Text, "select * from tb_permissao where id_permissao = " + id);

            return list.Count > 0 ? list[0] : null;
        }

        public Permissao SelectByDescricao(string descricao)
        {
            if (descricao == null)
                throw new ArgumentNullException("descricao");

            var list = this.SelectData(CommandType.Text, "select * from tb_permissao where descricao = '" + descricao + "'");

            return list.Count > 0 ? list[0] : null;
        }

        public Collection<Permissao> SelectAll()
        {
            return this.SelectData(CommandType.Text, "select * from tb_permissao");
        }

        public override int InsertData(Permissao entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("Url", DbType.String, entity.Url);
                this.DataAccess.AddParameter("Descricao", DbType.String, entity.Descricao);
                this.DataAccess.AddParameter("Observacoes", DbType.String, entity.Observacoes);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_INS_PERMISSAO");

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

        public override int DeleteData(Permissao entity)
        {
            throw new NotImplementedException();
        }

        public override int UpdateData(Permissao entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("Url", DbType.String, entity.Url);
                this.DataAccess.AddParameter("Descricao", DbType.String, entity.Descricao);
                this.DataAccess.AddParameter("Observacoes", DbType.String, entity.Observacoes);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_UPD_PERMISSAO");

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

        protected override Permissao CreateEntity(IDataRecord record)
        {
            if (record == null)
                throw new ArgumentNullException("record");

            return new Permissao()
            {
                IdPermissao = record["Id_Permissao"].ToLong(CultureInfo.InvariantCulture),
                Url = record["Url"].ToString(CultureInfo.InvariantCulture),
                Descricao = record["Descricao"].ToString(CultureInfo.CurrentCulture),
                Observacoes = record["Observacoes"].ToNullableString(CultureInfo.CurrentCulture)
            };
        }
    }
}