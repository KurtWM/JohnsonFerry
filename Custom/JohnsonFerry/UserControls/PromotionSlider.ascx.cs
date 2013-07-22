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
        return this.Setting("IsStretchHeight", "false", true);
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

    public int intCampus;
    public int intMaxItems;
    public int intImageType;
    public bool boolEventsOnly;
    public string sliderID;
    public bool boolIsFit;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!int.TryParse(CampusSetting, out intCampus))
      {
        intCampus = -1;
      }

      if (!int.TryParse(MaxItemsSetting, out intMaxItems))
      {
        intMaxItems = -1;
      }

      if (!int.TryParse(ImageTypeSetting, out intImageType))
      {
        intImageType = -1;
      }

      if (!Boolean.TryParse(EventsOnlySetting, out boolEventsOnly))
      {
        boolEventsOnly = false;
      }

      sliderID = this.ClientID + "_slider";

      WriteSlider();
    }

    void WriteSlider()
    {

      PromotionRequestCollection requestCollection = new PromotionRequestCollection();
      requestCollection.LoadCurrentWebRequests(TopicAreaIDSetting, "both", intCampus, intMaxItems, boolEventsOnly, intImageType);

      this.phSlider.Controls.Add((Control)new LiteralControl(string.Format("<div id='{0}' class='slider-outer'>\n", sliderID)));

      foreach (PromotionRequest promoRequest in requestCollection)
      {
        string strTarget = promoRequest.WebExternalLink == promoRequest.DetailUrl("", DetailPageSetting, EventDetailPageSetting) ? "_blank" : "_parent";
        try
        {
          boolIsFit = Convert.ToBoolean(IsStretchHeightSetting);
        }
        catch (FormatException)
        {
          Console.WriteLine("Unable to convert '{0}' to a Boolean.", IsStretchHeightSetting);
        }
        //this.phSlider.Controls.Add((Control)new LiteralControl("\t<div class='slider-slide' style='display: none;'>\n"));
        this.phSlider.Controls.Add((Control)new LiteralControl(string.Format("\t\t<a href='{0}' target='{1}' title='{2}' style='display: none;'>\n", promoRequest.DetailUrl("", DetailPageSetting, EventDetailPageSetting), strTarget, promoRequest.Title)));
        try
        {
          this.phSlider.Controls.Add((Control)new LiteralControl(string.Format("\t\t\t<img src='/CachedBlob.aspx?guid={0}' alt='{1}' style='width: 100%;{2}' />\n", promoRequest.Documents.GetFirstByType(intImageType).GUID, promoRequest.Title, boolIsFit ? " height: 100%;" : "")));
        }
        catch (System.NullReferenceException err)
        {
          Console.WriteLine("Error writing image tag. Message = {0}", err.Message);
        }
        this.phSlider.Controls.Add((Control)new LiteralControl("\t\t</a>\n"));
        //this.phSlider.Controls.Add((Control)new LiteralControl("\t</div>\n"));
      }
      this.phSlider.Controls.Add((Control)new LiteralControl("</div><!-- /.slider-outer -->\n"));

      
      
      this.phSlider.Controls.Add((Control)new LiteralControl("<div id='churchonline_counter'>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t<div class='live'>Live Now</div>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t<div class='info'>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t\t<div class='title'></div>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t\t<div class='description'></div>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t</div>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t<ul class='time'>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t\t<li><span class='days'></span>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t\t\t<span class='label'>days</span>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t\t</li>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t\t<li><span class='hours'></span>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t\t\t<span class='label'>hrs</span>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t\t</li>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t\t<li><span class='minutes'></span>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t\t\t<span class='label'>mins</span>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t\t</li>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t\t<li><span class='seconds'></span>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t\t\t<span class='label'>secs</span>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t\t</li>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("\t</ul>\n"));
      this.phSlider.Controls.Add((Control)new LiteralControl("</div>\n"));
      
      
      this.phSlider.Controls.Add((Control)new LiteralControl("<div id='output'></div>\n"));

      StringBuilder sliderScript = new StringBuilder();
      sliderScript.Append("$(window).load(function(){\n");
      sliderScript.Append(string.Format("\t$(\"#{0}\").cycle({{\n", sliderID, sliderID));
      sliderScript.Append(string.Format("\t\tfx: \'{0}\',\n", SliderTransitionSetting));
      sliderScript.Append(string.Format("\t\tspeed: {0},\n", SpeedSetting));
      sliderScript.Append(string.Format("\t\ttimeout: {0},\n", TimeoutSetting));
      sliderScript.Append(string.Format("\t\tpause: '{0}',\n", IsPauseSetting));
      sliderScript.Append(string.Format("\t\tfit: '{0}',\n", IsStretchHeightSetting));
      sliderScript.Append(string.Format("\t\twidth: '{0}',\n", SliderWidthSetting));
      if (SliderHeightSetting.Length > 0)
      {
        sliderScript.Append(string.Format("\t\theight: '{0}',\n", SliderHeightSetting));
      }
      sliderScript.Append("\t\tafter: onAfter,\n");
      sliderScript.Append("\t\tslideResize: '0',\n");
      sliderScript.Append("\t\tcontainerResize: '0',\n");
      sliderScript.Append(string.Format("\t\trandom: {0}\n", IsRandomSetting));
      sliderScript.Append("\t});\n");


      sliderScript.Append("function onAfter() {\n");
      sliderScript.Append("$('#output').html(\"Scroll complete for:<br>\" + this.href)\n");
      sliderScript.Append(".append('<h3>' + this.title + '</h3>');\n");
      sliderScript.Append("}\n");

      sliderScript.Append("$('#churchonline_counter').css({'position':'absolute','top':$('.slider-outer').position().top,'right':$('.slider-outer').css('right'),'z-index':'100'});\n");
      sliderScript.Append("countdowntimer('http://jfcontemporary.churchonline.org/event_times/next');\n");

      sliderScript.Append("});\n");

      Page.ClientScript.RegisterStartupScript(this.GetType(), sliderID, sliderScript.ToString(), true);


    }
  }
}