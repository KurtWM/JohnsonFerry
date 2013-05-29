<%@ Control Language="c#" Inherits="ArenaWeb.Templates.JohnsonFerryMain_ArenaNav" CodeFile="~/Custom/JohnsonFerry/Templates/JohnsonFerryMain_ArenaNav.ascx.cs" %>

<div class="container_16 pageContent">
        <div id="JFLogo" class="grid_4">
            <a href="http://www.johnsonferry.org"><img src="/Custom/Johnsonferry/Images/Johnson-Ferry-logo-top.png" alt="Johnson Ferry Logo" height="110" width="220" /></a></div>
        <div class="grid_3 prefix_4 siteNav">
		    &nbsp;</div>
        <div class="grid_1 siteNav">
            &nbsp;</div>
        <div class="grid_1 siteNav">
            &nbsp;</div>
        <div class="grid_1 siteNav">
            <asp:PlaceHolder ID="UserInfoPH" Runat="server"></asp:PlaceHolder></div>
        <div class="grid_1 siteNav">
            &nbsp;</div>
        <div class="grid_1 siteNav">
            <asp:PlaceHolder ID="LoginLogoutPH" Runat="server"></asp:PlaceHolder></div>
    <div class="clear"></div>
    <div class="container_16 pageHeader">
	    <div class="grid_16 topNavBar testclass">
		    <asp:PlaceHolder ID="NavPH" Runat="server" Visible="true"></asp:PlaceHolder>
	    </div>
    </div>
    <div class="container_16">
        <div id="TopPaneDiv" class="grid_16 TopPane" runat="server">
            <asp:PlaceHolder ID="TopPH" Runat="server"></asp:PlaceHolder></div>
    </div>
    <div class="container_16 MainContentArea MainContentAreaBackground">
        <div class="grid_16">&nbsp;</div>
        <div class="grid_13">
            <div id="ContentPaneDiv" class="ContentPane" runat="server">
                <asp:PlaceHolder ID="MainPH" Runat="server"></asp:PlaceHolder>&nbsp;</div>
        </div>
        <div class="grid_3">
            <div><!--[ADDTHIS]--></div>
            <div id="RightPaneDiv" class="RightPane" runat="server">
                <asp:PlaceHolder ID="RightPH" Runat="server"></asp:PlaceHolder></div>
        </div>
        <div class="container_16">
            <div id="BottomPaneDiv" class="grid_16 BottomPane" runat="server">
                <asp:PlaceHolder ID="BottomPH" Runat="server"></asp:PlaceHolder></div>
        </div>
        <div class="clear"></div>
    </div>
</div>

<div class="bottom_menu_container container_16">
</div>

<div id="footerText" class="container_16 footer-area">
    <asp:PlaceHolder ID="FooterPH" Runat="server"></asp:PlaceHolder>
    <div style="background: url(/Custom/Johnsonferry/Images/jf_logomark_whitecenter_60x50px.png) no-repeat 50%; height: 50px;">&nbsp;</div>
    <span class="footer-contact-info">Johnson Ferry Baptist Church&nbsp;&bull;&nbsp;955 Johnson Ferry Road&nbsp;&bull;&nbsp;Marietta, GA 30068&nbsp;&bull;&nbsp;770.973.6561&nbsp;&bull;&nbsp;24-Hour Prayer Line: 770.971.PRAY (7729)
    <span style="position: relative; top: 4px;">
        <a href="http://www.facebook.com/JohnsonFerry#!/pages/Marietta-GA/JohnsonFerry/176920017298?ref=ts" title="Follow Us on Facebook" target="_blank">
            <img src="/Custom/Johnsonferry/Images/facebook_16px.gif" height="16" width="16" style="margin-left: 14px;" /></a>
        <a href="http://www.twitter.com/JohnsonFerry" title="Follow Us on Twitter" target="_blank">
            <img src="/Custom/Johnsonferry/Images/twitter_16px.gif" height="16" width="16" style="margin-left: 6px;" /></a></span>
    <br />Copyright 2011 by Johnson Ferry Baptist Church. All rights reserved.</span>
</div>