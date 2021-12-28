using EndToEndDB.Data.EndToEnd;
using Microsoft.EntityFrameworkCore;
namespace EndToEnd.Data
{
    public class AnnonymousSuggestionService
    {
        private readonly EndtoEndContext _context;
        public AnnonymousSuggestionService(EndtoEndContext context)
        {
            _context = context;
        }
        public async Task<List<AnnonymousSuggestion>>
            GetSuggestionAsync(string strCurrentUser)
        {
            // Get Suggestions  
            return await _context.AnnonymousSuggestion
                 // Only get entries for the current logged in user
                 .Where(x => x.UserName == strCurrentUser)
                 // Use AsNoTracking to disable EF change tracking
                 // Use ToListAsync to avoid blocking a thread
                 .AsNoTracking().ToListAsync();
        }
        public Task<AnnonymousSuggestion>
    CreateSuggestionAsync(AnnonymousSuggestion objAnnonymousSuggestion)
        {
            _context.AnnonymousSuggestion.Add(objAnnonymousSuggestion);
            _context.SaveChanges();
            return Task.FromResult(objAnnonymousSuggestion);
        }
        public Task<bool>
    UpdateSuggestionAsync(AnnonymousSuggestion objAnnonymousSuggestion)
        {
            var ExistingAnnonymousSuggestion =
                _context.AnnonymousSuggestion
                .Where(x => x.Id == objAnnonymousSuggestion.Id)
                .FirstOrDefault();
            if (ExistingAnnonymousSuggestion != null)
            {
                ExistingAnnonymousSuggestion.Date =
                    objAnnonymousSuggestion.Date;
                ExistingAnnonymousSuggestion.Votes =
                    objAnnonymousSuggestion.Votes;
                ExistingAnnonymousSuggestion.Title =
                    objAnnonymousSuggestion.Title;
                ExistingAnnonymousSuggestion.Description =
                    objAnnonymousSuggestion.Description;
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    public Task<bool>
        DeleteSuggestionAsync(AnnonymousSuggestion objAnnonymousSuggestion)
    {
        var ExistingAnnonymousSuggestion =
            _context.AnnonymousSuggestion
            .Where(x => x.Id == objAnnonymousSuggestion.Id)
            .FirstOrDefault();
        if (ExistingAnnonymousSuggestion != null)
        {
            _context.AnnonymousSuggestion.Remove(ExistingAnnonymousSuggestion);
            _context.SaveChanges();
        }
        else
        {
            return Task.FromResult(false);
        }
        return Task.FromResult(true);
        }
    }
}