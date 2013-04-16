<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TagsToAttributes.ascx.cs" Inherits="ArenaWeb.UserControls.Custom.arena.TagsToAttributes" %>
<asp:Panel ID="pnl" runat="server">
<asp:Panel ID="pnlError" runat="server" Visible="false" class="errorText" style="padding: 5px; width: 400px; background-color: #FFF6D0; border: 1px solid #AB005A;">
    <asp:Literal ID="lcMessage" runat="server"></asp:Literal>
</asp:Panel>

<table cellpadding="3" cellspacing="0">
<tr>
    <td class="formLabel" nowrap="nowrap">Move Values From Tag</td>
    <td nowrap="nowrap">
        <Arena:ProfilePicker ID="ppTag" runat="server" AllowRemove="false" AllowSelectAllTypes="true" IncludeEvents="false" SelectableRoots="false"></Arena:ProfilePicker>
        <asp:RequiredFieldValidator ID="rfvTag" runat="server" ControlToValidate="ppTag" ErrorMessage="Please select a tag" ToolTip="Please select a tag">*</asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
    <td class="formLabel" nowrap="nowrap">To Attribute</td>
    <td nowrap="nowrap">
        <Arena:OptGroupDropDownList ID="drpAttribute" runat="server" CssClass="formItem"></Arena:OptGroupDropDownList>
        <asp:RequiredFieldValidator ID="rfvAttribute" runat="server" ControlToValidate="drpAttribute" InitialValue="-1" ToolTip="Please select an attribute" ErrorMessage="Please select an attribute">*</asp:RequiredFieldValidator>
        <asp:CheckBox ID="cbOverwrite" runat="server" Text="Overwrite Existing?" CssClass="smallText" />
    </td>
</tr>
<tr>
    <td colspan="2"><asp:Button ID="btnCreate" runat="server" Text="Move" OnClick="btnCreate_Click" CssClass="smallText" /></td>
</tr>
<tr>
    <td colspan="2" class="formLabel"><asp:Literal ID="lcResult" runat="server"></asp:Literal></td>
</tr>
</table>
</asp:Panel>