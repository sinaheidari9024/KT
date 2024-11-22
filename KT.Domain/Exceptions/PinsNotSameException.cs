namespace KT.Domain.Exceptions
{
    public class PinsNotSameException : KTException
    {
        public PinsNotSameException() : base("The Pin whcih you entere is not same.")
        { }
    }
}
