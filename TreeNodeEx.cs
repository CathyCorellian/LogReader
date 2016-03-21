using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LogReader
{
    class TreeNodeEx : TreeNode
    {
        public string conText;
        public string descriptionName;
        public string owner;
        public List<string> Description = new List<string>();
    }
}
