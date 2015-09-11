using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace VKeCRM.Framework.Web.UI.Controls
{
    /// <summary>
    /// Helper class to register the library resources to the web pages that will
    /// use the library controls.
    /// </summary>
    internal static class WebResourceManager
    {
        // Embedded resource name convention is "Namespace.Path.FileName").
        private static readonly string ResourceKeyPrefix = typeof (WebResourceManager).Namespace;

        // Gets the current page client-side script manager.
        internal static ClientScriptManager ScriptManager
        {
            get
            {
                Page currentPage = (Page) HttpContext.Current.Handler;
                return currentPage.ClientScript;
            }
        }

        /// <summary>
        /// Registers a client script file in the page that is using the control.
        /// </summary>
        /// <param name="scriptFile">The name of the script file resource.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        internal static void RegisterClientScriptInclude(string scriptFile)
        {
            string resourceName = BuildResourceName(scriptFile);

            string scriptUrl = ScriptManager.GetWebResourceUrl(typeof (WebResourceManager), resourceName);
            string includeScriptHtml = String.Format("<script src='{0}' type='text/javascript'/></script>", scriptUrl);

            AddHeaderIncludeElement(resourceName, includeScriptHtml);
        }

        /// <summary>
        /// Gets the URL for a script resource embedded in the control library.
        /// </summary>
        /// <param name="scriptFile">The name of the script file resource.</param>
        internal static string GetScriptResourceUrl(string scriptFile)
        {
            string resourceName = BuildResourceName(scriptFile);
            return ScriptManager.GetWebResourceUrl(typeof (WebResourceManager), resourceName);
        }

        /// <summary>
        /// Gets the URL for a image resource embedded in the control library.
        /// </summary>
        /// <param name="imageFile">The name of the image file resource.</param>
        internal static string GetImageResourceUrl(string imageFile)
        {
            string resourceName = BuildResourceName(imageFile);
            return ScriptManager.GetWebResourceUrl(typeof (WebResourceManager), resourceName);
        }

        /// <summary>
        /// Registers a stylesheet include to the page header.
        /// </summary>
        /// <param name="styleFile">The name of the stylesheet file resource.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        internal static void RegisterStylesheetInclude(string styleFile)
        {
            string resourceName = BuildResourceName(styleFile);

            string styleUrl = ScriptManager.GetWebResourceUrl(typeof (WebResourceManager), resourceName);
            string includeStyleHtml = String.Format("<link rel='stylesheet' type='text/css' href='{0}'/>", styleUrl);

            AddHeaderIncludeElement(resourceName, includeStyleHtml);
        }

        internal static void AddHeaderIncludeElement(string includeElementID, string includeElementHtml)
        {
            HtmlHead pageHeader = ((Page) HttpContext.Current.Handler).Header;

            if (pageHeader == null)
            {
                // Nothing can be done if the header is not defined as a server-side control.
                return;
            }

            // Make sure the library only adds the includes element once to the page.
            if (pageHeader.FindControl(includeElementID) == null)
            {
                LiteralControl includeElement = new LiteralControl(includeElementHtml);
                includeElement.ID = includeElementID;
                includeElement.EnableViewState = false;
                pageHeader.Controls.Add(includeElement);
            }
        }

        public static string BuildResourceName(string resourceFile)
        {
            // Replace all path separators by namespace separators.
            resourceFile = resourceFile.Replace(Path.DirectorySeparatorChar, Type.Delimiter);
            resourceFile = resourceFile.Replace(Path.AltDirectorySeparatorChar, Type.Delimiter);

            // Add the resource key prefix.
            if (resourceFile[0] != Type.Delimiter)
            {
                resourceFile = Type.Delimiter + resourceFile;
            }

            if (!resourceFile.StartsWith(ResourceKeyPrefix))
            {
                resourceFile = ResourceKeyPrefix + resourceFile;
            }

            return resourceFile;
        }
    }
}