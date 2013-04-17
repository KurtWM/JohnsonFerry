using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Collections.Specialized;
using Arena.Portal;
using Arena.Portal.UI;
using Arena.Security;
using Arena.Exceptions;
using Arena.Core;
using Arena.Marketing;
using Arena.DataLayer.Core;
using Arena.DataLayer.Marketing;

namespace ArenaWeb.Custom.JohnsonFerry.UserControls
{

  public partial class EventRegistrationList : PortalControl
  {
    protected bool eventsOnly;

    [PageSetting("Detail Page", "The page that should be used to display promotion details.", true)]
    public string DetailPageSetting
    {
      get
      {
        return this.Setting("DetailPage", "", true);
      }
    }

    [PageSetting("Event Detail Page", "The page that should be used to display event details.", true)]
    public string EventDetailPageSetting 
    { 
      get 
      { 
        return Setting("EventDetailPage", "", false); 
      } 
    }

    [ListFromSqlSetting("Topic Area ID", "The Topic Area IDs that should be used to filter promotions.", false, "", "SELECT lookup_id, lookup_value FROM core_lookup WHERE lookup_type_id = 69 AND active = 1 AND organization_id = 1 ORDER BY lookup_value", ListSelectionMode.Multiple)]
    public string TopicAreaIDSetting 
    { 
      get 
      { 
        return Setting("TopicAreaID", "", false); 
      } 
    }

    [ListFromSqlSetting("Dateless Event Types", "List of Event Types for which a start date is irrelevant. The start date for these Event Types will not be displayed.", false, "", "SELECT lookup_id, lookup_value FROM core_lookup WHERE lookup_type_id = 46 AND active = 1 AND organization_id = 1 ORDER BY lookup_value", ListSelectionMode.Multiple)]
    public string DatelessEventTypeIDSetting 
    { 
      get 
      { 
        return Setting("EventTypeID", "", false); 
      } 
    }

    [CustomListSetting("Area Filter", "Filter flag for area.", false, "primary", new string[] { "primary", "secondary", "both", "home" }, new string[] { "primary", "secondary", "both", "home" })]
    public string AreaFilterSetting
    {
      get
      {
        return this.Setting("AreaFilter", "primary", false);
      }
    }

    [ListFromSqlSetting("Event Types to Display", "List of Event Types to be displayed.", false, "", "SELECT lookup_id, lookup_value FROM core_lookup WHERE lookup_type_id = 46 AND active = 1 AND organization_id = 1 ORDER BY lookup_value", ListSelectionMode.Multiple)]
    public string DisplayEventTypeIDSetting 
    { 
      get 
      { 
        return Setting("DisplayEventTypeID", "", false); 
      } 
    }

    [NumericSetting("Max Items", "The maximum number of promotions to display (default = 6).", false)]
    public string MaxItemsSetting
    {
      get
      {
        return this.Setting("MaxItems", "6", false);
      }
    }

    [BooleanSetting("EventsOnly", "Flag indicating if only promotions tied to an event should be displayed.", false, false)]
    public string EventsOnlySetting
    {
      get
      {
        return this.Setting("EventsOnly", "false", false);
      }
    }

    [TextSetting("List Header", "The header for this list.", false)]
    public string HeaderSetting 
    { 
      get 
      { 
        return Setting("Header", "", false); 
      } 
    }

    [TextSetting("Hidden Date Text", "The text to show when an event's date is hidden. Default is no text.", false)]
    public string HiddenDateSetting 
    { 
      get 
      { 
        return Setting("HiddenDate", "", false); 
      } 
    }

    [CssSetting("Css File", "An optional CSS File to use for this module.", false)]
    public string CssFileSetting
    {
      get
      {
        return this.Setting("CssFile", "", false);
      }
    }

    [CampusSetting("Campus Filter", "The campus to filter promotions for.", false)]
    public string CampusSetting
    {
      get
      {
        return this.Setting("Campus", "", false);
      }
    }

    public bool TopicAreaIDIsEmpty = true;
    public bool luidIsEmpty = true;
    public string strTopicAreas;
    public string errMsg = "";
    public string cssStr;
    public int iTopicAreaNum = 0;
    public bool DisplayEventType = true;


    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Request["promotionID"] != null)
        this.Response.Redirect(new PromotionRequest(int.Parse(this.Request["promotionID"])).DetailUrl(this.Request, this.DetailPageSetting, this.EventDetailPageSetting), true);
      int maxItems = 6;
      try
      {
        maxItems = int.Parse(this.MaxItemsSetting);
      }
      catch
      {
      }
      this.eventsOnly = this.EventsOnlySetting.ToLower() == "true";
      string script = "";
      if (this.CssFileSetting != string.Empty)
        script = "<link href=\"" + this.Request.ApplicationPath + "/css/" + this.CssFileSetting + "\" type=\"text/css\" rel=\"stylesheet\" />";
      this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "promotionCss", script);
      int campusID = -1;
      if (this.CampusSetting != string.Empty)
      {
        try
        {
          campusID = int.Parse(this.CampusSetting);
        }
        catch
        {
        }
      }
      this.RegistrationRepeater.DataSource = (object)new PromotionRequestData().GetCurrentPromotionWebRequests_DT(this.TopicAreaIDSetting, this.AreaFilterSetting.ToLower(), campusID, maxItems, this.eventsOnly, -1, ArenaContext.Current.Organization.OrganizationID);
      this.RegistrationRepeater.DataBind();

      //cssStr = "<style type='text/css'>.TopicColumn_" + this.UniqueID.Replace("$", String.Empty) + " {display:none;}</style>";
      //TopicAreaIDIsEmpty = (TopicAreaIDSetting.Length == 0);
      //luidIsEmpty = (Request.QueryString["luid"] == null);

      //if (!TopicAreaIDIsEmpty) //if there is a LookupID set for this module instance it takes priority.
      //{
      //  strTopicAreas = TopicAreaIDSetting;
      //}
      //else if (!luidIsEmpty) //else if there is an "luid" QueryString value, use it instead.
      //{
      //  strTopicAreas = Request.QueryString["luid"];
      //}
      //else
      //{
      //  strTopicAreas = "";
      //  DefaultHeader.Visible = true;
      //}

      //if (HeaderSetting.Trim().Length > 0)
      //{
      //  DefaultHeader.Text = HeaderSetting;
      //}

      //BindRegistrationRepeater(RegistrationRepeater, "evnt_sp_JFBC_get_eventLiveList_ministryOption3", strTopicAreas, "@TopicAreas");

      //ErrorMsg.Text = errMsg;

      //if (strTopicAreas.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length == 1)
      //{
      //  cssLiteral.Text = cssStr;
      //}
      //else
      //{
      //  cssLiteral.Text = "";
      //}
    }

    /// <summary>
    /// event function for statement completed
    /// </summary>
    ///
    protected void Bind_SqlCommand_StatementCompleted(object sender, StatementCompletedEventArgs e)
    {
      if (e.RecordCount == 0)
      {
        errMsg = "<p class=\"errorText\">There are no valid events to show.</p>";
        ErrorMsg.Text = errMsg;
        DefaultHeader.Visible = true;
        RegistrationRepeater.Visible = false;
      }
    }

    protected void BindRegistrationRepeater(Repeater myRepeater, string myStoredProcedureName, string myParameter, string myParameterName)
    {
      using (SqlConnection connection = new Arena.DataLib.SqlDbConnection().GetDbConnection())
      {
        // Create the command and set its properties.
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = myStoredProcedureName;
        command.CommandType = CommandType.StoredProcedure;
        command.StatementCompleted += new StatementCompletedEventHandler(Bind_SqlCommand_StatementCompleted);

        // Add the input parameter and set its properties.
        SqlParameter parameter = new SqlParameter();
        parameter.ParameterName = myParameterName;
        parameter.SqlDbType = SqlDbType.VarChar;
        parameter.Direction = ParameterDirection.Input;
        parameter.Value = myParameter;

        // Add the OrganizationID parameter and set its properties.
        SqlParameter orgIdParam = new SqlParameter();
        orgIdParam.ParameterName = "@OrganizationId";
        orgIdParam.SqlDbType = SqlDbType.Int;
        orgIdParam.Direction = ParameterDirection.Input;
        orgIdParam.Value = ArenaContext.Current.Organization.OrganizationID;

        // Add the parameter to the Parameters collection. 
        command.Parameters.Add(parameter);
        command.Parameters.Add(orgIdParam);

        // Open the connection and execute the reader.
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();

        myRepeater.DataSource = reader;
        myRepeater.DataBind();

        reader.Close();
      }
    }

    protected void RegistrationRepeater_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
      // Execute the following logic for Items and Alternating Items.
      if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
      {
        int intPromoId;
        bool result = Int32.TryParse(((DataRowView)e.Item.DataItem)["promotion_request_id"].ToString(), out intPromoId);
        if (result)
        {
          SqlDataReader PromoSqlDataReader = new PromotionRequestData().GetPromotionRequestByID(intPromoId, ArenaContext.Current.Organization.OrganizationID);
          //PromoSqlDataReader.Close();
          //if (!PromoSqlDataReader.IsDBNull(PromoSqlDataReader.GetOrdinal("web_start_date")))
          //{
          int intWebStartDateColumn = PromoSqlDataReader.GetOrdinal("web_start_date");
          if (PromoSqlDataReader.HasRows)
          {
            while (PromoSqlDataReader.Read())
            {
              ((Label)e.Item.FindControl("StartDateLabel")).Text = PromoSqlDataReader.GetDateTime(intWebStartDateColumn).ToShortDateString(); // "<b>***Good***</b>";
            }
          }
          PromoSqlDataReader.Close();
          //}

    //    System.Data.Common.DbDataRecord rd = (System.Data.Common.DbDataRecord)e.Item.DataItem;

    //    CheckForDatelessEventTypes(e, rd);

    //    if (DisplayEventTypeIDSetting.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length > 0)
    //    {
    //      e.Item.Visible = ItemIsEventType(e, rd);
    //    }
        }

      }
    }

    //protected void CheckForDatelessEventTypes(RepeaterItemEventArgs e, System.Data.Common.DbDataRecord rd)
    //{
    //  string[] datelessEventTypes = DatelessEventTypeIDSetting.Split(',');
    //  String type_luid = (rd)["type_luid"].ToString();

    //  if (datelessEventTypes.Contains(type_luid))
    //  {
    //    ((Label)e.Item.FindControl("DateLabel")).Text = HiddenDateSetting;
    //  }
    //}

    //protected bool ItemIsEventType(RepeaterItemEventArgs e, System.Data.Common.DbDataRecord rd)
    //{
    //  string[] displayEventTypes = DisplayEventTypeIDSetting.Split(',');
    //  String type_luid = (rd)["type_luid"].ToString();

    //  return displayEventTypes.Contains(type_luid);

    //}
  }
}