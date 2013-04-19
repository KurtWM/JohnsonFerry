﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventRegistrationList.ascx.cs" Inherits="ArenaWeb.Custom.JohnsonFerry.UserControls.EventRegistrationList" %>
<%@ Register TagPrefix="Arena" Namespace="Arena.Portal.UI" Assembly="Arena.Portal.UI" %>

<!-- Control: RegistrationList -->

<asp:Literal ID="cssLiteral" runat="server"></asp:Literal>

<h1><asp:Literal ID="DefaultHeader" runat="server" Text="Johnson Ferry Event Registration"></asp:Literal></h1>
<asp:Label ID="ErrorMsg" runat="server"></asp:Label>
<asp:Label ID="SimpleMsg" runat="server"></asp:Label>
<asp:Repeater ID="RegistrationRepeater" runat="server" OnItemDataBound="RegistrationRepeater_ItemDataBound">
    <HeaderTemplate>
        <table id="RegistrationListTable" class="tablesorter stripeMe">
        <thead>
        <tr>
            <th style="width: 36px;">Details</td>
            <th class="TopicColumn_<%# this.UniqueID.Replace("$", String.Empty) %>">Ministry</td>
            <th>Event</td>
            <th style="width: 75px;">Start Date</td>
        </tr>
        </thead>
        <tbody>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td valign="top" align="center">
                <asp:ImageButton ID="AboutBtn" runat="server" 
                    CausesValidation="false" 
                    OnClientClick='<%# "openDialog(\"#eventdetail" + DataBinder.Eval(Container.DataItem, "promotion_request_id") + "\"); return false;" %>' 
                    ImageUrl="/images/information2.gif" />
                <div id='eventdetail<%# DataBinder.Eval(Container.DataItem, "promotion_request_id") %>' style="display: none; cursor: default;">
                    <div style="background-color: White;">
                        <div style="text-align: left; margin: 9px;">
                            <h3><%# DataBinder.Eval(Container.DataItem, "title") %></h3>
                            <p><%# DataBinder.Eval(Container.DataItem, "web_summary") %></p>
                            <p><strong><a href="default.aspx?page=<%=CurrentPortalPage.PortalPageID.ToString()%>&promotionId=<%# DataBinder.Eval(Container.DataItem, "promotion_request_id") %>">Learn More</a></strong></p>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                </div>
            </td>
            <td class="TopicColumn_<%# this.UniqueID.Replace("$", String.Empty) %>">
                <asp:Label id="TopicAreaLabel" Runat="server"/></td> 
            <td>
                <strong><a href="default.aspx?page=<%=CurrentPortalPage.PortalPageID.ToString()%>&promotionId=<%# DataBinder.Eval(Container.DataItem, "promotion_request_id") %>"><%# DataBinder.Eval(Container.DataItem, "title") %> </a></strong></td>
            <td style="text-align: right;">
                <asp:Label id="StartDateLabel" Runat="server"/></td> 
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </tbody>
        </table>
    </FooterTemplate>
</asp:Repeater>





