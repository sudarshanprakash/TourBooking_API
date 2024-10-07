using Microsoft.AspNetCore.JsonPatch;
using SOSE_API.DTO;
using SOSE_API.Models;

namespace SOSE_API.Interface
{
    public interface ITourInterface
    {
        IEnumerable<GetTourDTO> GetAllTours();
        GetTourDTO GetTourById(int id);
        GetTourDTO AddTour(TourDTO customerDto);
        GetTourDTO UpdateTour(int id, TourDTO customerDto);
        GetTourDTO PartialUpdateTour(int id, JsonPatchDocument<TourDTO> patchTour);

        void DeleteTour(int id);
    }
}
