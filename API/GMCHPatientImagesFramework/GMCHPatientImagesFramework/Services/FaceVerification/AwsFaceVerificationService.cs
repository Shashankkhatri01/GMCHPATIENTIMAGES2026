using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using ConfigurationFramework.Services.FaceVerification;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.FaceVerification
{
    public class AwsFaceVerificationService : IFaceVerificationService
    {
        private readonly IAmazonRekognition _rekognition;

        public AwsFaceVerificationService(
            IAmazonRekognition rekognition)
        {
            _rekognition = rekognition;
        }

        public async Task<FaceVerificationResult> CompareFacesAsync(
                Stream sourceImage,
                Stream targetImage,
                float similarityThreshold = 0)
        {
            using var sourceMemoryStream = new MemoryStream();
            using var targetMemoryStream = new MemoryStream();

            await sourceImage.CopyToAsync(sourceMemoryStream);
            await targetImage.CopyToAsync(targetMemoryStream);

            var request = new CompareFacesRequest
            {
                SourceImage = new Image
                {
                    Bytes = new MemoryStream(
                        sourceMemoryStream.ToArray())
                },

                TargetImage = new Image
                {
                    Bytes = new MemoryStream(
                        targetMemoryStream.ToArray())
                },

                // Get all similarity results.
                // Application will decide the threshold.
                SimilarityThreshold = 0
            };

            var response =
                await _rekognition.CompareFacesAsync(request);

            var bestMatch = response.FaceMatches?
                .OrderByDescending(x => x.Similarity)
                .FirstOrDefault();

            if (bestMatch == null)
            {
                return new FaceVerificationResult
                {
                    IsMatch = false,

                    Similarity = 0,

                    SourceFaceConfidence =
                        response.SourceImageFace?.Confidence ?? 0,

                    TargetFaceCount =
                        (response.FaceMatches?.Count ?? 0) +
                        (response.UnmatchedFaces?.Count ?? 0),

                    Message = "No face found in target image."
                };
            }

            return new FaceVerificationResult
            {
                // We are not deciding final PASS/FAIL here.
                // Caller will decide.
                IsMatch = true,

                Similarity = bestMatch.Similarity,

                SourceFaceConfidence =
                    response.SourceImageFace?.Confidence ?? 0,

                TargetFaceCount =
                    (response.FaceMatches?.Count ?? 0) +
                    (response.UnmatchedFaces?.Count ?? 0),

                Message = "Face comparison completed."
            };
        }
    }
}