using SupportDeskAPI.Models;

namespace SupportDeskAPI.Services
{
    public interface ISupportRequestService
    {
        List<SupportRequest> GetAll();
        SupportRequest? GetById(Guid id);
        SupportRequest Create(SupportRequest request);
        SupportRequest Update(SupportRequest request);
        bool Delete(Guid id);
        bool Resolve(Guid id);
    }
}
