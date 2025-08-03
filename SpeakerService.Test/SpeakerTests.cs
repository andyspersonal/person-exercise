namespace SpeakerService.Test
{
   public class SpeakerTests
   {
      [Fact]
      public void CalculateRegistrationFee_Is500_WhenExperienceCalculateRegistrationFee()
      {
         // Arrange
         var speaker = new GK.Talks.Speaker
         {
            ExperienceLevel = null
         }; 

         // Act
         speaker.CalculateRegistrationFee();


         // Assert
         Assert.Equal(500, speaker.RegistrationFee);
      }

      [Fact]
      public void CalculateRegistrationFee_Is0_WhenExperienceIs10()
      {
         // Arrange
         var speaker = new GK.Talks.Speaker
         {
            ExperienceLevel = 10
         };
         
         // Act
         speaker.CalculateRegistrationFee();
         
         // Assert
         Assert.Equal(0, speaker.RegistrationFee);
      }
   }
}