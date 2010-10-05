using System;
using System.Data;
using System.Globalization;

namespace OSWebCore
{
    public class PessoaBll : BaseBusinessLayer<Pessoa>
    {
        public PessoaBll(string strConnection)
            : base(DataProvider.SqlServer, strConnection)
        {
            ////
        }

        public Pessoa SelectById(long id)
        {
            var list = this.SelectData(CommandType.Text, "select * from tb_pessoa where id_pessoa = " + id);

            return list.Count > 0 ? list[0] : null;
        }

        public override int InsertData(Pessoa entity)
        {
            int result;

            try
            {
                this.DataAccess.OpenConnection();
                this.DataAccess.BeginTransaction();

                result = this.DataAccess.ExecuteNonQuery(CommandType.Text, "insert into tb_pessoa(nomecompleto, nomeconhecido, cpfcnpj, rgie, id_pessoatipo, dtnascimento, sexo, )");

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
            throw new NotImplementedException();
        }

        protected override Pessoa CreateEntity(IDataRecord record)
        {
            if (record == null)
                throw new ArgumentNullException("record");

            return new Pessoa()
            {
                IdPessoa = record[0].ToLong(),
                NomeCompleto = record[1].ToString(CultureInfo.InvariantCulture),
                NomeConhecido = record[2].ToString(CultureInfo.InvariantCulture),
                CpfCnpj = IsNull(record[3]) ? null : record[3].ToString(CultureInfo.InvariantCulture),
                RgIe = IsNull(record[4]) ? null : record[4].ToString(CultureInfo.InvariantCulture),
                IdPessoaTipo = record[5].ToLong(),
                DtNascimento = record[6].ToNullableDateTime(),
                Sexo = record[7].ToByte(),
                Arquivado = record[8].ToBool(),
                DtArquivacao = record[9].ToNullableDateTime(),
            };
        }
    }
}