
namespace Microservice_Net9_.Shared.Services
{
    public interface IIdentityService
    {
        public Guid UserId { get;}
        public string UserName { get;  }
        public List<string> Roles { get; }
    }
}
