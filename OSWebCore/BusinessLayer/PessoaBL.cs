using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;

namespace OSWebCore
{
    public class PessoaBL : BaseBusinessLayer<Pessoa>
    {
        public PessoaBL(string strConnection)
            : base(DataProvider.SqlServer, strConnection)
        {
            ////
        }

        public Pessoa SelectById(long id)
        {
            var list = this.SelectData(CommandType.Text, "select * from tb_pessoa where id_pessoa = " + id);

            return list.Count > 0 ? list[0] : null;
        }

        public Pessoa SelectByNome(string nome)
        {
            if (nome == null)
                throw new ArgumentNullException("nome");

            var list = this.SelectData(CommandType.Text, "select * from tb_pessoa where nomecompleto = '" + nome + "'");

            return list.Count > 0 ? list[0] : null;
        }

        public Collection<Pessoa> SelectAll()
        {
            return this.SelectData(CommandType.Text, "select * from tb_pessoa");
        }

        public override int InsertData(Pessoa entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("NomeCompleto", DbType.String, entity.NomeCompleto);
                this.DataAccess.AddParameter("NomeConhecido", DbType.String, entity.NomeConhecido);
                this.DataAccess.AddParameter("CpfCnpj", DbType.String, entity.CpfCnpj);
                this.DataAccess.AddParameter("RgIe", DbType.String, entity.RgIe);
                this.DataAccess.AddParameter("IdPessoaTipo", DbType.Int64, entity.IdPessoaTipo);
                this.DataAccess.AddParameter("DtNascimento", DbType.DateTime, entity.DtNascimento);
                this.DataAccess.AddParameter("Sexo", DbType.Byte, entity.Sexo);
                this.DataAccess.AddParameter("Arquivado", DbType.Boolean, entity.Arquivado);
                this.DataAccess.AddParameter("DtArquivacao", DbType.DateTime, entity.DtArquivacao);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_INS_PESSOA");

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

        public override int DeleteData(Pessoa entity)
        {
            throw new NotImplementedException();
        }

        public override int UpdateData(Pessoa entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                this.DataAccess.AddParameter("NomeCompleto", DbType.String, entity.NomeCompleto);
                this.DataAccess.AddParameter("NomeConhecido", DbType.String, entity.NomeConhecido);
                this.DataAccess.AddParameter("CpfCnpj", DbType.String, entity.CpfCnpj);
                this.DataAccess.AddParameter("RgIe", DbType.String, entity.RgIe);
                this.DataAccess.AddParameter("IdPessoaTipo", DbType.Int64, entity.IdPessoaTipo);
                this.DataAccess.AddParameter("DtNascimento", DbType.DateTime, entity.DtNascimento);
                this.DataAccess.AddParameter("Sexo", DbType.Byte, entity.Sexo);
                this.DataAccess.AddParameter("Arquivado", DbType.Boolean, entity.Arquivado);
                this.DataAccess.AddParameter("DtArquivacao", DbType.DateTime, entity.DtArquivacao);

                result = this.DataAccess.ExecuteNonQuery(CommandType.StoredProcedure, "P_UPD_PESSOA");

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

        protected override Pessoa CreateEntity(IDataRecord record)
        {
            if (record == null)
                throw new ArgumentNullException("record");

            return new Pessoa()
            {
                IdPessoa = record["Id_Pessoa"].ToLong(CultureInfo.InvariantCulture),
                NomeCompleto = record["NomeCompleto"].ToString(CultureInfo.CurrentCulture),
                NomeConhecido = record["NomeConhecido"].ToString(CultureInfo.CurrentCulture),
                CpfCnpj = record["CpfCnpj"].ToNullableString(CultureInfo.InvariantCulture),
                RgIe = record["RgIe"].ToNullableString(CultureInfo.InvariantCulture),
                IdPessoaTipo = record["IdPessoaTipo"].ToLong(CultureInfo.InvariantCulture),
                DtNascimento = record["DtNascimento"].ToDateTime(CultureInfo.CurrentCulture),
                Sexo = record["Sexo"].ToByte(CultureInfo.InvariantCulture),
                Arquivado = record["Arquivado"].ToBool(CultureInfo.InvariantCulture),
                DtArquivacao = record["DtArquivacao"].ToNullableDateTime(CultureInfo.CurrentCulture),
            };
        }
    }
}