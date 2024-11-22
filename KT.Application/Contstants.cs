namespace KT.Application
{
    public static class ExpressionPattern
    {
        public const string MobileNo = "^(\\+|[0-9])\\d{5,14}";
        
    }

    public static class Validation
    {
        public const int OTPLenght = 4;
        public const int PINLenght = 6;
    }

    public static class Message
    {
        public const string EmailSubject = "Koperasi Tentera Comfirmation Code";
        public const string EmailMessage = "Please Enter the following code: ";
    }
    public static class KTEventId
    {
        public const int BadRequest = 1001;
        public const int InternalServerError = 1002;
    }
}
