using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.Application.EventServices
{
    /// <summary>
    /// Provides a service to access races.
    /// </summary>
    public interface IRaceService
    {
        /// <summary>
        /// Get <see cref="Race"/> objects for a specific season that have completed.
        /// </summary>
        /// <param name="year">The year the season ends.</param>
        /// <returns>A task with a collection of <see cref="Domain.Race"/> objects.</returns>
        Task<IReadOnlyCollection<Race>> GetCompletedRacesForSeasonAsync(int year);
    }
}
