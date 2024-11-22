namespace KT.Domain.Exceptions
{
    public class InvalidMobileNumberIsStoredException : KTException

    {
        public InvalidMobileNumberIsStoredException(string mobile) : base($"invali mobile number is stroed in database {mobile}")
        { }
    }
}
