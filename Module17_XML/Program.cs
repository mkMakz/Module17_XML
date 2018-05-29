using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;


namespace Module17_XML

{
    /*1.	Прочитать содержимое XML файла со списком последних новостей по ссылке https://habrahabr.ru/rss/interesting/
Создать класс Item со свойствами: Title, Link, Description, PubDate.
Создать коллекцию типа List<Item> и записать в нее данные из файла.
2.	На основании задания 1, сериализовать лист полученных объектов в XML и записать в файл.
*/
    class Program
    {
        public class HabrNews
        {
            public string title { get; set; }
            public string Link { get; set; }
            public string Description { get; set; }
            public DateTime PubDate { get; set; }
           
        }
        static void Main(string[] args)
        {
            //XmlDocument doc = new XmlDocument();

            //XmlElement root = doc.CreateElement("User");
            //XmlElement name = doc.CreateElement("Name");
            //name.InnerText = "Alex";
            //XmlAttribute atr = doc.CreateAttribute("IIin");
            //atr.InnerText = "5675657656757";
            ////1
            //name.Attributes.Append(atr);
            //// orr 2
            ////name.SetAttribute("Iin", "5675657656757");
            //root.AppendChild(name);
            //doc.AppendChild(root);
            //doc.Save("UserAlex.xml");


            /*1.	Прочитать содержимое XML файла со списком последних новостей по ссылке https://habrahabr.ru/rss/interesting/
Создать класс Item со свойствами: Title, Link, Description, PubDate.
Создать коллекцию типа List<Item> и записать в нее данные из файла.
2.	На основании задания 1, сериализовать лист полученных объектов в XML и записать в файл.
*/

            List<HabrNews> habr = new List<HabrNews>();

            XmlDocument doc = new XmlDocument();
            doc.Load("https://habrahabr.ru/rss/interesting/");

            var rootElement = doc.DocumentElement;
            foreach (XmlNode item in rootElement.ChildNodes)
            {
                Console.WriteLine(">" +item.Name);
                foreach (XmlNode ch in item.ChildNodes)
                {
                    Console.WriteLine("->" + ch.Name);
                    if (ch.Name == "item")
                    {
                        HabrNews hn = new HabrNews();

                        foreach (XmlNode i in ch.ChildNodes)
                        {
                            if(i.Name == "title" )
                            {
                                hn.title = i.InnerText;
                            }
                            else if(i.Name == "link")
                            {
                                hn.Link = i.InnerText;
                            }
                            else if (i.Name == "description")
                            {
                                hn.Description = i.InnerText;
                            }
                            else if (i.Name == "pubDate")
                            {
                                hn.PubDate = DateTime.Parse(i.InnerText);
                            }
                    
                            // Console.WriteLine("--->" + i.Name);
                            // Console.WriteLine("------>" + i.InnerText);
                        }
                        habr.Add(hn);
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine();

            foreach (var item in habr)
            {
                Console.WriteLine(item.title);
                Console.WriteLine(item.Link);
                Console.WriteLine("");
            }

            Directory.CreateDirectory(@"C:\ListDir\");

            FileInfo fi = new FileInfo(@"C:\ListDir\List.txt");
          
            FileStream fst = fi.Create(); 
            fst.Close();

            StreamWriter sw = new StreamWriter(@"C:\ListDir\List.txt");
            foreach (var item in habr)
            {
                sw.Write(item.title);
                sw.Write(item.Link);
                sw.Write(item.Description);
                sw.Write(item.PubDate);
                sw.WriteLine("\n");
            }

            sw.Close();

        }
    }
}
