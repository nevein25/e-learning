using Moq;

namespace API.Helpers
{
    public static class MoqIFormFile
    {
        public static IFormFile CreateMockFormFile(string filePath)
        {
            // Read the video file into a byte array
            byte[] fileBytes = File.ReadAllBytes(filePath);

            // Create a memory stream from the byte array
            var memoryStream = new MemoryStream(fileBytes);

            // Create a mock IFormFile object
            var mockFormFile = new Mock<IFormFile>();

            // Setup the mock to return the memory stream
            mockFormFile.Setup(f => f.OpenReadStream()).Returns(memoryStream);
            mockFormFile.Setup(f => f.FileName).Returns(Path.GetFileName(filePath));
            mockFormFile.Setup(f => f.Length).Returns(memoryStream.Length);
            mockFormFile.Setup(f => f.ContentType).Returns("video/mp4"); // Set appropriate content type
            return mockFormFile.Object;
        }
    }
}
