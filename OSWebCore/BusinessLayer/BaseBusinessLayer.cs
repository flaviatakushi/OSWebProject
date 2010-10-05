using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace OSWebCore
{
    public abstract class BaseBusinessLayer<T> : IDisposable where T : BaseEntity
    {
        private bool isDisposed;
        private string lastSelectQuery;
        private CommandType lastSelectType;
        private DataAccess dataAccess;

        protected BaseBusinessLayer(DataProvider provider, string connectionString)
        {
            this.dataAccess = new DataAccess(provider, connectionString);
        }

        ~BaseBusinessLayer()
        {
            this.Dispose(false);
        }

        protected virtual string ConnectionString
        {
            get
            {
                return this.dataAccess.ConnectionString;
            }
        }

        protected virtual DataAccess DataAccess
        {
            get
            {
                return this.dataAccess;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void RefreshData()
        {
            this.SelectData(this.lastSelectType, this.lastSelectQuery);
        }

        public abstract int InsertData(T entity);

        public abstract int DeleteData(T entity);

        public abstract int UpdateData(T entity);

        protected static bool IsNull(object value)
        {
            return value == null || value == DBNull.Value;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    if (this.dataAccess != null)
                        this.dataAccess.Dispose();
                }

                this.isDisposed = true;
            }
        }

        protected virtual Collection<T> SelectData(CommandType commandType, string commandText)
        {
            this.dataAccess.OpenConnection();

            using (var reader = this.dataAccess.ExecuteReader(commandType, commandText, CommandBehavior.CloseConnection))
            {
                var list = reader.GetEnumerator(this.CreateEntity).ToList();

                this.lastSelectType = commandType;
                this.lastSelectQuery = commandText;

                return new Collection<T>(list);
            }
        }

        protected abstract T CreateEntity(IDataRecord record);
    }
}