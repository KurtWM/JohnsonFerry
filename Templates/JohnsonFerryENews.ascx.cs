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
    public partial class JohnsonFerryENews : PortalControl
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

        public System.Web.UI.WebControls.PlaceHolder Main { get { return MainPH; } }
        public System.Web.UI.WebControls.PlaceHolder Right { get { return RightPH; } }
        public System.Web.UI.WebControls.PlaceHolder Bottom { get { return BottomPH; } }
        public System.Web.UI.WebControls.PlaceHolder Footer { get { return FooterPH; } }

        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            
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
