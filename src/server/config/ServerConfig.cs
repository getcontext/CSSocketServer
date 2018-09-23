namespace cssocketserver.server.config {


using server.utils.FileUtils;

using java.io.File;

using javax.xml.parsers.DocumentBuilder;
using javax.xml.parsers.DocumentBuilderFactory;
using javax.xml.parsers.ParserConfigurationException;

using org.w3c.dom.Document;
using org.w3c.dom.Element;
using org.w3c.dom.Node;
using org.w3c.dom.NodeList;

using org.xml.sax.SAXException;
using org.xml.sax.SAXParseException;


public class ServerConfig: Config {
    private Map<string, string> parameters = new Dictionary<string, string>();

    public ServerConfig(string file) {
        read(file);
    }

    protected void read(string file) {
        try {
            string rootDir = "";
            try {
                rootDir =
                        getClass().getProtectionDomain().getCodeSource().getLocation().getPath().replace("%20", " ");
                rootDir = new File(rootDir).getParent();
            } catch (Exception e) {
                //throw e;
            }

            file = rootDir + FileUtils.FILE_SEPARATOR + file;

            DocumentBuilder docBuilder = DocumentBuilderFactory.newInstance().newDocumentBuilder();
            Document doc = docBuilder.parse(new File(file));
            doc.getDocumentElement().normalize();
            NodeList childNodes = doc.getChildNodes();

            parameters.clear();
            for (int s = 0; s < childNodes.getLength(); s++) {
                Node item = childNodes.item(s);
                if (item.getNodeType() == Node.ELEMENT_NODE) {
                    Element element = (Element) item;
                    NodeList tagName = element.getElementsByTagName("port");
                    parameters.put("port", tagName.item(0).getChildNodes().item(0).getNodeValue().trim());
                }
            }
        } catch (SAXParseException err) {
            Console.Out.WriteLine("parsing error");
        } catch (ParserConfigurationException e) {
            Console.Out.WriteLine("configuration error");
        } catch (SAXException e) {
            Console.Out.WriteLine("general sax parser error");
        } catch (IOException e) {
            Console.Out.WriteLine("config file error");
        } catch (Exception e) {
            Console.Out.WriteLine("general parser error");
        }
    }

    public string get(string key) {
        return parameters.get(key);
    }
}

}