using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using ConfigurationFramework.Services.FaceVerification;
using System.Collections.Generic;
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
        public async Task<FaceOcclusionResult> CheckFaceOcclusionAsync(
    byte[] imageBytes)
        {
            var request = new DetectFacesRequest
            {
                Image = new Amazon.Rekognition.Model.Image
                {
                    Bytes = new MemoryStream(imageBytes)
                },
                Attributes = new List<string>
        {
            "DEFAULT",
            "FACE_OCCLUDED"
        }
            };

            var response = await _rekognition.DetectFacesAsync(request);

            if (response.FaceDetails == null ||
                response.FaceDetails.Count == 0)
            {
                return new FaceOcclusionResult
                {
                    IsValid = false,
                    Message = "No face detected in the uploaded image."
                };
            }

            // Reject if a detected face is confidently reported as occluded.
            var occludedFace = response.FaceDetails.FirstOrDefault(
                f => f.FaceOccluded?.Value == true &&
                     f.FaceOccluded.Confidence.GetValueOrDefault() >= 80
            );

            if (occludedFace != null)
            {
                return new FaceOcclusionResult
                {
                    IsValid = false,
                    Message = "Face is partially blocked. Please upload a clear image."
                };
            }

            return new FaceOcclusionResult
            {
                IsValid = true,
                Message = "Face visibility check passed."
            };
        }

        public async Task<ImageQualityResult> CheckImageQualityAsync(
    byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                return new ImageQualityResult
                {
                    IsValid = false,
                    Message = "Uploaded image is empty."
                };
            }

            var request = new DetectFacesRequest
            {
                Image = new Amazon.Rekognition.Model.Image
                {
                    Bytes = new MemoryStream(imageBytes)
                },
                Attributes = new List<string> { "DEFAULT" }
            };

            var response = await _rekognition.DetectFacesAsync(request);

            if (response.FaceDetails == null ||
                response.FaceDetails.Count == 0)
            {
                return new ImageQualityResult
                {
                    IsValid = false,
                    Message = "No face detected. Please upload a clear image."
                };
            }

            // Initial POC thresholds only. Calibrate using real images.
            const float minSharpness = 40f;
            const float minBrightness = 25f;
            const float maxBrightness = 90f;

            foreach (var face in response.FaceDetails)
            {
                var quality = face.Quality;

                if (quality == null)
                {
                    return new ImageQualityResult
                    {
                        IsValid = false,
                        Message = "Unable to assess face image quality."
                    };
                }

                if (quality.Sharpness < minSharpness)
                {
                    return new ImageQualityResult
                    {
                        IsValid = false,
                        Message = "Image is blurry. Please upload a sharper image."
                    };
                }

                if (quality.Brightness < minBrightness)
                {
                    return new ImageQualityResult
                    {
                        IsValid = false,
                        Message = "Image is too dark. Please improve the lighting."
                    };
                }

                if (quality.Brightness > maxBrightness)
                {
                    return new ImageQualityResult
                    {
                        IsValid = false,
                        Message = "Image is overexposed. Please upload a clearer image."
                    };
                }
            }

            return new ImageQualityResult
            {
                IsValid = true,
                Message = "Image quality check passed."
            };
        }
    }
}