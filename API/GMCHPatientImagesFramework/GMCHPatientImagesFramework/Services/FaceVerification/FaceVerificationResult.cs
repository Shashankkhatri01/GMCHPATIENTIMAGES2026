namespace ConfigurationFramework.Services.FaceVerification
{
    public class FaceVerificationResult
    {
        public bool IsMatch { get; set; }
        public float? Similarity { get; set; }
        public float SourceFaceConfidence { get; set; }
        public int TargetFaceCount { get; set; }
        public string Message { get; set; }
    }
}
