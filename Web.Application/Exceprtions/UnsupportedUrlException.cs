namespace Web.Application.Exceprtions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class UnsupportedUrlException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public UnsupportedUrlException()
        {
        }

        public UnsupportedUrlException(string message) : base(message)
        {
        }

        public UnsupportedUrlException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UnsupportedUrlException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}