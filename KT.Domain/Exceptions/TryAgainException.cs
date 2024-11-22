namespace KT.Domain.Exceptions
{
    public class TryAgainException : KTException
    {
        public TryAgainException() :
            base("Register process takes more than 5 mins or Some error happens.Please try again.")
        { }
    }
}
