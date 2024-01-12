using SupportDeskAPI.Models;
using System.Collections.Generic;
using System;

namespace SupportDeskAPI.Services
{
    /// <summary>
    /// Service interface for handling support requests.
    /// </summary>
    public interface ISupportRequestService
    {
        /// <summary>
        /// Gets all support requests.
        /// </summary>
        /// <returns>A list of support requests.</returns>
        List<SupportRequest> GetAll();

        /// <summary>
        /// Gets a support request by ID.
        /// </summary>
        /// <param name="id">The ID of the support request.</param>
        /// <returns>The support request if found; null otherwise.</returns>
        SupportRequest? GetById(Guid id);

        /// <summary>
        /// Creates a new support request.
        /// </summary>
        /// <param name="request">The support request to create.</param>
        /// <returns>The created support request.</returns>
        SupportRequest Create(SupportRequest request);

        /// <summary>
        /// Updates an existing support request.
        /// </summary>
        /// <param name="request">The support request to update.</param>
        /// <returns>The updated support request.</returns>
        SupportRequest Update(SupportRequest request);

        /// <summary>
        /// Deletes a support request.
        /// </summary>
        /// <param name="id">The ID of the support request to delete.</param>
        /// <returns>True if the support request was deleted; false otherwise.</returns>
        bool Delete(Guid id);

        /// <summary>
        /// Resolves a support request.
        /// </summary>
        /// <param name="id">The ID of the support request to resolve.</param>
        /// <returns>True if the support request was resolved; false otherwise.</returns>
        bool Resolve(Guid id);
    }
}