<%@ Control Language="c#" Inherits="ArenaWeb.UserControls.custom.johnsonferry.GroupLocator" CodeFile="grouplocator.ascx.cs" %>
<%@ Register TagPrefix="Arena" Namespace="Arena.Portal.UI" Assembly="Arena.Portal.UI" %>

<input type="hidden" id="ihPersonList" runat="server" name="ihPersonList" />
<input type="hidden" id="ihSelectedPersonsList" runat="server" name="ihSelectedPersonsList" />
<asp:UpdatePanel ID="upLocator" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlSearch" Runat="server" CssClass="searchPanel" Visible="True" DefaultButton="btnSearch">
            <table cellpadding="3" cellspacing="0" border="0">
                <tr>
	                <td valign="top">
                        <table cellpadding="3" cellspacing="0" border="0">
                            <tr id="trDayOfWeek" runat="server">
                                <td valign="middle" align="right" class="formLabel" nowrap="nowrap"><%=MeetingDayCaption%>:</td>
                                <td valign="top">
                                    <div class="smallText">1st Choice:</div>
                                    <asp:DropDownList ID="ddlDayOfWeek" Runat="server" CssClass="formItem" />
                                </td>
                                <td valign="top">
                                    <div class="smallText">2nd Choice:</div>
                                    <asp:DropDownList ID="ddlDayOfWeek2" Runat="server" CssClass="formItem" />
                                </td>
                            </tr>
                            <tr id="trGroupTopic" runat="server">
                                <td valign="top" align="right" class="formLabel" nowrap="nowrap"><%=TopicCaption%>:</td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlGroupTopic" Runat="server" CssClass="formItem" />
                                </td>
                            </tr>
                            <tr id="trMaritalPreference" runat="server">
                                <td valign="middle" align="right" class="formLabel" nowrap="nowrap"><%=MaritalPreferenceCaption%>:</td>
                                <td colspan="2" valign="middle">
                                    <asp:DropDownList ID="ddlMaritalPreference" Runat="server" CssClass="formItem" />
                                </td>
                            </tr>
                            <tr id="trAgeRange" runat="server">
                                <td valign="middle" align="right" class="formLabel" nowrap="nowrap"><%=AgeGroupCaption%>:</td>
                                <td colspan="2" valign="middle">
                                    <asp:DropDownList ID="ddlGroupAge" Runat="server" CssClass="formItem" />
                                </td>
                            </tr>
                            <tr id="trGroupType" runat="server">
                                <td valign="top" align="right" class="formLabel" nowrap="nowrap"><%=TypeCaption%>:</td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlGroupType" Runat="server" CssClass="formItem" />
                                </td>
                            </tr>
                            <tr id="trArea" runat="server">
                                <td valign="top" align="right" class="formLabel" nowrap="nowrap">Area:</td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlAreas" Runat="server" CssClass="formItem" />
                                </td>
                            </tr>
                            <tr id="trProximity" runat="server">
                                <td valign="top" align="right" class="formLabel" nowrap="nowrap">By Proximity:</td>
                                <td colspan="2">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
	                                        <td class="smallText">
	                                            Less than <asp:TextBox ID="tbMiles" runat="server" CssClass="formItem" Width="40" />
		                                        miles from
	                                        </td>
	                                    </tr>
	                                    <tr>
	                                        <td class="formLabel" style="padding-top: 4px;">Address&nbsp;<asp:TextBox ID="tbAddress" Runat="server" CssClass="formItem" width="250" /></td>
	                                    </tr>
                                        <tr>
	                                        <td class="formLabel" colspan="2" style="padding-top: 4px;">City/State/Zip&nbsp;
		                                        <asp:TextBox ID="tbCity" Runat="server" CssClass="formItem" style="width:140px" />, 
		                                        <asp:TextBox ID="tbState" Runat="server" CssClass="formItem" style="width:30px" MaxLength="2" />&nbsp;
		                                        <asp:TextBox ID="tbZip" Runat="server" CssClass="formItem" style="width:60px" MaxLength="5" />
	                                        </td>
	                                        <td />
                                        </tr>
                                    </table>
                                </td> 
                            </tr>
                            <tr id="trKeyword" runat="server">
                                <td valign="top" align="right" class="formLabel" nowrap="nowrap" style="height: 24px"><asp:Label ID="lblSearch" Runat= "server" CssClass="formLabel" Text="KeyWord Search:" /></td>
                                <td colspan="2" style="height: 24px">
                                    <asp:TextBox ID="tbSearch" Runat="server" CssClass="formItem" TextMode= "SingleLine" MaxLength="2000" style="width:400px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="left">
                                    <asp:Button ID="btnSearch" Runat="server" Text="Search" CssClass="smallText" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlResults" Runat="server" CssClass="searchResults" Visible="False">
            <asp:Label ID="lblNoResults" CssClass="errorText" Runat="server" Visible="False"><%= NoResultsText %></asp:Label>
             <!-- KM-add; Added a TemplateColumn to display the meeting start time and adjusted the code-behind file's code to account for the additional column. -->
	        <Arena:DataGrid id="dgResults" Runat="server" AllowSorting="true">
	            <Columns>
		            <asp:boundcolumn HeaderText="ID" datafield="group_id" Visible="False" />
		            <asp:TemplateColumn HeaderStyle-CssClass="reportHeader" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="False">
			            <ItemTemplate>
			                <asp:RadioButton ID="rbSelect" Runat="server" GroupName="RadioSelect" />
			            </ItemTemplate>
		            </asp:TemplateColumn>		
                    <asp:BoundColumn HeaderText="Group" DataField="group_name" SortExpression="group_name" />
		            <asp:HyperLinkColumn HeaderText="Leader's Email" SortExpression="leader_email" DataTextField="leader_email" DataNavigateUrlField="leader_email" 
			            DataNavigateUrlFormatString="mailto:{0}" ItemStyle-Wrap="False" Visible = "False" />
		            <asp:boundcolumn HeaderText="Distance<br>(miles)" SortExpression="distance" datafield="distance" DataFormatString="{0:N2}"
			                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
			          <asp:BoundColumn HeaderText="Area" DataField="area_name" SortExpression="area_name" />
		              <asp:BoundColumn DataField="leader_name" SortExpression="leader_name" />
		              <asp:boundcolumn SortExpression="meeting_day" datafield="meeting_day" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
		              <asp:TemplateColumn SortExpression="meeting_start_time">
		                <HeaderTemplate>
		                    <asp:LinkButton CommandName="Sort" CommandArgument='<%# Eval("meeting_start_time", "{0:t}")%>' Text="Time" CausesValidation="False" runat="server" />
		                </HeaderTemplate>
		                <ItemTemplate>
                            <asp:Repeater ID="Schedule_Repeater" runat="server">
                                <ItemTemplate>
                                    <%# Eval("meeting_start_time", "{0:t}")%>
                                </ItemTemplate>
                            </asp:Repeater>
		                </ItemTemplate>
		              </asp:TemplateColumn>
		              <asp:boundcolumn HeaderText="Members" SortExpression="member_count" datafield="member_count" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
	                 <asp:boundcolumn SortExpression="group_type" datafield="group_type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
		             <asp:boundcolumn SortExpression="Age" datafield="Age" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
		             <asp:boundcolumn SortExpression="marital_status" datafield="marital_status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
		             <asp:boundcolumn SortExpression="topic" datafield="topic" ItemStyle-Wrap="False" />
		            <asp:boundcolumn SortExpression="group_desc" datafield="group_desc" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
	            </Columns>
            </Arena:DataGrid>
            
            <asp:DataGrid ID="testDataGrid" runat="server"></asp:DataGrid>
            <div id="textpart" runat="server" style="padding-top: 15px;">
                <table>
                    <tr>
                        <td valign="top" align="right" class="formLabel" nowrap="nowrap">
                            <asp:Label ID="Label1" Runat= "server" CssClass="formLabel" Text="Name:" />
                        </td>
                        <td><asp:TextBox ID="tbName" Columns="30" Runat="server" CssClass="formItem" style="width:250px" /></td>
	                </tr>
                    <tr>
                        <td valign="top" align="right" class="formLabel" nowrap="nowrap">
                            <asp:Label ID="Label2" Runat= "server" CssClass="formLabel" Text="*Email:" />
                        </td>
                        <td colspan="2">
	                        <asp:TextBox ID="tbEmail" Columns="30" Runat="server" CssClass="formItem" style="width:250px" />
	                        <asp:RequiredFieldValidator ID="rfEmail" ControlToValidate="tbEmail" CssClass="errorText" ErrorMessage="Please enter Your e-mail" runat="server" />
		                    <asp:RegularExpressionValidator id="reEmail" ControlToValidate="tbEmail" runat="server" ErrorMessage="Please enter a valid e-mail address" CssClass="errorText" ValidationExpression="[\w\.\'_%-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+" />
                        </td>
	                </tr>
	                <tr>
                        <td valign="top" align="right" class="formLabel" nowrap="nowrap">
                            <asp:Label ID="Label5" Runat= "server" CssClass="formLabel" Text="Phone:" />
                        </td>
                        <td><Arena:PhoneTextBox ID="tbPhone" runat="server" Width="250px" CssClass="formItem" /></td>
	                </tr>
	                <tr>
                        <td valign="top" align="right" class="formLabel" nowrap="nowrap">Notes:</td>
                        <td><asp:TextBox ID="tbNotes" Runat="server" CssClass="formItem" TextMode="MultiLine" Rows="5" MaxLength="1000" style="width:400px" /></td>
	                </tr>
                    <tr>
                        <td align="left" colspan="2"><asp:Button ID="btnSend" Runat="server" Text="Request" CssClass="smallText" /></td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

