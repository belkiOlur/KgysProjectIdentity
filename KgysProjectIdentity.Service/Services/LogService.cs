using KgysProjectIdentity.Repository.Models;

namespace KgysProjectIdentity.Service.Services
{
    public class LogService : ILogService
    {
        private readonly ILogDifferenceDetectService _detect;
        private readonly AppDbContext _context;
        public LogService(ILogDifferenceDetectService detect, AppDbContext context)
        {
            _detect = detect;
            _context = context;
        }
        public Task LogForAdd(string log)
        {
            _context.LogModel.Add(new LogModel { Log = log });
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        
    }
}
