﻿using Microsoft.Data.SqlClient;

namespace Domain.CustomExceptions
{
    public class DataAccessException : Exception
    {
        public SqlErrorCollection? SqlErrors { get; set; }

        public DataAccessException() : base()
        {
            
        }

        public DataAccessException(string message) : base(message)
        {

        }

        public DataAccessException(string? message, SqlErrorCollection sqlErrors) : base(message)
        {
            SqlErrors = sqlErrors;
        }
    }
}
