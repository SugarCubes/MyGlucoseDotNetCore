using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGlucoseDotNetCore.Services.Interfaces
{
    public interface IGlucoseEntriesRepository
    {
        Task<GlucoseEntries> ReadAsync( Guid id );
        IQueryable<GlucoseEntries> ReadAll();
        Task<GlucoseEntries> CreateAsync( GlucoseEntries project );
        Task UpdateAsync( Guid id, GlucoseEntriesViewModel project );
        Task DeleteAsync( Guid id );

    } // Interface

} // Namespace