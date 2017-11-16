using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Syndication;
using System.Xml;
using IndignadoFramework.Entities;
using Google.GData.YouTube;
using Google.YouTube;
using Google.GData.Client;
using System.Text.RegularExpressions;


namespace IndignadoFramework.UI.Mvc4.Models
{
    public static class FuentesDatos
    {
        private static string clave = "AI39si7IJH8QEKSNG3gHPGNBc4abICJ7qsJqbEFiSxC0aDR8cPAB944mYj96oUJ4WliRNJ8JNIKnBKvvrARb5h3esBPeYAlsZg";

        public static List<WebFeed> ReadFeed(FuenteWEB[] fuentes, string tematica)
        {
            List<WebFeed> lwf = new List<WebFeed>();
            try
            {

                foreach (FuenteWEB fweb in fuentes)
                {

                    if (fweb.Tipo.Equals("RSS"))
                    {
                        SyndicationFeedFormatter formatter = new Rss20FeedFormatter();
                        lwf.Add(ReadFeedRA(fweb.Url, formatter, tematica, true, true));
                    }
                    else if (fweb.Tipo.Equals("ATOM"))
                    {
                        SyndicationFeedFormatter formatter = new Atom10FeedFormatter();
                        lwf.Add(ReadFeedRA(fweb.Url, formatter, tematica, true, false));
                    }
                    else if (fweb.Tipo.Equals("YOUTUBE"))
                    {
                        lwf.Add(ReadFeedYT(fweb.Url, tematica));
                    }
                }
                return lwf;
            }
            catch (Exception ex)
            {
                return lwf;
            }
        }

        public static WebFeed ReadFeedRA(string url, SyndicationFeedFormatter formatter, string tematica, bool filtrar,bool RSS)
        {
            try
            {
                if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                    url = string.Format("http://{0}", url);

                using (XmlReader reader = XmlReader.Create(url))
                {
                    // le decimos al formateador que analice el XML del feed. 
                    formatter.ReadFrom(reader);
                }

                WebFeed webFeed = new WebFeed
                {
                    Title = formatter.Feed.Title.Text,
                    Description = formatter.Feed.Description.Text,
                    Language = formatter.Feed.Language,
                    LastUpdatedTime = formatter.Feed.LastUpdatedTime.DateTime,
                    Nodes = new List<WebFeedNode>()
                };

                if (RSS)
                    webFeed.Tipo = WebFeed.TipoFuente.RSS;
                else
                    webFeed.Tipo = WebFeed.TipoFuente.ATOM;

                if (formatter.Feed.Copyright != null)
                    webFeed.Copyright = formatter.Feed.Copyright.Text;

                foreach (SyndicationItem item in formatter.Feed.Items)
                {
                    if (itemRelacionado(item, tematica) || !filtrar)
                    {
                        webFeed.Nodes.Add(new WebFeedNode
                        {
                            Title = item.Title.Text,
                            PublishDate = item.PublishDate.DateTime,
                            LastUpdateTime = item.LastUpdatedTime.DateTime,
                            Summary = item.Summary.Text,
                            Link = item.Links.First().Uri.ToString()
                        });
                    }

                }

                return webFeed;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Filtrado de items de una url particular
        // Se incluye si el titulo o la descripcion contienen la cadena tematica
        private static bool itemRelacionado(SyndicationItem item, string tematica)
        {
            return (((RemoveAccentsWithRegEx(item.Title.Text)).IndexOf(tematica, StringComparison.OrdinalIgnoreCase) >= 0) || (RemoveAccentsWithRegEx(item.Summary.Text).IndexOf(tematica, StringComparison.OrdinalIgnoreCase) >= 0));
        }

        public static WebFeed ReadFeedYT(string url, string tematica)
        {
            YouTubeRequestSettings settings = new YouTubeRequestSettings("IndignadoFramework", clave);
            YouTubeRequest request = new YouTubeRequest(settings);

            //YouTubeQuery query = new YouTubeQuery(url);
            YouTubeQuery query = new YouTubeQuery(YouTubeQuery.DefaultVideoUri);

            //order results by the number of views (most viewed first)
            query.OrderBy = "relevance";

            // search for puppies and include restricted content in the search results
            // query.SafeSearch could also be set to YouTubeQuery.SafeSearchValues.Moderate
            query.Query = tematica;
            query.SafeSearch = YouTubeQuery.SafeSearchValues.None;

            Feed<Video> videoFeed = request.Get<Video>(query);

            WebFeed wf = new WebFeed
            {
                Title = "You Tube",
                Tipo = WebFeed.TipoFuente.VIDEO,
                Nodes = new List<WebFeedNode>()
            };

            foreach(Video video in videoFeed.Entries)
            {
                wf.Nodes.Add(new WebFeedNode()
                {
                    Link = video.WatchPage.ToString(),
                    ThumbnailUrl = video.Thumbnails.First().Url.ToString(),
                    Updated = video.Updated.ToString(),
                    Title = video.Title
                });

            }

            return wf;

        }

        public static string RemoveAccentsWithRegEx(string inputString)
        {
            Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
            Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
            Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
            Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
            Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
            inputString = replace_a_Accents.Replace(inputString, "a");
            inputString = replace_e_Accents.Replace(inputString, "e");
            inputString = replace_i_Accents.Replace(inputString, "i");
            inputString = replace_o_Accents.Replace(inputString, "o");
            inputString = replace_u_Accents.Replace(inputString, "u");
            return inputString;
        }

    }
}