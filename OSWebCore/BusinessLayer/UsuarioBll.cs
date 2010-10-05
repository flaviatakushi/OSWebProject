using System;
using System.Data;
using System.Globalization;

namespace OSWebCore
{
    public class UsuarioBll : BaseBusinessLayer<Usuario>
    {
        public UsuarioBll(string strConnection)
            : base(DataProvider.SqlServer, strConnection)
        {
            ////
        }

        public override int InsertData(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public override int DeleteData(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public override int UpdateData(Usuario entity)
        {
            throw new NotImplementedException();
        }

        protected override Usuario CreateEntity(IDataRecord record)
        {
            if (record == null)
                throw new ArgumentNullException("record");

            return new Usuario()
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