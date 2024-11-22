namespace KT.Domain.Exceptions
{
    public class MobileOTPIncorrectException : KTException
    {
        public MobileOTPIncorrectException() : base("The OTP you entered is not correct."){ }
    }
}
