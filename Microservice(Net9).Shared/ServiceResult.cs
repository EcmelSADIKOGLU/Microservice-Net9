using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json.Serialization;

namespace Microservice_Net9_.Shared
{
    public class ServiceResult
    {
        [JsonIgnore] public HttpStatusCode StatusCode { get; set; }

        public ProblemDetails? Fail { get; set; } //Başarılıysa null


        //lambda ile kullanırsan sadece get'i olan method'lar olur
        [JsonIgnore] public bool isSuccess => Fail is null;
        [JsonIgnore] public bool isFail => !isSuccess;

        //Static Factory Methods
        public static ServiceResult SuccessAsNoContent()
        {
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.NoContent
            };
        }

        public static ServiceResult ErrorAsNotFound()
        {
            return new ServiceResult
            {
                StatusCode = HttpStatusCode.NotFound,
                Fail = new ProblemDetails()
                {
                    Title = "Not Found",
                    Detail = "The request resource was not found"
                }

            };

        }


    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; } //if it is not success

        public static ServiceResult<T> SuccessAsOk(T data)
        {
            return new ServiceResult<T>()
            {
                StatusCode = HttpStatusCode.OK,
                Data = data
            };
        }
    }

}
