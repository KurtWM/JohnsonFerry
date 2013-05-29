namespace ArenaWeb.Custom.johnsonferry.UserControls
{
  using System;
  using System.Web;
  using System.Web.UI;
  using System.Web.UI.WebControls;
  using Arena.Core;
  using Arena.Portal;

  public partial class SetBackgroundColor : PortalControl
  {
    #region Module Settings

    [TextSetting("Page Background Color", "Optional setting to specify a background color for the page (ex: #FF0000).", false)]
    public string BgColorSetting { get { return Setting("BgColor", "", false); } }

    [CustomListSetting("Page Foreground Color", "Optional setting to specify a foreground color for the page.", true, "", new string[] { "Red", "Green", "Blue" }, new string[] { "#FF0000", "#00FF00", "#0000FF" })]
    public string FgColorListSetting { get { return Setting("FgColorList", "", false); } }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
      string strCss = "";

      if (BgColorSetting.Trim().Length > 0)
      {
        strCss = "html, body {background-color: " + BgColorSetting + " !important;}";
        BackgroundColorLabel.Text = BgColorSetting;
      }

      if (FgColorListSetting.Trim().Length > 0)
      {
        strCss = strCss + "p.goober {color: " + FgColorListSetting + " !important;}";
        ForegroundColorLabel.Text = FgColorListSetting;
      }

      CssLiteral.Text = "<style>" + strCss + "</style>";
    }
  }
}