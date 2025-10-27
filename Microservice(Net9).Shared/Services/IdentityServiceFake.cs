namespace Microservice_Net9_.Shared.Services
{
    public class IdentityServiceFake : IIdentityService
    {
        public Guid GetUserId => Guid.Parse("4c3cd3af-9151-4fe3-8ebf-0d968d8a4e1d");

        public string GetUserName => "Ecmel SADIKOĞLU";
    }
}
