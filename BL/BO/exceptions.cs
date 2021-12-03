using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL.BO
{
    public class exceptions:Exception //exceptions class 
    {
        [Serializable]
        public class NotFoundException : Exception // if the information does not exist 
        {
            public NotFoundException() : base() { }
            public NotFoundException(string message) : base(message) { }
            public NotFoundException(string message, Exception inner) : base(message, inner) { }
            public NotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }

        [Serializable]
        public class ExistException : Exception //if the information already exists
        {
            public ExistException() : base() { }
            public ExistException(string message) : base(message) { }
            public ExistException(string message, Exception inner) : base(message, inner) { }
            public ExistException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }

    }
}

