namespace KT.Domain.Specifications
{
    public class CustomerSpec : Specification<Customer>, ISingleResultSpecification
    {
        public CustomerSpec(string icNumber)
        {
            Query.Where(c => c.IcNumber == icNumber && c.IsActive);

        }
        public CustomerSpec(string mobileNo, string emailAddress)
        {
            Query.Where(c => c.MobileNo == mobileNo || c.EmailAddress == emailAddress);
        }

    }
}
