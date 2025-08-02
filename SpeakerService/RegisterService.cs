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
         // lets init some vars
         int? speakerId = null;
         bool good = false;
         bool appr = false;
         //var nt = new List<string> {"Node.js", "Docker"};
         var ot = new List<string>() { "Cobol", "Punch Cards", "Commodore", "VBScript" };

         //DEFECT #5274 CL 12/10/2010
         //We weren't filtering out the prodigy domain so I added it.
         var domains = new List<string>() { "aol.com", "prodigy.com", "compuserve.com" };

         if (!((speaker.FirstName == null) || (speaker.FirstName.Length == 0)))
         {
            if (!((speaker.LastName == null) || (speaker.LastName.Length == 0)))
            {
               if (!((speaker.Email == null) || (speaker.Email.Length == 0)))
               {
                  //put list of employers in array
                  var emps = new List<string>() { "Pluralsight", "Microsoft", "Google" };

                  good = speaker.Exp > 10 || speaker.HasBlog || speaker.Certifications.Count() > 3 || emps.Contains(speaker.Employer);

                  if (!good)
                  {
                     //need to get just the domain from the email
                     string emailDomain = speaker.Email.Split('@').Last();

                     if (!domains.Contains(emailDomain) && (!(speaker.Browser.Name == WebBrowser.BrowserName.InternetExplorer.ToString() && speaker.Browser.MajorVersion < 9)))
                     {
                        good = true;
                     }
                  }

                  if (good)
                  {
                     if (speaker.Sessions.Count() != 0)
                     {
                        foreach (var session in speaker.Sessions)
                        {
                           //foreach (var tech in nt)
                           //{
                           //    if (session.Title.Contains(tech))
                           //    {
                           //        session.Approved = true;
                           //        break;
                           //    }
                           //}

                           foreach (var tech in ot)
                           {
                              if (session.Title.Contains(tech) || session.Description.Contains(tech))
                              {
                                 session.Approved = false;
                                 break;
                              }
                              else
                              {
                                 session.Approved = true;
                                 appr = true;
                              }
                           }
                        }
                     }
                     else
                     {
                        return new RegisterResponse(RegisterError.NoSessionsProvided);
                     }

                     if (appr)
                     {
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
                     }
                     else
                     {
                        return new RegisterResponse(RegisterError.NoSessionsApproved);
                     }
                  }
                  else
                  {
                     return new RegisterResponse(RegisterError.SpeakerDoesNotMeetStandards);
                  }
               }
               else
               {
                  return new RegisterResponse(RegisterError.EmailRequired);
               }
            }
            else
            {
               return new RegisterResponse(RegisterError.LastNameRequired);
            }
         }
         else
         {
            return new RegisterResponse(RegisterError.FirstNameRequired);
         }

         //if we got this far, the speaker is registered.
         return new RegisterResponse((int)speakerId);
      }
   }
   
}
