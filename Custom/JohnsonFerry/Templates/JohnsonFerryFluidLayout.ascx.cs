namespace ArenaWeb.Custom.JohnsonFerry.Templates
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using System.Web.UI;
  using System.Web.UI.WebControls;
  using Arena.Portal;

  public partial class JohnsonFerryFluidLayout : PortalControl
  {
    #region Content Areas

    public System.Web.UI.WebControls.PlaceHolder Header { get { return HeaderCell; } }
    public System.Web.UI.WebControls.PlaceHolder Nav { get { return NavCell; } }
    public System.Web.UI.WebControls.PlaceHolder SubNav { get { return SubNavCell; } }
    public System.Web.UI.WebControls.PlaceHolder Main { get { return MainCell; } }
    public System.Web.UI.WebControls.PlaceHolder Footer { get { return FooterCell; } }
    public System.Web.UI.WebControls.PlaceHolder UserInfo { get { return UserInfoCell; } }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
      {
        BasePage.AddCssLink(Page, ResolveUrl("/custom/johnsonferry/css/bootstrap.css"));
        // Add styles here (i.e.: fixedToTop.css) that must cascade AFTER the standard bootstrap but BEFORE the  
        // responsive stylesheet, as noted on the Twitter Bootstrap site at http://twitter.github.io/bootstrap/components.html
        BasePage.AddCssLink(Page, ResolveUrl("/custom/johnsonferry/css/fixedToTop.css"));
        BasePage.AddCssLink(Page, ResolveUrl("/custom/johnsonferry/css/responsive.css"));
        BasePage.AddCssLink(Page, ResolveUrl("/custom/johnsonferry/css/main.css"));
        BasePage.AddJavascriptInclude(Page, ResolveUrl("/custom/johnsonferry/scripts/vendor/bootstrap.js"));
        BasePage.AddJavascriptInclude(Page, ResolveUrl("/custom/johnsonferry/scripts/vendor/modernizr-2.6.2-respond-1.1.0.min.js"));
        BasePage.AddJavascriptInclude(Page, ResolveUrl("/custom/johnsonferry/scripts/plugins.js"));
        BasePage.AddJavascriptInclude(Page, ResolveUrl("/custom/johnsonferry/scripts/main.js"));

        //System.Web.UI.HtmlControls.HtmlGenericControl main_js = new System.Web.UI.HtmlControls.HtmlGenericControl();
        //main_js.TagName = "script";
        //main_js.Attributes.Add("type", @"text/javascript");
        //main_js.Attributes.Add("src", ResolveUrl("/custom/johnsonferry/scripts/main.js"));
        //this.Page.Header.Controls.Add(main_js);

      

      }
  }
}