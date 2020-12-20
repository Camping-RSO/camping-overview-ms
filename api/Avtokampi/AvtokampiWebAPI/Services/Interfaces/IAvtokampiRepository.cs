using AvtokampiWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvtokampiWebAPI.Services.Interfaces
{
    public interface IAvtokampiRepository
    {
        Task<PagedList<Avtokampi>> GetPage(AvtokampiParameters avtokampiParameters);

        Task<List<Avtokampi>> GetAll();

        Task<Avtokampi> GetAvtokampByID(int kamp_id);

        Task<bool> CreateAvtokamp(Avtokampi avtokamp);

        Task<Avtokampi> UpdateAvtokamp(Avtokampi avtokamp, int avtokamp_id);

        Task<bool> RemoveAvtokamp(int avtokamp_id);

        Task<List<Ceniki>> GetCenikiAvtokampa(int kamp_id);

        Task<Ceniki> GetCenikAvtokampa(int cenik_id);

        Task<bool> CreateCenikAvtokampa(Ceniki cenik, int kamp_id);

        Task<Ceniki> UpdateCenik(Ceniki cenik, int cenik_id);

        Task<bool> RemoveCenikAvtokampa(int cenik_id);

        Task<List<Regije>> GetRegije();

        Task<List<Drzave>> GetDrzave();
    }
}
