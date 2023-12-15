using Siemens.Engineering.HW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartDriveParameterEditor
{


    /// <summary>
    /// Extends TreeNode with the Type of the Tag
    /// </summary>
    public class TiaTreeNode : System.Windows.Forms.TreeNode
    {
        private const int GROUP_IMG_INDEX = 0;
        private const int S120_IMG_INDEX = 1;
        private const int DO_IMG_INDEX = 1;
        private const int G120_IMG_INDEX = 1;


        private TiaTreeNodeType nodeType;

        /// <summary>
        /// Type of the node
        /// </summary>
        internal TiaTreeNodeType NodeType
        {
            get
            {
                return nodeType;
            }
        }

        /// <summary>
        /// Constructor
        /// Even if no reference  this Constructor is needed by the treeview
        /// </summary>
        public TiaTreeNode()
        {
            nodeType = TiaTreeNodeType.TiaDummyNode;
            Tag = "dummy";
            Text = "";
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodeText"></param>
        internal TiaTreeNode(string nodeText)
        {
            nodeType = TiaTreeNodeType.TiaDummyNode;
            Tag = nodeText;
            Text = nodeText;

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="NodeTag"></param>
        /// <param name="type"></param>
        internal TiaTreeNode(Object NodeTag, TiaTreeNodeType type)
        {
            ImageIndex = 0;
            SelectedImageIndex = 1;
            Tag = NodeTag;
            nodeType = type;
            string nodeText;
            switch (type)
            {
                case TiaTreeNodeType.TiaDeviceGroupNode:
                    nodeText = ((DeviceUserGroup)NodeTag).Name;
                    ImageIndex = GROUP_IMG_INDEX;
                    SelectedImageIndex = GROUP_IMG_INDEX;
                    break;
                case TiaTreeNodeType.TiaS120DeviceNode:
                    nodeText = ((DeviceItem)NodeTag).Name;

                    ImageIndex = S120_IMG_INDEX;
                    SelectedImageIndex = S120_IMG_INDEX;
                    break;

                case TiaTreeNodeType.TiaS120DONode:
                    nodeText = ((DeviceItem)NodeTag).Name;
                    ImageIndex = DO_IMG_INDEX;
                    SelectedImageIndex = DO_IMG_INDEX;
                    break;
                case TiaTreeNodeType.TiaG120DONode:
                case TiaTreeNodeType.TiaG110DONode:
                    nodeText = ((DeviceItem)NodeTag).Name;
                    ImageIndex = G120_IMG_INDEX;
                    SelectedImageIndex = G120_IMG_INDEX;
                    break;
                default:
                    throw new Exception("Unknown node type");

            }
            Text = nodeText;
            Name = nodeText;
        }

        /// <summary>
        /// Get a clone of a node
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            TiaTreeNode cloneNode = (TiaTreeNode)base.Clone();
            cloneNode.nodeType = NodeType;
            return cloneNode;


        }
    }


    /// <summary>
    /// Type of a Treenode coresponding to a Device Item or a user group
    /// </summary>
    enum TiaTreeNodeType
    {
        /// <summary>
        /// Dummy 
        /// </summary>
        TiaDummyNode,

        /// <summary>
        /// Node is user group
        /// </summary>
        TiaDeviceGroupNode,

        /// <summary>
        /// Node is a head module of a sinamics S120 device
        /// </summary>
        TiaS120DeviceNode,

        /// <summary>
        /// Node is a Sinamics S120 drive object
        /// </summary>
        TiaS120DONode,

        /// <summary>
        /// Node is a Sinamics G120 drive object
        /// </summary>
        TiaG120DONode,

        /// <summary>
        /// Node is a Sinamics G110 drive object
        /// </summary>
        TiaG110DONode
    }
}
