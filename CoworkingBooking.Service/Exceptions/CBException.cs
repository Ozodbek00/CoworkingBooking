namespace CoworkingBooking.Service.Exceptions
{
    public class CBException : Exception
    {
        public int Code { get; set; }

        public CBException(int code, string message): base(message)
        {
            this.Code = code;
        }
    }
}
