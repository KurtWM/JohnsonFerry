namespace ArenaWeb.Custom.JohnsonFerry.UserControls
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Web;
  using System.Web.Profile;
  using System.Web.UI;
  using System.Web.UI.WebControls;
  using ASP;
  using Arena.Portal;
  using Arena.Core;
  using Arena.Marketing;
  using Arena.Utility;

  public partial class ChurchonlineCountdown : PortalControl
  {

    [TextSetting("Next Event URL", "The URL that returns a JSON string containing the countdown timer data.", true)]
    public string NextEventUrlSetting
    {
      get
      {
        return this.Setting("NextEventUrl", "", true);
      }
    }

    [TextSetting("ID for Countdown Timer", "A text string (letters only; no spaces) that will be used as the ID attribute for the countdown timer. If multiple countdown timers are being used on a single page, each should have a different ID.", true)]
    public string CountdownIdSetting
    {
      get
      {
        return this.Setting("CountdownId", "", true);
      }
    }

    [TextSetting("Container for Countdown Timer", "A jQuery selector string for the DOM element where the countdown timer will be placed. If no container is specified, or if the container does not exist, the countdown timer will be placed in the body of the page. Use only double-quotes in the selector. (i.e.: '#cycle-div a[title=\"Promo 1\"]'", true)]
    public string CountdownContainerSetting
    {
      get
      {
        return this.Setting("CountdownContainer", "", true);
      }
    }

    [TextSetting("Link URL", "The URL to link to when the countdown timer is clicked.", false)]
    public string LinkUrlSetting
    {
      get
      {
        return this.Setting("LinkUrl", "", false);
      }
    }

    [CssSetting("Css File", "An optional CSS File to use for this module. Default: /Custom/JohnsonFerry/CSS/ChurchonlineCountdown.css", false)]
    public string CssFileSetting
    {
      get
      {
        return this.Setting("CssFile", "/Custom/JohnsonFerry/CSS/ChurchonlineCountdown.css", false);
      }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
      RegisterScripts();
      this.DataBind();
    }

    private void RegisterScripts()
    {
      BasePage.AddJavascriptInclude(Page, ResolveUrl("/Custom/JohnsonFerry/Scripts/ChurchonlineCountdown.js"));
      BasePage.AddCssLink(Page, ResolveUrl(CssFileSetting));
    }

  }
}