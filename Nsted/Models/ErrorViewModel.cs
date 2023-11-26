namespace Nsted.Models
{
    /// <summary>
    /// Modell for å representere informasjon relatert til en feil eller unntakssituasjon.
    /// </summary>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string InnerExceptionMessage { get; set; }
    }

}