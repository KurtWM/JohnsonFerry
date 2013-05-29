<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StaffContactList.ascx.cs" Inherits="ArenaWeb.UserControls.custom.johnsonferry.StaffContactList" %>
<%@ Register TagPrefix="Arena" Namespace="Arena.Portal.UI" Assembly="Arena.Portal.UI" %>

<script type="text/javascript">
    $(document).ready(function() {
        $.tablesorter.defaults.sortList = [[2, 0]];
        $.tablesorter.defaults.widgets = ['zebra'];
        $("#StaffContactListTable").tablesorter({ headers: { 0: { sorter: false}} });
        $('.closeBlockUI').click(function() {
            $.unblockUI();
        });
    }); 
</script>


<asp:Repeater ID="StaffContactRepeater" runat="server"
    DataSourceID="SqlDataSource1" 
    onitemdatabound="StaffContactRepeater_ItemDataBound">
    <HeaderTemplate>
        <h2>Staff Contact List</h2>
        <table id="StaffContactListTable" class="tablesorter {sortlist: [[2,0]]}">
        <thead>
        <tr>
            <th style="width: 5%;" class="{sorter: false}">Details</th>
            <th style="width: 10%;">First</th>
            <th style="width: 10%;">Last</th>
            <th style="width: 40%;">Ministry</th>
            <th style="width: 35%;">Title</th>
        </tr>
        </thead>
        <tbody>
    </HeaderTemplate>
    <ItemTemplate>
        <tr id="TRow" runat="server">
            <td valign="top" align="center">
                <asp:ImageButton ID="AboutBtn" runat="server" 
                    CausesValidation="false" 
                    OnClientClick='<%# "$.blockUI({ message: $(\"#contactdetail" + DataBinder.Eval(Container.DataItem, "person_id") + "\"), css: { width: \"275px\"} }); return false;" %>' 
                    ImageUrl="https://www.johnsonferry.org/images/view.gif" />
                <div id='contactdetail<%# DataBinder.Eval(Container.DataItem, "person_id") %>' style="display: none; cursor: default;">
                    <div style="background-color: #D6DEDE; border: none; text-align: left; padding-top: 3px;">
                    <p style="margin-left: 3px; position: relative; float: left; font-weight: bold;">Contact Information</p>
                    <div class="closeBlockUI" 
                            style="background: url('https://www.johnsonferry.org/app_themes/theme1/images/ui-icons_217bc0_256x240.png') no-repeat -100px -132px; width: 14px; height: 14px; margin: 4px 3px; position: relative; float: right;"></div>
                    <div style="clear: both;"></div></div>
                    <div style="background-color: White;">
                        <div style="text-align: left; margin: 9px;">
                            <h1><%# Eval("name")%></h1>
                            <p><strong><%# Eval("ministry")%></strong><br />
                            <em><%# Eval("title")%></em></p>
                            <p><%# Eval("business_phone")%></p>
                            <p><a href="mailto:<%# Eval("email")%>"><%# Eval("email")%></a></p>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                </div>
            </td>
            <td>
                <a href="https://jfaccess.johnsonferry.org/default.aspx?page=3409&guid=<%# Eval("guid")%>" style="display: none;"><%# Eval("nick_name")%></a>
                <strong><%# Eval("nick_name")%></strong></td> 
            <td>
                <strong><%# Eval("last_name")%></strong></td>
            <td>
                <%# Eval("ministry")%></td> 
            <td>
                <%# Eval("title")%></td> 
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </tbody>
        </table>
    </FooterTemplate>
</asp:Repeater>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:Arena %>" 
    SelectCommand="core_sp_JFBC_get_staff_contact_list" 
    SelectCommandType="StoredProcedure">
</asp:SqlDataSource>

