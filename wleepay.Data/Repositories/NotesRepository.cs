using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

namespace wleepay.Data.Repositories
{
    public class NotesRepository : Repository<Notes>, INotesRepository
    {

        public NotesRepository(WPContext context)
           : base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<Notes> AddUpdateNotes(Notes notes)
        {
            await WPContext.Notes.AddAsync(notes);
            WPContext.SaveChanges();

            return notes;

        }

    }
}
