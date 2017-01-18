namespace Domain.Exceptions
{
    using System;
    using System.Runtime.Serialization;


    public class DuplicateEntityException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public DuplicateEntityException()
        {
        }

        public DuplicateEntityException(string message) : base(message)
        {
        }

        public DuplicateEntityException(string message, Exception inner) : base(message, inner)
        {
        }

        protected DuplicateEntityException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}