using GK.Talks.SupportingClasses;

namespace GK.Talks
{
   public interface IRegisterService
   {
      /// <summary>
      /// Registers a speaker for the conference.
      /// </summary>
      /// <param name="speaker">The speaker to register</param>
      /// <returns>A response object that indicates success or failure and the generated speaker id if sucessfull.</returns>
      RegisterResponse Register(Speaker speaker);

      /// <summary>
      /// Validates if the speaker meets the registration requirements.
      /// </summary>
      /// <returns>An enum indicating the validation issue, or None if the spreaker is valid.</returns>
      RegisterError ValidateForRegistration(Speaker speaker);
   }
}
