namespace KT.Domain.Exceptions
{
    public class TypeMissmachtedException : KTException
    {
        public TypeMissmachtedException() :
            base("There is a problem in casting objects. Try again later.")
        { }
    }
}
