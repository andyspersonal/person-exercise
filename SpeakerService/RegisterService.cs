using GK.Talks.SupportingClasses;

namespace GK.Talks
{
   public class RegisterService
   {
      private readonly IRepository _repository; 
      
      public RegisterService(IRepository respository) 
      { 
         _repository = respository;
      }


      /// <summary>
      /// Registers a speaker for the conference.
      /// </summary>
      /// <param name="speaker">The speaker to register</param>
      /// <returns>A response object that indicates success or failure and the generated speaker id if sucessfull.</returns>
      public RegisterResponse Register(Speaker speaker)
      {         
         // validate the speaker
         var speakerRegistorError = speaker.ValidateForRegistration();
         if (speakerRegistorError != RegisterError.None)
         {
            return new RegisterResponse(speakerRegistorError);
         }

         //calculate the registration fee.
         speaker.CalculateRegistrationFee();

         // save to teh database.
         var speakerId = _repository.SaveSpeaker(speaker);
         return new RegisterResponse((int)speakerId); 
      }
   }
}
