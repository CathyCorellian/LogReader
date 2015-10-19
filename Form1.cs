using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace LogReader
{
    public partial class Form1 : Form
    {
        protected string owner;
        protected string ID;
        protected string job_name;
        protected string path;
        protected string passed_tests;
        protected string failed_tests;
        protected string desc_info;
        ArrayList al = new ArrayList(); //log's basic info

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1Load(object sender, EventArgs e)
        {
            name.Text = job_name;
            test_id.Text = ID;
            test_owner.Text = owner;
            passed.Text = passed_tests;
            failed.Text = failed_tests;
            desc_info = Description.Text;
        }

        protected class myTreeNode : TreeNode
        {
            public string conText;
            public string descriptionName;
            public string owner;
            public ArrayList Description = new ArrayList();
        }  //add new element in myTreeNode

        private void MarkFaildRed(RichTextBox discription)
        {
            Regex rx = new Regex(@"\*\*\*   .+", RegexOptions.Multiline);
            MatchCollection mc = rx.Matches(discription.Text);
            foreach (Match m in mc)
            {
                discription.Select(m.Index, m.Length);
                discription.SelectionColor = Color.Red;
                discription.SelectionFont = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold);
            }         
        }      //mark failure highlight red in discription text box

        private bool GudgeFile(string filePath,string logorsuite) 
        {
            string logID = "BEGIN RUN: ";
            string suiteID="<?xml version=\"1.0\" encoding=\"us-ascii\"?>";
            string file;
            try
            {
                using (StreamReader r = File.OpenText(filePath))
                {
                    file = r.ReadToEnd();
                    if (!(file.Contains(logID)) && logorsuite == "log")
                    {
                            MessageBox.Show("Please choose an other Log.");
                            return false;
                    }
                    else if (!(file.Contains(suiteID)) && logorsuite == "suite")
                    {
                        MessageBox.Show("Please choose an other Suite.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }   // If it is a BVT log

        private void PassedFailed(string FilePath)  
        {
            StreamReader r = File.OpenText(path);
            string input;
            while ((input = r.ReadLine()) != null)
            {
                string pass = "         Passed Tests:          ";
                string fail = "         Failed Tests:          ";
                string output;
                if (input.Contains(pass))   //passed tests
                {
                    int location = input.IndexOf(pass);
                    output = input.Substring(location + pass.Length);
                    passed.Text = output;
                }
                if (input.Contains(fail))   //failed tests
                {
                    int location = input.IndexOf(fail);
                    output = input.Substring(location + fail.Length);
                    failed.Text = output;
                }
            }
            r.Close();
        }  //TextBox(Passed ,Failed)

        private void GetTreeInfo(string FilePath)
        {
            treeView.Nodes[0].Nodes[0].Nodes[0].Nodes.Clear();         //treeview initialization
            treeView.Nodes[0].Nodes[0].Nodes[1].Nodes.Clear();
            treeView.Nodes[0].Nodes[1].Nodes.Clear();
            Description.Text = "";                  //clear the Description.text
            int case_passed = 0, case_failed = 0, script_error;
            string log;                               //log's name
            ArrayList BVT_log = new ArrayList();                         //log stores into arraylist
            bool i = false;                            //if the end of log's basic info
            ArrayList caseDescription = new ArrayList();        //case description
            ArrayList failed_casename = new ArrayList(); ArrayList passed_casename = new ArrayList(); ArrayList script_error_name = new ArrayList();  //tree node's amount
            StreamReader r = File.OpenText(path);
            string input;
            string bvt_name = "BEGIN RUN: ";
            string passed_tests_s = "         Passed Tests:          ";
            string failed_tests_s = "         Failed Tests:          ";
            string passed_verifications_s = "         Passed Verifications:  ";
            string output;
            try
            {
                al.Clear();
                while ((input = r.ReadLine()) != null)           //Get whole log's info
                {
                    BVT_log.Add(input);
                    if (input.Contains(passed_tests_s))    //calculator nodes
                    {
                        int location = input.IndexOf(passed_tests_s);
                        case_passed = int.Parse(output = input.Substring(location + passed_tests_s.Length));
                    }
                    if (input.Contains(failed_tests_s))   //calculator nodes
                    {
                        int location = input.IndexOf(failed_tests_s);
                        case_failed = int.Parse(output = input.Substring(location + failed_tests_s.Length));
                    }
                    if (input.Contains(passed_verifications_s))   //calculator nodes
                    {
                        int location = input.IndexOf(passed_verifications_s);
                        script_error = (int.Parse(output = input.Substring(location + passed_verifications_s.Length)) - case_failed);
                    }
                    if (input.Contains(bvt_name))      //get log's name
                    {
                        int start = input.IndexOf(":");
                        int end = input.IndexOf(",");
                        log = input.Substring(start, end - start);
                    }
                    if (!i)                     //basic info
                    {
                        string infor = System.Text.RegularExpressions.Regex.Replace(input, "\\[\\d+:\\d+:\\d+.\\d+\\]", "");
                        al.Add(infor);
                        Description.Text += (infor.ToString() + "\r\n");
                    }
                    if (input.Contains("] Physical Memory: "))
                    {
                        i = true;
                    }
                }

                for (int log_line = 0; log_line < BVT_log.Count; log_line++)
                {
                    string line = (string)BVT_log[log_line];
                    if (line.Contains(" was not run.  Treated as a Failure."))   //find case with" was not run.  Treated as a Failure."
                    {
                        string ID = System.Text.RegularExpressions.Regex.Match(line, "FAIL: CaseId \\d+ was not run.").ToString();
                        ID = System.Text.RegularExpressions.Regex.Replace(ID, "FAIL: CaseId ", "");
                        ID = System.Text.RegularExpressions.Regex.Replace(ID, "was not run.", "");
                        myTreeNode AddNode = new myTreeNode();
                        AddNode.Name = ID; AddNode.Text = ID;
                        AddNode.Description.Add("CaseId was not run.  Treated as a Failure.");
                        AddNode.ForeColor = Color.Gray;
                        treeView.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(AddNode);
                    }
                    if (line.Contains("***   ["))                                                 //find case with "***   ["
                    {
                        myTreeNode AddNode = new myTreeNode();
                        AddNode.ForeColor = Color.Red;
                        int testStart = 0; int testEnd = 0; int breakLine = 0; int breaksameline = 0;
                        int nameLocation;
                        for (int testLineStart = log_line; !(BVT_log[testLineStart].ToString().Contains("Test - D:\\") || BVT_log[testLineStart].ToString().Contains("Test - C:\\")) && testLineStart > 1; testLineStart--)
                        {
                            if (BVT_log[testLineStart].ToString().Contains("   BEGIN TEST:["))              //if it belong to case with "   BEGIN TEST:[" then break.
                            {
                                breakLine = 1;
                                break;
                            }
                            if (BVT_log[testLineStart - 1].ToString().Contains("Test - D:\\") || BVT_log[testLineStart - 1].ToString().Contains("Test - C:\\"))
                            {
                                testStart = testLineStart - 1;
                                nameLocation = BVT_log[testStart].ToString().LastIndexOf("\\");
                                AddNode.Text = BVT_log[testStart].ToString().Substring(nameLocation + 1);
                                AddNode.descriptionName = AddNode.Text;

                                if (treeView.Nodes[0].Nodes[1].Nodes.Count != 0)
                                {
                                    if (treeView.Nodes[0].Nodes[1].Nodes[treeView.Nodes[0].Nodes[1].Nodes.Count - 1].Text == AddNode.Text)
                                    {
                                        breaksameline = 1;
                                        break;
                                    }
                                }
                            }
                        }
                        if (testStart != 0 && breakLine == 0 && breaksameline == 0)  //if is the failed script without testcases
                        {
                            for (int testLineEnd = log_line; (!(BVT_log[testLineEnd].ToString().Contains("Test - D:\\") || BVT_log[testLineEnd].ToString().Contains("Test - C:\\"))) && (testEnd < BVT_log.Count); testLineEnd++)
                            {
                                if (BVT_log[testLineEnd].ToString().Contains("   END TEST:["))
                                {
                                    break;
                                }
                                if (testLineEnd + 1 == BVT_log.Count)   //if log include only one failed .js
                                {
                                    int basicInfoRows = 9;
                                    testEnd = testLineEnd - basicInfoRows;
                                    for (int focusOutPut = testStart; focusOutPut < testEnd; focusOutPut++)
                                    {
                                        AddNode.Description.Add(BVT_log[focusOutPut].ToString());
                                    }
                                    treeView.Nodes[0].Nodes[1].Nodes.Add(AddNode);
                                    break;
                                }
                                if (BVT_log[testLineEnd + 1].ToString().Contains("Test - D:\\") || BVT_log[testLineEnd + 1].ToString().Contains("Test - C:\\"))
                                {
                                    testEnd = testLineEnd;
                                    for (int focusOutPut = testStart; focusOutPut < testEnd; focusOutPut++)
                                    {
                                        AddNode.Description.Add(BVT_log[focusOutPut].ToString());
                                    }
                                    treeView.Nodes[0].Nodes[1].Nodes.Add(AddNode);
                                }
                            }
                        }
                        if (testStart != 0 && breakLine == 0 && breaksameline == 0) //if is the failed script with testcases
                        {
                            for (int testLineEnd = log_line; (!(BVT_log[testLineEnd].ToString().Contains("Test - D:\\") || BVT_log[testLineEnd].ToString().Contains("Test - C:\\"))) && (testEnd < BVT_log.Count); testLineEnd++)
                            {
                                if (BVT_log[testLineEnd].ToString().Contains("   END TEST:[") || BVT_log[testLineEnd].ToString().Contains("         Passed Tests:          "))
                                {
                                    break;
                                }
                                if (BVT_log[testLineEnd+1 ].ToString().Contains("   BEGIN TEST:["))
                                {
                                    testEnd = testLineEnd;
                                    for (int focusOutPut = testStart; focusOutPut < testEnd; focusOutPut++)
                                    {
                                        AddNode.Description.Add(BVT_log[focusOutPut].ToString());
                                    }
                                    treeView.Nodes[0].Nodes[1].Nodes.Add(AddNode);
                                }
                            }
                        }
                    }
                    if (line.Contains("   BEGIN TEST:["))                          //find case with"   BEGIN TEST:["
                    {
                        int CaseLenght = 0;
                        string ID = System.Text.RegularExpressions.Regex.Match(line, "] \\d+\\W+\\w+\\W").ToString();        //get ID
                        ID = System.Text.RegularExpressions.Regex.Replace(ID, "] ", "");
                        ID = System.Text.RegularExpressions.Regex.Replace(ID, " \\[\\w+\\]", "");
                        ID = System.Text.RegularExpressions.Regex.Replace(ID, " \\[\\w\\-", "");
                        ID = System.Text.RegularExpressions.Regex.Replace(ID, "\\: \\w+", "");
                        string owner = System.Text.RegularExpressions.Regex.Match(line, "\\[\\D+\\]").ToString();           //get owner
                        int nameLocation = line.LastIndexOf(":");                                                           //get name
                        string name = line.Substring(nameLocation + 1);
                        myTreeNode AddNode = new myTreeNode();
                        AddNode.Name = ID; AddNode.Text = ID;
                        AddNode.owner = owner; AddNode.descriptionName = name;
                        for (int jsLine = log_line; !(BVT_log[jsLine].ToString().Contains("Test - D:\\") || BVT_log[jsLine].ToString().Contains("Test - D:\\")); jsLine--)
                        {
                            if (BVT_log[jsLine - 1].ToString().Contains("Test - D:\\") || BVT_log[jsLine - 1].ToString().Contains("Test - C:\\"))
                            {
                                AddNode.Description.Add(BVT_log[jsLine -1].ToString());
                                AddNode.Description.Add("------------------------------------------------------------------------------------");
                                break;
                            }
                            if (jsLine - 1 == 0)
                            {
                                break;
                            }
                        }
                        for (int caseLine = log_line; !(BVT_log[caseLine - 1].ToString().Contains("   END TEST:[")); caseLine++)
                        {
                            AddNode.Description.Add(BVT_log[caseLine].ToString());
                        }
                        for (int caseStart = log_line; !(((string)BVT_log[caseStart]).Contains("   END TEST:[")); caseStart++)
                        {
                            CaseLenght++;
                        }
                        for (int j = 0; j <= CaseLenght; j++)     // 判断case ，failed or passed
                        {
                            if (((string)BVT_log[j + log_line]).Contains("***   ["))
                            {
                                AddNode.conText = System.Text.RegularExpressions.Regex.Replace(BVT_log[log_line + 1].ToString(), "      \\[\\d+:\\d+:\\d+.\\d+\\] CONTEXT: ", "");
                                AddNode.ForeColor = Color.Red;
                                treeView.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(AddNode);
                                break;
                            }
                            if (((string)BVT_log[j + log_line]).Contains("   END TEST:["))
                            {
                                treeView.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(AddNode);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                r.Close();
            }
        }    //ganarate whole treeview
        
        //private void InputLogClick(object sender, EventArgs e)   
        //{

        //    OpenFileDialog dlg = new OpenFileDialog();
        //    dlg.Filter = "txt(*.txt),log(*.log)|*.txt;*.log";
        //    dlg.ShowDialog();
        //    path = dlg.FileName;
        //    if (File.Exists(path))
        //    {
        //        if (GudgeFile(path, "log"))
        //        {
        //            int nameLocation = path.LastIndexOf("\\");                                                           //get name
        //            string fileName = path.Substring(nameLocation + 1);
        //            treeView.Nodes[0].Text  = fileName;
        //            PassedFailed(path);
        //            GetTreeInfo(path);
        //            this.treeView.CheckBoxes = true;
        //            Create_Suite.Enabled = true;                //active "create suite" button
        //        }
        //    }
        //}   //button "input log"
       
        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && Path.GetExtension(((string[])e.Data.GetData(DataFormats.FileDrop))[0]).ToLower() == ".log") 
                e.Effect = DragDropEffects.Copy;
        }//drop enter event

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            this.Text = path;
            if (File.Exists(path))
            {
                if (GudgeFile(path, "log"))
                {
                    int nameLocation = path.LastIndexOf("\\");                                                           //get name
                    string fileName = path.Substring(nameLocation + 1);
                    treeView.Nodes[0].Text = fileName;
                    PassedFailed(path);
                    GetTreeInfo(path);
                    this.treeView.CheckBoxes = true;
                    createSuiteButton.Enabled = true;                //active "create suite" button
                }
            }
        }//drop enter event

        //private void CreateSuiteClick(object sender, EventArgs e)
        //{
        //    string xmlPath;
        //    if (CheckCheckbox())
        //    {
        //        SaveFileDialog sdlg = new SaveFileDialog();
        //        //OpenFileDialog odlg = new OpenFileDialog();
        //        sdlg.Filter = "xml(*.xml)|*.xml";
        //        odlg.Filter = "xml(*.xml)|*.xml";
        //        MessageBox.Show("Input original SUITE.");
        //        odlg.ShowDialog();
        //        if (GudgeFile(odlg.FileName, "suite"))
        //        {
        //            if (odlg.FileName != "")
        //            {
        //                MessageBox.Show("Save your SUITE.");
        //                sdlg.ShowDialog();
        //                // OpenFileDialog odlg = new OpenFileDialog();
        //                // odlg.Filter = "xml(*.xml)|*.xml";       
        //                xmlPath = odlg.FileName;
        //                if (sdlg.FileName != "")
        //                {
        //                    CreateSuite(xmlPath, sdlg.FileName);
        //                    MessageBox.Show("Complete!");
        //                }
        //            }
        //        }
        //    }
        //}      //button "create suite" 

        private void CreateSuite(string xmlPath, string FileName)
        {
            try
            {
                string input;
                bool isExistFailedCase = false;
                foreach (myTreeNode tn in treeView.Nodes[0].Nodes[0].Nodes[0].Nodes)
                {
                    if (tn.ForeColor == Color.Red)
                    {
                        isExistFailedCase = true;
                    }
                }
                if (xmlPath != "")
                {
                    using (StreamWriter sw = new StreamWriter(FileName))
                    {
                        using (StreamReader sr = new StreamReader(xmlPath))
                        {
                            while ((input = sr.ReadLine()) != null)
                            {
                                foreach (myTreeNode tn in treeView.Nodes[0].Nodes[0].Nodes[0].Nodes)   //select from "failed cases"
                                {
                                    if (tn.ForeColor == Color.Red && tn.Checked == true)
                                    {
                                        string nodeID = System.Text.RegularExpressions.Regex.Replace(tn.Name, "\\(\\d+\\)", "");
                                        nodeID = System.Text.RegularExpressions.Regex.Replace(nodeID, " ", "");
                                        if (input.Contains(" context="))
                                        {
                                            if (input.Contains(nodeID) && input.Contains(tn.conText))
                                            {
                                                sw.WriteLine(input.ToString());
                                                break;
                                            }
                                        }
                                        else if (input.Contains(nodeID) || !input.Contains("Testcase id="))
                                        {
                                            sw.WriteLine(input.ToString());
                                            break;
                                        }
                                    }
                                }
                                foreach (myTreeNode tn in treeView.Nodes[0].Nodes[1].Nodes)     //select from "script error"
                                {
                                    if (input.ToUpper().Contains(tn.descriptionName.ToUpper()))
                                    {
                                        sw.WriteLine(input.ToString());
                                        break;
                                    }
                                    if (!isExistFailedCase)
                                    {
                                        if (!input.Contains("Testcase id"))
                                        {
                                            sw.WriteLine(input.ToString());
                                            break;
                                        }
                                    }
                                }
                            }
                            sr.Close();
                        }
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }       //create suite file

        private void createSuiteButton_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && Path.GetExtension(((string[])e.Data.GetData(DataFormats.FileDrop))[0]).ToLower() == ".xml") 
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void createSuiteButton_DragDrop(object sender, DragEventArgs e)
        {
            var xmlPath = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            if (CheckCheckbox() && GudgeFile(xmlPath, "suite"))
                using (var sdlg = new SaveFileDialog() { Filter = "xml(*.xml)|*.xml", InitialDirectory = Path.GetDirectoryName(xmlPath), FileName = "Failed_" + Path.GetFileNameWithoutExtension(xmlPath) })
                    if (sdlg.ShowDialog() == DialogResult.OK)
                        CreateSuite(xmlPath, sdlg.FileName);
        }

        private bool CheckCheckbox()
        {
            bool isCheck = false;
            foreach (myTreeNode tn in treeView.Nodes[0].Nodes[0].Nodes[0].Nodes) 
            {
                if (tn.Checked == true)
                {
                    isCheck = true;
                    break;
                }                  
            }
            foreach (myTreeNode tn in treeView.Nodes[0].Nodes[0].Nodes[1].Nodes)
            {
                if (tn.Checked == true)
                {
                    isCheck = true;
                    break;
                }
            }
            foreach (myTreeNode tn in treeView.Nodes[0].Nodes[1].Nodes)
            {
                if (tn.Checked == true)
                {
                    isCheck = true;
                    break;
                }
            }
            if (!isCheck)
            {
                MessageBox.Show("No node is choosed.Check the checkboxs.");
            }
            return isCheck;
        }      //check if any chckbox has been chcked

        private void CheckAllChildNodes(TreeNode treeNode, bool isNodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = isNodeChecked;
                if (node.Nodes.Count > 0 )
                {
                        this.CheckAllChildNodes(node, isNodeChecked);
                }
            }
        }    //check fall failed case checkboxss

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                Description.Clear();
                Description.Lines = (string[])al.ToArray(typeof(string));
            }
            else if (e.Node.Name == "failed_case" || e.Node.Name == "passed_case" || e.Node.Name == "script_error" || e.Node.Name == "case")
            { }
            else
            {
                myTreeNode myNode = (myTreeNode)e.Node;
                test_id.Text = myNode.Name;
                test_owner.Text = myNode.owner;
                name.Text = myNode.descriptionName;
                Description.Lines = (string[])myNode.Description.ToArray(typeof(string));
                MarkFaildRed(Description);
            }
        }      //selecte node event

        private void node_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
        }

        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.C && e.Control)
            {
                var stack = new Stack<IEnumerator>();
                List<string> idStrList = new List<string>();

                var enumerator = treeView.Nodes.GetEnumerator();

                while(true)
                {
                    while (enumerator.MoveNext())
                    {
                        var item = (TreeNode)enumerator.Current;
                        if (item.Nodes.Count == 0)
                        {
                            if (item.Checked)
                            {
                                idStrList.Add(item.Text);
                            }
                        }
                        else
                        {
                            stack.Push(enumerator);
                            enumerator = item.Nodes.GetEnumerator();
                        }
                    }

                    if (stack.Count > 0)
                    {
                        enumerator = stack.Pop();
                    }
                    else
                    {
                        break;
                    }
                }

                var textString = string.Join(", ", idStrList.ToArray());

                if (!string.IsNullOrEmpty(textString))
                {
                    Clipboard.SetText(textString);
                }
                

           
            }
        }

        private void createSuiteButton_Click(object sender, EventArgs e)
        {
            var suiteDirPath = Path.GetDirectoryName(path);
            var suiteNameStr = Path.GetFileNameWithoutExtension(path);
            var xmlPath = Path.Combine(suiteDirPath, suiteNameStr + ".xml");

            if (CheckCheckbox() && GudgeFile(xmlPath, "suite"))
            {
                using (var sdlg = new SaveFileDialog() { Filter = "xml(*.xml)|*.xml", InitialDirectory = suiteDirPath, FileName = "Failed_" + suiteNameStr })
                    if (sdlg.ShowDialog() == DialogResult.OK)
                        CreateSuite(xmlPath, sdlg.FileName);
            }
        }
    }
}
     
  

