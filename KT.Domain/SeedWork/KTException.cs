namespace KT.Domain.SeedWork
{
    public abstract class KTException : Exception
    {
        protected KTException(string message, params object[] arguments) : base(message)
        {
            AddArguments(arguments);
        }

        protected KTException(string message, Exception innerException, params object[] arguments) : base(message, innerException)
        {
            AddArguments(arguments);
        }

        public object[] Arguments { get; private set; }  
        private void AddArguments(params object[] arguments)
        {
            Arguments = new object[arguments.Length];
            arguments.CopyTo(Arguments, 0);
        }

    }
}
