using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microservice_Net9_.Web.Services
{
    public class ServiceResult
    {

        public ProblemDetails? Fail { get; set; }


        [JsonIgnore] public bool isSuccess => Fail is null;
        [JsonIgnore] public bool isFail => !isSuccess;

        public static ServiceResult Success()
        {
            return new ServiceResult();
        }

        public static ServiceResult Error(ProblemDetails problemDetails)
        {
            return new ServiceResult()
            {
                Fail = problemDetails
            };
        }

        public static ServiceResult Error(string title, string detail)
        {
            return new ServiceResult()
            {
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = detail,
                }
            };
        }

        public static ServiceResult Error(string title)
        {
            return new ServiceResult()
            {
                Fail = new ProblemDetails()
                {
                    Title = title
                }
            };
        }
       
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; } //if it is not success

        //200
        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T>()
            {
                Data = data
            };
        }


        public new static ServiceResult<T> Error(ProblemDetails problemDetails)
        {
            return new ServiceResult<T>()
            {
                Fail = problemDetails
            };
        }

        public new static ServiceResult<T> Error(string title, string detail)
        {
            return new ServiceResult<T>()
            {
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = detail,
                }
            };
        }

        public new static ServiceResult<T> Error(string title)
        {
            return new ServiceResult<T>()
            {
                Fail = new ProblemDetails()
                {
                    Title = title,
                }
            };
        }

    }
}
