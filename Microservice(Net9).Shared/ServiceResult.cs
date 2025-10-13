using Refit;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

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
                    Detail = "The request resource was not found",
                    Status = HttpStatusCode.NotFound.GetHashCode()
                }

            };

        }

        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
        {
            return new ServiceResult()
            {
                StatusCode = statusCode,
                Fail = problemDetails
            };
        }

        public static ServiceResult Error(string title, string detail, HttpStatusCode statusCode)
        {
            return new ServiceResult()
            {
                StatusCode = statusCode,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = detail,
                    Status = statusCode.GetHashCode() //int karşılığı
                }
            };
        }

        public static ServiceResult Error(string title, HttpStatusCode statusCode)
        {
            return new ServiceResult()
            {
                StatusCode = statusCode,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Status = statusCode.GetHashCode() //int karşılığı
                }
            };
        }

        public static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
        {
            return new ServiceResult()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails()
                {
                    Title = "Validation errors occured.",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode() //int karşılığı
                }
            };
        }

        public static ServiceResult ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult()
                {
                    Fail = new ProblemDetails()
                    {
                        Title = exception.Message,
                    },
                    StatusCode = exception.StatusCode
                };
            }
            else
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                });


                return new ServiceResult()
                {
                    Fail = problemDetails,
                    StatusCode = exception.StatusCode
                };
            }
        }


    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; } //if it is not success
        public string? UrlAsCreated { get; set; }

        //200
        public static ServiceResult<T> SuccessAsOk(T data)
        {
            return new ServiceResult<T>()
            {
                StatusCode = HttpStatusCode.OK,
                Data = data
            };
        }

        //201 => Created => response body header => location => api/products/5
        public static ServiceResult<T> SuccessAsCreated(T data, string url)
        {
            return new ServiceResult<T>()
            {
                StatusCode = HttpStatusCode.Created,
                Data = data,
                UrlAsCreated = url
            };
        }

        public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
        {
            return new ServiceResult<T>()
            {
                StatusCode = statusCode,
                Fail = problemDetails
            };
        }

        public new static ServiceResult<T> Error(string title, string detail, HttpStatusCode statusCode)
        {
            return new ServiceResult<T>()
            {
                StatusCode = statusCode,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = detail,
                    Status = statusCode.GetHashCode() //int karşılığı
                }
            };
        }

        public new static ServiceResult<T> Error(string title, HttpStatusCode statusCode)
        {
            return new ServiceResult<T>()
            {
                StatusCode = statusCode,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Status = statusCode.GetHashCode() //int karşılığı
                }
            };
        }

        public new static ServiceResult<T> ErrorFromValidation(IDictionary<string, object?> errors)
        {
            return new ServiceResult<T>()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails()
                {
                    Title = "Validation errors occured.",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode() //int karşılığı
                }
            };
        }

        public new static ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult<T>()
                {
                    Fail = new ProblemDetails()
                    {
                        Title = exception.Message,
                    },
                    StatusCode = exception.StatusCode
                };
            }
            else
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                });


                return new ServiceResult<T>()
                {
                    Fail = problemDetails,
                    StatusCode = exception.StatusCode
                };
            }
        }
    }

}
