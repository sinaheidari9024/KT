namespace KT.Domain
{
    public class Customer : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string IcNumber { get; private set; }
        public string MobileNo { get; private set; }
        public string CountryCode { get; private set; }
        public string EmailAddress { get; private set; }
        public string PIN { get; private set; }
        public bool IsActive { get; private set; }

        private Customer()
        {
            IsActive = true;
        }

        public Customer(string name, string icNumber, string emailAddress, string mobileNo, string countryCode, string pin) : this()
        {
            Name = name;
            IcNumber = icNumber;
            EmailAddress = emailAddress;
            MobileNo = mobileNo;
            CountryCode = countryCode;
            PIN = pin;
        }

        public void SetPin(string pin) => this.PIN = pin;
    }
}