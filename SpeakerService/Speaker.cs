using GK.Talks.SupportingClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GK.Talks
{
	/// <summary>
	/// Represents a single speaker
	/// </summary>
	/// </summary>
	public class Speaker
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int? ExperienceLevel { get; set; }
		public bool HasBlog { get; set; }
		public string BlogURL { get; set; }
		public WebBrowser Browser { get; set; }
		public List<string> Certifications { get; set; }
		public string Employer { get; set; }
		public decimal RegistrationFee { get; private set; }
		public List<Session> Sessions { get; set; }

      /// <summary>
      /// Validates if the speaker meets the registration requirements.
      /// </summary>
      /// <returns>An enum indicating the validation issue, or None if the spreaker is valid.</returns>
      public RegisterError ValidateForRegistration()
		{
			if (string.IsNullOrEmpty(FirstName))
			{
				return RegisterError.FirstNameRequired;
         }

         if (string.IsNullOrEmpty(LastName))
         {
            return RegisterError.LastNameRequired;
         }

         if (string.IsNullOrEmpty(Email))
         {
            return RegisterError.EmailRequired;
         }

         var approvedEmployers = new List<string>() { "Pluralsight", "Microsoft", "Google" };

         // possible case issue here.
         var hasMetStandards = ExperienceLevel > 10 || HasBlog || Certifications.Count > 3 || approvedEmployers.Contains(Employer);

         if (!hasMetStandards)
         {
            // need to get just the domain from the email
            // Possible issue if there is no @ in the email address.  Email address should be better validated
            string emailDomain = Email.Split('@').Last();  
            // DEFECT #5274 CL 12/10/2010
            // We weren't filtering out the prodigy domain so I added it.
            var domains = new List<string>() { "aol.com", "prodigy.com", "compuserve.com" };
            if (!domains.Contains(emailDomain) && (!(Browser.Name == WebBrowser.BrowserName.InternetExplorer.ToString() && Browser.MajorVersion < 9)))
            {
               hasMetStandards = true;
            }
         }

			if(!hasMetStandards)
			{
				return RegisterError.SpeakerDoesNotMeetStandards;
         }

         return ValidateSessions();
		}

      private RegisterError ValidateSessions()
      {
         if (Sessions == null || Sessions.Count == 0)
         {
            return RegisterError.NoSessionsProvided;
         }
         foreach (var session in Sessions)
         {
            if (!session.IsApproved())
            {
               return RegisterError.NoSessionsApproved;  //really this is any session is not approved.  Either chaneg the name of error or change the business logic.
            }
         }
         return RegisterError.None;
      }


      /// <summary>
      /// Calculates and sets the registration fee based on the speaker's experience.
      /// </summary>
      public void CalculateRegistrationFee()
      {
         switch (ExperienceLevel.GetValueOrDefault())
         {
            case <= 1:
               RegistrationFee = 500;
               break;
            case 2:
            case 3:
               RegistrationFee = 250;
               break;
            case 4:
            case 5:
               RegistrationFee = 100;
               break;
            case >= 6 and <= 9:
               RegistrationFee = 50;
               break;
            default:
               RegistrationFee = 0;
               break;
         }
      }
   }
}