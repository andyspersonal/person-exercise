using System.Collections.Generic;

namespace GK.Talks
{
   public class Session
   {
      public string Title { get; set; }

      public string Description { get; set; }

      public bool IsApproved()
      {
         // List of technologies that are not approved
         var technologies = new List<string>() { "Cobol", "Punch Cards", "Commodore", "VBScript" };
         foreach (var technology in technologies)
         {
            if (Title.Contains(technology) || Description.Contains(technology))
            {
               return false; 
            }
         }
         return true; 
      }  
   }
}
