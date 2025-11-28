using System;

namespace Part2.BLL
{
    public class StudentServiceException : Exception
    {
        public StudentServiceException(string message) : base(message) { }
    }
}