namespace KT.Application.DTOs
{
    public class SendMessageDTO
    {
        public MessageType SendType { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string OTP { get; set; }
        public string MobileNumber { get; set; }
    }
}
