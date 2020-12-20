using AvtokampiWebAPI.Models;
using AvtokampiWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvtokampiWebAPI.Services
{
    public class AvtokampiRepository : IAvtokampiRepository
    {
        private readonly avtokampiContext _db;

        public AvtokampiRepository(avtokampiContext db)
        {
            _db = db;
        }

        public async Task<PagedList<Avtokampi>> GetPage(AvtokampiParameters avtokampiParameters)
        {
            return await PagedList<Avtokampi>.ToPagedList(_db.Avtokampi.OrderBy(on => on.Naziv),
                                                            avtokampiParameters.PageNumber,
                                                            avtokampiParameters.PageSize);
        }

        public async Task<List<Avtokampi>> GetAll()
        {
            return await _db.Avtokampi.ToListAsync();
        }

        public async Task<Avtokampi> GetAvtokampByID(int kamp_id)
        {
            return await _db.Avtokampi.FindAsync(kamp_id);
        }

        public async Task<bool> CreateAvtokamp(Avtokampi avtokamp)
        {
            await _db.Avtokampi.AddAsync(avtokamp);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Avtokampi> UpdateAvtokamp(Avtokampi avtokamp, int avtokamp_id)
        {
            _db.Entry(avtokamp).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return await _db.Avtokampi.FindAsync(avtokamp_id);
        }

        public async Task<bool> RemoveAvtokamp(int avtokamp_id)
        {
            _db.Avtokampi.Remove(await _db.Avtokampi.FindAsync(avtokamp_id));
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Ceniki>> GetCenikiAvtokampa(int kamp_id)
        {
            return await _db.Ceniki.Where(o => o.Avtokamp == kamp_id).ToListAsync();
        }

        public async Task<Ceniki> GetCenikAvtokampa(int cenik_id)
        {
            return await _db.Ceniki.FindAsync(cenik_id);
        }

        public async Task<bool> CreateCenikAvtokampa(Ceniki cenik, int kamp_id)
        {
            await _db.Ceniki.AddAsync(cenik);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Ceniki> UpdateCenik(Ceniki cenik, int cenik_id)
        {
            _db.Entry(cenik).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return await _db.Ceniki.FindAsync(cenik_id);
        }

        public async Task<bool> RemoveCenikAvtokampa(int cenik_id)
        {
            _db.Ceniki.Remove(await _db.Ceniki.FindAsync(cenik_id));
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Regije>> GetRegije()
        {
            return await _db.Regije.ToListAsync();
        }

        public async Task<List<Drzave>> GetDrzave()
        {
            return await _db.Drzave.ToListAsync();
        }
    }
}
