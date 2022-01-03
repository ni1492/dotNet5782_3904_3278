using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace DO
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception inner) : base(message, inner) { }
        public NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public class ExistException : Exception
    {
        public ExistException() : base() { }
        public ExistException(string message) : base(message) { }
        public ExistException(string message, Exception inner) : base(message, inner) { }
        public ExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    public class DALException : Exception
    {
        public DALException() : base() { }
        public DALException(string message) : base(message) { }
        public DALException(string message, Exception inner) : base(message, inner) { }
        public DALException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }
         public class InvalidInformationException : Exception
    {
        public InvalidInformationException() : base() { }
        public InvalidInformationException(string message) : base(message) { }
        public InvalidInformationException(string message, Exception inner) : base(message, inner) { }
        public InvalidInformationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

}