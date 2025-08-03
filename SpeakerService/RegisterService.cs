using GK.Talks.SupportingClasses;
using System.Collections.Generic;
using System.Linq;

namespace GK.Talks
{
   public class RegisterService : IRegisterService
   {
      private readonly IRepository _repository;

      public RegisterService(IRepository respository)
      {
         _repository = respository;
      }


      /// <inheritdoc/>
      public RegisterResponse Register(Speaker speaker)
      {
         // validate the speaker
         var speakerRegistorError = this.ValidateForRegistration(speaker);
         if (speakerRegistorError != RegisterError.None)
         {
            return new RegisterResponse(speakerRegistorError);
         }

         //calculate the registration fee.
         speaker.CalculateRegistrationFee();

         // save to the database.
         var speakerId = _repository.SaveSpeaker(speaker);
         return new RegisterResponse(speakerId);
      }

      /// <inheritdoc/>
      public RegisterError ValidateForRegistration(Speaker speaker)
      {
         if (string.IsNullOrEmpty(speaker.FirstName))
         {
            return RegisterError.FirstNameRequired;
         }

         if (string.IsNullOrEmpty(speaker.LastName))
         {
            return RegisterError.LastNameRequired;
         }

         if (string.IsNullOrEmpty(speaker.Email))
         {
            return RegisterError.EmailRequired;
         }

         var approvedEmployers = new List<string>() { "Pluralsight", "Microsoft", "Google" };

         // possible case issue here.
         var personMeetsStandards = speaker.ExperienceLevel > 10 || speaker.HasBlog || speaker.Certifications.Count > 3 || approvedEmployers.Contains(speaker.Employer);
         var isModernBrowser = !(speaker.Browser.Name == WebBrowser.BrowserName.InternetExplorer.ToString() && speaker.Browser.MajorVersion < 9);
         var exemptEmailDomains = new List<string>() { "aol.com", "prodigy.com", "compuserve.com" };

         // Possible issue if there is no @ in the email address.  Email address should be better validated
         var personHasExemptEmailDomain = exemptEmailDomains.Contains(speaker.Email.Split('@').Last());

         if (!personMeetsStandards)
         {
            if (personHasExemptEmailDomain || isModernBrowser)
            {
               return RegisterError.SpeakerDoesNotMeetStandards;
            }
         }

         return ValidateSessions(speaker.Sessions);
      }

      private RegisterError ValidateSessions(IList<Session> sessions)
      {
         if (sessions.Count == 0)
         {
            return RegisterError.NoSessionsProvided;
         }

         if (sessions.Any(s => !s.IsApproved()))
         {
            //really this is any session is not approved.  Either change the name of error or change the business logic.
            return RegisterError.NoSessionsApproved;
         }
         
         return RegisterError.None;
      }
   }
}
