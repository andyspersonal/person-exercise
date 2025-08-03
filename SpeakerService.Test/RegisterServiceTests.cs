using GK.Talks;
using GK.Talks.SupportingClasses;
using Moq;
using RegisterError = GK.Talks.SupportingClasses.RegisterError;

namespace SpeakerService.Test
{
   public class RegisterServiceTests
   {
      private readonly Mock<IRepository> _repositoryMock;
      private readonly IRegisterService _registerService;

      public RegisterServiceTests()
      {
         _repositoryMock = new Mock<IRepository>();
         _registerService = new RegisterService(_repositoryMock.Object);
      }

      [Fact]
      public void Register_ValidPerson_UpdatesRepository()
      {
         // Setup
         var validSpeaker = CreateValidSpeaker();

         var expectedSpeakerId = 123;
         _repositoryMock.Setup(r => r.SaveSpeaker(It.IsAny<Speaker>()))
                        .Returns(expectedSpeakerId)
                        .Verifiable();

         // Act
         var response = _registerService.Register(validSpeaker);


         // Assert
         _repositoryMock.Verify();
         Assert.Equal(expectedSpeakerId, response.SpeakerId);
      }

      [Fact]
      public void Register_ValidPerson_ReturnsSpeakerId()
      {
         // Setup
         var validSpeaker = CreateValidSpeaker();

         var expectedSpeakerId = 123;
         _repositoryMock.Setup(r => r.SaveSpeaker(It.IsAny<Speaker>()))
                        .Returns(expectedSpeakerId);
         
         // Act
         var response = _registerService.Register(validSpeaker);


         // Assert
         Assert.Equal(expectedSpeakerId, response.SpeakerId);
         Assert.Equal(RegisterError.None, response.RegisterError);
      }

      [Fact]
      public void Register_RepositoryException_ThrowsException()
      {
         // Setup
         var validSpeaker = CreateValidSpeaker();


         _repositoryMock.Setup(r => r.SaveSpeaker(It.IsAny<Speaker>()))
                        .Throws(new Exception("Database error"))
                        .Verifiable();

         // Act & Assert
         Assert.Throws<Exception>(() => _registerService.Register(validSpeaker));
      }

      [Fact]
      public void Validate_ValidSpeaker_ReturnsNone()
      {
         // Setup
         var validSpeaker = CreateValidSpeaker();
         
         // Act
         var registerError = _registerService.ValidateForRegistration(validSpeaker);
         
         // Assert
         Assert.Equal(RegisterError.None, registerError);
      }  

      [Fact]
      public void Validate_InvalidEmail_ReturnsEmailRequired()
      {
         // Setup
         var validSpeaker = CreateValidSpeaker();
         validSpeaker.Email = "";
         
         // Act
         var registerError = _registerService.ValidateForRegistration(validSpeaker);

         // Assert
         Assert.Equal(RegisterError.EmailRequired, registerError);
      }

      [Fact]
      public void Validate_NoSessions_ReturnsNoSessionsProvided()
      {
         // Setup
         var validSpeaker = CreateValidSpeaker();
         validSpeaker.Sessions.Clear();

         // Act
         var registerError = _registerService.ValidateForRegistration(validSpeaker);

         // Assert
         Assert.Equal(RegisterError.NoSessionsProvided, registerError);
      }


      private Speaker CreateValidSpeaker()
      {
         var validSpeaker = new Speaker
         {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@exmaple.com",
            HasBlog = true,
            ExperienceLevel = 5,
            Browser = new WebBrowser(),
         };

         validSpeaker.Sessions.Add(new Session
         {
            Title = "Test Session",
            Description = "This is a test session",
         });

         return validSpeaker;
      }
   }
}
