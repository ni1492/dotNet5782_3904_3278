using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    namespace BO
    {
        public class exceptions : Exception //exceptions class 
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

            [Serializable]
            public class TimeException : Exception //if the information already exists
            {
                public TimeException() : base() { }
                public TimeException(string message) : base(message) { }
                public TimeException(string message, Exception inner) : base(message, inner) { }
                public TimeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
            }

        [Serializable]
            public class BatteryException : Exception //if the information already exists
            {
                public BatteryException() : base() { }
                public BatteryException(string message) : base(message) { }
                public BatteryException(string message, Exception inner) : base(message, inner) { }
                public BatteryException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
            }

            [Serializable]
            public class SlotsException : Exception //if the information already exists
            {
                public SlotsException() : base() { }
                public SlotsException(string message) : base(message) { }
                public SlotsException(string message, Exception inner) : base(message, inner) { }
                public SlotsException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
            }
            
            [Serializable]
            public class StatusException : Exception //if the information already exists
            {
                public StatusException() : base() { }
                public StatusException(string message) : base(message) { }
                public StatusException(string message, Exception inner) : base(message, inner) { }
                public StatusException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
            }
        
             [Serializable]
            public class DeleteException : Exception //if the information already exists
        {
            public DeleteException() : base() { }
            public DeleteException(string message) : base(message) { }
            public DeleteException(string message, Exception inner) : base(message, inner) { }
            public DeleteException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }
    }


