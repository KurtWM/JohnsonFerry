<%@ page trace="false" language="c#" validaterequest="false" inherits="ArenaWeb.CDefault, Arena" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=PortalTitle%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <asp:PlaceHolder ID="phHeaderScript" runat="server" Visible="false" />
</head>
<body>
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
    <asp:PlaceHolder ID="phGoogleAnalytics" runat="server" Visible="false" />
</body>
</html>
