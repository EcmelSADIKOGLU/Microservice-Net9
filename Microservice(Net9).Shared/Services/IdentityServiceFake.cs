
namespace Microservice_Net9_.Shared.Services
{
    public class IdentityServiceFake : IIdentityService
    {
        public Guid UserId => Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");

        public string UserName => "Ecmel SADIKOĞLU";

        public List<string> Roles => new List<string>();
    }
}
