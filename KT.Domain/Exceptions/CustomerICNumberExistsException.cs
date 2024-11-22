namespace KT.Domain.Exceptions
{
    public class CustomerICNumberExistsException : KTException
    {
        public CustomerICNumberExistsException(string ICNumer) :
            base($"a customer exists with this IC number: {ICNumer}")
        { }
    }
}
