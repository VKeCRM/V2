using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace VKeCRM.Common.Utility
{
   public class HTMLHelper
   {
       private static string m_html;
       private static HtmlDocument doc;

       public HTMLHelper(string html)
       {
           if (html == null)
               throw new ArgumentNullException("reader html fail");
           doc = new HtmlDocument();
           doc.LoadHtml(html);
       }

       public  string ProcessingFont()
       {
           var spanList = doc.DocumentNode.SelectNodes("//span");
           if (spanList == null)
               return doc.DocumentNode.InnerHtml;
           foreach (var span in spanList)
           {
               foreach (var attr in span.Attributes)
               {
                   if (attr.Name.ToLower() == "style")
                   {
                       if (attr.Value.StartsWith("font-family:Symbol"))
                       {
                           //attr.Value.Replace("font-family:Symbol","font-family:")
                       }
                       if (attr.Value.IndexOf("font-family:Symbol", System.StringComparison.Ordinal) == -1)
                       {
                           attr.Value += ";font-family:Arial;";
                       }
                   }
               }
           }
           return doc.DocumentNode.InnerHtml;
       }
   }
}
