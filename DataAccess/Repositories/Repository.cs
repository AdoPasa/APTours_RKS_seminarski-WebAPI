using Dapper;
using Dapper.FastCrud;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Repositories
{
    public abstract class Repository<TEntity, TPk> : IRepository<TEntity, TPk> where TEntity : class
    {
        internal IDbConnection Connection => new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

        public abstract TEntity FindByID(TPk id);
        public abstract IEnumerable<TEntity> FindAll();

        #region Add
        public bool Add(TEntity item)
        {
            try
            {
                using (var dbConnection = Connection)
                {
                    dbConnection.Insert(item);
                }
                return true;
            }
            catch (Exception ex) { }
            return false;
        }
        #endregion

        #region Update
        public bool Edit(TEntity item)
        {
            try
            {
                using (var dbConnection = Connection)
                {
                    dbConnection.Update(item);
                }
                return true;
            }
            catch (Exception ex) { }
            return false;
        }

        #endregion

        #region Remove
        public bool Remove(TEntity item)
        {
            try
            {
                using (var dbConnection = Connection)
                {
                    dbConnection.Delete(item);
                }
                return true;
            }
            catch (Exception ex) { }
            return false;
        }
        #endregion

        #region FunctionExecute

        protected IEnumerable<TResult> spExecute<TResult>(string spName, params object[] parameters) where TResult : class
        {
            var strParams = "";

            for (var i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];

                strParams += (param != null ? ("'" + param.ToString() + "'") : "null") + (i == (parameters.Length - 1) ? "" : ", ");
            }

            using (var dbConnection = Connection)
            {
                string query = "EXEC \"" + spName + "\" " + strParams;
                return Connection.Query<TResult>(query);
            }
        }

        protected void spExecuteWithNoResults(string spName, params object[] parameters)
        {
            var strParams = "";

            for (var i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];

                strParams += "'" + param.ToString() + "'" + (i == (parameters.Length - 1) ? "" : ", ");
            }

            using (var dbConnection = Connection)
            {
                Connection.Query("EXEC \"" + spName + "\" " + strParams);
            }
        }

        #endregion
    }
}
