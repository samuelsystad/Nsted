namespace Nsted.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string ErrorMessage { get; set; } // Add this property
        public string StackTrace { get; set; } // Add this property
        public string InnerExceptionMessage { get; set; } // Add this property
    }

}