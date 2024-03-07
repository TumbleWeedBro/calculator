using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace calculator
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //functions

        public static double outvalue = 0;
        public static double runvalue = 0;

        
        //teaching it BODMAS HAHA
         string func_BODMAS(string strdisplayvalue)
        {

            int numoperationsplus = 0;
            int numoperationsminus = 0;
            int numoperations = checkoperations(strdisplayvalue, '/');
            for (int m = 0; m < numoperations; m++)
            {
                strdisplayvalue = func_Division(strdisplayvalue);
            }

            numoperations = checkoperations(strdisplayvalue, '*');
            for (int m = 0; m < numoperations; m++)
            {
                strdisplayvalue = func_Multiplication(strdisplayvalue);
            }
            numoperationsplus = checkoperations(strdisplayvalue, '+'); 
            numoperationsminus = checkoperations(strdisplayvalue, '-');
            numoperations = numoperationsplus + numoperationsminus;
            for (int i = 0; i < numoperations; i++)
            {

                strdisplayvalue = calculatefunction(strdisplayvalue);
                runvalue += 1;
            }

            return strdisplayvalue;

        }

        string func_Division(string displayvalue)
        {
            StringBuilder value1 = new StringBuilder();
            StringBuilder value2 = new StringBuilder();

            char y = '/';
            int divisionIndex = displayvalue.IndexOf(y);
           
            int rightremovecounter = 0;
            int leftremovecounter = 0;
            if (divisionIndex >= 0 && displayvalue.Contains(y))
            {
                for (int i = divisionIndex - 1; i >= 0; i--)
                {
                    if (!char.IsDigit(displayvalue[i]))
                    {

                        break; 
                    }
                    rightremovecounter += 1;
                    value1.Append(displayvalue[i]);
                }
                try
                {
                    for (int i = divisionIndex + 1; i >= 0; i++)
                    {
                        if (!char.IsDigit(displayvalue[i]))
                        {
                            break;
                        }
                        leftremovecounter += 1;
                        value2.Append(displayvalue[i]);
                    }
                }
                catch
                {
                   
                }         
            }
            try
            {
                double intval1 = double.Parse(value1.ToString());
                double intval2 = double.Parse(value2.ToString());

                outvalue = intval1 / intval2;

                int startremove = divisionIndex - rightremovecounter;
                int endremove = (rightremovecounter + leftremovecounter + 1);

                displayvalue = displayvalue.Remove(startremove, endremove).Insert(startremove, (outvalue).ToString());
                displayvalue = displayvalue.Replace("++", "+" + (outvalue).ToString());
                displayvalue = displayvalue.Replace("+-", "-" + (outvalue).ToString());
                displayvalue = displayvalue.Replace("-+", "-" + (outvalue).ToString());
                displayvalue = displayvalue.Replace("--", "+" + (outvalue).ToString());
                displayvalue = displayvalue.Replace("()", "*" + (outvalue).ToString());
            }
            catch
            {

            }

            return displayvalue;
        }

        string func_Multiplication(string displayvalue)
        {
            StringBuilder value1 = new StringBuilder();
            StringBuilder value2 = new StringBuilder();
            char x = '*';
            int multiplicationIndex = displayvalue.IndexOf(x);
            int rightremovecounter = 0;
            int leftremovecounter = 0;
            if (multiplicationIndex >= 0 && displayvalue.Contains(x))
            {
                for (int i = multiplicationIndex - 1; i >= 0; i--)
                {
                    if (!char.IsDigit(displayvalue[i]))
                    {

                        break;
                    }
                    rightremovecounter += 1;
                    value1.Append(displayvalue[i]);
                }
                try
                {
                    for (int i = multiplicationIndex + 1; i >= 0; i++)
                    {
                        if (!char.IsDigit(displayvalue[i]))
                        {
                            break;
                        }
                        leftremovecounter += 1;
                        value2.Append(displayvalue[i]);
                    }
                }
                catch
                {

                }
            }
            try
            {
                double intval1 = double.Parse(value1.ToString());
                double intval2 = double.Parse(value2.ToString());

                outvalue = intval1 * intval2;

                int startremove = multiplicationIndex - rightremovecounter;
                int endremove = (rightremovecounter + leftremovecounter + 1);

                displayvalue = displayvalue.Remove(startremove, endremove).Insert(startremove, (outvalue).ToString());
                displayvalue = displayvalue.Replace("++", "+" + (outvalue).ToString());
                displayvalue = displayvalue.Replace("+-", "-" + (outvalue).ToString());
                displayvalue = displayvalue.Replace("-+", "-" + (outvalue).ToString()); 
                displayvalue = displayvalue.Replace("--", "+" + (outvalue).ToString());
                displayvalue = displayvalue.Replace("()", "*" + (outvalue).ToString());
            }
            catch
            {

            }
            return displayvalue;
        }

        //check operations
        
        int checkoperations(string displayvalue, char operation)
        {
            int numoperations = 0;
            foreach(char i in displayvalue)
            {
                if (i == operation)
                {
                    numoperations += 1;
                }
            }
            return numoperations;
        }
        //calculate
        string calculatefunction(string displayvalue)
        {
            StringBuilder value1 = new StringBuilder();
            StringBuilder value2 = new StringBuilder();
            StringBuilder operation = new StringBuilder();
            int count = 0;
            int removecounter = 0;
            foreach(char i in displayvalue)
            {
               
                if (char.IsDigit(i) && count == 0)
                {
                    value1.Append(i.ToString());
                    removecounter += 1;
                    continue;
                }
                else
                {
                    if (!char.IsDigit(i) && operation.Length == 0)
                    {
                        operation.Append(i.ToString());
                        count += 1;
                        removecounter += 1;
                        continue;
                       
                    }
                    else if (char.IsDigit(i) && count == 1)
                    {
                        value2.Append(i.ToString());
                        removecounter += 1;
                        continue;
                    }
                    else
                    {
                        for (int x = 0; x < removecounter; x++)
                        {
                            displayvalue = displayvalue.Remove(0, 1);
                            
                        }

                        break;
                    }

                }
            }

            double intValue1 = 0;
            if (runvalue == 0)
            {
                 intValue1 = double.Parse(value1.ToString());
            }

            double intValue2 = double.Parse(value2.ToString());

            switch (operation.ToString())
            {
                case "+":

                    outvalue = (runvalue == 0) ? intValue1 + intValue2 : outvalue + intValue2;
                    break;

                case "-":
                    outvalue = (runvalue == 0) ? intValue1 - intValue2 : outvalue - intValue2;
                    break;
                case "*":
                    outvalue = (runvalue == 0) ? intValue1 * intValue2 : outvalue * intValue2;
                    break;
                case "/":
                    outvalue = (runvalue == 0) ? intValue1 / intValue2 : outvalue / intValue2;
                    break;
            }

            return displayvalue;


        }
        //checking the number of brackets
        int checkbrackets(string displayvalue)
        {
            int intbracketcount = 0;
            foreach (char i in displayvalue)
            {
                if (i == '(')
                {
                    intbracketcount += 1;
                }
            }
            return intbracketcount ;
        }

        //removing brackets
        string removebrackets(string displayvalue)
        {
            int startIndex = -1;
            int endIndex = -1;
            runvalue = 0;
            startIndex = displayvalue.LastIndexOf('(');
            if(startIndex != -1)
            {
                endIndex = displayvalue.IndexOf(')', startIndex);

                if(endIndex != -1 && endIndex > startIndex)
                {
                    string substring = displayvalue.Substring(startIndex +1 , endIndex - startIndex - 1);
                    func_BODMAS(substring);
                    int removelength =  endIndex - startIndex + 1 ;
                    displayvalue = displayvalue.Remove(startIndex, removelength).Insert(startIndex, (outvalue).ToString());

                    return displayvalue;
                }

         
            }
            return "";
            
        }

        

        private void button0_Click(object sender, EventArgs e)
        {
            txtdisplay.AppendText("0");
        }

        private void point_Click(object sender, EventArgs e)
        {
            txtdisplay.AppendText(".");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtdisplay.AppendText("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtdisplay.AppendText("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtdisplay.AppendText("3");
        }



        private void button4_Click(object sender, EventArgs e)
        {
            txtdisplay.AppendText("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtdisplay.AppendText("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtdisplay.AppendText("6");
        }


        private void button7_Click(object sender, EventArgs e)
        {
            txtdisplay.AppendText("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            txtdisplay.AppendText("8");
        }


        private void button9_Click(object sender, EventArgs e)
        {
            txtdisplay.AppendText("9");
        }


        //operations

        private void bracket1_Click(object sender, EventArgs e)
        {
            txtdisplay.AppendText("(");
        }

        private void bracket2_Click(object sender, EventArgs e)
        {
            txtdisplay.AppendText(")");
        }


     
        private void back_Click(object sender, EventArgs e)
        {

        }

        private void AC_Click(object sender, EventArgs e)
        {
            txtdisplay.Clear();
            outvalue = 0;
            runvalue = 0;
            
        }

        private void minus_Click(object sender, EventArgs e)
        {

            txtdisplay.AppendText("-");

        }

        private void plus_Click(object sender, EventArgs e)
        {

            txtdisplay.AppendText("+");

        }


        private void divide_Click(object sender, EventArgs e)
        {

            txtdisplay.AppendText("/");

        }


        private void multiply_Click(object sender, EventArgs e)
        {

            txtdisplay.AppendText("*");

        }

       
        private void equal_Click(object sender, EventArgs e)
        {


            string strdisplayvalue = txtdisplay.Text;
            string bracketstring = "";

            int numbrackets = checkbrackets(strdisplayvalue);
            bracketstring = removebrackets(strdisplayvalue);
            for (int y = 0; y < numbrackets -1; y++)
            {

                bracketstring = removebrackets(bracketstring);
            }

            func_BODMAS(bracketstring);
          
            txtdisplay.Text = (outvalue).ToString();



        }

        private void txtdisplay_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
