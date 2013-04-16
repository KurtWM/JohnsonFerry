namespace ArenaWeb.UserControls.Custom.arena
{
	using System;
	using System.Linq;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using Arena.Core;
	using Arena.Portal;
	using Arena.Enums;
	using Arena.Document;

	public partial class TagsToAttributes : PortalControl
	{
		#region Page_Load
		protected void Page_Load(object sender, EventArgs e)
		{
			if (ArenaContext.Current.SelectedProfile != null)
			{
				ppTag.ProfileID = ArenaContext.Current.SelectedProfile.ProfileID;
				ppTag.Enabled = false;
			}

			if (!Page.IsPostBack)
			{
				drpAttribute.Items.Add(new ListItem("", "-1"));
				AddAttributeGroups();
			}
			pnlError.Visible = false;
		}
		#endregion

		/// <summary>
		/// Verifies that a valid Tag and Attribute were selected, then loops through the ProfileMembers of the selected Tag
		/// and calls the SaveAttributeValue method for each ProfileMember
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnCreate_Click(object sender, EventArgs e)
		{
			if (ppTag.ProfileID == -1 || drpAttribute.SelectedValue == "-1")
				return;

			Profile p = new Profile(ppTag.ProfileID);
			Arena.Core.Attribute attr = new Arena.Core.Attribute(int.Parse(drpAttribute.SelectedValue));
			if (p.ProfileID == -1 && attr.AttributeId != -1)
				return;

			int memberCount = 0;
			try
			{
				foreach (ProfileMember member in p.Members)
				{
					PersonAttribute pa = new PersonAttribute(member.PersonID, attr.AttributeId);
					if (pa.PersonID == -1)
						pa.PersonID = member.PersonID;
					if (SaveAttributeValue(pa, member, cbOverwrite.Checked))
						memberCount++;
				}
				lcResult.Text = string.Format("{0} Member(s) updated", memberCount.ToString());
			}
			catch (Exception ex)
			{
				lcMessage.Text = "The following errors have occurred:<br />" + ex.Message;
				pnlError.Visible = true;
				lcResult.Text = string.Format("{0} Member(s) updated", memberCount.ToString());
			}
		}

		/// <summary>
		/// Saves the correct value from ProfileMember into the passed in PersonAttribute.
		/// If the Attribute is a YesNo attribute, then the Attribute for the ProfileMember will be marked as yes.
		/// If the Attribute is a DateTime attribute, then the value will be the DateActive value of the ProfileMember.
		/// If the Attribute is a Lookup, then the value will be the Lookup ID that has a matching Value to the MemberNotes property of the ProfileMember.
		/// If the Attribute is any other type, the value will be the MemberNotes.
		/// </summary>
		/// <param name="personAttribute"></param>
		/// <param name="member"></param>
		private bool SaveAttributeValue(PersonAttribute personAttribute, ProfileMember member, bool overwrite)
		{
			bool updated = false;
			try
			{
				switch (personAttribute.AttributeType)
				{
					case DataType.YesNo:
						if (overwrite || !personAttribute.HasIntValue)
						{
							personAttribute.IntValue = 1;
							updated = true;
						}
						break;

					case DataType.DateTime:
						if (overwrite || !personAttribute.HasDateValue)
						{
							personAttribute.DateValue = member.DateActive;
							updated = true;
						}
						break;

					case DataType.Lookup:
						if (!string.IsNullOrEmpty(member.MemberNotes.Trim()) && (overwrite || !personAttribute.HasIntValue))
						{
							LookupType lt = new LookupType(int.Parse(personAttribute.TypeQualifier));
							Lookup lookup = lt.Values.SingleOrDefault(l => l.Value.ToUpper() == member.MemberNotes.Trim().ToUpper());
							if (lookup != null)
							{
								personAttribute.IntValue = lookup.LookupID;
								updated = true;
							}
						}
						break;

					default:
						if (overwrite || !personAttribute.HasStringValue)
						{
							personAttribute.StringValue = member.MemberNotes.Trim();
							updated = true;
						}
						break;
				}

				if (updated)
				{
					personAttribute.Save(CurrentOrganization.OrganizationID, "TagsToAttributes");
					// we must save the relationship between the document and the person
					if (personAttribute.AttributeType == DataType.Document)
					{
						if (personAttribute.IntValue != -1)
						{
							PersonDocument newDocument = new PersonDocument(personAttribute.PersonID, personAttribute.IntValue);
							newDocument.PersonID = personAttribute.PersonID;
							newDocument.DocumentID = personAttribute.IntValue;
							newDocument.SaveRelationship("TagsToAttributes");
						}
					}
				}

				return updated;
			}
			catch { throw; }
		}

		#region Attribute Drop Down
		private void AddAttributeGroups()
		{
			AttributeGroupCollection groups = new AttributeGroupCollection(CurrentOrganization.OrganizationID);
			foreach (AttributeGroup group in groups)
			{
				ListItem item = new ListItem(group.GroupName);
				item.Attributes["optgroup"] = "1";
				drpAttribute.Items.Add(item);
				AddAttributes(group);
			}
		}

		private void AddAttributes(AttributeGroup group)
		{
			foreach (Arena.Core.Attribute attr in group.Attributes)
			{
				ListItem item = new ListItem(attr.AttributeName, attr.AttributeId.ToString());
				drpAttribute.Items.Add(item);
			}
		}
		#endregion
	}
}