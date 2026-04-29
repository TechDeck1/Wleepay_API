using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class NotesService : INotesService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotesService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Notes> AddUpdateNotes(Notes notes)
        {
            notes.CreatedDate= DateTime.Now;
            notes.IsDeleted= false;
            var tickets = await _unitOfWork.Notes.AddUpdateNotes(notes);
            return tickets;
        }
    }
}
