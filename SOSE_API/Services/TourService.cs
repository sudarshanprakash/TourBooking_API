using Microsoft.AspNetCore.JsonPatch;
using SOSE_API.DTO;
using SOSE_API.Interface;
using SOSE_API.Models;
using System.Collections.ObjectModel;

namespace SOSE_API.Services
{
    public class TourService:ITourInterface
    {

        private readonly IRepository<Tour> _tourRepository;

        public TourService(IRepository<Tour> tourRepository)
        {
            _tourRepository = tourRepository;
        }

        public IEnumerable<GetTourDTO> GetAllTours()
        {
            return _tourRepository.GetAll(t => t.Review)
                .Select(t => new GetTourDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    Price = t.Price,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    Review = t.Review.Select(r => new ReviewDTO
                    {
                        Id = r.Id,
                        Comment = r.Comment,
                        Rating = r.Rating,
                        TourId = r.TourId,
                        UserId =r.ApplicationUserId
                        
                    }).ToList()
                });
        }


        public GetTourDTO GetTourById(int id)
        {
            var tour = _tourRepository.GetById(id, t => t.Review);
            if (tour == null) return null;

            return new GetTourDTO
            {
                Id = tour.Id,
                Name = tour.Name,
                Description = tour.Description,
                Price = tour.Price,
                StartDate = tour.StartDate,
                EndDate = tour.EndDate,
                Review = tour.Review.Select(r => new ReviewDTO
                {
                    Id = r.Id,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    TourId = r.TourId,
                    UserId = r.ApplicationUserId

                }).ToList()
            };
        }


        public GetTourDTO AddTour(TourDTO tourDto)
        {
            var tour = new Tour
            {
                Description = tourDto.Description,
                Price = tourDto.Price,
                Name = tourDto.Name,
                StartDate = tourDto.StartDate,
                EndDate = tourDto.EndDate,

            };

            _tourRepository.Insert(tour);
            _tourRepository.Save();
            return new GetTourDTO
            {
                Id = tour.Id,
                Name = tour.Name,
                Description = tour.Description,
                Price = tour.Price,
                StartDate = tour.StartDate,
                EndDate = tour.EndDate,
                Review = new List<ReviewDTO>() 
            };
        }

        public GetTourDTO UpdateTour(int id, TourDTO tourDto)
        {
            var tour = _tourRepository.GetById(id);
            if (tour == null) return null;
            Tour newTour = new Tour()
            {
                Id=tourDto.Id,
                Name = tourDto.Name,
                Description = tourDto.Description,
                Price = tourDto.Price,
                StartDate = tourDto.StartDate,
                EndDate = tourDto.EndDate
            };
           

            _tourRepository.Update(newTour);
            _tourRepository.Save();
            return new GetTourDTO
            {
                Id = newTour.Id,
                Name = newTour.Name,
                Description = newTour.Description,
                Price = newTour.Price,
                StartDate = newTour.StartDate,
                EndDate = newTour.EndDate

            };
        }

        public GetTourDTO PartialUpdateTour(int id, JsonPatchDocument<TourDTO> patchTour)
        {
            // Retrieve the existing customer entity
            var tourEntity = _tourRepository.GetById(id);
            if (tourEntity == null)
            {
                throw new ArgumentNullException(nameof(tourEntity), "Customer not found");
            }

            // Map entity to DTO to apply the patch
            TourDTO tourDto = new()
            {
                Id = tourEntity.Id,
                Name = tourEntity.Name,
                Description = tourEntity.Description,
                Price = tourEntity.Price,
                StartDate = tourEntity.StartDate,
                EndDate= tourEntity.EndDate,
                
                //ID = tourEntity.ID,
                //FullName = tourEntity.FullName,
                //Email = tourEntity.Email
            };

            // Apply the patch to the DTO
            patchTour.ApplyTo(tourDto);

            // Map the modified DTO back to the entity
            tourEntity.Name = tourDto.Name;
            tourEntity.Description = tourDto.Description;
            tourEntity.Price = tourDto.Price;
            tourEntity.StartDate = tourDto.StartDate;
            tourEntity.EndDate = tourDto.EndDate;
           
            

            // Update the entity in the database
            _tourRepository.Update(tourEntity);
            _tourRepository.Save();
            return new GetTourDTO
            {
                Id = tourEntity.Id,
                Name = tourEntity.Name,
                Description = tourEntity.Description,
                Price = tourEntity.Price,
                StartDate = tourEntity.StartDate,
                EndDate = tourDto.EndDate

            };

        }

        public void DeleteTour(int id)
        {
            _tourRepository.Delete(id);
            _tourRepository.Save();
        }
    }
}
