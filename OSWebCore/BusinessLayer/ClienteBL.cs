using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;

namespace OSWebCore
{
    public class ClienteBL : BaseBusinessLayer<Cliente>
    {
        public ClienteBL(string strConnection)
            : base(DataProvider.SqlServer, strConnection)
        {
            ////
        }

        public Cliente SelectById(long id)
        {
            var list = this.SelectData(CommandType.Text, "select * from tb_cliente where id_usuario = " + id);

            return list.Count > 0 ? list[0] : null;
        }

        public Cliente SelectByNome(string nome)
        {
            if (nome == null)
                throw new ArgumentNullException("nome");

            var list = this.SelectData(CommandType.Text, "select * from tb_usuario where nomecompleto = '" + nome + "'");

            return list.Count > 0 ? list[0] : null;
        }

        public Collection<Cliente> SelectAll()
        {
            return this.SelectData(CommandType.Text, "select * from tb_cliente");
        }

        public override int InsertData(Cliente entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("IdPessoa", DbType.Int64, entity.IdPessoa);
                this.DataAccess.AddParameter("NomeCompleto", DbType.String, entity.NomeCompleto);
                this.DataAccess.AddParameter("NomeConhecido", DbType.String, entity.NomeConhecido);
                this.DataAccess.AddParameter("CpfCnpj", DbType.String, entity.CpfCnpj);
                this.DataAccess.AddParameter("RgIe", DbType.String, entity.RgIe);
                this.DataAccess.AddParameter("IdPessoaTipo", DbType.Int64, entity.IdPessoaTipo);
                this.DataAccess.AddParameter("DtNascimento", DbType.DateTime, entity.DtNascimento);
                this.DataAccess.AddParameter("Sexo", DbType.Byte, entity.Sexo);
                this.DataAccess.AddParameter("Arquivado", DbType.Boolean, entity.Arquivado);
                this.DataAccess.AddParameter("DtArquivacao", DbType.DateTime, entity.DtArquivacao);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_INS_USUARIO");

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

        public override int DeleteData(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public override int UpdateData(Cliente entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("IdPessoa", DbType.Int64, entity.IdPessoa);
                this.DataAccess.AddParameter("NomeCompleto", DbType.String, entity.NomeCompleto);
                this.DataAccess.AddParameter("NomeConhecido", DbType.String, entity.NomeConhecido);
                this.DataAccess.AddParameter("CpfCnpj", DbType.String, entity.CpfCnpj);
                this.DataAccess.AddParameter("RgIe", DbType.String, entity.RgIe);
                this.DataAccess.AddParameter("IdPessoaTipo", DbType.Int64, entity.IdPessoaTipo);
                this.DataAccess.AddParameter("DtNascimento", DbType.DateTime, entity.DtNascimento);
                this.DataAccess.AddParameter("Sexo", DbType.Byte, entity.Sexo);
                this.DataAccess.AddParameter("Arquivado", DbType.Boolean, entity.Arquivado);
                this.DataAccess.AddParameter("DtArquivacao", DbType.DateTime, entity.DtArquivacao);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_UPD_USUARIO");

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

        protected override Cliente CreateEntity(IDataRecord record)
        {
            if (record == null)
                throw new ArgumentNullException("record");

            return new Cliente()
            {
                IdCliente = record["Id_Cliente"].ToLong(CultureInfo.InvariantCulture),
                IdPessoa = record["Id_Pessoa"].ToLong(CultureInfo.InvariantCulture),
                NomeCompleto = record["NomeCompleto"].ToString(CultureInfo.CurrentCulture),
                NomeConhecido = record["NomeConhecido"].ToString(CultureInfo.CurrentCulture),
                CpfCnpj = record["CpfCnpj"].ToNullableString(CultureInfo.InvariantCulture),
                RgIe = record["RgIe"].ToNullableString(CultureInfo.InvariantCulture),
                IdPessoaTipo = record["Id_PessoaTipo"].ToLong(CultureInfo.InvariantCulture),
                DtNascimento = record["DtNascimento"].ToDateTime(CultureInfo.CurrentCulture),
                Sexo = record["Sexo"].ToByte(CultureInfo.InvariantCulture),
                Arquivado = record["Arquivado"].ToBool(CultureInfo.InvariantCulture),
                DtArquivacao = record["DtArquivacao"].ToNullableDateTime(CultureInfo.CurrentCulture),
            };
        }
    }
}