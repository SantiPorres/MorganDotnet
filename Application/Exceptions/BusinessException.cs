﻿using System.Globalization;

namespace Application.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException() : base()
        {

        }

        public BusinessException(string message) : base(message)
        {

        }

        public BusinessException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
