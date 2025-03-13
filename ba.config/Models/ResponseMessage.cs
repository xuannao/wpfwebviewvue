namespace ba.config.Models
{
    public class ResponseMessage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名样式", Justification = "<挂起>")]
        public bool status { get; set; } = false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名样式", Justification = "<挂起>")]
        public object? data { get; set; }

        internal ResponseMessage(bool status, object? data)
        {
            this.status = status;
            this.data = data;
        }

        public static ResponseMessage Success(object? data)
        {
            return new ResponseMessage(true, data);
        }

        public static ResponseMessage Fail(object? data)
        {
            return new ResponseMessage(false, data);
        }
    }
}