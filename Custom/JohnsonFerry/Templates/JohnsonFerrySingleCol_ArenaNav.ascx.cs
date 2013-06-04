namespace ArenaWeb.Templates
{
    using System;
    using System.Text;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI;
    using System.IO;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Configuration;
    using Arena.Portal;

    /// <summary>
    ///		Summary description for ArenaMain.
    /// </summary>
    public partial class JohnsonFerrySingleCol_ArenaNav : PortalControl
    {
        #region Template Settings

        // Template Settings
        //[ModuleAttribute("Show Heading", "Optional Flag indicating if the Page Heading should be displayed on this page (True*/False).", false)]
        //public string ShowHeadingSetting { get { return CurrentPortalPage.Setting("ShowHeading", "true", false); } }

        //[ModuleAttribute("Show Bread Crumbs", "Optional Flag indicating if the Bread Crumb Trail should be displayed on this page (True*/False).", false)]
        //public string ShowBreadCrumbsSetting { get { return CurrentPortalPage.Setting("ShowBreadCrumbs", "true", false); } }

        //[ModuleAttribute("Email Subject", "Optional text of the email when a user click on the Email This Page link. If nothing, the email link will not display.", false)]
        //public string emailSubjectSetting { get { return CurrentPortalPage.Setting("emailSubject", "", false); } }

        #endregion

        #region Content Areas

        public System.Web.UI.WebControls.PlaceHolder UserInfo { get { return UserInfoPH; } }
        public System.Web.UI.WebControls.PlaceHolder LoginLogout { get { return LoginLogoutPH; } }
        public System.Web.UI.WebControls.PlaceHolder Nav { get { return NavPH; } }
        public System.Web.UI.WebControls.PlaceHolder Top { get { return TopPH; } }
        public System.Web.UI.WebControls.PlaceHolder Main { get { return MainPH; } }
        public System.Web.UI.WebControls.PlaceHolder Bottom { get { return BottomPH; } }
        public System.Web.UI.WebControls.PlaceHolder Footer { get { return FooterPH; } }

        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //Add jQuery UI to page head element.
            System.Web.UI.HtmlControls.HtmlGenericControl jquery_ui_js = new System.Web.UI.HtmlControls.HtmlGenericControl();
            jquery_ui_js.TagName = "script";
            jquery_ui_js.Attributes.Add("type", @"text/javascript");
            jquery_ui_js.Attributes.Add("src", "https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.24/jquery-ui.min.js");
            this.Page.Header.Controls.Add(jquery_ui_js);
            //Add jQuery Tools to page head element.
            System.Web.UI.HtmlControls.HtmlGenericControl jquery_tools_js = new System.Web.UI.HtmlControls.HtmlGenericControl();
            jquery_tools_js.TagName = "script";
            jquery_tools_js.Attributes.Add("type", @"text/javascript");
            jquery_tools_js.Attributes.Add("src", "/Custom/JohnsonFerry/Scripts/jquery.tools.min.js");
            this.Page.Header.Controls.Add(jquery_tools_js);
            //Add apycom_menu script to page head element.
            System.Web.UI.HtmlControls.HtmlGenericControl apycom_menu_js = new System.Web.UI.HtmlControls.HtmlGenericControl();
            apycom_menu_js.TagName = "script";
            apycom_menu_js.Attributes.Add("type", @"text/javascript");
            apycom_menu_js.Attributes.Add("src", "/Custom/JohnsonFerry/Scripts/apycom_menu.js");
            this.Page.Header.Controls.Add(apycom_menu_js);
            //Add jquery.tablesorter script to page head element.
            System.Web.UI.HtmlControls.HtmlGenericControl tablesorter_js = new System.Web.UI.HtmlControls.HtmlGenericControl();
            tablesorter_js.TagName = "script";
            tablesorter_js.Attributes.Add("type", @"text/javascript");
            tablesorter_js.Attributes.Add("src", "/Custom/JohnsonFerry/Scripts/jquery.tablesorter.min.js");
            this.Page.Header.Controls.Add(tablesorter_js);
            //Add jfbc jQuery to page head element.
            System.Web.UI.HtmlControls.HtmlGenericControl jfbc_jquery = new System.Web.UI.HtmlControls.HtmlGenericControl();
            jfbc_jquery.TagName = "script";
            jfbc_jquery.Attributes.Add("type", @"text/javascript");
            jfbc_jquery.Attributes.Add("src", "/Custom/JohnsonFerry/Scripts/jf_jquery.js");
            this.Page.Header.Controls.Add(jfbc_jquery);            
            //Add tablesorter css to page head element.
            System.Web.UI.HtmlControls.HtmlGenericControl tablesorter_css = new System.Web.UI.HtmlControls.HtmlGenericControl();
            tablesorter_css.TagName = "link";
            tablesorter_css.Attributes.Add("rel", @"stylesheet");
            tablesorter_css.Attributes.Add("type", @"text/css");
            tablesorter_css.Attributes.Add("href", "/Custom/JohnsonFerry/CSS/tablesorter.css");
            this.Page.Header.Controls.Add(tablesorter_css);
            //Add jQuery UI css theme to page head element.
            System.Web.UI.HtmlControls.HtmlGenericControl jquerytheme_css = new System.Web.UI.HtmlControls.HtmlGenericControl();
            jquerytheme_css.TagName = "link";
            jquerytheme_css.Attributes.Add("rel", @"stylesheet");
            jquerytheme_css.Attributes.Add("type", @"text/css");
            jquerytheme_css.Attributes.Add("href", "/Include/scripts/jqueryui/css/custom-theme/jquery-ui-.custom.css");
            this.Page.Header.Controls.Add(jquerytheme_css);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            //tblTitle.Visible = (ShowHeadingSetting.ToLower() != "false");
            //trTitle.Visible = tblTitle.Visible;
            //lblPageTitle.Text = this.Title;

            //phBreadCrumbs.Visible = (ShowBreadCrumbsSetting.ToLower() != "false");

            base.Render(writer);
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
