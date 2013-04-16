namespace ArenaWeb.Custom.JohnsonFerry.UserControls
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using System.Web.UI;
  using System.Web.UI.WebControls;
  using System.Globalization;
  using System.Xml;
  using System.Text;
  using System.IO;

  using Arena.Core;
  using Arena.Exceptions;
  using Arena.Portal;
  using Arena.Security;

  /// <summary>
  ///	Summary description for UserLogin.
  /// </summary>
  public partial class Sitemap : PortalControl
  {
    const string SITEMAP_CHANGEFREQ = "weekly";
    const float SITEMAP_PRIORITY = 0.5F;
    const int SITEMAP_MAXURLS = 50000;
    PortalPage rootPage = null;
    int intURLs = 0;
    //string strPermission = null;

    protected void Page_Load(object sender, EventArgs e)
    {
      // Set root page of sitemap.
      if (Session["RootPage"] != null)
      {
        rootPage = (PortalPage)Session["RootPage"];
      }
      else
      {
        rootPage = new PortalPage(3353);
        Session["RootPage"] = rootPage;
      }

      // Clear headers to ensure none
      // are sent to the requesting browser
      // and set the content type.
      Response.ClearHeaders();
      Response.Clear();
      Response.ClearContent();
      Response.ContentType = "text/xml";
      Response.ContentEncoding = Encoding.UTF8;
      BuildSiteMap(Response.Output);
    }

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

    // Recursively iterate through all elements and children (http://msdn.microsoft.com/en-us/library/wwc698z7.aspx)
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

      // Print each node recursively.
      foreach (PortalPage cp in portalPage.ChildPages)
      {
        BuildUrlRecursive(writer, cp);
      }
    }

    private void BuildURL(XmlWriter writer, string menuName, string URL, float priority, string lastMod, bool boolDisplayInNav, PortalPage portalPage)
    {
      writer.WriteStartElement("url");
      writer.WriteElementString("menuname", menuName);
      writer.WriteElementString("loc", URL);
      writer.WriteElementString("lastmod", lastMod);
      writer.WriteElementString("changefreq", SITEMAP_CHANGEFREQ);
      writer.WriteElementString("priority", priority.ToString("F02", CultureInfo.InvariantCulture));
      //writer.WriteElementString("navdisplay", boolDisplayInNav.ToString());
      //writer.WriteElementString("permissions", GetPagePermissions(portalPage));
      //writer.WriteElementString("allowanonymous", portalPage.Permissions.ContainsSubjectOperation(Arena.Security.SubjectType.Role, 1, Arena.Security.OperationType.View).ToString());

      writer.WriteEndElement();
    }
  }
}