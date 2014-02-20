namespace ArenaWeb.UserControls.custom.johnsonferry
{
    using Arena.Portal;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml;
    using System.Xml.XPath;

    public partial class GetPromotionsAPI : PortalControl
    {
        public string arena_api_url = "https://arenadev.jfbc.org/api.svc/";
        public string url_of_the_request = "";
        public string url_with_api_secret = "";
        public string api_sig = "";
        public string api_session = "";
        public string api_session_key = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Each request (except /help and /login) requires valid api_session and api_sig parameters.
            // https://example.org/Arena/api.svc/person/1234?api_session=MYSESSIONKEY&api_sig=MYSIGNATURE

            // See Arena API help page for explanation: https://example.org/Arena/api.svc/help or https://example.org/api.svc/help
            api_session = GetApiSession
            (
                ConfigurationManager.AppSettings["Arena_Api_User"],          // Add "Arena_Api_User" appSettings key in web.config
                ConfigurationManager.AppSettings["Arena_Api_User_Password"], // Add "Arena_Api_User_Password" appSettings key in web.config
                ConfigurationManager.AppSettings["Arena_ApiKey_Promotions"]  // Add "Arena_ApiKey_Promotions" appSettings key in web.config
            );

            api_session_key = GetSessionIdFromXml(api_session);


            // How to calculate API signature:
            // 1. Take the URL of the request (start after https://example.org/Arena/api.svc/) and lower case it.
            // NOTE: This URL is an example for retrieving Promotions from a specific TopicArea in the johnsonferry development site. 
            // It will need to be customized to work with your web site.
            url_of_the_request = "promotion/list?topicAreasList=13940&".ToLower() +
                "areaFilter=both&".ToLower() +
                "campusId=1&".ToLower() +
                "maxItems=3&".ToLower() +
                "eventsOnly=false&".ToLower() + 
                "api_session=" + api_session_key;

            // 2. Combine your application's API Secret with the string from above with the "_" character in between.
            url_with_api_secret = ConfigurationManager.AppSettings["Arena_ApiSecret_Promotions"] + "_" + url_of_the_request;

            // 3. Calculate the MD5 hash of the above string using UTF8 encoding to get your api_sig value. The value should be lowercase.
            api_sig = GetMd5Hash(url_with_api_secret);

            // 4. Append the api_sig to the request URL and send your request.
            //    i.e.: https://example.org/Arena/api.svc/person/248983?fields=FirstName,LastName&api_session=d8e18cee-76aa-42e1-b955-49827b863085&api_sig=a8e46e0569561bdb72513b2cf5a89da6
            ApiHyperLink.NavigateUrl = (arena_api_url + url_of_the_request + "&api_sig=" + api_sig);
            ApiHyperLink.Text = (arena_api_url + url_of_the_request + "&api_sig=" + api_sig);
            ApiHyperLink.Target = "_blank";
        }

        /// <summary>
        /// Get an api_session key, by sending an HTTP POST request to /login with a Content-Type of application/x-www-form-urlencoded. 
        /// </summary>
        /// <param name="username">a valid x-www-form-urlencoded Arena username</param>
        /// <param name="password">a valid x-www-form-urlencoded Arena password</param>
        /// <param name="api_key">a x-www-form-urlencoded identifier for the calling application. The application must be set up in Arena where you'll obtain an API Secret used to calculate api_sig.</param>
        /// <returns></returns>
        private string GetApiSession(string username, string password, string api_key)
        {
            try
            {
                api_session = GetPostResponse(arena_api_url + "login",
                    "?username=" + username +
                    "&password=" + password +
                    "&api_key=" + api_key);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("GetPostResponse() Error: " + ex.Message);
            }
            return api_session;
        }

        /// <summary>
        /// Get the SessionID value from the API session data.
        /// </summary>
        /// <param name="xml">API session data</param>
        /// <returns></returns>
        private string GetSessionIdFromXml(string xml)
        {
            string strSessionId = "";
            try
            {
                // Create an XmlNamespaceManager to resolve the default namespace.
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);

                // Select the SessionID xml node and get its value.
                XmlNode sessionid;
                XmlElement root = doc.DocumentElement;
                sessionid = root.SelectSingleNode("SessionID", nsmgr);
                strSessionId = sessionid.InnerText;
            }
            catch (XmlException ex)
            {
                ShowErrorMessage(string.Format("XmlDocument.LoadXml Error: {0}", ex));
            }
            catch (NullReferenceException ex)
            {
                ShowErrorMessage(string.Format("XmlNamespaceManager Error: {0}", ex));
            }
            catch (ArgumentNullException ex)
            {
                ShowErrorMessage(string.Format("XmlNamespaceManager.AddNameSpace Error: {0}", ex));
            }
            catch (ArgumentException ex)
            {
                ShowErrorMessage(string.Format("XmlNamespaceManager.AddNameSpace Error: {0}", ex));
            }
            catch (XPathException ex)
            {
                ShowErrorMessage(string.Format("XmlNamespaceManager Error: {0}", ex));
            }
            catch (Exception ex)
            {
                ShowErrorMessage(string.Format("XmlNamespaceManager Error: {0}", ex));
            }
            return strSessionId;
        }

        /// <summary>
        /// Return response string based on a given url and querystring.
        /// Example from: http://stackoverflow.com/questions/10797995/send-http-post-request-through-asp-net
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        private string GetPostResponse(string url, string postData)
        {
            // create the POST request
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            //webRequest.ContentType = "application/json; charset=utf-8";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = postData.Length;

            // POST the data
            using (StreamWriter requestWriter2 = new StreamWriter(webRequest.GetRequestStream()))
            {
                requestWriter2.Write(postData);
            }

            //  This actually does the request and gets the response back
            HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();

            string responseData = string.Empty;

            using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                // dumps the HTML from the response into a string variable
                responseData = responseReader.ReadToEnd();
            }

            return responseData;
        }

        /// <summary>
        /// Example from: http://sharp-coders.com/microsoft-net/c-sharp/calculate-md5-hash-in-c-sharp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private void ShowErrorMessage(string message)
        {
            ErrorMsg.Text += "<p>" + message + "</p>";
            ErrorMsg.Visible = true;
        }
    }
}