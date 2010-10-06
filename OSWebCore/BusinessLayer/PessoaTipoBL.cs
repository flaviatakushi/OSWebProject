using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;

namespace OSWebCore
{
    public class PessoaTipoBL : BaseBusinessLayer<PessoaTipo>
    {
        public PessoaTipoBL(string strConnection)
            : base(DataProvider.SqlServer, strConnection)
        {
            ////
        }

        public PessoaTipo SelectById(long id)
        {
            var list = this.SelectData(CommandType.Text, "select * from tb_pessoatipo where id_pessoatipo = " + id);

            return list.Count > 0 ? list[0] : null;
        }

        public PessoaTipo SelectByDescricao(string descricao)
        {
            if (descricao == null)
                throw new ArgumentNullException("descricao");

            var list = this.SelectData(CommandType.Text, "select * from tb_pessoatipo where descricao = '" + descricao + "'");

            return list.Count > 0 ? list[0] : null;
        }

        public Collection<PessoaTipo> SelectAll()
        {
            return this.SelectData(CommandType.Text, "select * from tb_pessoatipo");
        }

        public override int InsertData(PessoaTipo entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("Descricao", DbType.String, entity.Descricao);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_INS_PESSOATIPO");

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

        public override int DeleteData(PessoaTipo entity)
        {
            throw new NotImplementedException();
        }

        public override int UpdateData(PessoaTipo entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("Descricao", DbType.String, entity.Descricao);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_UPD_PESSOATIPO");

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

        protected override PessoaTipo CreateEntity(IDataRecord record)
        {
            if (record == null)
                throw new ArgumentNullException("record");

            return new PessoaTipo()
            {
                IdPessoaTipo = record["Id_PessoaTipo"].ToLong(CultureInfo.InvariantCulture),
                Descricao = record["Descricao"].ToString(CultureInfo.CurrentCulture)
            };
        }
    }
}