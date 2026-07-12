namespace GMCHPatientImagesDtos.DTOs
{
    public class ReturnObject<T>
    {

        public bool Success { get; set; }
        public string Message { get; set; }
        public T ReturnValue { get; set; }
        public bool Status { get; set; }
    }
}
