using LosCasaAngular.Models;

namespace LosCasaAngular.DAL;

public interface InterListingRepository
{
    Task<IEnumerable<Listing>?> GetAll();
    Task<Listing?> GetListingById(int id);
    Task<bool> Create(Listing listing);
    Task<bool> Update(Listing listing);
    Task<bool> Delete(int id);
}

