using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class FormMain : Form
    {
        private string memorycalc = "";
        private string moushen = "";
        private double result = 0;
        private bool buf = true;
        private int format = 10;

        public FormMain()
        {
            InitializeComponent();
        }

        private void num_Click(object sender, EventArgs e)
        {
            Button butt = (Button)sender;
            if (outputmain.Text.Length > 10 && buf == false) return;
            if ((outputmain.Text == "0" && butt.Name == "num0") || (outputmain.Text.IndexOf(",") > 0 && butt.Name == "numz")) return;
            if ((outputmain.Text == "0" || buf == true))
            {
                buf = false;
                if (butt.Name == "numz")
                {
                    outputmain.Text += butt.Text;
                }
                else
                {
                    outputmain.Text = butt.Text;
                }
            }
            else
            {
                outputmain.Text += butt.Text;
            }
        }

        private void clear()
        {
            moushen = "";
            result = 0;
            buf = true;
            outputsub.Text = "";
            outputmain.Text = "0";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(format == 10)
            {
                double tmp = -Convert.ToDouble(outputmain.Text);
                outputmain.Text = Convert.ToString(tmp);
            }
            if(format == 2)
            {
                Int64 tmp = -Convert.ToInt64(outputmain.Text, 2);
                outputmain.Text = Convert.ToString(tmp, 2);
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            buf = true;
            outputmain.Text = "0";
        }

        private void button32_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (outputmain.Text == "0") return;
            if (outputmain.Text.Length > 0)
            {
                if (outputmain.Text.Length == 1)
                {
                    outputmain.Text = "0";
                }
                else
                {
                    outputmain.Text = outputmain.Text.Remove(outputmain.Text.Length - 1);
                }
            }
        }

        private bool moushens(double tmp, string moush)
        {
            if (outputsub.Text.Length == 0 || outputsub.Text.LastIndexOf("=") == outputsub.Text.Length - 2)
            {
                if(format == 10)
                {
                    outputsub.Text = tmp + " " + moush + " ";
                }
                if(format == 2)
                {
                    outputsub.Text = Convert.ToString(Convert.ToInt64(tmp), 2) + " " + moush + " ";
                }
            }
            else if (outputsub.Text.LastIndexOf(moush) != outputsub.Text.Length - 2 && buf == true)
            {
                int ind = outputsub.Text.Length - 2;
                outputsub.Text = outputsub.Text.Remove(ind, 1);
                outputsub.Text = outputsub.Text.Insert(ind, moush);
            }
            else if (outputsub.Text.LastIndexOf(moush) == outputsub.Text.Length - 2 && buf == true)
            {
                buf = true;
                return true;
            }
            else
            {
                if (format == 10)
                {
                    outputsub.AppendText(tmp + " " + moush + " ");
                }
                if (format == 2)
                {
                    outputsub.AppendText(Convert.ToString(Convert.ToInt64(tmp), 2) + " " + moush + " ");
                }

                switch (moushen)
                {
                    case "+":
                        result += tmp;
                        break;
                    case "-":
                        result -= tmp;
                        break;
                    case "×":
                        result *= tmp;
                        break;
                    case "÷":
                        if (tmp == 0)
                        {
                            moushen = "";
                            result = 0;
                            buf = true;
                            outputsub.Text = "";
                            outputmain.Text = "Ошибка";
                            return false;
                        }
                        result /= tmp;
                        break;
                    case "^":
                        result = Math.Pow(result, tmp);
                        break;
                }
                if (format == 10)
                {
                    outputmain.Text = Convert.ToString(result);
                }
                if (format == 2)
                {
                    outputmain.Text = Convert.ToString(Convert.ToInt64(result), 2);
                }
            }
            if (moushen != moush)
            {
                moushen = moush;
            }
            return false;
        }

        private void actions_Click(object sender, EventArgs e)
        {
            if (outputmain.Text == "Ошибка" ||
                outputmain.Text == "не число" ||
                outputmain.Text == "-∞" ||
                outputmain.Text == "∞" ||
                outputmain.Text.IndexOf("min") >= 0 ||
                outputmain.Text.IndexOf("max") >= 0)
            {
                clear();
                return;
            }
            Button bn = (Button)sender;
            double tmp = 0;
            if(format == 10)
            {
                tmp = Convert.ToDouble(outputmain.Text);
            }
            if (format == 2)
            {
                tmp = Convert.ToInt64(outputmain.Text, 2);
            }
            if (moushen == "")
            {
                result = tmp;
            }

            switch (bn.Name)
            {
                case "actionadd":
                    if (moushens(tmp, "+")) return;
                    break;
                case "actionsub":
                    if (moushens(tmp, "-")) return;
                    break;
                case "actionmul":
                    if (moushens(tmp, "×")) return;
                    break;
                case "actiondiv":
                    if (moushens(tmp, "÷")) return;
                    break;
                case "actionpow":
                    if (moushens(tmp, "^")) return;
                    break;
                case "actionequal":
                    if (outputsub.Text.Length == 0 || outputsub.Text.LastIndexOf("=") == outputsub.Text.Length - 2)
                    {
                        return;
                    }
                    if (format == 10)
                    {
                        outputsub.AppendText(tmp + " = ");
                    }
                    if (format == 2)
                    {
                        outputsub.AppendText(Convert.ToString(Convert.ToInt64(tmp), 2) + " = ");
                    }
                    switch (moushen)
                    {
                        case "+":
                            result += tmp;
                            break;
                        case "-":
                            result -= tmp;
                            break;
                        case "×":
                            result *= tmp;
                            break;
                        case "÷":
                            if (tmp == 0)
                            {
                                moushen = "";
                                result = 0;
                                buf = true;
                                outputsub.Text = "";
                                outputmain.Text = "Ошибка";
                                return;
                            }
                            result /= tmp;
                            break;
                        case "^":
                            result = Math.Pow(result, tmp);
                            break;
                    }
                    if (format == 10)
                    {
                        outputmain.Text = Convert.ToString(result);
                    }
                    if (format == 2)
                    {
                        outputmain.Text = Convert.ToString(Convert.ToInt64(result), 2);
                    }
                    moushen = "";
                    break;
            }
            buf = true;
        }

        private void memory_Click(object sender, EventArgs e)
        {
            Button bn = (Button)sender;
            switch (bn.Name)
            {
                case "memoryms":
                    if (memorycalc == outputmain.Text) return;
                    memorycalc = outputmain.Text;
                    outputmem.Text = memorycalc;
                    memorymr.BackColor = Color.DarkGray;
                    memorymr.Enabled = true;
                    memorymc.BackColor = Color.DarkGray;
                    memorymc.Enabled = true;
                    break;
                case "memorymr":
                    if (outputsub.Text.Length != 0 && outputsub.Text.LastIndexOf("=") == outputsub.Text.Length - 2)
                    {
                        moushen = "";
                        result = 0;
                        buf = true;
                        outputsub.Text = "";
                    }
                    outputmain.Text = memorycalc;
                    break;
                case "memorymc":
                    if (memorycalc != "")
                    {
                        memorycalc = "";
                        outputmem.Text = memorycalc;
                        memorymr.BackColor = Color.DimGray;
                        memorymr.Enabled = false;
                        memorymc.BackColor = Color.DimGray;
                        memorymc.Enabled = false;
                    }
                    break;
            }
        }

        private void function_Click(object sender, EventArgs e)
        {
            if (outputmain.Text == "Ошибка" ||
                outputmain.Text == "не число" ||
                outputmain.Text == "-∞" ||
                outputmain.Text == "∞" ||
                outputmain.Text.IndexOf("min") >= 0 ||
                outputmain.Text.IndexOf("max") >= 0)
            {
                clear();
                return;
            }
            Button bn = (Button)sender;
            double tmp = Convert.ToDouble(outputmain.Text);
            string func = "";
            double res = 0;

            switch (bn.Name)
            {
                case "functionfact":
                    func = "fact";
                    res = 1;
                    for (int i = 1; i <= tmp; i++)
                    {
                        res *= i;
                    }
                    break;
                case "functionabs":
                    func = "abs";
                    res = Math.Abs(tmp);
                    break;
                case "functiontan":
                    func = "tan";
                    res = Math.Tan(tmp);
                    break;
                case "functionsin":
                    func = "sin";
                    res = Math.Sin(tmp);
                    break;
                case "functioncos":
                    func = "cos";
                    res = Math.Cos(tmp);
                    break;
                case "functionsqrt":
                    func = "sqrt";
                    res = Math.Sqrt(tmp);
                    break;
                case "functionsqr":
                    func = "sqr";
                    res = Math.Pow(tmp, 2);
                    break;
                case "functionpro":
                    if (moushen == "+" || moushen == "-")
                    {
                        res = result * tmp / 100;
                    }
                    else if(moushen == "×" || moushen == "÷")
                    {
                        res = tmp / 100;
                    }
                    outputmain.Text = Convert.ToString(res);
                    return;
                case "functionexp":
                    func = "exp";
                    res = Math.Exp(tmp);
                    break;
                case "functionln":
                    func = "ln";
                    res = Math.Log(tmp, Math.E);
                    break;
                case "functionlog2":
                    func = "log2";
                    res = Math.Log(tmp, 2);
                    break;
                case "functionlg":
                    func = "lg";
                    res = Math.Log(tmp, 10);
                    break;
            }

            switch (moushen)
            {
                case "+":
                    result += res;
                    break;
                case "-":
                    result -= res;
                    break;
                case "×":
                    result *= res;
                    break;
                case "÷":
                    if (res == 0)
                    {
                        moushen = "";
                        result = 0;
                        buf = true;
                        outputsub.Text = "";
                        outputmain.Text = "Ошибка";
                        return;
                    }
                    result /= res;
                    break;
                case "^":
                    result = Math.Pow(result, res);
                    break;
                case "":
                    result = res;
                    break;
            }
            if (outputsub.Text.Length == 0 || outputsub.Text.LastIndexOf("=") == outputsub.Text.Length - 2)
            {
                outputsub.Text = func + "(" + tmp + ") = ";
            }
            else
            {
                outputsub.AppendText(func + "(" + tmp + ") = ");
            }
            if(Double.IsNaN(res) || Double.IsInfinity(res))
            {
                outputsub.Text = "";
            }
            outputmain.Text = Convert.ToString(result);
            moushen = "";
            buf = true;
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (outputmain.Text == "Ошибка" ||
                outputmain.Text == "не число" ||
                outputmain.Text.IndexOf("min") >= 0 ||
                outputmain.Text.IndexOf("max") >= 0)
            {
                clear();
                return;
            }
            if (memorycalc == "")
            {
                if (memorycalc == outputmain.Text) return;
                memorycalc = outputmain.Text;
                outputmem.Text = memorycalc;
                memorymr.BackColor = Color.DarkGray;
                memorymr.Enabled = true;
                memorymc.BackColor = Color.DarkGray;
                memorymc.Enabled = true;
                outputmain.Text = "0";
                buf = true;
            }
            else
            {
                if (format == 10)
                {
                    double tmp1 = Convert.ToDouble(outputmain.Text);
                    double tmp2 = Convert.ToDouble(memorycalc);
                    if (tmp1 < tmp2)
                    {
                        outputmain.Text = "min(" + tmp1 + ")";
                    }
                    else
                    {
                        outputmain.Text = "min(" + tmp2 + ")";
                    }
                }
                if (format == 2)
                {
                    Int64 tmp1 = Convert.ToInt64(outputmain.Text, 2);
                    Int64 tmp2 = Convert.ToInt64(memorycalc, 2);
                    if (tmp1 < tmp2)
                    {
                        outputmain.Text = "min(" + Convert.ToString(tmp1, 2) + ")";
                    }
                    else
                    {
                        outputmain.Text = "min(" + Convert.ToString(tmp2, 2) + ")";
                    }
                }
                buf = true;
                memorycalc = "";
                outputmem.Text = memorycalc;
                memorymr.BackColor = Color.DimGray;
                memorymr.Enabled = false;
                memorymc.BackColor = Color.DimGray;
                memorymc.Enabled = false;
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (outputmain.Text == "Ошибка" ||
                outputmain.Text == "не число" ||
                outputmain.Text.IndexOf("min") >= 0 ||
                outputmain.Text.IndexOf("max") >= 0)
            {
                clear();
                return;
            }
            if (memorycalc == "")
            {
                if (memorycalc == outputmain.Text) return;
                memorycalc = outputmain.Text;
                outputmem.Text = memorycalc;
                memorymr.BackColor = Color.DarkGray;
                memorymr.Enabled = true;
                memorymc.BackColor = Color.DarkGray;
                memorymc.Enabled = true;
                outputmain.Text = "0";
                buf = true;
            }
            else
            {
                if (format == 10)
                {
                    double tmp1 = Convert.ToDouble(outputmain.Text);
                    double tmp2 = Convert.ToDouble(memorycalc);
                    if (tmp1 > tmp2)
                    {
                        outputmain.Text = "max(" + tmp1 + ")";
                    }
                    else
                    {
                        outputmain.Text = "max(" + tmp2 + ")";
                    }
                }
                if (format == 2)
                {
                    Int64 tmp1 = Convert.ToInt64(outputmain.Text, 2);
                    Int64 tmp2 = Convert.ToInt64(memorycalc, 2);
                    if (tmp1 > tmp2)
                    {
                        outputmain.Text = "max(" + Convert.ToString(tmp1, 2) + ")";
                    }
                    else
                    {
                        outputmain.Text = "max(" + Convert.ToString(tmp2, 2) + ")";
                    }
                }
                buf = true;
                memorycalc = "";
                outputmem.Text = memorycalc;
                memorymr.BackColor = Color.DimGray;
                memorymr.Enabled = false;
                memorymc.BackColor = Color.DimGray;
                memorymc.Enabled = false;
            }
        }

        private void format_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                if (format == 10) return;
                format = 10;
                clear();
                memorycalc = "";
                outputmem.Text = memorycalc;
                actionpow.Enabled = true;
                functionabs.Enabled = true;
                functioncos.Enabled = true;
                functionexp.Enabled = true;
                functionfact.Enabled = true;
                functionlg.Enabled = true;
                functionln.Enabled = true;
                functionlog2.Enabled = true;
                functionpro.Enabled = true;
                functionsin.Enabled = true;
                functionsqr.Enabled = true;
                functionsqrt.Enabled = true;
                functiontan.Enabled = true;
                numz.Enabled = true;
                num2.Enabled = true;
                num3.Enabled = true;
                num4.Enabled = true;
                num5.Enabled = true;
                num6.Enabled = true;
                num7.Enabled = true;
                num8.Enabled = true;
                num9.Enabled = true;
            }
            if (radioButton2.Checked == true)
            {
                if (format == 2) return;
                format = 2;
                clear();
                memorycalc = "";
                outputmem.Text = memorycalc;
                actionpow.Enabled = false;
                functionabs.Enabled = false;
                functioncos.Enabled = false;
                functionexp.Enabled = false;
                functionfact.Enabled = false;
                functionlg.Enabled = false;
                functionln.Enabled = false;
                functionlog2.Enabled = false;
                functionpro.Enabled = false;
                functionsin.Enabled = false;
                functionsqr.Enabled = false;
                functionsqrt.Enabled = false;
                functiontan.Enabled = false;
                numz.Enabled = false;
                num2.Enabled = false;
                num3.Enabled = false;
                num4.Enabled = false;
                num5.Enabled = false;
                num6.Enabled = false;
                num7.Enabled = false;
                num8.Enabled = false;
                num9.Enabled = false;
            }
        }
    }
}
