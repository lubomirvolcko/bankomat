using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bankomat_
{
    public partial class Form1 : Form
    {

        string pass = "";
        string name = "";
        string officialPass;
        string isBlocked;
        int attempts = 0;

        public Form1()
        {
            InitializeComponent();

            setOkEnabledFalse(); // block button OK
            setNumsEnabledFalse(); // block nums buttons
            btnInsert.Enabled = false;
            btnPullOut.Enabled = false;
            btnC.Enabled = false;
            txtDisplay.Text = "choose a user";

            txtDisplay.TextAlign = HorizontalAlignment.Center; // set text align in textBox to center
        }

        private void txtDisplay_TextChanged(object sender, EventArgs e)
        {
            if(txtDisplay.Text.Length == 4)
            {
                setNumsEnabledFalse(); // block nums buttons
                setOkEnabledTrue(); // set button OK to enable
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //btn janko
            name = "janko";
            btnInsert.Enabled = true;
        }

        private void btnFerko_Click(object sender, EventArgs e)
        {
            name = "ferko";
            btnInsert.Enabled = true;
        }

        private void btnSlavo_Click(object sender, EventArgs e)
        {
            name = "slavo";
            btnInsert.Enabled = true;
        }

        private void btnMato_Click(object sender, EventArgs e)
        {
            name = "mato";
            btnInsert.Enabled = true;
        }

        private void btnJozef_Click(object sender, EventArgs e)
        {
            name = "jozef";
            btnInsert.Enabled = true;
        }

        private void btnErik_Click(object sender, EventArgs e)
        {
            name = "erik";
            btnInsert.Enabled = true;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "9";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "0";
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "";
            setNumsEnabledTrue();
            setOkEnabledFalse();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(txtDisplay.Text == "errPin1" || txtDisplay.Text == "errPin2" || txtDisplay.Text == "enter a pin")
            {
                setTxtColorBlack();
                txtDisplay.Text = "";
                txtDisplay.PasswordChar = '*';
                btnC.Enabled = true;
                setNumsEnabledTrue();
                setOkEnabledFalse();
            }
            else if(txtDisplay.Text == "errPin3")
            {
                txtDisplay.Text = "errBlocked";
            }
            else if (txtDisplay.Text == "pin ok")
            {
                closeConnection();
                openConnection();
                setTxtColorBlack();
                txtDisplay.Text = "";
                txtDisplay.PasswordChar = '*';
                setNumsEnabledFalse();
                setOkEnabledFalse();
                btnC.Enabled = true;
                name = "";
            }
            else if (txtDisplay.Text == "errBlocked")
            {
                closeConnection();
                openConnection();
                setTxtColorBlack();
                txtDisplay.Text = "pull out";
                setNumsEnabledFalse();
                setOkEnabledFalse();
                btnC.Enabled = true;
                name = "";
            }
            else if(txtDisplay.Text == "Goodbye")
            {
                setUsersEnabledTrue();
                txtDisplay.Text = "choose a user";
                setOkEnabledFalse();
                btnInsert.Enabled = false;
            }
            else if(txtDisplay.Text == name)
            {
                setNumsEnabledTrue();
                txtDisplay.Text = "eneter a pin";
            }
            else if(txtDisplay.Text == "Hello " + name)
            {
                if (isBlocked == "1")
                {
                    txtDisplay.PasswordChar = '\0';
                    setTxtColorRed();
                    txtDisplay.Text = "errBlocked";
                    setOkEnabledTrue();
                }
                else
                {
                    txtDisplay.Text = "enter a pin";
                }
            }
            else
            {
                if (isBlocked == "1")
                {
                    cardBlocked();
                }                    
                else
                {
                    txtDisplay.PasswordChar = '\0';
                    pass = txtDisplay.Text;

                    if (attempts < 3)
                    {
                        if (pass != officialPass)
                        {
                            setTxtColorRed();
                            btnC.Enabled = false;
                            attempts += 1;
                            switch (attempts)
                            {
                                case 1: txtDisplay.Text = "errPin1"; break;
                                case 2: txtDisplay.Text = "errPin2"; break;
                                case 3: txtDisplay.Text = "errPin3"; break;
                            }
                        }
                        else if (pass == officialPass)
                        {
                            setTxtColorGreen();
                            txtDisplay.Text = "pin ok";
                            attempts = 0;
                        }
                    }
                    else if(attempts == 3)
                    {
                        txtDisplay.Text = "errBlocked";
                        cardBlocked();
                        setBlockCardDbs();
                    }

                }
            }
            
            
        }

        public void cardBlocked()
        {
            setTxtColorRed();
            txtDisplay.PasswordChar = '\0';
            txtDisplay.Text = "errBlocked";
        }

        public void setTxtColorRed()
        {
            txtDisplay.ForeColor = Color.Red;
        }

        public void setTxtColorGreen()
        {
            txtDisplay.ForeColor = Color.Green;
        }

        public void setTxtColorBlack()
        {
            txtDisplay.ForeColor = Color.Black;
        }

        public void setNumsEnabledFalse()
        {
            btn0.Enabled = false;
            btn1.Enabled = false;
            btn2.Enabled = false;
            btn3.Enabled = false;
            btn4.Enabled = false;
            btn5.Enabled = false;
            btn6.Enabled = false;
            btn7.Enabled = false;
            btn8.Enabled = false;
            btn9.Enabled = false;
        }

        public void setNumsEnabledTrue()
        {
            btn0.Enabled = true;
            btn1.Enabled = true;
            btn2.Enabled = true;
            btn3.Enabled = true;
            btn4.Enabled = true;
            btn5.Enabled = true;
            btn6.Enabled = true;
            btn7.Enabled = true;
            btn8.Enabled = true;
            btn9.Enabled = true;
        }

        public void setOkEnabledFalse()
        {
            btnOk.Enabled = false;
        }

        public void setOkEnabledTrue()
        {
            btnOk.Enabled = true;
        }

        private void btn1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        public void conToDbs()
        {
            var dbCon = DBconnections.Instance();
            dbCon.DatabaseName = "bankomat";
            if (dbCon.IsConnect())
            {
                if (dbCon.Connection.State == ConnectionState.Open)
                {
                    selectFromDb();
                }
                else
                {
                    openConnection();
                    selectFromDb();
                }
                
            }
        }

        public void selectFromDb()
        {
            var dbCon = DBconnections.Instance();
            string query = "SELECT pass, blocked FROM users where name like '" + name + "';";
            MySqlCommand cmd = new MySqlCommand(query, dbCon.Connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                officialPass = reader.GetString(0);
                isBlocked = reader.GetString(1);
            }
        }

        public void setBlockCardDbs()
        {
            var dbCon = DBconnections.Instance();
            dbCon.DatabaseName = "bankomat";
            if (dbCon.IsConnect())
            {
                string query = "Update users SET blocked='1' where name like '" + name + "';";
                MySqlCommand cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    officialPass = reader.GetString(0);
                    isBlocked = reader.GetString(1);
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            btnInsert.Enabled = false;
            btnPullOut.Enabled = true;
            setUsersEnabledFalse();
            setTextDisplay();
            setOkEnabledTrue();
            conToDbs();
            
            setUsersEnabledFalse();
        }

        public void closeConnection()
        {
            var dbCon = DBconnections.Instance();
            dbCon.Connection.Close();
        }

        public void openConnection()
        {
            var dbCon = DBconnections.Instance();
            dbCon.Connection.Open();
        }

        public void setUsersEnabledTrue()
        {
            btnJanko.Enabled = true;
            btnFerko.Enabled = true;
            btnSlavo.Enabled = true;
            btnMato.Enabled = true;
            btnJozef.Enabled = true;
            btnErik.Enabled = true;
        }

        public void setUsersEnabledFalse()
        {
            btnJanko.Enabled = false;
            btnFerko.Enabled = false;
            btnSlavo.Enabled = false;
            btnMato.Enabled = false;
            btnJozef.Enabled = false;
            btnErik.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnPullOut_Click(object sender, EventArgs e)
        {
            setOkEnabledTrue();
            btnPullOut.Enabled = false;
            closeConnection();
            setNumsEnabledFalse();
            txtDisplay.PasswordChar = '\0';
            setTxtColorBlack();
            txtDisplay.Text = "Goodbye";
            btnC.Enabled = false;
        }

        public void setTextDisplay()
        {
            txtDisplay.PasswordChar = '\0';
            txtDisplay.Text = "Hello " + name;
        }
    }
}
