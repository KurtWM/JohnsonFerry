﻿namespace ArenaWeb.Templates
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
    public partial class H5BP_BS : PortalControl
    {
        #region Template Settings
        #endregion

        #region Content Areas
        public System.Web.UI.WebControls.PlaceHolder Main { get { return MainPH; } }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
        }

        protected override void Render(HtmlTextWriter writer)
        {
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
