namespace Web.Application.Exceprtions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class DuplicateUserException : Exception
    {
        public DuplicateUserException()
        {
        }

        public DuplicateUserException(string message) : base(message)
        {
        }

        public DuplicateUserException(string message, Exception inner) : base(message, inner)
        {
        }

        protected DuplicateUserException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}