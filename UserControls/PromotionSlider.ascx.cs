namespace ArenaWeb.Custom.JohnsonFerry.UserControls
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Web;
  using System.Web.Profile;
  using System.Web.UI;
  using System.Web.UI.WebControls;
  using ASP;
  using Arena.Portal;
  using Arena.Core;
  using Arena.Marketing;
  using Arena.Utility;
  using System.Web.UI.HtmlControls;
  using System.Xml;
  using System.Xml.XPath;
  using System.Xml.Xsl; 

  public partial class PromotionSlider : PortalControl
  {
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
        return this.Setting("EventDetailPage", "", true);
      }
    }

    [TextSetting("Topic Area Lookups", "The Topic Area Lookup IDs (comma-delimited list) that should be used to filter promotions.", false)]
    public string TopicAreaIDSetting
    {
      get
      {
        return this.Setting("TopicAreaID", "", false);
      }
    }

    [DocumentTypeSetting("Image Type", "By default the promotion's Web Summary Image will be used.  This can be overriden to use a specific document type from the promotion's attached Media.", false)]
    public string ImageTypeSetting
    {
      get
      {
        return this.Setting("ImageType", "", false);
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

    [CampusSetting("Campus Filter", "The campus to filter promotions for.", false)]
    public string CampusSetting
    {
      get
      {
        return this.Setting("Campus", "", false);
      }
    }

    [TextSetting("Slider Width", "The width that the browser will render the Slider.", true)]
    public string SliderWidthSetting
    {
      get
      {
        return this.Setting("SliderWidth", "", true);
      }
    }

    [TextSetting("Slider Height", "The height that the browser will render the Slider. (A number or \"auto\")", false)]
    public string SliderHeightSetting
    {
      get
      {
        return this.Setting("SliderHeight", "", false);
      }
    }

    [BooleanSetting("Distort Image to Height", "If set to true, images will stretch or squash vertically to fit the Slider Height. If set to false, images will maintain their proper vertical proportions. Default is false.", true, false)]
    public string IsStretchHeightSetting
    {
      get
      {
        return this.Setting("IsStretchHeight", "false", false);
      }
    }

    [CustomListSetting("Slider Transition Type", "The type of transition displayed between promotions. Default is fade", false, "Swap", new string[] { "blindX", "blindY", "blindZ", "cover", "curtainX", "curtainY", "fade", "fadeZoom", "growX", "growY", "scrollUp", "scrollDown", "scrollLeft", "scrollRight", "scrollHorz", "scrollVert", "shuffle", "slideX", "slideY", "toss", "turnUp", "turnDown", "turnLeft", "turnRight", "uncover", "wipe", "zoom" }, new string[] { "blindX", "blindY", "blindZ", "cover", "curtainX", "curtainY", "fade", "fadeZoom", "growX", "growY", "scrollUp", "scrollDown", "scrollLeft", "scrollRight", "scrollHorz", "scrollVert", "shuffle", "slideX", "slideY", "toss", "turnUp", "turnDown", "turnLeft", "turnRight", "uncover", "wipe", "zoom" })]
    public string SliderTransitionSetting
    {
      get
      {
        return this.Setting("SliderTransition", "fade", false);
      }
    }

    [NumericSetting("Timeout", "The timeout option specifies how many milliseconds will elapse between the start of each transition. Default is 2000 (2 seconds).", false)]
    public string TimeoutSetting
    {
      get
      {
        return this.Setting("Timeout", "2000", false);
      }
    }

    [NumericSetting("Speed", "The speed option defines the number of milliseconds it will take to transition from one slide to the next. Default is 1000 (1 second).", false)]
    public string SpeedSetting
    {
      get
      {
        return this.Setting("Speed", "1000", false);
      }
    }

    [BooleanSetting("Pause", "The pause option causes the slideshow to pause when the mouse hovers over the slide. Default is true.", true, true)]
    public string IsPauseSetting
    {
      get
      {
        return this.Setting("IsPause", "true", false);
      }
    }

    [BooleanSetting("Random", "The random option causes the slides to be shown in random order, rather than sequential. Default is false.", true, false)]
    public string IsRandomSetting
    {
      get
      {
        return this.Setting("IsRandom", "false", false);
      }
    }

    //[BooleanSetting("Show Navigation", "Determines whether the small navigation graphic will appear in the bottom right corner of the Slider.", true, true)]
    //public string ShowNavigationSetting
    //{
    //  get
    //  {
    //    return this.Setting("ShowNavigation", "true", true);
    //  }
    //}

    [TextSetting("Background Color", "Hex value of the background color of the Slider movie.", true)]
    public string BgColorSetting
    {
      get
      {
        return this.Setting("BgColor", "", true);
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      int intCampus;
      if (!int.TryParse(CampusSetting, out intCampus))
      {
        intCampus = -1;
      }

      int intMaxItems;
      if (!int.TryParse(MaxItemsSetting, out intMaxItems))
      {
        intMaxItems = -1;
      }

      int intImageType;
      if (!int.TryParse(ImageTypeSetting, out intImageType))
      {
        intImageType = -1;
      }

      bool boolEventsOnly;
      if (!Boolean.TryParse(EventsOnlySetting, out boolEventsOnly))
      {
        boolEventsOnly = false;
      }

      string sliderID = this.ClientID + "_slider";

      PromotionRequestCollection requestCollection = new PromotionRequestCollection();
      requestCollection.LoadCurrentWebRequests(TopicAreaIDSetting, "both", intCampus, intMaxItems, boolEventsOnly, intImageType);

      // Paging and Navigation code is commented out:
      //this.phSlider.Controls.Add((Control)new LiteralControl(string.Format("<div id='nav_{0}' class='nav' style='display: none; position: absolute; z-index: 1000; margin: 0 padding: 3px; background-color: #ffffff; border: 1px solid #dddddd;' />")));
      this.phSlider.Controls.Add((Control)new LiteralControl(string.Format("<div id='{0}' class='slider-outer'>\n", sliderID)));

      foreach (PromotionRequest promoRequest in requestCollection)
      {
        //string strTarget = "_parent"; // promoRequest.WebExternalLink == promoRequest.DetailUrl("", DetailPageSetting, EventDetailPageSetting) ? "_blank" : "_parent";
        string strTarget = promoRequest.WebExternalLink == promoRequest.DetailUrl("", DetailPageSetting, EventDetailPageSetting) ? "_blank" : "_parent";

        this.phSlider.Controls.Add((Control)new LiteralControl(string.Format("<div class='item'>\n")));
        this.phSlider.Controls.Add((Control)new LiteralControl(string.Format("\t<a href='{0}' target='{1}'>\n", promoRequest.DetailUrl("", DetailPageSetting, EventDetailPageSetting), strTarget)));
        this.phSlider.Controls.Add((Control)new LiteralControl(string.Format("\t\t<img src='/CachedBlob.aspx?guid={0}' alt='{1}' style='width: 100%; {2}'>\n", promoRequest.Documents.GetFirstByType(intImageType).GUID, promoRequest.Title, Convert.ToBoolean(IsStretchHeightSetting) ? "height: 100%;" : "")));
        this.phSlider.Controls.Add((Control)new LiteralControl("\t</a>\n"));
        this.phSlider.Controls.Add((Control)new LiteralControl("</div>\n"));
      }
      this.phSlider.Controls.Add((Control)new LiteralControl("</div><!-- /.slider-outer -->\n"));

      StringBuilder sliderScript = new StringBuilder();
      sliderScript.Append("$(document).ready(function(){\n");

      // Paging and Navigation code is commented out:
      //sliderScript.Append(string.Format("\t\t$(\'#{0}\').hover(\n", sliderID));
      //sliderScript.Append(string.Format("\t\tfunction() {{ $(\'#nav_{0}\').fadeIn(); }},\n", sliderID));
      //sliderScript.Append(string.Format("\t\tfunction() {{ $(\'#nav_{0}\').fadeOut(); }}\n", sliderID));
      //sliderScript.Append(");\n\n");

      sliderScript.Append(string.Format("$(\"#{0}\").cycle({{\n", sliderID, sliderID));
      sliderScript.Append(string.Format("fx: \'{0}\',\n", SliderTransitionSetting));
      sliderScript.Append(string.Format("speed: {0},\n", SpeedSetting));
      sliderScript.Append(string.Format("timeout: {0},\n", TimeoutSetting));
      sliderScript.Append(string.Format("pause: {0},\n", IsPauseSetting));
      sliderScript.Append("fit: 1,\n");
      sliderScript.Append(string.Format("width: {0},\n", SliderWidthSetting));
      sliderScript.Append(string.Format("height: {0},\n", SliderHeightSetting));
      sliderScript.Append(string.Format("random: {0}\n", IsRandomSetting));
      // Paging and Navigation code is commented out:
      //sliderScript.Append(string.Format("\t\tpager: \'#nav_{0}\'\n", sliderID));
      sliderScript.Append("\t});\n");
      sliderScript.Append("});\n");

      Page.ClientScript.RegisterStartupScript(this.GetType(), sliderID, sliderScript.ToString(), true);
    }
  }
}