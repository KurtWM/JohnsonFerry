<%@ WebService Language="C#" Class="PromotionService" %>

//using Arena.Core;
//using Arena.DataLayer.Core;
//using Arena.Enums;
//using Arena.Marketing;
//using Arena.Security;
//using Arena.Services;
//using Arena.Services.Contracts;
//using Arena.Services.Exceptions;
//using Arena.Utility;
//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Data;
//using System.Data.SqlClient;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.ServiceModel;
//using System.ServiceModel.Activation;
//using System.ServiceModel.Web;
//using System.Web;
//using System.Web.Services;
//using System.Web.Services.Protocols;
//using System.Web.Script.Serialization;

using Arena.Core;
using Arena.Marketing;
using Arena.Services;
using Arena.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using System.Linq;



[WebService(Namespace = "http://johnsonferry.org/webservices/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class PromotionService : System.Web.Services.WebService
{

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public PromotionRequestListResult GetPromotionsJSON(string topicAreasList, string areaFilter, int campusId, int maxItems, bool eventsOnly, int documentTypeId)
    {
        return this.GetPromotions(topicAreasList, areaFilter, campusId, maxItems, eventsOnly, documentTypeId);
    }

    public PromotionRequestListResult GetPromotions(string topicAreasList, string areaFilter, int campusId, int maxItems, bool eventsOnly, int documentTypeId)
    {
        PromotionRequestCollectionMapper collectionMapper = new PromotionRequestCollectionMapper(new PromotionRequestMapper());
        int result = -1;
        if (!Enumerable.Contains<char>((IEnumerable<char>)topicAreasList, ',') && !int.TryParse(topicAreasList, out result))
            throw new ArgumentOutOfRangeException("The topicAreasList parameter must be an integer or a comma delimited list of integers");
        string str = topicAreasList;
        char[] chArray = new char[1]
    {
    ','
    };
        foreach (string s in str.Split(chArray))
        {
            if (!int.TryParse(s, out result))
                throw new ArgumentOutOfRangeException("The topicAreasList parameter must be an integer or a comma delimited list of integers");
        }
        PromotionRequestCollection requestCollection = new PromotionRequestCollection();
        requestCollection.LoadCurrentWebRequests(topicAreasList ?? "", areaFilter ?? "", campusId == 0 ? -1 : campusId, maxItems, eventsOnly, documentTypeId == 0 ? -1 : documentTypeId);
        PromotionRequestListResult requestListResult = new PromotionRequestListResult();
        requestListResult.Promotions = collectionMapper.FromArena((IEnumerable<Arena.Marketing.PromotionRequest>)requestCollection);
        return requestListResult;
    }
    
    
    
}
