<%@ Page Trace="false" Language="c#" ValidateRequest="false" Inherits="ArenaWeb.CDefault, Arena" %>

<!DOCTYPE html>
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9"> <![endif]-->
<!--[if gt IE 8]><!-->
<html class="no-js">
<!--<![endif]-->
<head runat="server">
  <!-- Begin Boilerplate meta tags here -->
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
  <meta name="description" content="">
  <meta name="viewport" content="width=device-width">
  <!-- End Boilerplate meta tags here -->
  <!-- Begin Arena title tag here -->
  <title>
    <%=PortalTitle%></title>
  <!-- End Arena title tag here -->
  <!-- Place favicon.ico and apple-touch-icon.png in the root directory -->
  <!-- Begin Arena header script placeholder here -->
  <!-- These stylesheets and scripts will be added to the end of the document HEAD -->
  <asp:PlaceHolder ID="phHeaderScript" runat="server" Visible="false" />
  <!-- End Arena header script placeholder here -->
</head>
<body>
  <!-- Begin Boilerplate outdated browser test here -->
  <!--[if lt IE 7]>
            <p class="chromeframe">You are using an <strong>outdated</strong> browser. Please <a href="http://browsehappy.com/">upgrade your browser</a> or <a href="http://www.google.com/chromeframe/?redirect=true">activate Google Chrome Frame</a> to improve your experience.</p>
        <![endif]-->
  <!-- End Boilerplate outdated browser test here -->
  <!-- Begin Arena site or application content here -->
  <div id="fieldHint" class="fieldHintBox">
  </div>
  <form runat="server" id="frmMain" enctype="multipart/form-data">
  <asp:ScriptManager ID="smScripts" runat="server">
    <Scripts>
      <asp:ScriptReference Path="~/Include/scripts/webkit.js" />
    </Scripts>
    <Services>
      <asp:ServiceReference Path="~/WebServices/ClickToCall.asmx" />
    </Services>
  </asp:ScriptManager>
  <div id="popOver" style="position: absolute; visibility: hidden; z-index: 200000;">
  </div>
  <asp:ValidationSummary ID="valSummary" runat="server" CssClass="errorText" DisplayMode="BulletList"
    ShowMessageBox="True" ShowSummary="False" HeaderText="Please correct the following:">
  </asp:ValidationSummary>
  <input type="hidden" id="ihPersonListID" name="ihPersonListID" />
  <input type="hidden" id="ihRefreshButtonID" name="ihRefreshButtonID" />
  <asp:PlaceHolder ID="phTemplate" runat="server" />
  <asp:PlaceHolder ID="phDynamic" runat="server" Visible="False" />
  <asp:UpdateProgress ID="upProgress" runat="server" DynamicLayout="false" DisplayAfter="30">
    <ProgressTemplate>
    </ProgressTemplate>
  </asp:UpdateProgress>
  <ajax:AlwaysVisibleControlExtender ID="avceProgress" runat="server" TargetControlID="upProgress"
    HorizontalSide="Right" VerticalSide="Top" HorizontalOffset="0" VerticalOffset="0">
  </ajax:AlwaysVisibleControlExtender>
  </form>
  <!-- End Arena site or application content here -->
  <!-- Begin Boilerplate JavaScripts here -->
  <script type="text/javascript">    window.jQuery || document.write('<script src="custom/johnsonferry/scripts/vendor/jquery-1.9.0.min.js"><\/script>')</script>
  <!-- End Boilerplate JavaScripts here -->
  <!-- Begin Arena site Google Analytics content here -->
  <asp:PlaceHolder ID="phGoogleAnalytics" runat="server" Visible="false" />
  <!-- End Arena site Google Analytics content here -->
  <!-- Begin Boilerplate Google Analytics content here -->
  <!-- HTML5 Boilerplate: Google Analytics: change UA-XXXXX-X to be your site's ID. -->
  <!--
        <script>
            var _gaq=[['_setAccount','UA-XXXXX-X'],['_trackPageview']];
            (function(d,t){var g=d.createElement(t),s=d.getElementsByTagName(t)[0];
            g.src=('https:'==location.protocol?'//ssl':'//www')+'.google-analytics.com/ga.js';
            s.parentNode.insertBefore(g,s)}(document,'script'));
        </script>
		-->
  <!-- End Boilerplate Google Analytics content here -->
</body>
</html>
