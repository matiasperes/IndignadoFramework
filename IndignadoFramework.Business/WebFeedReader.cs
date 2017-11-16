using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Syndication;
using System.Xml;
using IndignadoFramework.Entities;

namespace IndignadoFramework.Business
{
    public static class WebFeedReader
    {

        public static WebFeed ReadFeed(string url, string tipo)
        {
            try
            {
                if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                    url = string.Format("http://{0}", url);

                SyndicationFeedFormatter formatter = null;
                
                if (tipo.Equals("RSS"))
                    formatter = new Rss20FeedFormatter();
                else if (tipo.Equals("ATOM"))
                    formatter = new Atom10FeedFormatter();
                
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

                //if (formatter.Feed.Copyright != null)
                //    webFeed.Copyright = formatter.Feed.Copyright.Text;

                foreach (SyndicationItem item in formatter.Feed.Items)
                {
                    StringBuilder text = new StringBuilder().AppendFormat(item.Summary.Text);
                    string summary = text.ToString();
                    if (summary.Length > 256)
                        summary = summary.Remove(256);
                    
                    webFeed.Nodes.Add(new WebFeedNode
                    {
                        Title = item.Title.Text,
                        PublishDate = item.PublishDate.DateTime,
                        LastUpdateTime = item.LastUpdatedTime.DateTime,
                        Summary = summary,
                        Link = item.Links.First().Uri.ToString()
                    });

                    
                }

                return webFeed;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new WebFeed{Nodes = new List<WebFeedNode>()};
            }
        }

        public static WebFeed ReadFeedFiltrado(string url, string tipo, string tematica)
        {
            try
            {
                if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                    url = string.Format("http://{0}", url);

                SyndicationFeedFormatter formatter = null;

                if (tipo.Equals("RSS"))
                    formatter = new Rss20FeedFormatter();
                else if (tipo.Equals("ATOM"))
                    formatter = new Atom10FeedFormatter();

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
                    Copyright = formatter.Feed.Copyright.Text,
                    Nodes = new List<WebFeedNode>()
                };

                foreach (SyndicationItem item in formatter.Feed.Items)
                {
                    if (itemRelacionado(item,tematica))
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
            return (item.Title.Text.Contains(tematica) || item.Summary.Text.Contains(tematica));
        }

    }
}
