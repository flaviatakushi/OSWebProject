using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using OSWebCore.Properties;

namespace OSWebCore
{
    public class Pessoa : BaseEntity
    {
        private readonly DateTime minDtValue = new DateTime(1900, 1, 1);

        private long idPessoa;
        private string nomeCompleto;
        private string nomeConhecido;
        private string cpfCnpj;
        private string rgIe;
        private long idPessoaTipo;
        private DateTime? dtNascimento;
        private byte sexo;
        private bool arquivado;
        private DateTime? dtArquivacao;

        [EntityProperty(true)]
        public long IdPessoa
        {
            get
            {
                return this.idPessoa;
            }
            set
            {
                this.idPessoa = value;
            }
        }

        [EntityProperty(true)]
        public string NomeCompleto
        {
            get
            {
                return this.nomeCompleto;
            }
            set
            {
                this.nomeCompleto = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public string NomeConhecido
        {
            get
            {
                return this.nomeConhecido;
            }
            set
            {
                this.nomeConhecido = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public string CpfCnpj
        {
            get
            {
                return this.cpfCnpj;
            }
            set
            {
                this.cpfCnpj = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public string RgIe
        {
            get
            {
                return this.rgIe;
            }
            set
            {
                this.rgIe = CleanString(value);
            }
        }

        [EntityProperty(true)]
        public long IdPessoaTipo
        {
            get
            {
                return this.idPessoaTipo;
            }
            set
            {
                this.idPessoaTipo = value;
            }
        }

        [EntityProperty(true)]
        public DateTime? DtNascimento
        {
            get
            {
                return this.dtNascimento;
            }
            set
            {
                this.dtNascimento = value;
            }
        }

        [EntityProperty(true)]
        public byte Sexo
        {
            get
            {
                return this.sexo;
            }
            set
            {
                this.sexo = value;
            }
        }

        [EntityProperty(true)]
        public bool Arquivado
        {
            get
            {
                return this.arquivado;
            }
            set
            {
                this.arquivado = value;
            }
        }

        [EntityProperty(true)]
        public DateTime? DtArquivacao
        {
            get
            {
                return this.dtArquivacao;
            }
            set
            {
                this.dtArquivacao = value;
            }
        }

        protected override Collection<BaseRule> CreateRules()
        {
            Collection<BaseRule> rules = new Collection<BaseRule>();

            rules.Add(new SimpleRule("NomeCompleto", "Campo 'Nome' não pode ser branco.", () => { return this.nomeCompleto != null; }));
            rules.Add(new SimpleRule("NomeCompleto", "Campo 'Nome' não pode exceder o limite de 80 caracteres.", () => { return this.nomeCompleto.Length <= 80; }));
            rules.Add(new SimpleRule("NomeConhecido", "Campo 'Nome Conhecido' não pode ser branco.", () => { return this.nomeConhecido != null; }));
            rules.Add(new SimpleRule("NomeConhecido", "Campo 'Nome Conhecido' não pode exceder o limite de 80 caracteres.", () => { return this.nomeConhecido.Length <= 80; }));
            rules.Add(new SimpleRule("CpfCnpj", "Campo 'CPF / CNPJ' é inválido.", () => { return this.cpfCnpj == null || (ValidateCpf(this.cpfCnpj) || ValidateCnpj(this.cpfCnpj)); }));
            rules.Add(new SimpleRule("IdPessoaTipo", "Campo 'Tipo de Pessoa' é inválido.", () => { return this.idPessoaTipo > 0; }));
            rules.Add(new SimpleRule("DtNascimento", "Campo 'Data de Nascimento' deve possuir uma data igual ou superior a 01/01/1900.", () => { return !this.dtNascimento.HasValue || this.dtNascimento.Value >= this.minDtValue; }));
            rules.Add(new SimpleRule("Sexo", "Campo 'Sexo' é inválido.", () => { return this.sexo == 0 || this.sexo == 1; }));
            rules.Add(new SimpleRule("DtArquivacao", "Campo 'Data de Arquivação' deve possuir uma data igual ou superior a 01/01/1900.", () => { return !this.dtArquivacao.HasValue || this.dtArquivacao.Value >= this.minDtValue; }));

            return rules;
        }

        private static bool ValidateCpf(string value)
        {
            if (Regex.IsMatch(value, Resources.CpfPattern))
            {
                int[] cpf = Array.ConvertAll(value.Where(x => { return char.IsDigit(x); }).ToArray(), x => { return Convert.ToInt32(x.ToString(), CultureInfo.InvariantCulture); });

                if (cpf.Distinct().LongCount() != 11)
                    return false;

                int digit1, digit2;

                digit1 = (10 * cpf[0]) + (9 * cpf[1]) + (8 * cpf[2]);
                digit1 += (7 * cpf[3]) + (6 * cpf[4]) + (5 * cpf[5]);
                digit1 += (4 * cpf[6]) + (3 * cpf[7]) + (2 * cpf[8]);
                digit1 = 11 - (digit1 % 11);

                if (digit1 >= 10)
                    digit1 = 0;

                digit2 = (11 * cpf[0]) + (10 * cpf[1]) + (9 * cpf[2]);
                digit2 += (8 * cpf[3]) + (7 * cpf[4]) + (6 * cpf[5]);
                digit2 += (5 * cpf[6]) + (4 * cpf[7]) + (3 * cpf[8]);
                digit2 += 2 * digit1;
                digit2 = 11 - (digit2 % 11);

                if (digit2 >= 10)
                    digit2 = 0;

                return digit1 == cpf[9] && digit2 == cpf[10];
            }

            return false;
        }

        private static bool ValidateCnpj(string value)
        {
            if (Regex.IsMatch(value, Resources.CnpjPattern))
            {
                int[] cnpj = Array.ConvertAll(value.Where(x => { return char.IsDigit(x); }).ToArray(), x => { return Convert.ToInt32(x.ToString(), CultureInfo.InvariantCulture); });

                if (cnpj.Distinct().LongCount() != 14)
                    return false;

                int digit1, digit2;

                digit1 = (5 * cnpj[0]) + (4 * cnpj[1]) + (3 * cnpj[2]) + (2 * cnpj[3]);
                digit1 += (9 * cnpj[4]) + (8 * cnpj[5]) + (7 * cnpj[6]) + (6 * cnpj[7]);
                digit1 += (5 * cnpj[8]) + (4 * cnpj[9]) + (3 * cnpj[10]) + (2 * cnpj[11]);
                digit1 = 11 - (digit1 % 11);

                if (digit1 >= 10)
                    digit1 = 0;

                digit2 = (6 * cnpj[0]) + (5 * cnpj[1]) + (4 * cnpj[2]) + (3 * cnpj[3]);
                digit2 += (2 * cnpj[4]) + (9 * cnpj[5]) + (8 * cnpj[6]) + (7 * cnpj[7]);
                digit2 += (6 * cnpj[8]) + (5 * cnpj[9]) + (4 * cnpj[10]) + (3 * cnpj[11]);
                digit2 += 2 * digit1;
                digit2 = 11 - (digit2 % 11);

                if (digit2 >= 10)
                    digit2 = 0;

                return digit1 == cnpj[12] && digit2 == cnpj[13];
            }

            return false;
        }
    }
}