using System;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace OSWebCore
{
    public sealed class DataAccessBuilder
    {
        private DataAccessBuilder()
        {
            ////
        }

        public static DbCommand CreateCommand(DataProvider dataProvider)
        {
            DbCommand dbCommand;

            switch (dataProvider)
            {
                case DataProvider.Odbc:
                    dbCommand = new OdbcCommand();
                    break;

                case DataProvider.OleDB:
                    dbCommand = new OleDbCommand();
                    break;

                case DataProvider.SqlServer:
                    dbCommand = new SqlCommand();
                    break;

                default:
                    dbCommand = null;
                    break;
            }

            return dbCommand;
        }

        public static DbConnection CreateConnection(DataProvider dataProvider)
        {
            DbConnection dbConnection;

            switch (dataProvider)
            {
                case DataProvider.Odbc:
                    dbConnection = new OdbcConnection();
                    break;

                case DataProvider.OleDB:
                    dbConnection = new OleDbConnection();
                    break;

                case DataProvider.SqlServer:
                    dbConnection = new SqlConnection();
                    break;

                default:
                    dbConnection = null;
                    break;
            }

            return dbConnection;
        }

        public static DbConnectionStringBuilder CreateConnectionStringBuilder(DataProvider dataProvider)
        {
            DbConnectionStringBuilder dbConnectionStringBuilder;

            switch (dataProvider)
            {
                case DataProvider.Odbc:
                    dbConnectionStringBuilder = new OdbcConnectionStringBuilder();
                    break;

                case DataProvider.OleDB:
                    dbConnectionStringBuilder = new OleDbConnectionStringBuilder();
                    break;

                case DataProvider.SqlServer:
                    dbConnectionStringBuilder = new SqlConnectionStringBuilder();
                    break;

                default:
                    dbConnectionStringBuilder = null;
                    break;
            }

            return dbConnectionStringBuilder;
        }

        public static DbDataAdapter CreateDataAdapter(DataProvider dataProvider)
        {
            DbDataAdapter dbDataAdapter;

            switch (dataProvider)
            {
                case DataProvider.Odbc:
                    dbDataAdapter = new OdbcDataAdapter();
                    break;

                case DataProvider.OleDB:
                    dbDataAdapter = new OleDbDataAdapter();
                    break;

                case DataProvider.SqlServer:
                    dbDataAdapter = new SqlDataAdapter();
                    break;

                default:
                    dbDataAdapter = null;
                    break;
            }

            return dbDataAdapter;
        }

        public static DbParameter CreateParameter(DataProvider dataProvider)
        {
            DbParameter dbParameter;

            switch (dataProvider)
            {
                case DataProvider.Odbc:
                    dbParameter = new OdbcParameter();
                    break;

                case DataProvider.OleDB:
                    dbParameter = new OleDbParameter();
                    break;

                case DataProvider.SqlServer:
                    dbParameter = new SqlParameter();
                    break;

                default:
                    dbParameter = null;
                    break;
            }

            return dbParameter;
        }
    }
}