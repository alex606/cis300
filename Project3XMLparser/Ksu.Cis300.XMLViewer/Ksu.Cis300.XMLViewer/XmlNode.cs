using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ksu.Cis300.XMLViewer
{
    public struct XmlNode
    {
        private string _nodeName;
        private string _nodeValue;
        private XmlNodeType _nodeType;
        
        public string nodeName
        {
            get { return _nodeName; }
            set { _nodeName = value; }
        }
        public string nodeValue
        {
            get { return _nodeValue; }
            set { _nodeValue = value; }
        }

        public XmlNodeType nodeType
        {
            get { return _nodeType; }
            set { _nodeType = value; }
        }
    }
}
