<%@ Control Language="C#" CodeFile="SetBackgroundColor.ascx.cs" Inherits="ArenaWeb.Custom.johnsonferry.UserControls.SetBackgroundColor" %>
<%@ Register TagPrefix="Arena" Namespace="Arena.Portal.UI" Assembly="Arena.Portal.UI" %>

<asp:Literal ID="CssLiteral" runat="server" />
<p class="goober">The background color for this page is <asp:Label ID="BackgroundColorLabel" runat="server">undefined</asp:Label>.<br />
The foreground color for this page is <asp:Label ID="ForegroundColorLabel" runat="server">undefined</asp:Label>.</p>