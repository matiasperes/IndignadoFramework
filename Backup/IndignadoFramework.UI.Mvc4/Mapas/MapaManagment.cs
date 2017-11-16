using IndignadoFramework.UI.Mvc4.ImageryService;
using IndignadoFramework.UI.Mvc4.GeocodeService;

namespace IndignadoFramework.UI.Mvc4.Mapas
{
    public class MapaManagment
    {

        private GeocodeService.Location GeocodeAddress(string address)
        {
            GeocodeRequest geocodeRequest = new GeocodeRequest();
            // Set the credentials using a valid Bing Maps Key
            geocodeRequest.Credentials = new GeocodeService.Credentials();
            geocodeRequest.Credentials.ApplicationId = "AjKX5fPclkvH3YTYhLImWMour1KISHMrcFVBrXzVsjqwMLiWobOq83esCN1ra0Q0";
            // Set the full address query
            geocodeRequest.Query = address;

            // Set the options to only return high confidence results 
            ConfidenceFilter[] filters = new ConfidenceFilter[1];
            filters[0] = new ConfidenceFilter();
            filters[0].MinimumConfidence = GeocodeService.Confidence.High;
            GeocodeOptions geocodeOptions = new GeocodeOptions();
            geocodeOptions.Filters = filters;
            geocodeRequest.Options = geocodeOptions;
            // Make the geocode request
            GeocodeServiceClient geocodeService = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
            GeocodeResponse geocodeResponse = geocodeService.Geocode(geocodeRequest);

            if (geocodeResponse.Results.Length > 0)
                if (geocodeResponse.Results[0].Locations.Length > 0)
                    return geocodeResponse.Results[0].Locations[0];
            return null;
        }

        public string GetMapUri(double latitude, double longitude, int zoom, string mapStyle, int width, int height)
        {
            ImageryService.Pushpin[] pins = new ImageryService.Pushpin[1];
            ImageryService.Pushpin pushpin = new ImageryService.Pushpin();
            pushpin.Location = new ImageryService.Location();
            pushpin.Location.Latitude = latitude;
            pushpin.Location.Longitude = longitude;
            pushpin.IconStyle = "2";
            pins[0] = pushpin;
            MapUriRequest mapUriRequest = new MapUriRequest();
            // Set credentials using a valid Bing Maps Key
            mapUriRequest.Credentials = new ImageryService.Credentials();
            mapUriRequest.Credentials.ApplicationId = "AjKX5fPclkvH3YTYhLImWMour1KISHMrcFVBrXzVsjqwMLiWobOq83esCN1ra0Q0";

            // Set the location of the requested image
            mapUriRequest.Pushpins = pins;
            // Set the map style and zoom level
            MapUriOptions mapUriOptions = new MapUriOptions();
            //mapUriOptions.ZoomLevel = 17;
            switch (mapStyle.ToUpper())
            {
                case "HYBRID":
                    mapUriOptions.Style = ImageryService.MapStyle.AerialWithLabels;
                    break;
                case "ROAD":
                    mapUriOptions.Style = ImageryService.MapStyle.Road;
                    break;
                case "AERIAL":
                    mapUriOptions.Style = ImageryService.MapStyle.Aerial;
                    break;
                default:
                    mapUriOptions.Style = ImageryService.MapStyle.Road;
                    break;
            }

            mapUriOptions.ZoomLevel = 10;
            // Set the size of the requested image to match the size of the image control
            mapUriOptions.ImageSize = new ImageryService.SizeOfint();
            mapUriOptions.ImageSize.Height = height;
            mapUriOptions.ImageSize.Width = width;
            mapUriRequest.Options = mapUriOptions;

            ImageryServiceClient imageryService = new ImageryServiceClient("BasicHttpBinding_IImageryService");
            MapUriResponse mapUriResponse = imageryService.GetMapUri(mapUriRequest);

            return mapUriResponse.Uri;
        }


        public string MapAddress(string address, int zoom, string mapStyle, int width, int height)
        {
            GeocodeService.Location latlong = GeocodeAddress(address);
            double latitude = latlong.Latitude;
            double longitude = latlong.Longitude;
            return GetMapUri(latitude, longitude, zoom, mapStyle, width, height);
        }
        
        


    }
}