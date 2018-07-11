using API.ViewModels;
using Core.DTO;
using Core.Entities;
using Core.Resources;
using DataAccess.Repositories.IRepository;
using DataAccess.Repositories.IRepository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/Tours")]
    public class ToursController : BaseApiController
    {
        private IToursDTORepository _dbToursDTO;
        private IFavoriteToursRepository _dbFavoriteTours;
        private IToursAdditionalInformationsRepository _dbIToursAdditionalInformations;
        private ITourAgendaRepository _dbITourAgenda;
        private ITourReviewsRepository _dbTourReviews;
        private ITourDatesRepository _dbTourDates;
        private ITourReservationsRepository _dbTourReservations;


        public ToursController(IUsersRepository dbUsers, IRolesRepository dbRoles, IToursDTORepository dbToursDTO, IFavoriteToursRepository dbFavoriteTours,
            IToursAdditionalInformationsRepository dbIToursAdditionalInformations, ITourAgendaRepository dbITourAgenda, ITourReviewsRepository dbTourReviews,
            ITourDatesRepository dbTourDates, ITourReservationsRepository dbTourReservations) : base(dbUsers, dbRoles)
        {
            _dbToursDTO = dbToursDTO;
            _dbFavoriteTours = dbFavoriteTours;
            _dbIToursAdditionalInformations = dbIToursAdditionalInformations;
            _dbITourAgenda = dbITourAgenda;
            _dbTourReviews = dbTourReviews;
            _dbTourDates = dbTourDates;
            _dbTourReservations = dbTourReservations;
        }

        public IHttpActionResult GetTours([FromUri]int page, string filter, int numberOfResults)
        {
            Users user = GetUserOfAuthToken();

            if (user == null)
                return Unauthorized();

            if (page < 1)
                page = 1;

            if (string.IsNullOrEmpty(filter))
                filter = "active";

            if (numberOfResults < 0)
                numberOfResults = API.Configuration.DefaultResultsPerPage;
            else if (numberOfResults > API.Configuration.ResultsPerPageLimit)
                numberOfResults = API.Configuration.ResultsPerPageLimit;

            List<ToursDTO> tours;

            switch (filter)
            {
                case "active":
                    tours = _dbToursDTO.FindActiveByParamaters(user.UserID, (page - 1), numberOfResults).ToList();
                    tours.ForEach(x => x.UpcomingDateString = (x.UpcomingDate.HasValue ? x.UpcomingDate.Value.ToString(Resource.DateFormat) : null));
                    break;
                case "upcoming":
                    tours = _dbToursDTO.FindUpcomingByParamaters(user.UserID, (page - 1), numberOfResults).ToList();
                    tours.ForEach(x => x.ReservedAtString = (x.ReservedAt.HasValue ? x.ReservedAt.Value.ToString(Resource.DateFormat) : null));
                    break;
                case "saved":
                    tours = _dbToursDTO.FindSavedByParamaters(user.UserID, (page - 1), numberOfResults).ToList();
                    tours.ForEach(x => x.UpcomingDateString = (x.UpcomingDate.HasValue ? x.UpcomingDate.Value.ToString(Resource.DateFormat) : null));
                    break;
                case "finished":
                    tours = _dbToursDTO.FindFinishedByParamaters(user.UserID, (page - 1), numberOfResults).ToList();
                    tours.ForEach(x => x.ReservedAtString = (x.ReservedAt.HasValue ? x.ReservedAt.Value.ToString(Resource.DateFormat) : null));
                    break;
                default:
                    tours = null;
                    break;
            }

            return Ok(tours);
        }

        [HttpGet]
        [Route("AddToFavorite")]
        public IHttpActionResult AddToFavorite([FromUri]int tourId)
        {
            Users user = GetUserOfAuthToken();

            if (user == null)
                return Unauthorized();

            if (_dbFavoriteTours.Add(new FavoriteTours { TourID = tourId, UserID = user.UserID }))
                return Ok();

            return BadRequest();
        }

        [HttpGet]
        [Route("RemoveFromFavorite")]
        public IHttpActionResult RemoveFromFavorite([FromUri]int tourId)
        {
            Users user = GetUserOfAuthToken();

            if (user == null)
                return Unauthorized();

            if (_dbFavoriteTours.Remove(new FavoriteTours { TourID = tourId, UserID = user.UserID }))
                return Ok();

            return BadRequest();
        }

        [HttpGet]
        [Route("Details")]
        public IHttpActionResult Details([FromUri]int tourId) {
            Users user = GetUserOfAuthToken();

            if (user == null)
                return Unauthorized();

            VMTourDetails model = new VMTourDetails
            {
                TourAgenda = _dbITourAgenda.FindByTourID(tourId).OrderBy(x => x.Day).ThenBy(x => x.Time).ToList(),
                ToursAdditionalInformations = _dbIToursAdditionalInformations.FindByTourID(tourId).ToList(),
                TourReviews = _dbTourReviews.FindByTourID(tourId).Select(x =>
                {
                    x.CreatedAt = x.CreatedAtDate.ToString(Resource.DateFormatLong);
                    return x;
                }).OrderByDescending(x => x.CreatedAt).ToList()
            };

            ToursAdditionalInformationsDTO additionalInfo = model.ToursAdditionalInformations[0];
            additionalInfo.AdditionalInformationType = Resource.TourDuration;
            additionalInfo = model.ToursAdditionalInformations[1];
            additionalInfo.AdditionalInformationType = Resource.NumberOfPlaces;

            return Ok(model);
        }

        [HttpPost]
        [Route("{tourId}/addReview")]
        public IHttpActionResult AddReview(int tourId, [FromBody]TourReviews model)
        {
            if (CurrentUser == null)
                return Unauthorized();

            if (tourId != model.TourID)
                return BadRequest();

            model.UserID = CurrentUser.UserID;

            if (!_dbTourReviews.Add(model))
                return BadRequest();

            return Ok();
        }

        [HttpGet]
        [Route("{tourId}/dates")]
        public IHttpActionResult GetDates(int tourId)
        {
            if (CurrentUser == null)
                return Unauthorized();

            List<TourDates> dates = _dbTourDates.FindActiveByTourID(tourId).OrderBy(x => x.Date).ToList();
            dates.ForEach(x => x.DateString = x.Date.ToString(Resource.DateFormat));

            return Ok(dates);
        }

        [HttpPost]
        [Route("{tourId}/addReservation")]        
        public IHttpActionResult AddReservation(int tourId, [FromBody]TourReservations model) {
            if (CurrentUser == null)
                return Unauthorized();

            if (tourId != model.TourID)
                return BadRequest();

            model.UserID = CurrentUser.UserID;

            if (!_dbTourReservations.Add(model))
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Route("{tourId}/cancelReservation/{reservationId}")]
        public IHttpActionResult CancelReservation(int tourId, int reservationId) {
            if (CurrentUser == null)
                return Unauthorized();

            if (!_dbTourReservations.Remove(CurrentUser.UserID, tourId, reservationId))
                return BadRequest();

            return Ok();
        }
    }
}
