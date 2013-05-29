<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RegistrationList.ascx.cs" Inherits="ArenaWeb.Custom.JohnsonFerry.UserControls.RegistrationList" %>
<%@ Register TagPrefix="Arena" Namespace="Arena.Portal.UI" Assembly="Arena.Portal.UI" %>

<!-- Control: RegistrationList -->

<asp:Literal ID="cssLiteral" runat="server"></asp:Literal>

<h1><asp:Literal ID="DefaultHeader" runat="server" Text="Johnson Ferry Event Registration"></asp:Literal></h1>
<!--<asp:Repeater ID="TopicRepeater" runat="server">
    <ItemTemplate>
        <h2><%# Eval("lookup_value")%> Event Registration</h2>
    </ItemTemplate>
</asp:Repeater>
-->

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
                    OnClientClick='<%# "$.blockUI({ message: $(\"#eventdetail" + DataBinder.Eval(Container.DataItem, "profile_id") + "\"), css: { width: \"275px\"} }); return false;" %>' 
                    ImageUrl="/images/information2.gif" />
                <div id='eventdetail<%# DataBinder.Eval(Container.DataItem, "profile_id") %>' style="display: none; cursor: default;">
                    <div style="background-color: #D6DEDE; border: none; text-align: left; padding-top: 3px;">
                    <p style="margin-left: 3px; position: relative; float: left; font-weight: bold;">Event Information</p>
                    <div class="closeBlockUI" style="background: Transparent url(/Custom/JohnsonFerry/Images/ui-icons_217bc0_256x240.png) no-repeat -100px -132px; width: 14px; height: 14px; margin: 4px 3px; position: relative; float: right;"></div>
                    <div style="clear: both;"></div></div>
                    <div style="background-color: White;">
                        <div style="text-align: left; margin: 9px;">
                            <h3><%# Eval("profile_name")%></h3>
                            <p><%# Eval("profile_desc")%></p>
                            <p><%# Eval("details")%></p>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                </div>
            </td>
            <td class="TopicColumn_<%# this.UniqueID.Replace("$", String.Empty) %>">
                <%# Eval("lookup_value")%></td> 
            <td>
                <strong><a href="/default.aspx?page=<%# PageIDSetting %>&eventId=<%# Eval("profile_id")%>"><%# Eval("profile_name")%></a></strong></td>
            <td style="text-align: right;">
                <asp:Label id="DateLabel" Text='<%# DataBinder.Eval(Container.DataItem, "start", "{0:d}")%>' Runat="server"/></td> 
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




