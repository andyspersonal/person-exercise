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
		/// Register a speaker
		/// Prams	
		/// strFirstName speakers first name
		///	strLastName ^^^ last name
		/// Email the email
		/// blogs etc.....
		/// </summary>
		/// <returns>speakerID</returns>	
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
         if (speaker.Exp <= 1)
         {
            speaker.RegistrationFee = 500;
         }
         else if (speaker.Exp >= 2 && speaker.Exp <= 3)
         {
            speaker.RegistrationFee = 250;
         }
         else if (speaker.Exp >= 4 && speaker.Exp <= 5)
         {
            speaker.RegistrationFee = 100;
         }
         else if (speaker.Exp >= 6 && speaker.Exp <= 9)
         {
            speaker.RegistrationFee = 50;
         }
         else
         {
            speaker.RegistrationFee = 0;
         }


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
