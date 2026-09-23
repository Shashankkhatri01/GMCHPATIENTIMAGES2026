using ConfigurationFramework.Services.FaceVerification;
using System.IO;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.FaceVerification
{
    public class NoOpFaceVerificationService : IFaceVerificationService
    {
        public Task<FaceVerificationResult> CompareFacesAsync(
            Stream sourceImage,
            Stream targetImage,
            float similarityThreshold = 0)
        {
            return Task.FromResult(new FaceVerificationResult
            {
                IsMatch = false,
                Similarity = 0,
                SourceFaceConfidence = 0,
                TargetFaceCount = 0,
                Message = "Face authentication is disabled."
            });
        }
    }
}