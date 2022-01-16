using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Checker.AccountChecker;

namespace Account_Checker_DLL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gameversions = "4.00"; // Auto Update Version 
            growid = textBox1.Text;
            password = textBox2.Text;

            if (checkBox1.Checked)
            {
                mac = textBox3.Text;
            }
            else
            {
                mac = GetRandomMacAddress();
            }

            ip = getip();
            port = getport();
            check();
            if (login == "Success")
            {
                foreach (string world in allworld)
                {//create listviewitem1
                    var listViewItem = new ListViewItem(world);

                    listView1.Items.Add(listViewItem);
                }
                foreach (InventoryItem item in player.inventory.items)
                {//create  listviewitem2
                    ItemDatabase.ItemDefinition itemDef = ItemDatabase.GetItemDef(item.itemID);
                    string[] row = { itemDef.itemName, item.amount.ToString() };

                    var listViewItem = new ListViewItem(row);

                    listView2.Items.Add(listViewItem);
                }
                label1.Text = ("World Lock : " + worldlock);
                //create  label1
                label2.Text = ("Gems : " + gems);
                //create  label2
                label3.Text = ("Level : " + level);
                //create  label3
                label4.Text = ("Supporter : " + support);
                //create  label4
                label8.Text = "Success Login";
            }
            else
            {
                label8.Text = "False information gived";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //deneme
            if (checkBox1.Checked)
            {
                textBox3.Enabled = true;
            }
            else if (!checkBox1.Checked)
            {
                textBox3.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hideconsole();
            textBox3.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string repitemname = textbox4.Text;
            string contents;

            WebClient client = new WebClient();
            /*WebProxy wp = new WebProxy("185.242.104.112", 8080); //rate limit bypass
            client.Proxy = wp;*/
            client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36"); //cloudflare bypass
            contents = client.DownloadString("https://growstocks.xyz/item/" + repitemname);
            using (StringReader reader = new StringReader(contents))
            {
                string lines;
                while ((lines = reader.ReadLine()) != null)
                {
                    if (lines.Contains("<p>Price: <b>"))
                    {
                        lines = lines.Replace("<p>Price: <b>", "");
                        lines = lines.Replace("</b></p>", "");
                        lines = Regex.Replace(lines, @"\s+", "");
                        label11.Text = lines;
                    }
                }
            }
        }
    }
}
