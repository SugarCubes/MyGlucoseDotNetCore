using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Services.Interfaces
{
    public interface IGlucoseEntryRepository
    {
        Task<GlucoseEntry> ReadAsync( Guid id );
        IQueryable<GlucoseEntry> ReadAll();
        Task<GlucoseEntry> CreateAsync( GlucoseEntry project );
        Task UpdateAsync( Guid id, GlucoseEntryViewModel project );
        Task DeleteAsync( Guid id );

    } // Interface

} // Namespace