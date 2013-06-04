namespace ArenaWeb.UserControls.Custom.BWD.CSSNavBar
{
	using System;
	using System.Xml;
	using System.Xml.Xsl;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Arena.Portal;
	using Arena.Portal.UI;
    using Arena.Security;
	using Arena.Exceptions;
	using Arena.SmallGroup;

	/// <summary>
	///		Summary description for XmlTransformation.
	/// </summary>
	public partial class PageNavigation : PortalControl
	{
		#region Module Settings

		// Module Settings
		[FileSetting("XsltUrl", "The path to the Xslt file to use (Example: '~/XSLT/PageNavigation.xslt')", true)]
		public string XSLTSetting { get { return Setting("XSLT", "", true); } }

		[PageSetting("Root Page", "The page to display the child pages of.  The default is the current page.", false)]
		public string HomePageIDSetting { get { return Setting("HomePageID", "", false); } }

        [TextSetting("CSS Class Name", "The CSS Class to use for the ouput of this module.  The default is CustCSSMenu.", false)]
        public string CSSClassNameSetting { get { return Setting("CSSClassName", "", false); } }

		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Start Building XML
			XmlDocument xdoc = null;

			string defaultGroup = string.Empty;

			int curPersonID = -1;
			if (CurrentPerson != null)
				curPersonID = CurrentPerson.PersonID;

			string parentPageID = CurrentPortalPage.PortalPageID.ToString();
			if (HomePageIDSetting != string.Empty)
				parentPageID = HomePageIDSetting.Trim();

			string menuSessionVar = "arenaPageMenu_" + parentPageID;

			if (Session[menuSessionVar] != null)
			{
				xdoc = (XmlDocument)Session[menuSessionVar];
				if (xdoc.DocumentElement.Attributes["personid"].Value != curPersonID.ToString())
					xdoc = null;
			}

			if (Request.QueryString["RefreshCache"] != null)
				xdoc = null;

			if (xdoc == null)
            {
                // Get the Home Page
                PortalPage homePage = CurrentPortal.GetPage(Int32.Parse(parentPageID));
                if (homePage == null)
                    throw new ModuleException(CurrentPortalPage, CurrentModule, string.Format("The settings for the '{0}' " +
                        "PageNavigation module does not contain a valid HomePageID. " +
                        "It must include this settings, and it must be a valid page id.  For example: " +
                        "HomePageID=10", CurrentModule.Title));

                xdoc = new XmlDocument();

                XmlNode xNavigationNode = xdoc.CreateNode(XmlNodeType.Element, "navigation", xdoc.NamespaceURI);
                xdoc.AppendChild(xNavigationNode);

                XmlAttribute xattr = xdoc.CreateAttribute("personid");
                xattr.Value = curPersonID.ToString();
                xNavigationNode.Attributes.Append(xattr);

                string cssValue = "CustCSSMenu";
                if (CSSClassNameSetting != string.Empty)
                    cssValue = CSSClassNameSetting;

                xattr = xdoc.CreateAttribute("cssclass");
                xattr.Value = cssValue;
                xNavigationNode.Attributes.Append(xattr);
                    
                    
                

                foreach (PortalPage mainPage in homePage.ChildPages)
                    if (mainPage.DisplayInNav)
                        if (mainPage.Permissions.Allowed(OperationType.View, CurrentUser))
                        {
                            XmlNode xGroupNode = xdoc.CreateNode(XmlNodeType.Element, "group", xdoc.NamespaceURI);
                            xNavigationNode.AppendChild(xGroupNode);

                            XmlAttribute xAttr = xdoc.CreateAttribute("", "name", xdoc.NamespaceURI);
                            xAttr.Value = mainPage.Name;
                            xGroupNode.Attributes.Append(xAttr);

                            xAttr = xdoc.CreateAttribute("", "pageid", xdoc.NamespaceURI);
                            xAttr.Value = mainPage.PortalPageID.ToString();
                            xGroupNode.Attributes.Append(xAttr);

                            if (CurrentPortalPage.PortalPageID == mainPage.PortalPageID)
                            {
                                xAttr = xdoc.CreateAttribute("", "currentPage", xdoc.NamespaceURI);
                                xAttr.Value = "true";
                                xGroupNode.Attributes.Append(xAttr);
                            }

                            if (mainPage.Setting("NavBarIcon", string.Empty, false) != string.Empty)
                            {
                                xAttr = xdoc.CreateAttribute("", "navbaricon", xdoc.NamespaceURI);
                                xAttr.Value = mainPage.Setting("NavBarIcon", string.Empty, false);
                                xGroupNode.Attributes.Append(xAttr);
                            }

                            if (mainPage.Setting("NavBarHoverIcon", string.Empty, false) != string.Empty)
                            {
                                xAttr = xdoc.CreateAttribute("", "navbarhovericon", xdoc.NamespaceURI);
                                xAttr.Value = mainPage.Setting("NavBarHoverIcon", string.Empty, false);
                                xGroupNode.Attributes.Append(xAttr);
                            }

                            xAttr = xdoc.CreateAttribute("", "target", xdoc.NamespaceURI);
                            xAttr.Value = mainPage.Setting("target", "_self", false);
                            xGroupNode.Attributes.Append(xAttr);

                            foreach (PortalPage subPage in mainPage.ChildPages)
                                if (subPage.DisplayInNav)
                                    if (subPage.Permissions.Allowed(OperationType.View, CurrentUser))
                                    {
                                        XmlNode xItemNode = xdoc.CreateNode(XmlNodeType.Element, "item", xdoc.NamespaceURI);
                                        xGroupNode.AppendChild(xItemNode);

                                        xAttr = xdoc.CreateAttribute("", "name", xdoc.NamespaceURI);
                                        xAttr.Value = subPage.Name;
                                        xItemNode.Attributes.Append(xAttr);

                                        xAttr = xdoc.CreateAttribute("", "pageid", xdoc.NamespaceURI);
                                        xAttr.Value = subPage.PortalPageID.ToString();
                                        xItemNode.Attributes.Append(xAttr);

                                        if (CurrentPortalPage.PortalPageID == mainPage.PortalPageID)
                                        {
                                            xAttr = xdoc.CreateAttribute("", "currentPage", xdoc.NamespaceURI);
                                            xAttr.Value = "true";
                                            xItemNode.Attributes.Append(xAttr);
                                        }

                                        if (subPage.Setting("NavBarIcon", string.Empty, false) != string.Empty)
                                        {
                                            xAttr = xdoc.CreateAttribute("", "navbaricon", xdoc.NamespaceURI);
                                            xAttr.Value = subPage.Setting("NavBarIcon", string.Empty, false);
                                            xItemNode.Attributes.Append(xAttr);
                                        }

                                        if (subPage.Setting("NavBarHoverIcon", string.Empty, false) != string.Empty)
                                        {
                                            xAttr = xdoc.CreateAttribute("", "navbarhovericon", xdoc.NamespaceURI);
                                            xAttr.Value = subPage.Setting("NavBarHoverIcon", string.Empty, false);
                                            xItemNode.Attributes.Append(xAttr);
                                        }

                                        xAttr = xdoc.CreateAttribute("", "target", xdoc.NamespaceURI);
                                        xAttr.Value = subPage.Setting("target", "_self", false);
                                        xItemNode.Attributes.Append(xAttr);

                                        foreach (PortalPage subTwoPage in subPage.ChildPages)
                                            if (subTwoPage.DisplayInNav)
                                                if (subTwoPage.Permissions.Allowed(OperationType.View, CurrentUser))
                                                {
                                                    XmlNode xSubNode = xdoc.CreateNode(XmlNodeType.Element, "subitem", xdoc.NamespaceURI);
                                                    xItemNode.AppendChild(xSubNode);

                                                    xAttr = xdoc.CreateAttribute("", "name", xdoc.NamespaceURI);
                                                    xAttr.Value = subTwoPage.Name;
                                                    xSubNode.Attributes.Append(xAttr);

                                                    xAttr = xdoc.CreateAttribute("", "pageid", xdoc.NamespaceURI);
                                                    xAttr.Value = subTwoPage.PortalPageID.ToString();
                                                    xSubNode.Attributes.Append(xAttr);

                                                    if (CurrentPortalPage.PortalPageID == mainPage.PortalPageID)
                                                    {
                                                        xAttr = xdoc.CreateAttribute("", "currentPage", xdoc.NamespaceURI);
                                                        xAttr.Value = "true";
                                                        xSubNode.Attributes.Append(xAttr);
                                                    }

                                                    if (subTwoPage.Setting("NavBarIcon", string.Empty, false) != string.Empty)
                                                    {
                                                        xAttr = xdoc.CreateAttribute("", "navbaricon", xdoc.NamespaceURI);
                                                        xAttr.Value = subTwoPage.Setting("NavBarIcon", string.Empty, false);
                                                        xSubNode.Attributes.Append(xAttr);
                                                    }

                                                    if (subTwoPage.Setting("NavBarHoverIcon", string.Empty, false) != string.Empty)
                                                    {
                                                        xAttr = xdoc.CreateAttribute("", "navbarhovericon", xdoc.NamespaceURI);
                                                        xAttr.Value = subTwoPage.Setting("NavBarHoverIcon", string.Empty, false);
                                                        xSubNode.Attributes.Append(xAttr);
                                                    }

                                                    xAttr = xdoc.CreateAttribute("", "target", xdoc.NamespaceURI);
                                                    xAttr.Value = subTwoPage.Setting("target", "_self", false);
                                                    xSubNode.Attributes.Append(xAttr);

                                                    foreach (PortalPage subThreePage in subTwoPage.ChildPages)
                                                        if (subThreePage.DisplayInNav)
                                                            if (subThreePage.Permissions.Allowed(OperationType.View, CurrentUser))
                                                            {
                                                                XmlNode xSub2Node = xdoc.CreateNode(XmlNodeType.Element, "subitem3", xdoc.NamespaceURI);
                                                                xSubNode.AppendChild(xSub2Node);

                                                                xAttr = xdoc.CreateAttribute("", "name", xdoc.NamespaceURI);
                                                                xAttr.Value = subThreePage.Name;
                                                                xSub2Node.Attributes.Append(xAttr);

                                                                xAttr = xdoc.CreateAttribute("", "pageid", xdoc.NamespaceURI);
                                                                xAttr.Value = subThreePage.PortalPageID.ToString();
                                                                xSub2Node.Attributes.Append(xAttr);

                                                                if (CurrentPortalPage.PortalPageID == mainPage.PortalPageID)
                                                                {
                                                                    xAttr = xdoc.CreateAttribute("", "currentPage", xdoc.NamespaceURI);
                                                                    xAttr.Value = "true";
                                                                    xSub2Node.Attributes.Append(xAttr);
                                                                }

                                                                if (subThreePage.Setting("NavBarIcon", string.Empty, false) != string.Empty)
                                                                {
                                                                    xAttr = xdoc.CreateAttribute("", "navbaricon", xdoc.NamespaceURI);
                                                                    xAttr.Value = subThreePage.Setting("NavBarIcon", string.Empty, false);
                                                                    xSub2Node.Attributes.Append(xAttr);
                                                                }

                                                                if (subThreePage.Setting("NavBarHoverIcon", string.Empty, false) != string.Empty)
                                                                {
                                                                    xAttr = xdoc.CreateAttribute("", "navbarhovericon", xdoc.NamespaceURI);
                                                                    xAttr.Value = subThreePage.Setting("NavBarHoverIcon", string.Empty, false);
                                                                    xSub2Node.Attributes.Append(xAttr);
                                                                }

                                                                xAttr = xdoc.CreateAttribute("", "target", xdoc.NamespaceURI);
                                                                xAttr.Value = subTwoPage.Setting("target", "_self", false);
                                                                xSubNode.Attributes.Append(xAttr);

                                                                foreach (PortalPage subFourPage in subThreePage.ChildPages)
                                                                    if (subFourPage.DisplayInNav)
                                                                        if (subFourPage.Permissions.Allowed(OperationType.View, CurrentUser))
                                                                        {
                                                                            XmlNode xSub3Node = xdoc.CreateNode(XmlNodeType.Element, "subitem4", xdoc.NamespaceURI);
                                                                            xSub2Node.AppendChild(xSub3Node);

                                                                            xAttr = xdoc.CreateAttribute("", "name", xdoc.NamespaceURI);
                                                                            xAttr.Value = subFourPage.Name;
                                                                            xSub3Node.Attributes.Append(xAttr);

                                                                            xAttr = xdoc.CreateAttribute("", "pageid", xdoc.NamespaceURI);
                                                                            xAttr.Value = subFourPage.PortalPageID.ToString();
                                                                            xSub3Node.Attributes.Append(xAttr);

                                                                            if (CurrentPortalPage.PortalPageID == mainPage.PortalPageID)
                                                                            {
                                                                                xAttr = xdoc.CreateAttribute("", "currentPage", xdoc.NamespaceURI);
                                                                                xAttr.Value = "true";
                                                                                xSub3Node.Attributes.Append(xAttr);
                                                                            }

                                                                            if (subFourPage.Setting("NavBarIcon", string.Empty, false) != string.Empty)
                                                                            {
                                                                                xAttr = xdoc.CreateAttribute("", "navbaricon", xdoc.NamespaceURI);
                                                                                xAttr.Value = subFourPage.Setting("NavBarIcon", string.Empty, false);
                                                                                xSub3Node.Attributes.Append(xAttr);
                                                                            }

                                                                            if (subFourPage.Setting("NavBarHoverIcon", string.Empty, false) != string.Empty)
                                                                            {
                                                                                xAttr = xdoc.CreateAttribute("", "navbarhovericon", xdoc.NamespaceURI);
                                                                                xAttr.Value = subFourPage.Setting("NavBarHoverIcon", string.Empty, false);
                                                                                xSub3Node.Attributes.Append(xAttr);
                                                                            }

                                                                            xAttr = xdoc.CreateAttribute("", "target", xdoc.NamespaceURI);
                                                                            xAttr.Value = subTwoPage.Setting("target", "_self", false);
                                                                            xSub3Node.Attributes.Append(xAttr);

                                                                        }
                                                            }
                                                }
                                    }
                        }

                Session[menuSessionVar] = xdoc;
            }

            

			xmlMain.Document = xdoc;
			xmlMain.XslFileURL = XSLTSetting;
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
