using System.Web.Http;
using Unity;
using Unity.WebApi;

using DataAccess.Repositories.Repository;
using DataAccess.Repositories.IRepository;
using API.Helpers;
using DataAccess.Repositories.Repository.DTO;
using DataAccess.Repositories.IRepository.DTO;
using API.Helpers.FileManager;

namespace API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            //Core.Entities
            container.RegisterType<IAuthenticationTokensRepository, AuthenticationTokensRepository>();
            container.RegisterType<IRolesRepository, RolesRepository>();
            container.RegisterType<IUserRolesRepository, UserRolesRepository>();
            container.RegisterType<IUsersRepository, UsersRepository>();

            container.RegisterType<IToursRepository, ToursRepository>();
            container.RegisterType<IFavoriteToursRepository, FavoriteToursRepository>();
            container.RegisterType<IAdditionalInformationTypesRepository, AdditionalInformationTypesRepository>();
            container.RegisterType<IToursAdditionalInformationsRepository, ToursAdditionalInformationsRepository>();
            container.RegisterType<ITourAgendaRepository, TourAgendaRepository>();
            container.RegisterType<ITourDatesRepository, TourDatesRepository>();
            container.RegisterType<ITourReservationsRepository, TourReservationsRepository>();
            container.RegisterType<ITourReviewsRepository, TourReviewsRepository>();

            container.RegisterType<IToursDTORepository, ToursDTORepository>();

            //Services
            container.RegisterType<IFileManager, FileManager>();            

            //Core.DTO
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}