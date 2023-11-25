using LosCasaAngular.Models;

namespace LosCasaAngular.DAL;

public interface InterListingRepository
{
    Task<IEnumerable<Listing>?> GetAll();
    Task<Listing?> GetListingById(int id);
    Task<bool> Create(Listing listing);
    Task<bool> Update(Listing listing);
    Task<bool> Delete(int id);

    Task<IEnumerable<Rent>?> GetAllRents();
    Task<Rent?> GetRentById(int id);
    Task<bool> CreateRent(Rent rent);
    Task<bool> UpdateRent(Rent rent);
    Task<bool> DeleteRent(int id);
}

