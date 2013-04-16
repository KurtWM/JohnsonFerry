namespace ArenaWeb.UserControls.custom.johnsonferry
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Web.UI;
    
    using Arena.Core;
    using Arena.Core.Communications;
    using Arena.DataLayer.SmallGroup;
    using Arena.DataLib; //KM-add
    using Arena.Exceptions;
    using Arena.Organization;
    using Arena.Portal;
    using Arena.Portal.UI;
    using Arena.Security;
    using Arena.SmallGroup;
    using Arena.Utility;

    /// <summary>
    ///	Summary description for GroupLocator.
    /// </summary>
    public partial class GroupLocator : PortalControl
    {
        #region Module Settings

        [BooleanSetting("Assign as Pending Registrant", "Flag indicating if the person should be assigned as a pending registrant if they choose a group.", false, true)]
        public string PendingRegistraintSetting { get { return Setting("PendingRegistraint", "true", false); } }

        [TextSetting("Group Leader Email ", "This sets the email address that the request is sent to if the group leader does not have a valid email address.", false)]
        public string GroupLeaderEmailSetting { get { return Setting("GroupLeaderEmail", "false", false); } }

		[ClusterCategorySetting("Category ID", "The ID of the category of this small group cluster.", true)]
		public string CategorySetting { get { return Setting("Category", "", true); } }

        //[NumericSetting("Limit Search Results to Group Cluster ID", "This limits the search results from the group locator to the group cluster (and child clusters) specified.", false)]
        //public string LimitSearchResultsSetting { get { return Setting("LimitSearchResults", "-1", false); } }

        [ListFromSqlSetting("Limit Search Results to Group Cluster", "This limits the search results from the group locator to the selected group cluster (and child clusters) specified.", false, "-1", "SELECT group_cluster_id, cluster_name FROM smgp_group_cluster WHERE organization_id = ##OrganizationId## ORDER BY cluster_name")]
        public string LimitSearchResultsSetting { get { return Setting("LimitSearchResults", "-1", false); } }

        [TextSetting("No Results Text", "The text to display if no results are found.", false)]
        public string NoResultsSetting { get { return Setting("NoResults", "", false); } }

        [PageSetting("Redirect Page", "The page to redirect to.", true)]
        public string RedirectPageIDSetting { get { return Setting("RedirectPageID", "", true); } }

        [BooleanSetting("Require Area Selection", "Flag indicating if the areas drop down should require an area be selected.", false, true)]
        public string RequireAreaSetting { get { return Setting("RequireArea", "true", false); } }

        [BooleanSetting("Show Day Of Week", "Flag indicating if the day of week choices should be displayed.", false, true)]
        public string ShowDayOfWeekSetting { get { return Setting("ShowDayOfWeek", "true", false); } }

        [BooleanSetting("Show Group Topic", "Flag indicating if the group topic choices should be displayed.", false, true)]
        public string ShowGroupTopicSetting { get { return Setting("ShowGroupTopic", "true", false); } }

        [BooleanSetting("Show Marital Status", "Flag indicating if the marital status choices should be displayed.", false, true)]
        public string ShowMaritalStatusSetting { get { return Setting("ShowMaritalStatus", "true", false); } }

        [BooleanSetting("Show Age Range", "Flag indicating if the age range choices should be displayed.", false, true)]
        public string ShowAgeRangeSetting { get { return Setting("ShowAgeRange", "true", false); } }

        [BooleanSetting("Show Group Type", "Flag indicating if the group type choices should be displayed.", false, true)]
        public string ShowGroupTypeSetting { get { return Setting("ShowGroupType", "true", false); } }

        [BooleanSetting("Show Areas", "Flag indicating if the areas should be displayed.", false, true)]
        public string ShowAreasSetting { get { return Setting("ShowAreas", "true", false); } }

        [BooleanSetting("Show Proximity", "Flag indicating if the proximity should be displayed.", false, true)]
        public string ShowProximitySetting { get { return Setting("ShowProximity", "true", false); } }

        [BooleanSetting("Show Keyword", "Flag indicating if the keyword should be displayed.", false, true)]
        public string ShowKeywordSetting { get { return Setting("ShowKeyword", "true", false); } }

		[BooleanSetting("Send Cluster Leader Email", "Flag indicating if the leader of the cluster of the selected group should also get the followup email.", false, false)]
		public string SendClusterLeaderEmailSetting { get { return Setting("SendClusterLeaderEmail", "false", false); } }

        //KM-add; a setting to choose whether to display the member count column
        [BooleanSetting("Show Members Column - johnsonferry", "Flag indicating if the members should be displayed in the results.", false, false)]
        public string DisplayMembersColumnSetting { get { return Setting("DisplayMembersColumn", "false", false); } }

        //KM-add; a setting to choose whether to display the area column
        [BooleanSetting("Show Areas Column - johnsonferry", "Flag indicating if the area should be displayed in the results.", false, false)]
        public string DisplayAreaColumnSetting { get { return Setting("DisplayAreaColumn", "false", false); } }

        #endregion

        #region Variables

        protected string TypeCaption = "Type";
        protected string TopicCaption = "Topic";
        protected string MeetingDayCaption = "Day"; //KM-edit; Changed "Meeting Day" to "Day"
        protected string MeetingStartTimeCaption = "Time"; //KM-add
        protected string AgeGroupCaption = "Age Group";
        protected string MaritalPreferenceCaption = "Marital Preference";
        protected string DescriptionCaption = "Notes";
        protected string NoResultsText = "There currently are no groups that match the criteria you specified.<br>Please try your search again.  To get more results you may want to try being less specific in your search criteria.";
        protected string mrole = "Member";
        protected string LeaderCaption = "Leader";
        protected int CategoryID = -1;
        protected string Itemtype = "Group";

        private bool _useGroupType = false;

        int groupType = -1;
        int groupTopic = -1;
        int groupMeetingDay1 = -1;
        int groupMeetingDay2 = -1;
        int groupAgeGroup = -1;
        int groupMaritalPreference = -1;
        int groupDistance = -1;
        private Address groupAddress = null;
        string groupSearchText = "";
        int groupAreaId = -1;

        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            _useGroupType = (CurrentOrganization.Settings["UseGroupType"] != null && CurrentOrganization.Settings["UseGroupType"].Trim().ToLower() == "true");

            CategoryID = Int32.Parse(CategorySetting);
            
            SetCaptions();
            RegisterScripts();

            if (!Page.IsPostBack)
            {
                this.CurrentPortalPage.TemplateControl.Title = "Group Locator";
                
                ddlGroupType.Items.Clear();
                ddlGroupTopic.Items.Clear();
                ddlGroupAge.Items.Clear();
                ddlMaritalPreference.Items.Clear();

                //GroupType
                ddlGroupType.Items.Add(new ListItem("Any", "-1"));
                ddlGroupType.Items[0].Selected = true;
                if (_useGroupType)
                {
                    LookupCollection luTypes = new LookupType(SystemLookupType.SmallGroupType).Values;
                    foreach (Lookup luType in luTypes)
                        if (luType.Value.ToLower() != "unknown")
                            ddlGroupType.Items.Add(new ListItem(luType.Value, luType.LookupID.ToString()));
                }
                else
                {
                    //SqlDataReader rdr = new GroupClusterData().GetClusterRegistrationTypes(Arena.Core.ArenaContext.Current.Organization.OrganizationID);
                    SqlDataReader rdr = new GroupClusterData().GetClusterTypes(Int16.Parse(CategorySetting), Arena.Core.ArenaContext.Current.Organization.OrganizationID); //KM-edit; took ", Arena.Core.ArenaContext.Current.Organization.OrganizationID);" out of GetClusterTypes. There were too many items.
                    while (rdr.Read())
                        if ((bool)rdr["allow_registration"])
                            ddlGroupType.Items.Add(new ListItem(rdr["type_name"].ToString(), rdr["cluster_type_id"].ToString()));
                    rdr.Close();
                }

                //GroupTopic
                LookupCollection lookupCollection = new LookupType(SystemLookupType.SmallGroupTopic).Values;
                trGroupTopic.Visible = (lookupCollection.Count > 0);
                ddlGroupTopic.Items.Add(new ListItem("Any", "-1"));
                ddlGroupTopic.Items[0].Selected = true;
                foreach (Lookup lookup in lookupCollection)
                    if (lookup.Value != "Unknown" && lookup.Value != "Any")
                        ddlGroupTopic.Items.Add(new ListItem(lookup.Value, lookup.LookupID.ToString()));

                //DayOfWeek
                lookupCollection = new LookupType(SystemLookupType.DayOfWeek).Values;
                trDayOfWeek.Visible = (lookupCollection.Count > 0);
                ddlDayOfWeek.Items.Add(new ListItem("Any", "-1"));
                ddlDayOfWeek.Items[0].Selected = true;
                ddlDayOfWeek2.Items.Add(new ListItem("Any", "-1"));
                ddlDayOfWeek2.Items[0].Selected = true;
                foreach (Lookup lookup in lookupCollection)
                    if (lookup.Value != "Unknown" && lookup.Value != "Any")
                    {
                        ddlDayOfWeek.Items.Add(new ListItem(lookup.Value, lookup.LookupID.ToString()));
                        ddlDayOfWeek2.Items.Add(new ListItem(lookup.Value, lookup.LookupID.ToString()));
                    }

                //GroupAge
                lookupCollection = new LookupType(SystemLookupType.AgeRangePreference).Values;
                trAgeRange.Visible = (lookupCollection.Count > 0);
                ddlGroupAge.Items.Add(new ListItem("Any", "-1"));
                ddlGroupAge.Items[0].Selected = true;
                foreach (Lookup lookup in lookupCollection)
                    if (lookup.Value != "Unknown" && lookup.Value != "Any")
                        ddlGroupAge.Items.Add(new ListItem(lookup.Value, lookup.LookupID.ToString()));

                //GroupMaritalPreference
                lookupCollection = new LookupType(SystemLookupType.MaritalPreference).Values;
                trMaritalPreference.Visible = (lookupCollection.Count > 0);
                ddlMaritalPreference.Items.Add(new ListItem("Any", "-1"));
                ddlMaritalPreference.Items[0].Selected = true;
                foreach (Lookup lookup in lookupCollection)
                    if (lookup.Value != "Unknown" && lookup.Value != "Any")
                        ddlMaritalPreference.Items.Add(new ListItem(lookup.Value, lookup.LookupID.ToString()));

                AreaCollection areas = new AreaCollection(CurrentOrganization.OrganizationID);
                trArea.Visible = (areas.Count > 0);
                foreach (Area area in areas)
                {
                    ddlAreas.Items.Add(new ListItem(area.Name, area.AreaID.ToString()));
                }
                if (RequireAreaSetting == "false")
                    ddlAreas.Items.Insert(0, new ListItem("", "-1"));

                if (CurrentPerson != null && CurrentPerson.PersonID != -1)
                {
                    if (CurrentPerson.PrimaryAddress != null && CurrentPerson.PrimaryAddress.AddressID != -1)
                    {
                        tbAddress.Text = CurrentPerson.PrimaryAddress.StreetLine1;
                        tbCity.Text = CurrentPerson.PrimaryAddress.City;
                        tbState.Text = CurrentPerson.PrimaryAddress.State;
                        tbZip.Text = CurrentPerson.PrimaryAddress.PostalCode;
                    }
                }

                ShowSearch();
            }
        }

        private bool SetDisplay()
        {
            trDayOfWeek.Visible = (ShowDayOfWeekSetting == "true");
            trGroupTopic.Visible = (ShowGroupTopicSetting == "true");
            trMaritalPreference.Visible = (ShowMaritalStatusSetting == "true");
            trAgeRange.Visible = (ShowAgeRangeSetting == "true");
            trGroupType.Visible = (ShowGroupTypeSetting == "true");
            trArea.Visible = (ShowAreasSetting == "true");
            trProximity.Visible = (ShowProximitySetting == "true");
            trKeyword.Visible = (ShowKeywordSetting == "true");

            return (trDayOfWeek.Visible || trGroupTopic.Visible || trAgeRange.Visible || trArea.Visible || trGroupType.Visible || trKeyword.Visible || trMaritalPreference.Visible || trProximity.Visible);
        }

        private void RegisterScripts()
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("\n\n<script language=\"javascript\">\n");
            sbScript.Append("\tfunction UnCheckAllRBs(rbCurrent)\n");
            sbScript.Append("\t{\n");
            sbScript.Append("\t\tvar coll = document.getElementsByTagName('INPUT');\n");
            sbScript.Append("\t\tif (coll != null)\n");
            sbScript.Append("\t\t\tfor (i = 0; i < coll.length; i++)\n");
            sbScript.Append("\t\t\t\tif (coll[i].type == 'radio')\n");
            sbScript.Append("\t\t\t\t\tcoll[i].checked = false;\n");
            sbScript.Append("\t\trbCurrent.checked = true;\n");
            sbScript.Append("\t}\n");
            sbScript.Append("</script>\n\n");

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RegistrationDetail", sbScript.ToString());
        }

        private void SetCaptions()
        {
            if (CategoryID != -1)
            {
                Category cat = new Category(CategoryID);
                if (cat.CategoryName != string.Empty)
                    Itemtype = cat.CategoryName;
                if (cat.TypeCaption != string.Empty)
                    TypeCaption = cat.TypeCaption;
                if (cat.TopicCaption != string.Empty)
                    TopicCaption = cat.TopicCaption;
                if (cat.MeetingDayCaption != string.Empty)
                    MeetingDayCaption = cat.MeetingDayCaption;
                if (cat.MeetingStartTimeCaption != string.Empty)
                    MeetingStartTimeCaption = cat.MeetingStartTimeCaption; //KM-add
                if (cat.AgeGroupCaption != string.Empty)
                    AgeGroupCaption = cat.AgeGroupCaption;
                if (cat.MaritalPreferenceCaption != string.Empty)
                    MaritalPreferenceCaption = cat.MaritalPreferenceCaption;
                if (cat.DescriptionCaption != string.Empty)
                    DescriptionCaption = cat.DescriptionCaption;
                if (cat.LeaderCaption != string.Empty)
                    LeaderCaption = cat.LeaderCaption;
                if (NoResultsSetting != string.Empty)
                    NoResultsText = NoResultsSetting;
            }
        }

        private void ShowSearch()
        {
            if (SetDisplay())
            {
                pnlSearch.Visible = true;
                pnlResults.Visible = false;
            }
            else
            {
                btnSearch.Visible = false;
                ShowResults();
            }
        }
        
        private void ShowResults()
        {
            RebindList();
            
            pnlSearch.Visible = true;
            pnlResults.Visible = true;
        }

        private void RebindList()
        {
            groupType = Int32.Parse(ddlGroupType.SelectedValue);
            groupTopic = Int32.Parse(ddlGroupTopic.SelectedValue);
            groupMeetingDay1 = Int32.Parse(ddlDayOfWeek.SelectedValue);
            groupMeetingDay2 = Int32.Parse(ddlDayOfWeek2.SelectedValue);
            groupAgeGroup = Int32.Parse(ddlGroupAge.SelectedValue);
            groupMaritalPreference = Int32.Parse(ddlMaritalPreference.SelectedValue);
            groupSearchText = tbSearch.Text;
            groupAreaId = Int32.Parse(ddlAreas.SelectedValue);
            groupAddress = new Address(tbAddress.Text, string.Empty, tbCity.Text, tbState.Text, tbZip.Text);

            if (tbMiles.Text.Length > 0)
            {
                try { groupDistance = Int32.Parse(tbMiles.Text); }
                catch { }
            }

            //char[] chArr = new char[] { ',' };
            //testDataGrid.DataSource = new GroupData().GetGroupsWithLocator(groupAddress.XAxis, groupAddress.YAxis, groupAddress.ZAxis, CategoryID, groupTopic, _useGroupType, groupType, groupMeetingDay1, groupMeetingDay2, groupAgeGroup, groupMaritalPreference, groupDistance, groupAreaId, groupSearchText.Split(chArr), Int32.Parse(LimitSearchResultsSetting)); //KM-edit; Removed ", true, true, Arena.Core.ArenaContext.Current.Organization.OrganizationID);" from method. There were too many items and these seemed extraneous.
            //testDataGrid.DataBind();




            dgResults.Columns[10].HeaderText = TypeCaption; //KM-edit; change Columns[9] to Columns[10]
            dgResults.Columns[7].HeaderText = MeetingDayCaption;
            dgResults.Columns[8].HeaderText = MeetingStartTimeCaption; //KM-add
            dgResults.Columns[11].HeaderText = AgeGroupCaption; //KM-edit; change Columns[10] to Columns[11]
            dgResults.Columns[13].HeaderText = TopicCaption; //KM-edit; change Columns[12] to Columns[13]
            dgResults.Columns[12].HeaderText = MaritalPreferenceCaption; //KM-edit; change Columns[11] to Columns[12]
            dgResults.Columns[14].HeaderText = DescriptionCaption; //KM-edit; change Columns[13] to Columns[14]
            dgResults.Columns[6].HeaderText = LeaderCaption;
            char[] chArr = new char[] { ',' };
            dgResults.DataSource = new GroupData().GetGroupsWithLocator(groupAddress.XAxis, groupAddress.YAxis, groupAddress.ZAxis, CategoryID, groupTopic, _useGroupType, groupType, groupMeetingDay1, groupMeetingDay2, groupAgeGroup, groupMaritalPreference, groupDistance, groupAreaId, groupSearchText.Split(chArr), Int32.Parse(LimitSearchResultsSetting)); //KM-edit; Removed ", true, true, Arena.Core.ArenaContext.Current.Organization.OrganizationID);" from method. There were too many items and these seemed extraneous.
            dgResults.ItemType = Arena.Utility.ArenaTextTools.Pluralize(Itemtype);
            dgResults.ItemBgColor = CurrentPortalPage.Setting("ItemBgColor", string.Empty, false);
            dgResults.ItemAltBgColor = CurrentPortalPage.Setting("ItemAltBgColor", string.Empty, false);
            dgResults.ItemMouseOverColor = CurrentPortalPage.Setting("ItemMouseOverColor", string.Empty, false);
            dgResults.AddEnabled = false;
            dgResults.EditEnabled = false;
            dgResults.MergeEnabled = false;
            dgResults.MoveEnabled = false;
            dgResults.DeleteEnabled = false;
            dgResults.AllowPaging = true;
            dgResults.AllowSorting = true;
            dgResults.ExportEnabled = false;
            dgResults.Columns[4].Visible = Convert.ToBoolean(ShowProximitySetting);
            dgResults.Columns[5].Visible = Convert.ToBoolean(DisplayAreaColumnSetting); //KM-add; sets the display of the area column
            dgResults.Columns[9].Visible = Convert.ToBoolean(DisplayMembersColumnSetting); //KM-add; sets the display of the member count column
            dgResults.DataBind();

            if (dgResults.Items.Count <= 0)
            {
                dgResults.Visible = false;
                textpart.Visible = false;
                lblNoResults.Visible = true;
            }
            else
            {
                dgResults.Visible = true;
                textpart.Visible = true;
                lblNoResults.Visible = false;
            }
        }

        private void dgResults_Rebind(object source, System.EventArgs e)
        {
            RebindList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ShowResults();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            int groupid = -1;
            string strLeaderEmail = "";
			string strClusterLeaderEmail = "";
            
            foreach (DataGridItem item in dgResults.Items)
            {
                RadioButton rb = (RadioButton)item.FindControl("rbSelect");
                if (rb.Checked)
                {
                    groupid = Int32.Parse(rb.Attributes["groupid"]);
                    break;
                }
            }

			bool sendClusterLeaderEmail = bool.Parse(SendClusterLeaderEmailSetting);
            Group group = new Group(groupid);
            strLeaderEmail = group.Leader.Emails.FirstActive;
            if (strLeaderEmail.Length == 0 || sendClusterLeaderEmail)
            {
                GroupCluster parentCluster = group.GroupCluster;
                while (parentCluster.GroupClusterID != -1)
                {
					if (parentCluster.Leader.Emails.FirstActive != string.Empty)
					{
						strLeaderEmail = (!string.IsNullOrEmpty(strLeaderEmail) ? strLeaderEmail : parentCluster.Leader.Emails.FirstActive);
						strClusterLeaderEmail = parentCluster.Leader.Emails.FirstActive;
						break;
					}
					else if (parentCluster.Admin != null && parentCluster.Admin.PersonID != parentCluster.Leader.PersonID && parentCluster.Admin.Emails.FirstActive != string.Empty)
					{
						strLeaderEmail = (!string.IsNullOrEmpty(strLeaderEmail) ? strLeaderEmail : parentCluster.Admin.Emails.FirstActive);
						strClusterLeaderEmail = parentCluster.Admin.Emails.FirstActive;
						break;
					}
                    parentCluster = new GroupCluster(parentCluster.ParentClusterID);
                }
            }

			if (strLeaderEmail.Length == 0 && GroupLeaderEmailSetting != string.Empty)
				strLeaderEmail = GroupLeaderEmailSetting;
			else if (strLeaderEmail.Length == 0)
				strLeaderEmail = CurrentOrganization.Settings["GroupLocatorEmail"];
            
			SmallGroupLocator smallGroupLocator = new SmallGroupLocator();
			Dictionary<string, string> fields = new Dictionary<string, string>();
			fields.Add("##GroupName##", group.Title);
			fields.Add("##GroupID##", group.GroupID.ToString());
			fields.Add("##Name##", tbName.Text);
			fields.Add("##Phone##", tbPhone.PhoneNumber);
			fields.Add("##Email##", tbEmail.Text);
			fields.Add("##Notes##", tbNotes.Text);			
			smallGroupLocator.Send(strLeaderEmail, fields);
			if (sendClusterLeaderEmail && strLeaderEmail != strClusterLeaderEmail)
				smallGroupLocator.Send(strClusterLeaderEmail, fields);

            //set pending registratant
            if (PendingRegistraintSetting.ToLower() == "true" && CurrentPerson != null && CurrentPerson.PersonID != -1)
            {
                Registration registration = new Registration();
                registration.OrganizationID = CurrentPortal.OrganizationID;

                if (_useGroupType)
                {
                    Lookup grpType = group.GroupType;
					try
					{
						registration.ClusterType = new ClusterType(Int32.Parse(grpType.Qualifier));
					}
					catch
					{
						throw new ArenaApplicationException(string.Format("Invalid Cluster Type ID for Small Group Type '{0}'", grpType.Value));
					}
                    registration.GroupType = grpType;
                }
                else
                {
                    registration.ClusterType = group.ClusterType;
                    registration.GroupType = null;
                }
                if (group.GroupType.ToString() != string.Empty)
                    registration.GroupType = group.GroupType;

                registration.Notes = tbNotes.Text;
                registration.Persons.Add(CurrentPerson);
                registration.DayOfWeek.Add(group.MeetingDay);
                registration.AgeRange.Add(group.PrimaryAge);
                registration.MaritalPreference.Add(group.PrimaryMaritalStatus);
                registration.SetCluster();
                registration.GroupID = group.GroupID;

                registration.Save(CurrentPortal.OrganizationID, CurrentUser.Identity.Name);

            }

            Response.Redirect(string.Format("~/default.aspx?page={0}", RedirectPageIDSetting.ToString()), true);

        }

        private void dgResults_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            RebindList();
        }

        private void dgResults_ItemDataBound(object sender, DataGridItemEventArgs e)
        {

            ListItemType li = (ListItemType)e.Item.ItemType;
            if (li == ListItemType.Item || li == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;

                RadioButton rb = (RadioButton)e.Item.FindControl("rbSelect");
                rb.Attributes.Add("onclick", "UnCheckAllRBs(this);");
                rb.Attributes.Add("groupid", row["group_id"].ToString());
                if (e.Item.ItemIndex == 0)
                    rb.Checked = true;
				try
				{
					if (decimal.Parse(row["distance"].ToString()) < 0)
						e.Item.Cells[4].Text = string.Empty;
				}
				catch
				{
					e.Item.Cells[4].Text = string.Empty;
				}

                // KM-edit; Add start time via the Repeater "Schedule_Repeater"
                GetMeetingTimesByGroupId((Repeater)e.Item.FindControl("Schedule_Repeater"), DataBinder.Eval(e.Item.DataItem, "group_id").ToString(), row); 

            }
        }

        #region Web Form Designer generated code

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgResults.SortCommand += new DataGridSortCommandEventHandler(dgResults_SortCommand);
            this.dgResults.ItemDataBound += new DataGridItemEventHandler(dgResults_ItemDataBound);
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.btnSend.Click += new EventHandler(btnSend_Click);
            this.dgResults.ReBind += new DataGridReBindEventHandler(dgResults_Rebind);
            //this.btnCancel.Click += new EventHandler(btnCancel_Click);

        }

        #endregion


        #region johnsonferry custom code

        static void GetMeetingTimesByGroupId(Repeater myRepeater, string group_id, DataRowView row)
        {
            using (SqlConnection connection = new Arena.DataLib.SqlDbConnection().GetDbConnection())
            {
                // Create the command and set its properties.
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "smgp_sp_JFBC_get_groupStartAndEndTimesByGroupID";
                command.CommandType = CommandType.StoredProcedure;

                // Add the input parameter and set its properties.
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@group_id";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = group_id;

                // Add the parameter to the Parameters collection. 
                command.Parameters.Add(parameter);

                // Open the connection and execute the reader.
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                myRepeater.DataSource = reader;
                //myRepeater.ItemDataBound += Schedule_Repeater_ItemDataBound();
                myRepeater.DataBind();

                reader.Close();
            }
        }

        private void Schedule_Repeater_ItemDataBound(object sender, RepeaterCommandEventArgs e)
        {

        }

        #endregion

    }
}