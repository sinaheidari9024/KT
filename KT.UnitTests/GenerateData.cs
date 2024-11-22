namespace KT.UnitTests.Application
{
    public static class GenerateData
    {
        public static Customer GetCustmerWithSameMobile()
        {
            return new Customer("Robbie Stwart", "123123123", "test222@gmail.com"
                            , "09876543211", "1", "123456");    
        }

        public static Customer GetCustmerWithSameEmail()
        {
            return new Customer("Robbie Stwart", "123123123", "test@gmail.com"
                            , "09876543212", "1", "123456");
        }

        public static Customer GetCustomer()
        {
            return new Customer("Robbie Stwart", "123123123", "test3@gmail.com"
                            , "09876543212", "1", "123456");
        }

        public static CacheDTO GetUnexceptedStateCacheData()
        {
            return new CacheDTO("Robbie Stwart", "09876543212", "1",CacheState.ComfirmEmailOTP, "test3@gmail.com"
                            , "1234","3456");
        }

        public static CacheDTO GetCacheData()
        {
            return new CacheDTO("Robbie Stwart", "09876543212", "1", CacheState.PreInit, "test@gmail.com"
                            , "1234", "3456");
        }

        public static string GetMobileNumberExistsMessage(string mobile) => $"a customer exists with mobile number {mobile}"; 
        public static string GetEmailExistsMessage(string email) => $"a customer exists with email address {email}"; 
        public static string GetTryAgainMessage() => "Register process takes more than 5 mins or Some error happens.Please try again."; 





    }
}
