namespace KT.Domain.Exceptions
{
    public class CustomerMobileNumberExistsException : KTException
    {
        public CustomerMobileNumberExistsException(string mobileNo) :
            base($"a customer exists with mobile number {mobileNo}")
        { }
    }
}
