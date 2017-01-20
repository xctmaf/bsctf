namespace Web.Application.Exceprtions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class IncorrectUrlException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public IncorrectUrlException()
        {
        }

        public IncorrectUrlException(string message) : base(message)
        {
        }

        public IncorrectUrlException(string message, Exception inner) : base(message, inner)
        {
        }

        protected IncorrectUrlException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}