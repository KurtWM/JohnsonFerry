/**********************************************************************
* Description:   Sitemap page
* Created By:    Kurt Meredith
* Date Created:  2/7/2013
**********************************************************************/
using System;
using System.Text;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Web.SessionState;
using System.Web.UI;
using Arena.Core;
using Arena.Portal;
using Arena.Portal.UI;
using Arena.Exceptions;

namespace ArenaWeb.Custom.JohnsonFerry
{
  public partial class SiteMap : Page, IRequiresSessionState
  {
    const string SITEMAP_CHANGEFREQ = "weekly";
    const float SITEMAP_PRIORITY = 0.5F;
    const int SITEMAP_MAXURLS = 50000;
    PortalPage rootPage = null;
    int intURLs = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
      // Set root page of sitemap.
      // Currently hard-coded. Need to find a way to make this automated or user-settable.
      rootPage = new PortalPage(3353);

      this.Page.EnableViewState = false;
      this.Response.BufferOutput = true;

      this.Response.Clear();
      this.Response.ContentType = "text/xml";
      this.Response.AddHeader("Content-Type", "text/xml");
      this.Response.ContentEncoding = Encoding.UTF8;
      BuildSiteMap(this.Response.Output);
      this.Response.End();
    }

    /// <summary>
    /// Sets up sitemap XML document.
    /// </summary>
    /// <param name="textWriter"></param>
    private void BuildSiteMap(TextWriter textWriter)
    {
      XmlWriterSettings settings = new XmlWriterSettings();
      settings.Indent = true;
      settings.Encoding = Encoding.UTF8;
      settings.OmitXmlDeclaration = false;
      XmlWriter writer = XmlWriter.Create(textWriter, settings);

      // Build XML header
      writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
      writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
      writer.WriteAttributeString("xsi", "schemaLocation", null, "http://www.sitemaps.org/schemas/sitemap/0.9");

      BuildUrlRecursive(writer, rootPage);

      writer.WriteEndElement();
      writer.Close();
    }

    /// <summary>
    /// Recursively iterate through portal pages and their child pages 
    /// Iteration method: (http://msdn.microsoft.com/en-us/library/wwc698z7.aspx)
    /// </summary>
    /// <param name="writer">The XmlWriter that contains the sitemap XML</param>
    /// <param name="portalPage">The PortalPage object for the current page</param>
    private void BuildUrlRecursive(XmlWriter writer, PortalPage portalPage)
    {
      // Call the BuildURL function.
      intURLs += 1;
      string menuName = portalPage.MenuName;
      string URL = Request.Url.GetLeftPart(UriPartial.Authority) + "/" + portalPage.NavigationUrl;
      string lastMod = portalPage.DateModified.ToString("yyyy-MM-dd");
      string strTitle = portalPage.Title;
      bool boolDisplayInNav = portalPage.DisplayInNav;

      // Check that the page allows View permissions to anonymous visitors and that the page is visible in the navigation
      int iSubjectKey = 1; // All Users
      bool boolAllowAnonymousView = portalPage.Permissions.ContainsSubjectOperation(Arena.Security.SubjectType.Role, iSubjectKey, Arena.Security.OperationType.View);
      if (boolAllowAnonymousView && boolDisplayInNav)
      {
        BuildURL(writer, menuName, URL, SITEMAP_PRIORITY, lastMod, boolDisplayInNav, portalPage);
      }

      // Create each node recursively.
      foreach (PortalPage cp in portalPage.ChildPages)
      {
        BuildUrlRecursive(writer, cp);
      }
    }

    /// <summary>
    /// Writes a parent XML tag (url) for the current page
    /// </summary>
    /// <param name="writer">The XmlWriter that contains the sitemap XML</param>
    /// <param name="menuName">The page's menu name string</param>
    /// <param name="URL">The page's URL string</param>
    /// <param name="priority">The priority of this URL relative to other URLs on the site 
    /// Valid values range from 0.0 to 1.0.</param>
    /// <param name="lastMod">The date of last modification of the page 
    /// This date should be in W3C Datetime format (YYYY-MM-DD)</param>
    /// <param name="boolDisplayInNav">A flag that tells if the page is shown in the site navigation</param>
    /// <param name="portalPage">The PortalPage object for the current page</param>
    private void BuildURL(XmlWriter writer, string menuName, string URL, float priority, string lastMod, bool boolDisplayInNav, PortalPage portalPage)
    {
      writer.WriteStartElement("url");
      writer.WriteElementString("menuname", menuName);
      writer.WriteElementString("loc", URL);
      writer.WriteElementString("lastmod", lastMod);
      writer.WriteElementString("changefreq", SITEMAP_CHANGEFREQ);
      writer.WriteElementString("priority", priority.ToString("F02", CultureInfo.InvariantCulture));
      writer.WriteEndElement();
    }
  }
}