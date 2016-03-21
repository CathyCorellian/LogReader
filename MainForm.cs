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



namespace LogReader
{
    public partial class MainForm : Form
    {
        protected string logFilePath;
        List<string> logBasicInfoStrList = new List<string>(); //log's basic info

        public MainForm()
        {
            InitializeComponent();
        }

        private void testcaseTreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) && Path.GetExtension(((string[])e.Data.GetData(DataFormats.FileDrop))[0]).ToLower() == ".log") ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void testcaseTreeView_DragDrop(object sender, DragEventArgs e)
        {
            string filePath = null;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                if (File.Exists(filePath = ((string[])e.Data.GetData(DataFormats.FileDrop))[0]))
                    if (Path.GetExtension(logFilePath) == ".log")
                    {
                        logFilePath = filePath;

                        //using (var r = File.OpenText(logFilePath))
                        //{
                        //    string input = null;
                        //    while ((input = r.ReadLine()) != null)
                        //    {
                        //        string pass = "         Passed Tests:          ";
                        //        string fail = "         Failed Tests:          ";
                        //        string output;
                        //        if (input.Contains(pass))   //passed tests
                        //        {
                        //            int location = input.IndexOf(pass);
                        //            output = input.Substring(location + pass.Length);
                        //            testcasePassTextBox.Text = output;
                        //        }
                        //        if (input.Contains(fail))   //failed tests
                        //        {
                        //            int location = input.IndexOf(fail);
                        //            output = input.Substring(location + fail.Length);
                        //            testcaseFailTextBox.Text = output;
                        //        }
                        //    }
                        //}

                        fn_GetTreeInfo(logFilePath);
                        createSuiteButton.Enabled = true;
                    }
                    else
                        MessageBox.Show("this is not a log file");
                else
                    MessageBox.Show("the log file doesn't not exist");
            else
                MessageBox.Show("this is not a file");
        }

        private void testcaseTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                testcaseRichTextBox.Clear();
                testcaseRichTextBox.Lines = logBasicInfoStrList.ToArray();
            }
            else if (e.Node.Name == "failed_case" || e.Node.Name == "passed_case" || e.Node.Name == "script_error" || e.Node.Name == "case")
            {
                testcaseRichTextBox.Clear();
            }
            else
            {
                TreeNodeEx myNode = (TreeNodeEx)e.Node;
                testcaseRichTextBox.Lines = myNode.Description.ToArray();
                fn_MarkFaildRed(testcaseRichTextBox);
            }
        }

        private void testcaseTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    fn_CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
        }

        private void testcaseTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            //if(e.KeyCode == Keys.C && e.Control)
            //{
            //    var stack = new Stack<IEnumerator>();
            //    List<string> idStrList = new List<string>();
            //    var enumerator = testcaseTreeView.Nodes.GetEnumerator();

            //    while(true)
            //    {
            //        while (enumerator.MoveNext())
            //        {
            //            var item = (TreeNode)enumerator.Current;
            //            if (item.Nodes.Count == 0)
            //            {
            //                if (item.Checked)
            //                {
            //                    idStrList.Add(item.Text);
            //                }
            //            }
            //            else
            //            {
            //                stack.Push(enumerator);
            //                enumerator = item.Nodes.GetEnumerator();
            //            }
            //        }

            //        if (stack.Count > 0)
            //        {
            //            enumerator = stack.Pop();
            //        }
            //        else
            //        {
            //            break;
            //        }
            //    }

            //    var textString = string.Join(", ", idStrList.ToArray());
            //    if (!string.IsNullOrEmpty(textString))
            //    {
            //        Clipboard.SetText(textString);
            //    }
            //}
        }




        private void createSuiteButton_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) && Path.GetExtension(((string[])e.Data.GetData(DataFormats.FileDrop))[0]).ToLower() == ".xml") ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void createSuiteButton_DragDrop(object sender, DragEventArgs e)
        {
            string suiteFilePath = null;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                if (File.Exists(suiteFilePath = ((string[])e.Data.GetData(DataFormats.FileDrop))[0]))
                    if (Path.GetExtension(suiteFilePath) == ".xml")
                        using (var saveFileDialog = new SaveFileDialog() { Filter = "xml(*.xml)|*.xml", InitialDirectory = Path.GetDirectoryName(suiteFilePath), FileName = "Failed_" + Path.GetFileName(suiteFilePath) })
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                fn_CreateSuite(suiteFilePath, saveFileDialog.FileName);
                            else ;
                    else
                        MessageBox.Show("this is not a suite file");
                else
                    MessageBox.Show("the suite file doesn't not exist");
            else
                MessageBox.Show("this is not a file");
        }

        private void createSuiteButton_Click(object sender, EventArgs e)
        {
            var suiteDirPath = Path.GetDirectoryName(logFilePath);
            var suiteNameStr = Path.GetFileNameWithoutExtension(logFilePath);
            var xmlPath = Path.Combine(suiteDirPath, suiteNameStr + ".xml");

            using (var sdlg = new SaveFileDialog() { Filter = "xml(*.xml)|*.xml", InitialDirectory = suiteDirPath, FileName = "Failed_" + suiteNameStr })
                if (sdlg.ShowDialog() == DialogResult.OK)
                    fn_CreateSuite(xmlPath, sdlg.FileName);
            
        }





        private void fn_GetTreeInfo(string logFilePath)
        {
            //treeview initialization
            testcaseTreeView.Nodes[0].Nodes[0].Nodes[0].Nodes.Clear();
            testcaseTreeView.Nodes[0].Nodes[0].Nodes[1].Nodes.Clear();
            testcaseTreeView.Nodes[0].Nodes[1].Nodes.Clear();

            //clear the Description.text
            testcaseRichTextBox.Text = "";                  
            int case_passed = 0, case_failed = 0, script_error;
            //log's name
            string log;                               
            ArrayList BVT_log = new ArrayList();                         //log stores into arraylist
            bool i = false;                            //if the end of log's basic info
            ArrayList caseDescription = new ArrayList();        //case description
            ArrayList failed_casename = new ArrayList(); ArrayList passed_casename = new ArrayList(); ArrayList script_error_name = new ArrayList();  //tree node's amount
            StreamReader r = File.OpenText(logFilePath);
            string input;
            string bvt_name = "BEGIN RUN: ";
            string passed_tests_s = "         Passed Tests:          ";
            string failed_tests_s = "         Failed Tests:          ";
            string passed_verifications_s = "         Passed Verifications:  ";
            string output;
            try
            {
                logBasicInfoStrList.Clear();
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
                        logBasicInfoStrList.Add(infor);
                        testcaseRichTextBox.Text += (infor.ToString() + "\r\n");
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
                        TreeNodeEx AddNode = new TreeNodeEx();
                        AddNode.Name = ID; AddNode.Text = ID;
                        AddNode.Description.Add("CaseId was not run.  Treated as a Failure.");
                        AddNode.ForeColor = Color.Gray;
                        testcaseTreeView.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(AddNode);
                    }
                    if (line.Contains("***   ["))                                                 //find case with "***   ["
                    {
                        TreeNodeEx AddNode = new TreeNodeEx();
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

                                if (testcaseTreeView.Nodes[0].Nodes[1].Nodes.Count != 0)
                                {
                                    if (testcaseTreeView.Nodes[0].Nodes[1].Nodes[testcaseTreeView.Nodes[0].Nodes[1].Nodes.Count - 1].Text == AddNode.Text)
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
                                    testcaseTreeView.Nodes[0].Nodes[1].Nodes.Add(AddNode);
                                    break;
                                }
                                if (BVT_log[testLineEnd + 1].ToString().Contains("Test - D:\\") || BVT_log[testLineEnd + 1].ToString().Contains("Test - C:\\"))
                                {
                                    testEnd = testLineEnd;
                                    for (int focusOutPut = testStart; focusOutPut < testEnd; focusOutPut++)
                                    {
                                        AddNode.Description.Add(BVT_log[focusOutPut].ToString());
                                    }
                                    testcaseTreeView.Nodes[0].Nodes[1].Nodes.Add(AddNode);
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
                                if (BVT_log[testLineEnd + 1].ToString().Contains("   BEGIN TEST:["))
                                {
                                    testEnd = testLineEnd;
                                    for (int focusOutPut = testStart; focusOutPut < testEnd; focusOutPut++)
                                    {
                                        AddNode.Description.Add(BVT_log[focusOutPut].ToString());
                                    }
                                    testcaseTreeView.Nodes[0].Nodes[1].Nodes.Add(AddNode);
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
                        TreeNodeEx AddNode = new TreeNodeEx();
                        AddNode.Name = ID; AddNode.Text = ID;
                        AddNode.owner = owner; AddNode.descriptionName = name;
                        for (int jsLine = log_line; !(BVT_log[jsLine].ToString().Contains("Test - D:\\") || BVT_log[jsLine].ToString().Contains("Test - D:\\")); jsLine--)
                        {
                            if (BVT_log[jsLine - 1].ToString().Contains("Test - D:\\") || BVT_log[jsLine - 1].ToString().Contains("Test - C:\\"))
                            {
                                AddNode.Description.Add(BVT_log[jsLine - 1].ToString());
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
                        for (int j = 0; j <= CaseLenght; j++)     // determine case failed or passed
                        {
                            if (((string)BVT_log[j + log_line]).Contains("***   ["))
                            {
                                AddNode.conText = System.Text.RegularExpressions.Regex.Replace(BVT_log[log_line + 1].ToString(), "      \\[\\d+:\\d+:\\d+.\\d+\\] CONTEXT: ", "");
                                AddNode.ForeColor = Color.Red;
                                testcaseTreeView.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(AddNode);
                                break;
                            }
                            if (((string)BVT_log[j + log_line]).Contains("   END TEST:["))
                            {
                                testcaseTreeView.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(AddNode);
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
        }

        //check fall failed case checkboxss
        private void fn_CheckAllChildNodes(TreeNode treeNode, bool isNodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = isNodeChecked;
                if (node.Nodes.Count > 0)
                {
                    fn_CheckAllChildNodes(node, isNodeChecked);
                }
            }
        }    

        private void fn_CreateSuite(string xmlPath, string FileName)
        {
            try
            {
                string input;
                bool isExistFailedCase = false;
                foreach (TreeNodeEx tn in testcaseTreeView.Nodes[0].Nodes[0].Nodes[0].Nodes)
                {
                    if (tn.ForeColor == Color.Red)
                    {
                        isExistFailedCase = true;
                    }
                }
                if (xmlPath != "")
                {
                    using (StreamWriter sw = new StreamWriter(FileName))
                    using (StreamReader sr = new StreamReader(xmlPath))
                    {
                        while ((input = sr.ReadLine()) != null)
                        {
                            foreach (TreeNodeEx tn in testcaseTreeView.Nodes[0].Nodes[0].Nodes[0].Nodes)   //select from "failed cases"
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
                            foreach (TreeNodeEx tn in testcaseTreeView.Nodes[0].Nodes[1].Nodes)     //select from "script error"
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
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //mark failure highlight red in discription text box
        private void fn_MarkFaildRed(RichTextBox discription)
        {
            Regex rx = new Regex(@"\*\*\*   .+", RegexOptions.Multiline);
            MatchCollection mc = rx.Matches(discription.Text);
            foreach (Match m in mc)
            {
                discription.Select(m.Index, m.Length);
                discription.SelectionColor = Color.Red;
                discription.SelectionFont = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold);
            }
        }


     
    }
}