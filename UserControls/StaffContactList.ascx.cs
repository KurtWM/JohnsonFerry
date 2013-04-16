using System;
using System.IO;
using System.Text;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using Arena.Portal;
using Arena.Portal.UI;
using Arena.Security;
using Arena.Exceptions;
using Arena.Core;

namespace ArenaWeb.UserControls.custom.johnsonferry
{

    public partial class StaffContactList : PortalControl
    {
        [CssSetting("Css File Name", "This is a css file name", false)]
        public string CssFileSetting { get { return Setting("CssFile", "", false); } }

        [PageSetting("Detail Page", "The page to use for displaying the contact's details", true)]
        public string PageIDSetting { get { return Setting("PageID", "", false); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            BasePage.AddJavascriptInclude(Page, "/Custom/JohnsonFerry/Scripts/jquery.blockUI.js");
            BasePage.AddJavascriptInclude(Page, "/Custom/JohnsonFerry/Scripts/jquery.metadata.js"); //Required for setting inline options in tablesorter 
            BasePage.AddJavascriptInclude(Page, "/Custom/JohnsonFerry/Scripts/jquery.tablesorter.min.js");
            BasePage.AddCssLink(Page, "/Custom/JohnsonFerry/CSS/tablesorter.css");
        }

        protected void StaffContactRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Execute the following logic for Items and Alternating Items.
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                string sPhone = Convert.ToString(dr["business_phone"]).Trim();
                string sEmail = Convert.ToString(dr["email"]).Trim();
                if ((sPhone == "") || (sEmail == ""))
                {
                    ((ImageButton)e.Item.FindControl("AboutBtn")).Visible = false;
                    ((HtmlTableRow)e.Item.FindControl("TRow")).Visible = false;
                }
            }
        }
    }
}