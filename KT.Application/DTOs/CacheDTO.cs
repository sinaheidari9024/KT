namespace KT.Application.DTOs
{
    public record CacheDTO(string Name, string Mobile, string CountryCode, CacheState State, string Email, string MobileOTP, string EmailOTP);
    public record UpdateCacheDTO(string ICNumber, string Name, string MobileNo, string CountryCode, CacheState State, string Email);

}
