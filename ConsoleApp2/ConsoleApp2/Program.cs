using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using CBR;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Request request = new Request();
            request.initProgram();
            Console.WriteLine("Hello World!");
        }
    }
    class Request
    {

        public Request()
        {

        }

        public void initProgram()
        {
            DailyInfoSoapClient dailyInfoSoapClient = new DailyInfoSoapClient(DailyInfoSoapClient.EndpointConfiguration.DailyInfoSoap);
            var data = dailyInfoSoapClient.EnumValutes(false);


            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.Async = false;
            readerSettings.MaxCharactersInDocument = 0;
            XmlReader xmlReader = XmlReader.Create(this.getXmlStream(data.Nodes[1].ToString()));

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Diffgr));
            Diffgr entity = (Diffgr)xmlSerializer.Deserialize(xmlReader);
            Console.WriteLine("Hello World!");


        }

        private Stream getXmlStream(string _node)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(_node);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }
    }

    public class EnumValutes
    {
        public string Vcode { get; }
        public string Vname { get; }
        public string VEngname { get; }
        public string Vnom { get; }
        public string VcommonCode { get; }
        public string VnumCode { get; }
        public string VcharCode { get; }
    }

    public class ValuteData
    {
        [XmlArrayAttribute("EnumValutes")]
        public EnumValutes[] EnumValutes { get; }
    }

    [XmlRoot(ElementName = "diffgram", Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1")]
    public class Diffgr
    {
        [XmlArrayAttribute("ValuteData")]
        public ValuteData[] ValuteData { get; }
    }

}
