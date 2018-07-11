using System;

namespace DataAccess
{
    public class StoredProcedures
    {
        #region General
        public static class General
        {
            public const string SelectAllFrom = "SELECT * FROM ";
            public const string DeleteFrom = "DELETE FROM ";
        }
        #endregion

        #region Users
        public static class Users {
            public const string UsersSelectByAuthToken = "sp_UsersSelectByAuthToken";
            public const string UsersProfileDTOSelectByUserID = "fn_UsersProfileDTOSelectByUserID";
            public const string UsersSelectByContact = "fn_UsersSelectByContact";
        }
        #endregion

        #region AuthenticationTokens
        public static class AuthenticationTokens {
            public const string AuthenticationTokensDeactivateByDeviceToken = "sp_AuthenticationTokensDeactivateByDeviceToken";
            public const string AuthenticationTokensDeactivateByUserID = "sp_AuthenticationTokensDeactivateByUserID";
        }
        #endregion

        #region Cities
        public static class Cities
        {
            public const string CitiesSelectAll = "fn_CitiesDTOSelectAll";
        }
        #endregion

        #region Roles
        public static class Roles
        {
            public const string RolesSelectByUserID = "sp_RolesSelectByUserID";
            public const string RolesDeleteByRoleID = "sp_RolesDeleteByRoleID";
        }
        #endregion

        #region Tours
        public static class Tours {
            public const string ToursDTOSelectActiveByParameters = "sp_ToursDTOSelectActiveByParameters";
            public const string ToursDTOSelectUpcomingByParameters = "sp_ToursDTOSelectUpcomingByParameters";
            public const string ToursDTOSelectSavedByParameters = "sp_ToursDTOSelectSavedByParameters";
            public const string ToursDTOSelectFinishedByParameters = "sp_ToursDTOSelectFinishedByParameters";
        }
        #endregion

        #region TourReviews
        public static class TourReviews
        {
            public const string TourReviewsDTOSelectByTourID = "sp_TourReviewsDTOSelectByTourID";
            public const string TourReviewsInsert = "sp_TourReviewsInsert";
        }
        #endregion

        #region TourReviews
        public static class ToursAdditionalInformations
        {
            public const string ToursAdditionalInformationsDTOSelectByTourID = "sp_ToursAdditionalInformationsDTOSelectByTourID";
        }
        #endregion

        #region TourDates
        public static class TourDates {
            public const string TourDatesSelectActiveByTourID = "sp_TourDatesSelectActiveByTourID";
        }
        #endregion

        #region TourReservations
        public static class TourReservations
        {
            public const string TourReservationsInsert = "sp_TourReservationsInsert";
            public const string TourReservationsDelete = "sp_TourReservationsDelete";            
        }
        #endregion
    }
}
