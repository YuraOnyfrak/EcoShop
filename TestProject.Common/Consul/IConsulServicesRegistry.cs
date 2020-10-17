using System.Threading.Tasks;
using Consul;

namespace TestProject.Common.Consul
{
    public interface IConsulServicesRegistry
    {
        Task<AgentService> GetAsync(string name);
    }
}