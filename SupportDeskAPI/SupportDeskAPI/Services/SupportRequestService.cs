using SupportDeskAPI.Models;
using System.Collections.Concurrent;

namespace SupportDeskAPI.Services
{
    public class SupportRequestService : ISupportRequestService
    {
        private static ConcurrentDictionary<Guid, SupportRequest> SupportRequests = new ConcurrentDictionary<Guid, SupportRequest>();

        public List<SupportRequest> GetAll()
        {
            return SupportRequests.Values.ToList();
        }

        public SupportRequest? GetById(Guid id)
        {
            SupportRequests.TryGetValue(id, out var request);

            return request;
        }

        public SupportRequest Create(SupportRequest request)
        {
            request.Id = Guid.NewGuid();
            request.CreatedAt = DateTime.Now;
            request.Resolved = false;

            SupportRequests[request.Id] = request;

            return request;
        }

        public SupportRequest Update(SupportRequest request)
        {
            if (SupportRequests.ContainsKey(request.Id))
            {
                SupportRequests[request.Id] = request;
                return request;
            }

            throw new KeyNotFoundException($"No SupportRequest with ID {request.Id} was found.");
        }

        public bool Resolve(Guid id)
        {
            if (SupportRequests.TryGetValue(id, out var request))
            {
                request.Resolved = true;
                SupportRequests[id] = request;
                
                return true;
            }
            return false;
        }

        public bool Delete(Guid id)
        {
            return SupportRequests.TryRemove(id, out _);
        }
    }
}