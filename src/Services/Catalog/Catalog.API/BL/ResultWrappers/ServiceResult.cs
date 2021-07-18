﻿using Catalog.API.BL.Enums;

namespace Catalog.API.BL.ResultWrappers
{
    public class ServiceResult
    {
        protected ServiceResult() { }

        public ServiceResult(ServiceResultType result)
        {
            Result = result;
        }

        public ServiceResult(ServiceResultType result, string message)
        {
            Result = result;
            Message = message;
        }

        public ServiceResultType Result { get; set; }
        public string Message { get; set; }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult() { }

        public ServiceResult(ServiceResultType result) : base(result)
        { }

        public ServiceResult(ServiceResultType result, string message) : base (result, message)
        { }

        public ServiceResult(ServiceResultType result, T data)
        {
            Result = result;
            Data = data;
        }

        public ServiceResult(ServiceResultType result, string message, T data)
        {
            Result = result;
            Message = message;
            Data = data;
        }

        public T Data { get; set; }
    }
}
