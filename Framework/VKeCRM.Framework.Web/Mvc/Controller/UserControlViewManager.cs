using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;


namespace VKeCRM.Framework.Web.Mvc.Controller
{
    public class UserControlViewManager<T> where T : UserControl
    {
        private Page m_pageHolder;

        public T LoadViewControl(string path)
        {
            this.m_pageHolder = new Page();
            return (T)this.m_pageHolder.LoadControl(path);
        }

        public string RenderView(T control)
        {
            StringWriter output = new StringWriter();

            this.m_pageHolder.Controls.Add(control);
            HttpContext.Current.Server.Execute(this.m_pageHolder, output, false);

            return output.ToString();
        }
    }
}
