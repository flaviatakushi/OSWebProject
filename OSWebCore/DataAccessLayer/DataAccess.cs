using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using OSWebCore.Properties;

[assembly:CLSCompliant(true)]
namespace OSWebCore
{
    public enum DataProvider
    {
        Odbc,
        OleDB,
        SqlServer
    }

    public sealed class DataAccess : IDisposable
    {
        private bool isDisposed;
        private bool errorOnTransaction;

        private DataProvider dataProvider;

        private DbCommand dbCommand;
        private DbConnection dbConnection;
        private DbConnectionStringBuilder dbConnectionStringBuilder;
        private DbDataReader dbDataReader;
        private DbTransaction dbTransaction;

        private Dictionary<string, DbParameter> dbParameters = new Dictionary<string, DbParameter>();

        public DataAccess(DataProvider dataProvider, string connectionString)
        {
            if (!Enum.IsDefined(typeof(DataProvider), dataProvider))
                throw new ArgumentException(Resources.DataAccessInvalidDataProvider, "dataProvider");

            this.dataProvider = dataProvider;
            this.dbConnectionStringBuilder = DataAccessBuilder.CreateConnectionStringBuilder(dataProvider);
            this.dbConnectionStringBuilder.ConnectionString = connectionString;
        }

        ~DataAccess()
        {
            this.Dispose(false);
        }

        public string ConnectionString
        {
            get
            {
                return this.dbConnectionStringBuilder.ConnectionString;
            }
            set
            {
                this.dbConnectionStringBuilder.ConnectionString = value;
            }
        }

        public ConnectionState ConnectionState
        {
            get
            {
                return this.dbConnection.State;
            }
        }

        public DataProvider DataProvider
        {
            get
            {
                return this.dataProvider;
            }
        }

        public DbCommand Command
        {
            get
            {
                return this.dbCommand;
            }
        }

        public DbConnection Connection
        {
            get
            {
                return this.dbConnection;
            }
        }

        public DbConnectionStringBuilder ConnectionStringBuilder
        {
            get
            {
                return this.dbConnectionStringBuilder;
            }
        }

        public DbDataReader DataReader
        {
            get
            {
                return this.dbDataReader;
            }
        }

        public DbTransaction Transaction
        {
            get
            {
                return this.dbTransaction;
            }
        }

        public ReadOnlyDictionary<string, DbParameter> Parameters
        {
            get
            {
                return new ReadOnlyDictionary<string, DbParameter>(this.dbParameters);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void OpenConnection()
        {
            if (this.dbConnection == null)
                this.dbConnection = DataAccessBuilder.CreateConnection(this.dataProvider);

            if (this.dbConnection.State != ConnectionState.Open)
            {
                this.dbConnection.ConnectionString = this.dbConnectionStringBuilder.ConnectionString;
                this.dbConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (this.dbConnection != null && this.dbConnection.State != ConnectionState.Closed)
                this.dbConnection.Close();
        }

        public void BeginTransaction()
        {
            this.BeginTransaction(IsolationLevel.Unspecified);
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (!this.errorOnTransaction)
            {
                if (this.dbConnection == null || this.dbConnection.State != ConnectionState.Open)
                {
                    this.errorOnTransaction = true;
                    throw new InvalidOperationException(Resources.DataAccessConnectionNotOpened);
                }

                this.dbTransaction = this.dbConnection.BeginTransaction(isolationLevel);
            }

            this.errorOnTransaction = false;
        }

        public void CommitTransaction()
        {
            if (!this.errorOnTransaction)
            {
                if (this.dbTransaction == null)
                {
                    this.errorOnTransaction = true;
                    throw new InvalidOperationException(Resources.DataAccessTransactionNotInitialized);
                }

                this.dbTransaction.Commit();
            }

            this.errorOnTransaction = false;
        }

        public void RollbackTransaction()
        {
            if (!this.errorOnTransaction)
            {
                if (this.dbTransaction == null)
                {
                    this.errorOnTransaction = true;
                    throw new InvalidOperationException(Resources.DataAccessTransactionNotInitialized);
                }

                this.dbTransaction.Rollback();
            }

            this.errorOnTransaction = false;
        }

        public void AddParameter(string parameterName, DbType parameterType, object parameterValue)
        {
            this.AddParameter(parameterName, parameterType, parameterValue, 0, ParameterDirection.Input);
        }

        public void AddParameter(string parameterName, DbType parameterType, object parameterValue, int parameterSize)
        {
            this.AddParameter(parameterName, parameterType, parameterValue, parameterSize, ParameterDirection.Input);
        }

        public void AddParameter(string parameterName, DbType parameterType, object parameterValue, int parameterSize, ParameterDirection parameterDirection)
        {
            parameterName = (parameterName ?? string.Empty).Trim();

            if (parameterName.Length == 0)
                throw new ArgumentException(Resources.DataAccessInvalidParameterName, "parameterName");

            if (this.dbParameters.ContainsKey(parameterName))
                throw new ArgumentException(Resources.DataAccessParameterNameAlreadyExists.Replace("{0}", parameterName), "parameterName");

            DbParameter parameter = DataAccessBuilder.CreateParameter(this.dataProvider);

            parameter.DbType = parameterType;
            parameter.Direction = parameterDirection;
            parameter.ParameterName = parameterName;
            parameter.Size = parameterSize;
            parameter.Value = parameterValue;

            this.dbParameters.Add(parameterName, parameter);
        }

        public void RemoveParameters(params string[] parameterNames)
        {
            if (parameterNames == null || parameterNames.Contains(null))
                throw new ArgumentNullException("parameterNames");

            if (parameterNames.Length == 0)
                this.dbParameters.Clear();
            else
                parameterNames.ForEach(x => this.dbParameters.Remove(x));
        }

        public void CloseReader()
        {
            if (this.dbDataReader != null)
            {
                if (!this.dbDataReader.IsClosed)
                    this.dbDataReader.Close();
            }
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            this.CreateCommand(commandType, commandText);

            return this.dbCommand.ExecuteNonQuery();
        }

        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            this.CreateCommand(commandType, commandText);

            return this.dbCommand.ExecuteScalar();
        }

        public DataSet ExecuteDataSet(CommandType commandType, string commandText, string tableName)
        {
            this.CreateCommand(commandType, commandText);

            DataSet dataSet = new DataSet();
            dataSet.Locale = CultureInfo.InvariantCulture;

            using (DbDataAdapter dbDataAdapter = DataAccessBuilder.CreateDataAdapter(this.dataProvider))
            {
                dbDataAdapter.SelectCommand = this.dbCommand;
                dbDataAdapter.Fill(dataSet, tableName);
            }

            return dataSet;
        }

        public DbDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            return this.ExecuteReader(commandType, commandText, CommandBehavior.Default);
        }

        public DbDataReader ExecuteReader(CommandType commandType, string commandText, CommandBehavior commandBehavior)
        {
            this.CreateCommand(commandType, commandText);

            this.dbDataReader = this.dbCommand.ExecuteReader(commandBehavior);

            return this.dbDataReader;
        }

        private void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    if (this.dbParameters != null)
                        this.dbParameters.Clear();

                    if (this.dbDataReader != null)
                    {
                        if (!this.dbDataReader.IsClosed)
                            this.dbDataReader.Close();

                        this.dbDataReader.Dispose();
                    }

                    if (this.dbTransaction != null)
                        this.dbTransaction.Dispose();

                    if (this.dbCommand != null)
                        this.dbCommand.Dispose();

                    if (this.dbConnection != null)
                    {
                        if (this.dbConnection.State != ConnectionState.Closed)
                            this.dbConnection.Close();

                        this.dbConnection.Dispose();
                    }
                }

                this.isDisposed = true;
            }
        }

        private void CreateCommand(CommandType commandType, string commandText)
        {
            if (this.dbCommand == null)
                this.dbCommand = DataAccessBuilder.CreateCommand(this.dataProvider);

            this.dbCommand.CommandText = commandText;
            this.dbCommand.CommandType = commandType;
            this.dbCommand.Connection = this.dbConnection;
            this.dbCommand.Transaction = this.dbTransaction;
            this.dbCommand.Parameters.Clear();

            this.dbParameters.ForEach(x => this.dbCommand.Parameters.Add(x.Value));
        }
    }
}