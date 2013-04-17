<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventRegistrationList.ascx.cs" Inherits="ArenaWeb.Custom.JohnsonFerry.UserControls.EventRegistrationList" %>
<%@ Register TagPrefix="Arena" Namespace="Arena.Portal.UI" Assembly="Arena.Portal.UI" %>

<!-- Control: RegistrationList -->

<asp:Literal ID="cssLiteral" runat="server"></asp:Literal>

<h1><asp:Literal ID="DefaultHeader" runat="server" Text="Johnson Ferry Event Registration"></asp:Literal></h1>
<asp:Label ID="ErrorMsg" runat="server"></asp:Label>
<asp:Label ID="SimpleMsg" runat="server"></asp:Label>
<asp:Repeater ID="RegistrationRepeater" runat="server" OnItemDataBound="RegistrationRepeater_ItemDataBound">
    <HeaderTemplate>
        <table id="RegistrationListTable" class="tablesorter">
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
                    OnClientClick='<%# "$.blockUI({ message: $(\"#eventdetail" + DataBinder.Eval(Container.DataItem, "promotion_request_id") + "\"), css: { width: \"275px\"} }); return false;" %>' 
                    ImageUrl="/images/information2.gif" />
                <div id='eventdetail<%# DataBinder.Eval(Container.DataItem, "promotion_request_id") %>' style="display: none; cursor: default;">
                    <div style="background-color: #D6DEDE; border: none; text-align: left; padding-top: 3px;">
                    <p style="margin-left: 3px; position: relative; float: left; font-weight: bold;">Event Information</p>
                    <div class="closeBlockUI" style="background: Transparent url(/Custom/JohnsonFerry/Images/ui-icons_217bc0_256x240.png) no-repeat -100px -132px; width: 14px; height: 14px; margin: 4px 3px; position: relative; float: right;"></div>
                    <div style="clear: both;"></div></div>
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
                [ministry]</td> 
            <td>
                <strong><a href="default.aspx?page=<%=CurrentPortalPage.PortalPageID.ToString()%>&promotionId=<%# DataBinder.Eval(Container.DataItem, "promotion_request_id") %>"><%# DataBinder.Eval(Container.DataItem, "title") %> </a></strong></td>
            <td style="text-align: right;">
                <asp:Label id="StartDateLabel" Text='[date]' Runat="server"/></td> 
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </tbody>
        </table>
    </FooterTemplate>
</asp:Repeater>

<script src="/Custom/JohnsonFerry/Scripts/jquery.blockUI.js" type="text/javascript"></script>

<script type="text/javascript">
  $(document).ready(function () {
    $.tablesorter.defaults.sortList = [[2, 0]];
    $.tablesorter.defaults.widgets = ['zebra'];
    $("#RegistrationListTable").each(function (index) {
      $(this).tablesorter({ headers: { 0: { sorter: false}} });
    });
    $('.closeBlockUI').click(function () {
      $.unblockUI();
    });
  }); 
</script>




