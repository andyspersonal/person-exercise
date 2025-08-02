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
		public int? Exp { get; set; }
		public bool HasBlog { get; set; }
		public string BlogURL { get; set; }
		public WebBrowser Browser { get; set; }
		public List<string> Certifications { get; set; }
		public string Employer { get; set; }
		public int RegistrationFee { get; set; }
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

         var hasMetStandards = Exp > 10 || HasBlog || Certifications.Count() > 3 || approvedEmployers.Contains(Employer);

         if (!hasMetStandards)
         {
            // need to get just the domain from the email
            // Possible null exception is there is no @ in the email address.  EMail address should be better validated
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
               return RegisterError.NoSessionsApproved;
            }
         }
         return RegisterError.None;
      }
   }
}