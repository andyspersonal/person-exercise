namespace GK.Talks.SupportingClasses
{
   public class WebBrowser
   {
      public string Name { get; set; }

      public int MajorVersion { get; set; }

      public enum BrowserName
      {
         InternetExplorer,
         Firefox,
         Chrome,
         Safari,
         Edge,
         Opera,
      }
   }
}
