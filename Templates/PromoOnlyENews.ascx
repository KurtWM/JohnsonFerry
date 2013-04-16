<%@ Control Language="c#" Inherits="ArenaWeb.Templates.PromoOnlyENews" CodeFile="~/Custom/JohnsonFerry/Templates/PromoOnlyENews.ascx.cs" %>
<style type="text/css">
        /* Client-specific Styles */
    #outlook a
        {
            padding: 0;
        }
    body
        {
            width: 100% !important;
        }
    .NewsletterPromotionHeader
        {
            color: #000;
            font-family: Arial; 
            font-size: 14px;
            font-weight: bold;
            font-style: italic; 
            line-height: 150%; 
            text-align: left;
        } 
     .NewsletterPromotionTitle
        {
            color: #000;
            font-family: Arial; 
            font-size: 12px;
            font-weight: bold;
            font-style: italic; 
            line-height: 110%; 
            text-align: left;
            margin-top: 10px;
        }   
    .ReadMsgBody
        {
            width: 100%;
        }
    .ExternalClass
        {
            width: 100%;
        }
    body
        {
            -webkit-text-size-adjust: none;
        }
    body
        {
            margin: 0;
            padding: 0;
        }
    img
        {
            border: 0;
            height: auto;
            line-height: 100%;
            outline: none;
            text-decoration: none;
        }
    table td
        {
            border-collapse: collapse;
        }
    #backgroundTable
        {
            height: 100% !important;
            margin: 0;
            padding: 0;
            width: 100% !important;
        }
    body, #backgroundTable
        {
            background-color: #FAFAFA;
        }
    #templateContainer
        {
            border: 1px solid #DDDDDD;
        }
    h1, .h1
        {
            color: #202020;
            display: block; 
            font-family: Arial; 
            font-size: 34px; 
            font-weight: bold; 
            line-height: 100%;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0; 
            text-align: left;
        }
    h2, .h2
        {
            color: #202020;
            display: block; 
            font-family: Arial; 
            font-size: 30px; 
            font-weight: bold; 
            line-height: 100%;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0; 
            text-align: left;
        }
    h3, .h3
        {
            color: #202020;
            display: block; 
            font-family: Arial; 
            font-size: 26px; 
            font-weight: bold; 
            line-height: 100%;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0; 
            text-align: left;
        }
    h4, .h4
        {
            color: #202020;
            display: block; 
            font-family: Arial; 
            font-size: 22px; 
            font-weight: bold;
            line-height: 100%;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0; 
            text-align: left;
        }
    #templatePreheader
        {
            background-color: #FAFAFA;
        }
    .preheaderContent td
        {
            color: #505050; 
            font-family: Arial;
            font-size: 10px;
            line-height: 100%;
            text-align: left;
        }
    .preheaderContent div a:link, .preheaderContent div a:visited, .preheaderContent div a .yshortcuts 
        {
            color: #336699; 
            font-weight: normal;
            text-decoration: underline;
        }
    #templateHeader
        {
            background-color: #FFFFFF;
            border-bottom: 0;
        }
    .headerContent
        {
            color: #202020;
            font-family: Arial;
            font-size: 34px;
            font-weight: bold; 
            line-height: 100%;
            padding: 0; 
            text-align: center; 
            vertical-align: middle;
        }
    .headerContent a:link, .headerContent a:visited, .headerContent a .yshortcuts 
        {
            color: #336699; 
            font-weight: normal; 
            text-decoration: underline;
        }
    #headerImage
        {
            height: auto;
            max-width: 600px;
        }
    #templateContainer, .bodyContent
        {
            background-color: #FFFFFF;
        }
    .bodyContent div
        {
            color: #505050; 
            font-family: Arial; 
            font-size: 14px; 
            line-height: 110%; 
            text-align: left;
        }
    .bodyContent div a:link, .bodyContent div a:visited, .bodyContent div a .yshortcuts
        {
            color: #336699; 
            font-weight: normal; 
            text-decoration: underline;
        }
    .bodyContent img
        {
            display: inline;
            height: 100px;
            width: 100px;
        }
    #templateSidebar
        {
            background-color: #FFFFFF;
            border-left: 0;
        }
    .sidebarContent, .NewsletterPromotionSummary, .NewsletterPromotionLink
        {
            color: #505050; 
            font-family: Arial; 
            font-size: 12px; 
            line-height: 110%;
            text-align: left;
        }
    .sidebarContent div a:link, .sidebarContent div a:visited, .sidebarContent div a .yshortcuts 
        {
            color: #336699; 
            font-weight: normal;
            font-family: Arial; 
            font-size: 12px; 
            line-height: 110%; 
            text-decoration: underline;
        }
    .sidebarContent img
        {
            display: inline;
            height: auto;
        }

    #templateFooter
        {
            background-color: #FFFFFF; 
            border-top: 0;
        }
    .footerContent div
        {
            color: #707070; 
            font-family: Arial; 
            font-size: 12px; 
            line-height: 110%; 
            text-align: left;
        }
    .footerContent div a:link, .footerContent div a:visited, .footerContent div a .yshortcuts 
        {
            color: #336699;
            font-family: Arial; 
            font-size: 12px; 
            line-height: 110%;  
            font-weight: normal;
            text-decoration: underline;
        }
    .footerContent img
        {
            display: inline;
        }
    #social
        {
            background-color: #FAFAFA; 
            border: 0;
        }
    #social div
        {
            text-align: center;
        }
    #utility
        {
            background-color: #FFFFFF;
            border: 0;
        }
    #utility div
        {
            text-align: center;
        }
   

    </style>
<body leftmargin="0" marginwidth="0" topmargin="0" marginheight="0" offset="0">
    <center>
        <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%" id="backgroundTable">
            <tr>
                <td align="center" valign="top">
                    <!-- // Begin Template Preheader \\ -->
                    <table border="0" cellpadding="10" cellspacing="0" width="600" id="templatePreheader">
                        <tr>
                            <td valign="top" class="preheaderContent">
                                <!-- // Begin Module: Standard Preheader \ -->
                                <table border="0" cellpadding="10" cellspacing="0" width="100%">
                                    <tr>
                                        <td valign="top">
                               
                                                View Johnson Ferry Churchwide ENews on our <a href="http://jfaccess.johnsonferry.org/default.aspx?page=3427" target="_blank">website</a>. 
                                           
                                        </td>
                                        <!-- *|IFNOT:ARCHIVE_PAGE|* -->
                                        <td valign="top" width="190">
                                                                              
                                        </td>
                                        <!-- *|END:IF|* -->
                                    </tr>
                                </table>
                                <!-- // End Module: Standard Preheader \ -->
                            </td>
                        </tr>
                    </table>
                    <!-- // End Template Preheader \\ -->
                    <table border="0" cellpadding="0" cellspacing="0" width="600" id="templateContainer">
                        <tr>
                            <td align="center" valign="top">
                                <!-- // Begin Template Header \\ -->
                                <table border="0" cellpadding="0" cellspacing="0" width="600" id="templateHeader">
                                    <tr>
                                        <td class="headerContent">
                                            <!-- // Begin Module: Standard Header Image \\ -->
                                            <img src="http:/jfaccess.johnsonferry.org/publicassets/images/newsletters/ENewsHeader.png"
                                                style="max-width: 600px;" id="headerImage campaign-icon" />
                                            <!-- // End Module: Standard Header Image \\ -->
                                        </td>
                                    </tr>
                                </table>
                                <!-- // End Template Header \\ -->
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <!-- // Begin Template Body \\ -->
                                <table border="0" cellpadding="0" cellspacing="0" width="600" id="templateBody">
                                    <tr>
                                        <td valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td valign="top" class="bodyContent">
                                                        <!-- // Begin Module: Standard Content \\ -->
                                                        <table border="0" cellpadding="10" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <div id="ContentPaneDiv" class="ContentPane" runat="server">
                                                                     <asp:PlaceHolder ID="MainPH" Runat="server"></asp:PlaceHolder>&nbsp;</div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table border="0" cellpadding="10" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <div id="BottomPaneDiv" class="BottomPane" runat="server">
                                                                        <asp:PlaceHolder ID="BottomPH" Runat="server"></asp:PlaceHolder></div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <!-- // End Module: Standard Content \\ -->
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <!-- // Begin Sidebar \\  -->
                                        <td valign="top" width="200" id="templateSidebar">
                                            <table border="0" cellpadding="0" cellspacing="0" width="200">
                                                <tr>
                                                    <td valign="top" class="sidebarContent">
                                                        <!-- // Begin Module: Social Block with Icons \\ -->
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td valign="top" style="padding-top: 10px; padding-right: 10px; padding-left: 10px;">
                                                                    <table border="0" cellpadding="0" cellspacing="4">
                                                                        <tr>
                                                                            <td align="left" valign="middle" width="16">
                                                                                <img src="http://gallery.mailchimp.com/653153ae841fd11de66ad181a/images/sfs_icon_facebook.png"
                                                                                    style="margin: 0 !important;" />
                                                                            </td>
                                                                            <td align="left" valign="top">
                                                                                <div>
                                                                                    <a href="http://www.facebook.com/JohnsonFerry#!/pages/Marietta-GA/JohnsonFerry/176920017298?ref=ts">Friend on Facebook</a>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="middle" width="16">
                                                                                <img src="http://gallery.mailchimp.com/653153ae841fd11de66ad181a/images/sfs_icon_twitter.png"
                                                                                    style="margin: 0 !important;" />
                                                                            </td>
                                                                            <td align="left" valign="top">
                                                                                <div>
                                                                                    <a href="http://www.twitter.com/JohnsonFerry">Follow on Twitter</a>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <!-- // End Module: Social Block with Icons \\ -->
                                                        <!-- // Begin Module: Top Image with Content \\ -->
                                                        <table border="0" cellpadding="10" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <div id="RightPaneDiv" class="RightPane" runat="server">
                                                                        <asp:PlaceHolder ID="RightPH" Runat="server"></asp:PlaceHolder></div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                       
                                                        <!-- // End Module: Top Image with Content \\ -->
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <!-- // End Sidebar \\ -->
                                    </tr>
                                </table>
                                <!-- // End Template Body \\ -->
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <!-- // Begin Template Footer \\ -->
                                <table border="0" cellpadding="10" cellspacing="0" width="600" id="templateFooter">
                                    <tr>
                                        <td valign="top" class="footerContent">
                                            <!-- // Begin Module: Standard Footer \\ -->
                                            <table border="0" cellpadding="10" cellspacing="0" width="100%">
                                                <tr>
                                                    <td colspan="2" valign="middle" id="social">
                                                        <div>
                                                            &nbsp;<a href="http://www.twitter.com/JohnsonFerry">follow on Twitter</a> | <a href="http://www.facebook.com/JohnsonFerry#!/pages/Marietta-GA/JohnsonFerry/176920017298?ref=ts">
                                                                friend on Facebook</a>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" valign="middle" id="utility">
                                                        <div>
                                                             <asp:PlaceHolder ID="FooterPH" Runat="server"></asp:PlaceHolder>
                                                             <div>Johnson Ferry Baptist Church&nbsp;&bull;&nbsp;955 Johnson Ferry Road&nbsp;&bull;&nbsp;Marietta, GA 30068&nbsp;&bull;&nbsp;770.973.6561&nbsp;<br/>
                                                             24-Hour Prayer Line: 770.971.PRAY (7729)&nbsp;
                                                             <br/>Copyright 2011 by Johnson Ferry Baptist Church. All rights reserved.
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!-- // End Module: Standard Footer \\ -->
                                        </td>
                                    </tr>
                                </table>
                                <!-- // End Template Footer \\ -->
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
</body>