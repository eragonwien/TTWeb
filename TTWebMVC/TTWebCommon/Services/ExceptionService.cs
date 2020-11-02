using System;
using MySql.Data.MySqlClient;
using TTWebCommon.Models.Common.Exceptions;

namespace TTWebCommon.Services
{
    public interface IExceptionService
    {
        Exception HandleMySqlException(MySqlException mySqlException);
    }

    public class ExceptionService : IExceptionService
    {
        public Exception HandleMySqlException(MySqlException ex)
        {
            var errorCode = (MySqlErrorCode) ex.Number;
            switch (errorCode)
            {
                case MySqlErrorCode.DuplicateKeyEntry:
                    return new WebApiDuplicateInsertException("Resource already exists");
                default:
                    return ex;
            }
        }
    }
}