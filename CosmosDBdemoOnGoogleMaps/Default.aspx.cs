using Subgurim.Controles;
using Subgurim.Controles.GoogleChartIconMaker;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Configuration;

namespace CosmosDBdemoOnGoogleMaps
{
    public partial class _Default : Page
    {
        public DocumentClient client;
        IQueryable<JsonData> locationQuery;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConfigureGoogleMap(ConnectAndQuery(client));
            }
        }
        protected string FormatChildren(string[] children)
        {
            string html = string.Empty;
            if (children != null && children.Length > 0)
            {
                foreach (string s in children)
                {
                    html += "<div>" + s + "</div>";
                }
            }
            return html;
        }
        protected IQueryable<JsonData> ConnectAndQuery(DocumentClient client)
        {
            string EndpointUrl = ConfigurationManager.AppSettings["EndpointUrl"];
            string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];
            client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
            locationQuery = client.CreateDocumentQuery<JsonData>(UriFactory.CreateDocumentCollectionUri("locations", "location1"), queryOptions);
            return locationQuery;

        }
        protected bool ConfigureGoogleMap(IQueryable<JsonData> locationQuery)
        {
            PinIcon p;
            GMarker gm;
            GInfoWindow win;
            GLatLng mainLocation = new GLatLng(0.0, 0.0);
            GMap1.setCenter(mainLocation, 2);
            XPinLetter xpinLetter = new XPinLetter(PinShapes.pin_star, "H", Color.Blue, Color.White, Color.Chocolate);
            GMap1.Add(new GMarker(mainLocation, new GMarkerOptions(new GIcon(xpinLetter.ToString(), xpinLetter.Shadow()))));
            foreach (var location in locationQuery)
            {
                p = new PinIcon(PinIcons.home, Color.Cyan);
                gm = new GMarker(new GLatLng(Convert.ToDouble(location.LocLat), Convert.ToDouble(location.LocLong)),
                                            new GMarkerOptions(new GIcon(p.ToString(), p.Shadow())));
                win = new GInfoWindow(gm, location.name + " <a href='" + location.ReadMoreUrl + "'>Read more...</a>" + FormatChildren(location.children) + (!string.IsNullOrEmpty(location.imageUrl) ? "<img src=\"" + location.imageUrl + "\" />" : ""), false, GListener.Event.mouseover);
                GMap1.Add(win);
            }
            return true;
        }
    }

    //Class to define JSON structure to query Cosomos DB.
    public class JsonData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string LocLat { get; set; }
        public string LocLong { get; set; }
        public string ReadMoreUrl { get; set; }
        public string[] children { get; set; }
        public string imageUrl { get; set; }

    }



}