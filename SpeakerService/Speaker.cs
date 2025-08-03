using GK.Talks.SupportingClasses;
using System.Collections.Generic;

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
		public List<string> Certifications { get; private set; } = new List<string>();
      public string Employer { get; set; }
		public decimal RegistrationFee { get; private set; }
		public List<Session> Sessions { get; private set; } = new List<Session>();


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