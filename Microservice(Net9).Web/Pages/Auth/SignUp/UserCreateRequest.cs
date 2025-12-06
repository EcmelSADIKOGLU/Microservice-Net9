using System.Text.Json.Serialization;

namespace Microservice_Net9_.Web.Pages.Auth.SignUp
{
    public record UserCreateRequest(
    [property: JsonPropertyName("username")] string UserName,
    [property: JsonPropertyName("firstName")] string FirstName,
    [property: JsonPropertyName("lastName")] string LastName,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("enabled")] bool Enabled,
    [property: JsonPropertyName("credentials")] List<Credential> Credentials
);
}
