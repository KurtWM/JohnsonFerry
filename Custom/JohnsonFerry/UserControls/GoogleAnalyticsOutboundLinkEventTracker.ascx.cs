namespace ArenaWeb.Custom.JohnsonFerry.UserControls
{
  using Arena;
  using Arena.Core;
  using Arena.Portal;
  using Arena.Portal.UI;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using System.Web.UI;
  using System.Web.UI.HtmlControls;
  using System.Web.UI.WebControls;
  using System.Text;


  public partial class GoogleAnalyticsOutboundLinkEventTracker : PortalControl
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (ArenaContext.Current.GetSetting("GoogleAnalyticsAccount") != null && ArenaContext.Current.GetSetting("GoogleAnalyticsAccount").Trim() != string.Empty)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("\n<script type=\"text/javascript\">\n");
        stringBuilder.Append("\t$(document).ready(function() {\n");
        stringBuilder.Append("\tif (window._gat && window._gat._getTracker) {\n");
        stringBuilder.Append("\t\t// Add GA outbound link tracking event to all outbound anchor tags.\n");
        stringBuilder.Append("\t\tvar a = document.getElementsByTagName('a');\n");
        stringBuilder.Append("\t\tfor (i = 0; i < a.length; i++) {\n");
        stringBuilder.Append("\t\t\tif (a[i].href.indexOf(location.host) === -1 && /^https?:\\/\\//i.test(a[i].href)) {\n");
        stringBuilder.Append("\t\t\t\t//console.log((a[i].innerText.length > 0 ? a[i].innerText : \"\") + (a[i].title.length > 0 ? a[i].title : \"\") + \" (\" + a[i].href.replace(/^https?:\\/\\//i, '') + \")\");\n");
        stringBuilder.Append("\t\t\t\ta[i].onclick = function () {\n");
        stringBuilder.Append("\t\t\t\t\t_gaq.push(['_trackEvent', 'OutboundLinks', 'Click', (this.innerText.length > 0 ? this.innerText : \"\") + (this.title.length > 0 ? this.title : \"\") + \" (\" + this.href.replace(/^https?:\\/\\//i, '') + \")\"]);\n");
        stringBuilder.Append("\t\t\t\t}\n");
        stringBuilder.Append("\t\t\t}\n");
        stringBuilder.Append("\t\t}\n");
        stringBuilder.Append("\t}\n");
        stringBuilder.Append("\t});\n");
        stringBuilder.Append("</script>\n");

        phGoogleEventTracker.Controls.Add((Control)new LiteralControl(((object)stringBuilder).ToString()));
        phGoogleEventTracker.Visible = true;
      }
    }
  }
}