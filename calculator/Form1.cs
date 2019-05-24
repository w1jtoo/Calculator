using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class Form1 : Form
    {
        public double Number;
        private bool _clearRealLabel;
        readonly Calc _calc;

        int _mrcPressed; //количество нажатий кнопки MRC

        private bool _pressedZero;
        public Form1()
        {
            InitializeComponent();

            _calc = new Calc();

            labelNumber.Text = @"0";
            labelRealNumbers.Text = "";
        }

        //кнопка Очистка (CE)
        private void buttonClear_Click(object sender, EventArgs e)
        {
            labelNumber.Text = @"0";
            labelRealNumbers.Text = "";
            _calc.Clear_A();
            FreeButtons();
            OnAllNumberButton();
            _mrcPressed = 0;
        }

        //кнопка изменения знака у числа
        private void buttonChangeSign_Click(object sender, EventArgs e)
        {

            if (labelNumber.Text[0] == '-')
            {
                labelNumber.Text = labelNumber.Text.Remove(0, 1);
            }

            else
            {
                labelNumber.Text = @"-" + labelNumber.Text;
            }
        }

        private void buttonPoint_Click(object sender, EventArgs e)
        {
            if ((labelNumber.Text.IndexOf(",", StringComparison.Ordinal) == -1) &&
                (labelNumber.Text.IndexOf("∞", StringComparison.Ordinal) == -1))
                labelNumber.Text += ",";
        }

        private void button0_Click(object sender, EventArgs e)
        {
            labelNumber.Text += @"0";
            
            if (!_pressedZero)
                _pressedZero = true;

            CorrectNumber();

            }

        private void button1_Click(object sender, EventArgs e)
        {
            labelNumber.Text += @"1";

            CorrectNumber();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            labelNumber.Text += @"2";

            CorrectNumber();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            labelNumber.Text += @"3";

            CorrectNumber();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            labelNumber.Text += @"4";

            CorrectNumber();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            labelNumber.Text += @"5";

            CorrectNumber();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            labelNumber.Text += @"6";

            CorrectNumber();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            labelNumber.Text += @"7";


            CorrectNumber();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            labelNumber.Text += @"8";

            CorrectNumber();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            labelNumber.Text += @"9";

            CorrectNumber();
        }

        //удаляем лишний ноль впереди числа, если таковой имеется
        private void CorrectNumber()
        {
            //labelNumber.Text = number.ToString();
            if (labelNumber.Text.IndexOf("∞", StringComparison.Ordinal) != -1)
                labelNumber.Text = labelNumber.Text.Substring(0, labelNumber.Text.Length - 1);

            
            if (labelNumber.Text[0] == '0' && (labelNumber.Text.IndexOf(",", StringComparison.Ordinal) != 1))
                labelNumber.Text = labelNumber.Text.Remove(0, 1);

            
            if (labelNumber.Text[0] == '-')
                if (labelNumber.Text[1] == '0' && (labelNumber.Text.IndexOf(",", StringComparison.Ordinal) != 2))
                    labelNumber.Text = labelNumber.Text.Remove(1, 1);

            if (labelNumber.Text.IndexOf("∞", StringComparison.Ordinal) != -1)
                labelNumber.Text = labelNumber.Text.Substring(0, labelNumber.Text.Length - 1);

            if (labelNumber.Text.Length > 10)
            {
                OffAllNumberButton();

            }
            if (_clearRealLabel)
            {
                _clearRealLabel = false;
                labelRealNumbers.Text = "";
            }
            //else OnAllNumberButton();
        }



        //кнопка Равно
        private void buttonCalc_Click(object sender, EventArgs e)
        {
            if (!buttonMult.Enabled)
            {
                labelRealNumbers.Text += labelNumber.Text;
                labelNumber.Text = _calc.Multiplication(Convert.ToDouble(labelNumber.Text)).ToString();
                _clearRealLabel = true;

            }

            if ((labelRealNumbers.Text.IndexOf("÷", StringComparison.Ordinal) != -1) && labelNumber.Text == "0")
            {
                labelRealNumbers.Text = @"error";
            }
            else if (!buttonDiv.Enabled)
            {
                labelRealNumbers.Text += labelNumber.Text;
                labelNumber.Text = _calc.Division(Convert.ToDouble(labelNumber.Text)).ToString();
                _clearRealLabel = true;
            }

            if (!buttonPlus.Enabled)
            {
                labelRealNumbers.Text += labelNumber.Text;
                labelNumber.Text = _calc.Sum(Convert.ToDouble(labelNumber.Text)).ToString();
                _clearRealLabel = true;
            }
            if (!buttonMinus.Enabled)
            {
                labelRealNumbers.Text += labelNumber.Text;
                labelNumber.Text = _calc.Subtraction(Convert.ToDouble(labelNumber.Text)).ToString();
                _clearRealLabel = true;
            }

            //if (!buttonSqrtX.Enabled)
            //    labelNumber.Text = C.SqrtX(Convert.ToDouble(labelNumber.Text)).ToString();

            if (!buttonDegreeY.Enabled)
            {
                labelRealNumbers.Text += labelNumber.Text;
                labelNumber.Text = _calc.DegreeY(Convert.ToDouble(labelNumber.Text)).ToString();
                _clearRealLabel = true;
            }
            _calc.Clear_A();
            FreeButtons();
            

            _mrcPressed = 0;
        }





        //кнопка Умножение
        private void buttonMult_Click(object sender, EventArgs e)
        {
            if(CanPress())
            {
                _calc.Put_A(Convert.ToDouble(labelNumber.Text));
                labelRealNumbers.Text =labelNumber.Text +  @" × ";
                 
                buttonMult.Enabled = false;
                OnAllNumberButton();
                labelNumber.Text = @"0";
            }
        }

        //кнопка Деление
        private void buttonDiv_Click(object sender, EventArgs e)
        {
            if (CanPress())
            {

                _calc.Put_A(Convert.ToDouble(labelNumber.Text));
                labelRealNumbers.Text = labelNumber.Text + @" ÷ ";
                buttonDiv.Enabled = false;
                OnAllNumberButton();
                labelNumber.Text = @"0";
                OnAllNumberButton();
            }
        }

        //кнопка Сложение
        private void buttonPlus_Click(object sender, EventArgs e)
        {

            _calc.Put_A(Convert.ToDouble(labelNumber.Text));
            labelRealNumbers.Text = labelNumber.Text + " + ";
            buttonPlus.Enabled = false;
            OnAllNumberButton();
            labelNumber.Text = @"0";

        }

        //кнопка Вычитание
        private void buttonMinus_Click(object sender, EventArgs e)
        {
            if (CanPress())
            {
                _calc.Put_A(Convert.ToDouble(labelNumber.Text));
                labelRealNumbers.Text = labelNumber.Text + " - ";

                buttonMinus.Enabled = false;
                OnAllNumberButton();
                labelNumber.Text = @"0";
            }
        }

        //кнопка Корень произвольной степени
        //private void buttonSqrtX_Click(object sender, EventArgs e)
        //{

        //    if (CanPress())
        //    { 
        //        if ((Convert.ToDouble(labelNumber.Text) == (int)(Convert.ToDouble(labelNumber.Text))) &&
        //            ((Convert.ToDouble(labelNumber.Text) >= 0.0)))
        //        {
                    
        //                C.Put_A(Convert.ToDouble(labelNumber.Text));

        //                buttonSqrtX.Enabled = false;
        //                labelRealNumbers.Text = "sqrt(" + labelNumber.Text + ")";

        //                labelNumber.Text = "0";
        //            }
        //            else 
        //            MessageBox.Show("error");
        //        }
        //}

        //кнопка Возведение в произвольную степень
        private void buttonDegreeY_Click(object sender, EventArgs e)
        {
            if (CanPress())
            {
                _calc.Put_A(Convert.ToDouble(labelNumber.Text));

                buttonDegreeY.Enabled = false;
                labelRealNumbers.Text = labelNumber.Text + @" ^ ";
                OnAllNumberButton();
                labelNumber.Text = @"0";
            }
        }

        //кнопка Корень квадратный
        private void buttonSqrt_Click(object sender, EventArgs e)
        {

            if (CanPress())
            {
                if (
                    ((Convert.ToDouble(labelNumber.Text) >= 0.0)))
                {
                    _calc.Put_A(Convert.ToDouble(labelNumber.Text));

                    labelNumber.Text = _calc.Sqrt().ToString();
                    labelRealNumbers.Text = $@"sqrt({labelNumber.Text})";
                    OnAllNumberButton();
                    _calc.Clear_A();
                    FreeButtons();
                }
                else
                    labelRealNumbers.Text = @"error";
                OnAllNumberButton();
                CorrectNumber();
            }
        }

        //кнопка Квадрат числа
        private void buttonSquare_Click(object sender, EventArgs e)
        {
            if (CanPress())
            {
                _calc.Put_A(Convert.ToDouble(labelNumber.Text));
                labelRealNumbers.Text = labelNumber.Text + @"²";
                labelNumber.Text = _calc.Square().ToString(CultureInfo.CurrentCulture);

                OnAllNumberButton();
                _calc.Clear_A();
                FreeButtons();
            }
            OnAllNumberButton();
        }

        //кнопка Факториал
        private void buttonFactorial_Click(object sender, EventArgs e)
        {
            if (CanPress())
            {
                if ((Math.Abs(Convert.ToDouble(labelNumber.Text) - (int)(Convert.ToDouble(labelNumber.Text))) < 1e-3) && 
                    ((Convert.ToDouble(labelNumber.Text) >= 0.0)))
                {
                    _calc.Put_A(Convert.ToDouble(labelNumber.Text));
                    labelRealNumbers.Text = labelNumber.Text + "!";
                    labelNumber.Text = _calc.Factorial().ToString();

                    _calc.Clear_A();
                    FreeButtons();
                }
                else
                    labelRealNumbers.Text = @"error";
                OnAllNumberButton();
            }
        }

        //кнопка М+
        private void buttonMPlus_Click(object sender, EventArgs e)
        {
            _calc.M_Sum(Convert.ToDouble(labelNumber.Text));
        }

        //кнопка М-
        private void buttonMMinus_Click(object sender, EventArgs e)
        {
            _calc.M_Subtraction(Convert.ToDouble(labelNumber.Text));
        }

        //кнопка М*
        private void buttonMMult_Click(object sender, EventArgs e)
        {
            _calc.M_Multiplication(Convert.ToDouble(labelNumber.Text));
        }

        //кнопка М/
        private void buttonMDiv_Click(object sender, EventArgs e)
        {
            _calc.M_Division(Convert.ToDouble(labelNumber.Text));
        }

        //кнопка МRC
        private void buttonMRC_Click(object sender, EventArgs e)
        {
            if (CanPress())
            {
                _mrcPressed++;

                if (_mrcPressed == 1)
                    labelNumber.Text = _calc.MemoryShow().ToString();

                if (_mrcPressed == 2)
                {
                    _calc.Memory_Clear();
                    labelNumber.Text = "0";

                    _mrcPressed = 0;
                }
            }
        }





        private void OffAllNumberButton()
        {
            button0.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            buttonPoint.Enabled = false;
        }
        private void OnAllNumberButton()
        {
            button0.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            buttonPoint.Enabled = true;
        }


        //проверяет не нажата ли еще какая-либо из кнопок мат.операций useless
        private bool CanPress()
        {

            if (!buttonMult.Enabled)
                return false;

            if (!buttonDiv.Enabled)
                return false;

            if (!buttonPlus.Enabled)
                return false;

            if (!buttonMinus.Enabled)
                return false;

            //if (!buttonSqrtX.Enabled)
            //    return false;

            if (!buttonDegreeY.Enabled)
                return false;

            return true;
        }

        //снятие нажатия всех кнопок мат.операций
        private void FreeButtons()
        {
            buttonMult.Enabled = true;
            buttonDiv.Enabled = true;
            buttonPlus.Enabled = true;
            buttonMinus.Enabled = true;
            //buttonSqrtX.Enabled = true;
            buttonDegreeY.Enabled = true;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}