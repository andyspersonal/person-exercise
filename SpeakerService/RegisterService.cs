using GK.Talks.SupportingClasses;
using System;
using System.Collections.Generic;
using System.Linq;

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
         
         int? speakerId = null;
         var ot = new List<string>() { "Cobol", "Punch Cards", "Commodore", "VBScript" };

         var speakerRegistorError = speaker.ValidateForRegistration();

         if (speakerRegistorError != RegisterError.None)
         {
            return new RegisterResponse(speakerRegistorError);
         }

         //if we got this far, the speaker is approved
         //let's go ahead and register him/her now.
         //First, let's calculate the registration fee. 
         //More experienced speakers pay a lower fee.
         speaker.CalculateRegistrationFee();


         //Now, save the speaker and sessions to the db.
         try
         {
            speakerId = _repository.SaveSpeaker(speaker);
         }
         catch (Exception e)
         {
            //in case the db call fails 
         }
         
        
              
        

         //if we got this far, the speaker is registered.
         return new RegisterResponse((int)speakerId);
      }
   }
   
}
