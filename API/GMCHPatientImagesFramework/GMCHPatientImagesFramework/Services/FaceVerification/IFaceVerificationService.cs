using ConfigurationFramework.Services.FaceVerification;
using System.IO;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.FaceVerification
{
    public interface IFaceVerificationService
    {
        Task<FaceVerificationResult> CompareFacesAsync(
            Stream sourceImage,
            Stream targetImage,
            float similarityThreshold = 80);
    }
}