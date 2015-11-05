/* Author: Alexander Wu
 * XML Viewer Homework 3
 */


using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Ksu.Cis300.XMLViewer
{
    /// <summary>
    /// XML Tree View form
    /// </summary>
    public partial class MainGUI : Form
    {
        public MainGUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A method to get a single node. Returns a TreeNode representing the node at which the XmlReader is currently positioned
        /// </summary>
        /// <param name="xmlReader"></param>
        /// <returns></returns>
        private TreeNode getSingleNode(XmlReader xmlReader)
        {
            XmlNode newXMLNode = new XmlNode();
            TreeNode newTreeNode = new TreeNode();
            newXMLNode.nodeName = xmlReader.Name;
            newXMLNode.nodeType = xmlReader.NodeType;
            newXMLNode.nodeValue = xmlReader.Value;
            newTreeNode.Tag = newXMLNode;
            
            if(xmlReader.Name != "")
            {
                newTreeNode.Text = newXMLNode.nodeName;
            }
            else
            {
                newTreeNode.Text = "<" + newXMLNode.nodeType + ">";
            }
            return newTreeNode;
        }

        /// <summary>
        ///  return a TreeNode that is the root of a tree representing this Element node with descendants
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        private TreeNode getTreeRootedAtElementNode(XmlReader xmlDoc)
        {
            TreeNode MainTreeRoot = getSingleNode(xmlDoc);
            TreeNode Attributes = new TreeNode();
            Attributes.Text = "<Attributes>";

            while (xmlDoc.MoveToNextAttribute())
            {
                Attributes.Nodes.Add(getSingleNode(xmlDoc));
            }
            MainTreeRoot.Nodes.Add(Attributes);
            xmlDoc.Read();
            while(!xmlDoc.EOF)
            {
                if(xmlDoc.NodeType == XmlNodeType.Element)
                {
                    using (XmlReader childElement = xmlDoc.ReadSubtree())
                    {
                        childElement.Read();
                        MainTreeRoot.Nodes.Add(getTreeRootedAtElementNode(childElement));
                    }
                }

                else if(xmlDoc.NodeType == XmlNodeType.EndElement)
                {
                    return MainTreeRoot;
                }
                else
                {
                    MainTreeRoot.Nodes.Add(getSingleNode(xmlDoc));
                }
                xmlDoc.Read();
            }
            return MainTreeRoot;
        }

        /// <summary>
        /// Event handler for opening an xml file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxOpenFileOption_Click(object sender, EventArgs e)
        {
            if(uxOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fn = uxOpenFileDialog.FileName;
                try
                {
                    using (StreamReader sr = new StreamReader(fn))
                    {
                        XmlReaderSettings settings = new XmlReaderSettings();
                        settings.DtdProcessing = DtdProcessing.Ignore;
                        settings.IgnoreWhitespace = true;

                        using(XmlReader xmlData =  XmlReader.Create(sr, settings))
                        {
                            TreeNode newTreeNode = new TreeNode();
                            while (xmlData.Read())
                            {
                                // Only read until html tag is reached
                                if (xmlData.NodeType == XmlNodeType.Element)
                                {
                                    newTreeNode = getTreeRootedAtElementNode(xmlData);
                                    uxTreeView.Nodes.Add(newTreeNode);
                                    break;
                                }
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Event handler for selecting different nodes from the tree view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode curTreeNode = uxTreeView.SelectedNode;
                XmlNode curXmlNode = (XmlNode)curTreeNode.Tag;

                uxTextNodeName.Text = curXmlNode.nodeName.ToString();
                uxTextNodeType.Text = curXmlNode.nodeType.ToString();
                uxTextNodeValue.Text = curXmlNode.nodeValue.ToString();
            }
            catch
            {
                uxTextNodeName.Text = "";
                uxTextNodeType.Text = "None";
                uxTextNodeValue.Text = "";
            }
        }
    }
}
