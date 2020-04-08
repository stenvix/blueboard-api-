namespace BlueBoard.Contract.Common
{
    public class ErrorCodes
    {
        public static string NotFound => "not_found";

        public static string EmptyEmail => "empty_email";
        public static string EmailInUse => "email_in_use";
        public static string EmptyProfile => "empty_profile";
        public static string EmptyFirstName => "empty_first_name";
        public static string EmptyLastName => "empty_last_name";
        public static string EmptyPhone => "empty_phone";
        public static string EmptyPassword => "empty_password";
        public static string EmptyCountry => "empty_country";
        public static string EmptyLogin => "empty_login";
        public static string EmptyUsername => "empty_username";
        public static string EmptyTrip => "empty_trip";
        public static string EmptyTripId => "empty_trip_id";
        public static string EmptyName => "empty_name";
        public static string EmptyStartDate => "empty_start_date";
        public static string EmptyEndDate => "empty_end_date";


        public static string InvalidId => "invalid_id";
        public static string InvalidData => "invalid_data";
        public static string InvalidEmail => "invalid_email";
        public static string InvalidCredentials => "invalid_credentials";
        public static string HasNoPermissions => "has_no_permissions";
        public static string InvalidStartDate => "invalid_start_date";
        public static string InvalidEndDate => "invalid_end_date";
        public static string InvalidPhone => "invalid_phone";
        public static string InvalidPasswordLength => "invalid_password_length";
        public static string InvalidOperation => "invalid_operation";
        public static string InvalidToDate => "invalid_to_date";
        public static string InvalidLogin => "invalid_login";
        public static string InvalidUsername => "invalid_username";
        public static string InvalidQuery => "invalid_query";
        public static string InvalidFirstNameLength => "invalid_first_name_length";
        public static string InvalidLastNameLength => "invalid_last_name_length";
        public static string InvalidUsernameLength => "invalid_username_length";
        public static string InvalidPhoneLength => "invalid_phone_length";
        public static string InvalidNameLength => "invalid_name_length";
        public static string InvalidDescriptionLength => "invalid_description_length";

        public static string AlreadyExists => "already_exists";
    }
}
