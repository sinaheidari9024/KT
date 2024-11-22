namespace KT.Application.DTOs
{
    public record RegisterCustomerDTO(string Mobile, string Email);

    public record CreateCustomerDTO(string Name, string ICNumber, string Email, string MobileNo, string CountryCode);

    public static class CustomerMapper
    {
        public static RegisterCustomerDTO NoPinSet(this Customer customer) => new RegisterCustomerDTO(string.Empty, string.Empty);
        public static RegisterCustomerDTO GetMaskedMobileNumber(this Customer customer)
        {
            var mobile = customer.MobileNo;
            var email = customer.EmailAddress;
            if (mobile.Length == 11)
            {
                var maskedEmailAdress = email.Substring(1, 1) + "********" + email.Substring(email.IndexOf("@"), email.Length - email.IndexOf("@"));
                var lastFourNumber = mobile.Substring(mobile.Length - 4);
                return new RegisterCustomerDTO(string.Concat("*******" + lastFourNumber), maskedEmailAdress);
            }

            throw new InvalidMobileNumberIsStoredException(mobile);

        }
        public static CreateCustomerDTO Map(this Customer customer) =>
            new(customer.Name, customer.IcNumber, customer.EmailAddress, customer.MobileNo, customer.CountryCode);

    }
}
