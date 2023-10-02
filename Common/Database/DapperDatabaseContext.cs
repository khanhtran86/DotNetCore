using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Microsoft.Data.SqlClient;


namespace DotnetCoreVCB.Common.Database
{
    public class DapperDatabaseContext
    {
        protected string ConnectionString { get; }

        /// <summary>
        /// Instantiate a <see cref="DapperDatabaseContext"/> object with default connection of type <see cref="Microsoft.Data.SqlClient.SqlConnection"/>.
        /// </summary>
        /// <param name="connectionString">The connection string to the database of the current context</param>
        public DapperDatabaseContext(string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Connection string must have a valid value.");
            }
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Gets the default object of <see cref="Microsoft.Data.SqlClient.SqlConnection"/>.
        /// </summary>
        public virtual IDbConnection Connection => new SqlConnection(ConnectionString);

        /// <inheritdoc/>
        public IEnumerable<T> Exec<T>(string storedProcedure, IEnumerable<IDataParameter> parameters)
        {
            ThrowIfInvalidStoredProcedure(storedProcedure);
            dynamic paraObj = GeneratePrameters(parameters);
            return Exec<T>(storedProcedure, paraObj);
        }

        /// <inheritdoc/>
        public IEnumerable<T> Exec<T>(string storedProcedure, dynamic parameterObject = null)
        {
            ThrowIfInvalidStoredProcedure(storedProcedure);
            var paraObj = (object)parameterObject;

            return Connection.Query<T>(storedProcedure, param: paraObj, commandType: CommandType.StoredProcedure);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> ExecAsync<T>(string storedProcedure, IEnumerable<IDataParameter> parameters)
        {
            ThrowIfInvalidStoredProcedure(storedProcedure);
            dynamic paraObj = GeneratePrameters(parameters);
            return await ExecAsync<T>(storedProcedure, paraObj);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> ExecAsync<T>(string storedProcedure, dynamic parameterObject = null)
        {
            ThrowIfInvalidStoredProcedure(storedProcedure);
            var paraObj = (object)parameterObject;

            return await Connection.QueryAsync<T>(storedProcedure, param: paraObj, commandType: CommandType.StoredProcedure);
        }

        /// <inheritdoc/>
        public int ExecNonQuery(string storedProcedure, IEnumerable<IDataParameter> parameters)
        {
            ThrowIfInvalidStoredProcedure(storedProcedure);
            dynamic paraObjQuery = GeneratePrameters(parameters);
            return ExecNonQuery(storedProcedure, paraObjQuery);
        }

        /// <inheritdoc/>
        public int ExecNonQuery(string storedProcedure, dynamic parameterObject = null)
        {
            ThrowIfInvalidStoredProcedure(storedProcedure);
            var objPara = (object)parameterObject;

            return Connection.Execute(storedProcedure, param: objPara, commandType: CommandType.StoredProcedure);
        }

        /// <inheritdoc/>
        public async Task<int> ExecNonQueryAsync(string storedProcedure, IEnumerable<IDataParameter> parameters)
        {
            dynamic paraObjQuery = GeneratePrameters(parameters);
            return await ExecNonQueryAsync(storedProcedure, paraObjQuery);
        }

        /// <inheritdoc/>
        public async Task<int> ExecNonQueryAsync(string storedProcedure, dynamic parameterObject = null)
        {
            ThrowIfInvalidStoredProcedure(storedProcedure);
            var objPara = (object)parameterObject;

            return await Connection.ExecuteAsync(storedProcedure, param: objPara, commandType: CommandType.StoredProcedure);
        }

        /// <inheritdoc/>
        public T ExecSingle<T>(string storedProcedure, IEnumerable<IDataParameter> parameters)
        {
            ThrowIfInvalidStoredProcedure(storedProcedure);
            dynamic paramObj = GeneratePrameters(parameters);
            return ExecSingle<T>(storedProcedure, paramObj);
        }

        /// <inheritdoc/>
        public T ExecSingle<T>(string storedProcedure, dynamic parameterObject = null)
        {
            ThrowIfInvalidStoredProcedure(storedProcedure);
            var obj = (object)parameterObject;
            return Connection.QuerySingleOrDefault<T>(storedProcedure, param: obj, commandType: CommandType.StoredProcedure);
        }

        /// <inheritdoc/>
        public async Task<T> ExecSingleAsync<T>(string storedProcedure, IEnumerable<IDataParameter> parameters)
        {
            ThrowIfInvalidStoredProcedure(storedProcedure);
            dynamic paramObj = GeneratePrameters(parameters);
            return await ExecSingleAsync<T>(storedProcedure, paramObj);
        }

        /// <inheritdoc/>
        public async Task<T> ExecSingleAsync<T>(string storedProcedure, dynamic parameterObject = null)
        {
            ThrowIfInvalidStoredProcedure(storedProcedure);
            var objPara = (object)parameterObject;

            return await Connection.QuerySingleOrDefaultAsync<T>(storedProcedure, param: objPara, commandType: CommandType.StoredProcedure);
        }

        public T ExecSingleWithTransaction<T>(string storedProcedure, IEnumerable<IDataParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public T ExecSingleWithTransaction<T>(string storedProcedure, dynamic parameterObject = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> ExecSingleWithTransactionAsync<T>(string storedProcedure, IEnumerable<IDataParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public Task<T> ExecSingleWithTransactionAsync<T>(string storedProcedure, dynamic parameterObject = null)
        {
            throw new NotImplementedException();
        }

        public void ExecWithTransaction(string storedProcedure, IEnumerable<IDataParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public void ExecWithTransaction(string storedProcedure, dynamic parameterObject = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> ExecWithTransaction<T>(string storedProcedure, IEnumerable<IDataParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> ExecWithTransaction<T>(string storedProcedure, dynamic parameterObject = null)
        {
            throw new NotImplementedException();
        }

        public Task ExecWithTransactionAsync(string storedProcedure, IEnumerable<IDataParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public Task ExecWithTransactionAsync(string storedProcedure, dynamic parameterObject = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerator<T>> ExecWithTransactionAsync<T>(string storedProcedure, IEnumerable<IDataParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerator<T>> ExecWithTransactionAsync<T>(string storedProcedure, dynamic parameterObject = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Processing parameters prepare execute command
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private dynamic GeneratePrameters(IEnumerable<IDataParameter> parameters)
        {
            if (parameters == null || parameters.Count() == 0)
                return null;

            dynamic execParamObj = null;
            if (parameters != null && parameters.Count() > 0)
            {
                foreach (var param in parameters)
                {
                    string prop = param.ParameterName.TrimStart(new char[] { '@' });
                    execParamObj[prop] = param.Value;
                }
            }
            return execParamObj;
        }

        /// <summary>
        /// Throw exception when store name is invalid
        /// </summary>
        /// <param name="name"></param>
        private void ThrowIfInvalidStoredProcedure(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Must provide a non-empty name of procedure", "storedProcedure");
            if (name.Contains(" "))
                throw new ArgumentException("Name of procedure contains illegal character(s)", "storedProcedure");
        }

    }
}
