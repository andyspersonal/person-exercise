namespace GK.Talks.SupportingClasses
{
   public class RegisterResponse
   {
      public RegisterError RegisterError { get; }

      public int SpeakerId { get; }

      public RegisterResponse(RegisterError registerError)
      {
         RegisterError = registerError;
      }


      public RegisterResponse(int speakerId)
      {
         SpeakerId = speakerId;
      }
   }  
}
