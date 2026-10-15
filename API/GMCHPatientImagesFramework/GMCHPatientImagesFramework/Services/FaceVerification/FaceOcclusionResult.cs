namespace ConfigurationFramework.Services.FaceVerification
{
    public class FaceOcclusionResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
    public class ImageQualityResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}
