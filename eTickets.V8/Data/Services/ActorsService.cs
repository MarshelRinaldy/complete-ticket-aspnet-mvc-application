using eTickets.V8.Data.Base;
using eTickets.V8.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace eTickets.V8.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        private readonly AppDbContext _context;
        public ActorsService(AppDbContext context) : base(context) { }

    }
}
