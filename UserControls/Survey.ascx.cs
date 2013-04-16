namespace ArenaWeb.Custom.johnsonferry.UserControls.SpiritualGiftsSurvey
{
  using System;
  using System.IO;
  using System.Text;
  using System.Data;
  using System.Drawing;
  using System.Web;
  using System.Web.UI;
  using System.Web.UI.WebControls;
  using System.Web.UI.HtmlControls;
  using System.Configuration;

  using Arena.Assignments;
  using Arena.Core;
  using Arena.Core.SMS;
  using Arena.DataLayer.Core;
  using Arena.DataLayer.SmallGroup;
  using Arena.Document;
  using Arena.Enums;
  using Arena.Event;
  using Arena.Exceptions;
  using Arena.Organization;
  using Arena.Phone;
  using Arena.Portal;
  using Arena.Portal.UI;
  using Arena.Protection;
  using Arena.Security;
  using Arena.SmallGroup;
  using Arena.Utility;
  using Microsoft.Practices.EnterpriseLibrary.Validation;

  /// <summary>
  ///		Summary description for SpiritualGiftsSurvey.
  /// </summary>
  public partial class Survey : PortalControl
  {
    #region Module Settings

    // Module Settings
    [PageSetting("Login Page", "Page to redirect users to when they are required to login.", true)]
    public string LoginPageIDSetting { get { return Setting("LoginPageID", "", true); } }

    #endregion

    #region Private Variables

    private Person Person = null;
    private bool EditEnabled = false;
    private bool _publicSite = true;
    private bool _editSecurityEnabled = false;

    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
      EditEnabled = CurrentModule.Permissions.Allowed(OperationType.Edit, CurrentUser);

      LoadPerson();
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
      RegisterScripts();

      DisplayPersonGifts();

      hideFromAnonymous();
    }

    #region Utility Methods

    private void ShowMessage(string message)
    {
      lblMessage.Text += message + "; ";
      lblMessage.Visible = true;
    }

    private void RegisterScripts()
    {
      BasePage.AddJavascriptInclude(Page, ResolveUrl("/Custom/JohnsonFerry/Scripts/jquery.numericalscale.js"));
      BasePage.AddCssLink(Page, ResolveUrl("/Custom/JohnsonFerry/CSS/spiritualgiftsurvey.css"));
    }

    private void DisplayPersonGifts()
    {
      if (Person != null)
      {
        if (Person.Gifts.Count > 0)
        {
          phCurrentGifts.Controls.Add(new LiteralControl("<h3>Current Spiritual Gifts</h3>"));
          phCurrentGifts.Controls.Add(new LiteralControl("<p>"));
          phCurrentGifts.Controls.Add(new LiteralControl("According to our records, you have previously taken a Spiritual Gifts survey. Please review the following list of gifts. You may still take the Spiritual Gifts survey if you wish."));
          phCurrentGifts.Controls.Add(new LiteralControl("</p>"));
          phCurrentGifts.Controls.Add(new LiteralControl("<p>"));
        }
        foreach (Lookup Gift in Person.Gifts)
        {
          try
          {
            phCurrentGifts.Controls.Add(new LiteralControl("<strong>" + Gift.Value + "</strong>"));
            phCurrentGifts.Controls.Add(new LiteralControl("<br />"));
          }
          catch (Exception ex)
          {
            ShowMessage("DisplayPersonGifts() Error: " + ex.Message);
          }
        }
        phCurrentGifts.Controls.Add(new LiteralControl("</p>"));
      }
    }

    #endregion

    public void LoadPerson()
    {
      if (Person == null)
      {
        int PersonID = -1;
        bool isMe = true;


        if (PersonID != -1)
          Person = new Person(PersonID);
        else if (isMe)
        {
          Person = CurrentPerson;
        }

        if (Person == null || Person.PersonID == -1)
        {
          Person = null;
        }
      }
    }

    protected void hideFromAnonymous()
    {
      if (Person == null)
      {
        btnSaveGifts.Visible = false;
        hdnSelectedVals.Visible = false;
      }
      else
      {
        btnSaveGifts.Visible = true;
        hdnSelectedVals.Visible = true;
      }
    }

    protected void btnSaveGifts_Click(object sender, EventArgs e)
    {
      if (Person == null)
      {
        Response.Redirect(string.Format("~/default.aspx?page={0}&redirect={1}",
          LoginPageIDSetting, Request.RawUrl));
      }
      else
      {
        Person.Gifts.Clear();
        LookupCollection gifts = new LookupType(SystemLookupType.SpiritualGifts).Values;
        foreach (Lookup gift in gifts)
        {
          string[] split = gift.Value.Split(new Char[] { '/', ' ', ',', '.', ':', '\t' });

          foreach (string s in split)
          {
            if (hdnSelectedVals.Value.Contains(s.Trim()))
            {
              Lookup Gift = new Lookup(gift.LookupID);
              Person.Gifts.Add(Gift);
            }
          }
        }
        Person.SaveGifts(CurrentPortal.OrganizationID, CurrentUser.Identity.Name);

        DisplayConfirmation();
      }
    }

    protected void DisplayConfirmation()
    {
      SurveyPanel.Visible = false;
      ConfirmationPanel.Visible = true;
      if (Person != null)
      {
        if (Person.Gifts.Count > 0)
        {
          phCurrentGiftsNew.Controls.Add(new LiteralControl("<h3>Current Spiritual Gifts</h3>"));
          phCurrentGiftsNew.Controls.Add(new LiteralControl("<p>"));
        }
        foreach (Lookup Gift in Person.Gifts)
        {
          try
          {
            phCurrentGiftsNew.Controls.Add(new LiteralControl("<strong>" + Gift.Value + "</strong>"));
            phCurrentGiftsNew.Controls.Add(new LiteralControl("<br />"));
          }
          catch (Exception ex)
          {
            ShowMessage("DisplayPersonGifts() Error: " + ex.Message);
          }
        }
        phCurrentGifts.Controls.Add(new LiteralControl("</p>"));
      }
    }

  }
}
