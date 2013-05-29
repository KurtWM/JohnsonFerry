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
using Arena.DataLayer.Core;

namespace ArenaWeb.Custom.JohnsonFerry.UserControls
{

  public partial class RegistrationList : PortalControl
  {
    [PageSetting("Detail Page", "The page to use for displaying the event details.", true)]
    public string PageIDSetting { get { return Setting("PageID", "", false); } }

    //[BooleanSetting("List All Current Events When No Topic Area ID is Provided", "Flag indicating whether to list all current events, regardless of Topic Area, when no Topic Area is specified in the module Settings or in the URL QueryString. Defaults to false and should be left to false in most cases.", true, false)]
    //public bool ListAllEnabledSetting { get { return Convert.ToBoolean(Setting("ListAllEnabled", "false", true)); } }

    [ListFromSqlSetting("Topic Areas", "List of various ministries.", false, "", "SELECT lookup_id, lookup_value FROM core_lookup WHERE lookup_type_id = 69 AND active = 1 AND organization_id = 1 ORDER BY lookup_value", ListSelectionMode.Multiple)]
    public string TopicAreaIDSetting { get { return Setting("TopicAreaID", "", false); } }

    [ListFromSqlSetting("Dateless Event Types", "List of Event Types for which a start date is irrelevant. The start date for these Event Types will not be displayed.", false, "", "SELECT lookup_id, lookup_value FROM core_lookup WHERE lookup_type_id = 46 AND active = 1 AND organization_id = 1 ORDER BY lookup_value", ListSelectionMode.Multiple)]
    public string DatelessEventTypeIDSetting { get { return Setting("EventTypeID", "", false); } }

    [ListFromSqlSetting("Event Types to Display", "List of Event Types to be displayed.", false, "", "SELECT lookup_id, lookup_value FROM core_lookup WHERE lookup_type_id = 46 AND active = 1 AND organization_id = 1 ORDER BY lookup_value", ListSelectionMode.Multiple)]
    public string DisplayEventTypeIDSetting { get { return Setting("DisplayEventTypeID", "", false); } }

    [TextSetting("List Header", "The header for this list.", false)]
    public string HeaderSetting { get { return Setting("Header", "", false); } }

    [TextSetting("Hidden Date Text", "The text to show when an event's date is hidden. Default is no text.", false)]
    public string HiddenDateSetting { get { return Setting("HiddenDate", "", false); } }

    //[NumericSetting("Max Days Before Date is Hidden", "Some events are set up to be ongoing for a long period of time. This makes the date value for the event appear misleading in the list. Enter the maximum number of days that an occurrence can last before the date value will be removed from the list. Defaults to 14 days.", false)]
    //public string MaxDaysSetting { get { return Setting("MaxDays", "14", false); } }


    public bool TopicAreaIDIsEmpty = true;
    public bool luidIsEmpty = true;
    public string strTopicAreas;
    public string errMsg = "";
    public string cssStr;
    //public string[] datelessEventTypes;
    //public string[] displayEventTypes;
    public int iTopicAreaNum = 0;
    public bool DisplayEventType = true;

    //  protected void Page_PreRender(object sender, EventArgs e)
    //{
    //      if (DatelessEventTypeIDSetting.Length > 0)
    //      {
    //        datelessEventTypes = DatelessEventTypeIDSetting.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    //      }

    //      if (DisplayEventTypeIDSetting.Length > 0)
    //      {
    //        displayEventTypes = DisplayEventTypeIDSetting.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    //      }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
      cssStr = "<style type='text/css'>.TopicColumn_" + this.UniqueID.Replace("$", String.Empty) + " {display:none;}</style>";
      TopicAreaIDIsEmpty = (TopicAreaIDSetting.Length == 0);
      luidIsEmpty = (Request.QueryString["luid"] == null);

      if (!TopicAreaIDIsEmpty) //if there is a LookupID set for this module instance it takes priority.
      {
        strTopicAreas = TopicAreaIDSetting;
      }
      else if (!luidIsEmpty) //else if there is an "luid" QueryString value, use it instead.
      {
        strTopicAreas = Request.QueryString["luid"];
      }
      else
      {
        strTopicAreas = "";
        DefaultHeader.Visible = true;
        //if (!ListAllEnabledSetting)
        //{
        //    errMsg = "<p class=\"errorText\">There are no valid events to show. No valid Lookup ID has been provided.</p>";
        //    RegistrationRepeater.Visible = false;
        //}

      }

      if (HeaderSetting.Trim().Length > 0)
      {
        DefaultHeader.Text = HeaderSetting;
      }

      BindRegistrationRepeater(RegistrationRepeater, "evnt_sp_JFBC_get_eventLiveList_ministryOption3", strTopicAreas, "@TopicAreas");

      ErrorMsg.Text = errMsg;

      //string[] items;
      if (strTopicAreas.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length == 1)
      {
        cssLiteral.Text = cssStr;
      }
      else
      {
        cssLiteral.Text = "";
      }
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
        System.Data.Common.DbDataRecord rd = (System.Data.Common.DbDataRecord)e.Item.DataItem;

        CheckForDatelessEventTypes(e, rd);

        if (DisplayEventTypeIDSetting.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length > 0)
        {
          e.Item.Visible = ItemIsEventType(e, rd);
        }
      }
    }

    protected void CheckForDatelessEventTypes(RepeaterItemEventArgs e, System.Data.Common.DbDataRecord rd)
    {
      string[] datelessEventTypes = DatelessEventTypeIDSetting.Split(',');
      String type_luid = (rd)["type_luid"].ToString();

      if (datelessEventTypes.Contains(type_luid))
      {
        ((Label)e.Item.FindControl("DateLabel")).Text = HiddenDateSetting;
      }

      //foreach (string eventType in datelessEventTypes)
      //{
      //  if (eventType.Trim() == type_luid.Trim())
      //  {
      //    ((Label)e.Item.FindControl("DateLabel")).Text = HiddenDateSetting;
      //    break;
      //  }
      //}
    }

    protected bool ItemIsEventType(RepeaterItemEventArgs e, System.Data.Common.DbDataRecord rd)
    {
      string[] displayEventTypes = DisplayEventTypeIDSetting.Split(',');
      String type_luid = (rd)["type_luid"].ToString();

      return displayEventTypes.Contains(type_luid);

      //foreach (string eventType in displayEventTypes)
      //{
      //  if (eventType.Trim() == type_luid.Trim())
      //  {
      //    DisplayEventType = true;
      //  }
      //  else
      //  {
      //    DisplayEventType = false;
      //    break;
      //  }
      //}
      //return DisplayEventType;
    }


    public class Evaluation
    {
      private string profile_id;
      private string lookup_id;
      private string lookup_value;
      private string profile_name;
      private string profile_desc;
      private string details;
      private string start;
      private string end;
      private string contact_name;
      private string contact_phone;
      private string contact_email;

      public Evaluation(string profile_id, string lookup_id, string lookup_value, string profile_name, string profile_desc, string details, string start, string end, string contact_name, string contact_phone, string contact_email)
      {
        this.profile_id = profile_id;
        this.lookup_id = lookup_id;
        this.lookup_value = lookup_value;
        this.profile_name = profile_name;
        this.profile_desc = profile_desc;
        this.details = details;
        this.start = start;
        this.end = end;
        this.contact_name = contact_name;
        this.contact_phone = contact_phone;
        this.contact_email = contact_email;
      }

      public string ProfileID
      {
        get
        {
          return profile_id;
        }
      }

      public string LookupID
      {
        get
        {
          return lookup_id;
        }
      }

      public string LookupValue
      {
        get
        {
          return lookup_value;
        }
      }

      public string ProfileName
      {
        get
        {
          return profile_name;
        }
      }

      public string ProfileDesc
      {
        get
        {
          return profile_desc;
        }
      }

      public string Details
      {
        get
        {
          return details;
        }
      }

      public string Start
      {
        get
        {
          return start;
        }
      }

      public string End
      {
        get
        {
          return end;
        }
      }

      public string ContactName
      {
        get
        {
          return contact_name;
        }
      }

      public string ContactPhone
      {
        get
        {
          return contact_phone;
        }
      }

      public string ContactEmail
      {
        get
        {
          return contact_email;
        }
      }
    }



  }
}