using MySql.Data.MySqlClient;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToS_Data
{
    public partial class MainWindow : Form
    {
        string[] itemRowsPt, etcRowsPt;
        string[] itemRowsEn, etcRowsEn;
        string[] itemRowsKr, etcRowsKr;

        string[] newItemRows, newEtcRows;
 
        MySqlConnection connection;

        public MainWindow()
        {
            InitializeComponent();

            connection = new MySqlConnection("Server=localhost;Database=tosdb;Uid=root;Pwd=;CharSet=utf8");

            try
            {
                connection.Open();
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Falha ao conectar no servidor MySQL.", this.Text);
                Environment.Exit(0);
            }

            if (File.Exists(Directory.GetCurrentDirectory() + @"\ITEM_NEW.tsv") && File.Exists(Directory.GetCurrentDirectory() + @"\ETC_NEW.tsv"))
            {
                newItemRows = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\ITEM_NEW.tsv");
                newEtcRows = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\ETC_NEW.tsv");
                MessageBox.Show("Arquivos \"_NEW.tsv\" carregado.", this.Text);
            }
        }

        private void maps_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DirectoryInfo diretoryGen = new DirectoryInfo(folderBrowserDialog.SelectedPath);
            FileInfo[] files = diretoryGen.GetFiles("*.ies");
            connection.Open();

            foreach (FileInfo file in files)
            {
                string[] rows = File.ReadAllLines(file.FullName);
                string fileName = file.Name.Replace("gentype_", "").Replace("GenType_", "").Replace(".ies", "");

                for (int a = 1; a < rows.Length; a++)
                {
                    string query = string.Empty;
                    query = "INSERT INTO gentypemap (id,MapName," + rows[0].Replace(",Range,", ",Range0,").Replace(",Leave,", ",Leave0,") + ") VALUES (null,'" + fileName + "', ";
                    string values = "'" + rows[a].Replace(", ", @"\, ").Replace(@"\", @"\\") + "');";

                    MySqlCommand command = new MySqlCommand(query + values, connection);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch
                    {
                        File.WriteAllText(@"C:\Users\emanu\Desktop\Erro.txt", query + values, Encoding.UTF8);
                        MessageBox.Show(query + values, "Erro");
                    }
                }
            }
            connection.Close();
            MessageBox.Show("Finalizado.", this.Text);
        }

        private void anchor_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DirectoryInfo diretoryGen = new DirectoryInfo(folderBrowserDialog.SelectedPath);
            FileInfo[] files = diretoryGen.GetFiles("*.ies");
            connection.Open();

            foreach (FileInfo file in files)
            {
                string[] rows = File.ReadAllLines(file.FullName);
                string fileName = file.Name.Replace("anchor_", "").Replace("Anchor_", "").Replace(".ies", "");

                for (int a = 1; a < rows.Length; a++)
                {
                    string query = string.Empty;
                    query = "INSERT INTO anchormap (id,MapName," + rows[0] + ") VALUES (null,'" + fileName + "', ";
                    string values = "'" + rows[a].Replace(", ", @"\, ").Replace(@"\", @"\\") + "');";

                    MySqlCommand command = new MySqlCommand(query + values, connection);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch
                    {
                        File.WriteAllText(@"C:\Users\emanu\Desktop\Erro.txt", query + values, Encoding.UTF8);
                        MessageBox.Show(query + values, "Erro");
                    }
                }
            }
            connection.Close();
            MessageBox.Show("Finalizado.", this.Text);
        }

        private void downloadData_Click(object sender, EventArgs e)
        {
            downloadData.Enabled = false;
            new Thread(downloadDataFromGit) { IsBackground = true }.Start();
            downloadData.Enabled = true;
        }


        private void tableGen_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string[] rows = File.ReadAllLines(openFileDialog.FileName);

            string name = Path.GetFileName(openFileDialog.FileName).Replace(".ies", "");
            string table = "CREATE TABLE " + name + " (\n\tClassID int NOT NULL AUTO_INCREMENT,";
            string[] colName = rows[0].Split(',');
            string[] colValue = rows[1].Split(new string[] { "',\'" }, StringSplitOptions.None);

            for (int a = 1; a < colName.Length; a++)
            {
                try
                {
                    int.Parse(colValue[a]);
                    table += "\n\t" + colName[a] + " INT DEFAULT 0,";
                }
                catch
                {
                    if (colName[a].Contains("Desc"))
                    {
                        table += "\n\tDescription TEXT,";
                    } else {
                        table += "\n\t" + colName[a] + " VARCHAR(85) CHARACTER SET utf8 COLLATE utf8_general_ci,";
                    }
                }
            }
            table += "\n\n\tPRIMARY KEY(ClassID)\n);";

            File.WriteAllText(Directory.GetCurrentDirectory() + @"\" + name + ".sql", table, Encoding.UTF8);
            MessageBox.Show("Finalizado.", this.Text);
        }

        private void importDataTable_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = Regex.Replace(tableName.Text, @"([A-Z])", "_" + new Regex(@"([A-Z])").Match(tableName.Text).Value) + ".ies";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string[] rows = File.ReadAllLines(openFileDialog.FileName);
            string query = string.Empty;

            connection.Open();
            for (int a = 1; a < rows.Length; a++)
            {
                string[] rowData = rows[a].Split(new string[] { "',\'" }, StringSplitOptions.None);
                query = "INSERT INTO " + tableName.Text + " VALUES (";

                for (int b = 0; b < rowData.Length; b++)
                {
                    string data = rowData[b];
                    query += "'" + data.Replace(@"\", @"\\").Replace("'", "\\'") + "',";
                }
                query = query.Remove(query.Length - 1);
                query += ");";

                MySqlCommand command = new MySqlCommand(query, connection);
                try {
                    command.ExecuteNonQuery();
                } catch {
                    File.WriteAllText(@"C:\Users\emanu\Desktop\Erro.txt", query, Encoding.UTF8);
                    MessageBox.Show(query, this.Text);
                }
            }

            connection.Close();
            MessageBox.Show("Finalizado", this.Text);
        }

        void downloadDataFromGit()
        {
            using (WebClient a = new WebClient())
            {
                itemRowsPt = a.DownloadString("https://raw.githubusercontent.com/Treeofsavior/PortugueseTranslation/master/ITEM_BP.tsv").Replace("\t\n", "\n").Split('\n');
                itemRowsEn = a.DownloadString("https://raw.githubusercontent.com/Treeofsavior/EnglishTranslation/master/EnglishTranslation-master/ITEM.tsv").Replace("\t\n", "\n").Split('\n');
                itemRowsKr = a.DownloadString("https://raw.githubusercontent.com/Treeofsavior/EnglishTranslation/master/EnglishTranslation-master/ITEM_kor.tsv").Replace("\t\n", "\n").Split('\n');

                etcRowsPt = a.DownloadString("https://raw.githubusercontent.com/Treeofsavior/PortugueseTranslation/master/ETC_BP.tsv").Replace("\t\n", "\n").Split('\n');
                etcRowsEn = a.DownloadString("https://raw.githubusercontent.com/Treeofsavior/EnglishTranslation/master/EnglishTranslation-master/ETC.tsv").Replace("\t\n", "\n").Split('\n');
                etcRowsKr = a.DownloadString("https://raw.githubusercontent.com/Treeofsavior/EnglishTranslation/master/EnglishTranslation-master/ETC_kor.tsv").Replace("\t\n", "\n").Split('\n');
            }

            newItemRows = new string[itemRowsKr.Length];
            newEtcRows = new string[etcRowsKr.Length];
            
            try {
                for (int a = 0; a < itemRowsKr.Length - 1; a += 1)
                {
                    string[] valuesPt = null;
                    try
                    {
                        valuesPt = itemRowsPt[a].Split('\t');
                    }
                    catch { }
                    string[] valuesEn = itemRowsEn[a].Split('\t');
                    string[] valuesKr = itemRowsKr[a].Split('\t');
                    string tempString = string.Empty;

                    if (valuesEn.Length < 2)
                    {
                        tempString = valuesKr[0] + "\t" + valuesKr[1];
                        itemRowsEn[a] = tempString;
                        valuesEn = itemRowsEn[a].Split('\t');
                    }

                    if (valuesPt == null || valuesPt.Length < 2)
                    {
                        tempString = valuesEn[0] + "\t" + valuesEn[1];
                        newItemRows[a] = tempString.TrimStart().TrimEnd();
                    }
                    else
                    {
                        newItemRows[a] = itemRowsPt[a].TrimStart().TrimEnd();
                    }
                }

                for (int a = 0; a < etcRowsKr.Length - 1; a += 1)
                {
                    string[] valuesPt = null;
                    try
                    {
                        valuesPt = etcRowsPt[a].Split('\t');
                    }
                    catch { }

                    string[] valuesEn = etcRowsEn[a].Split('\t');
                    string[] valuesKr = etcRowsKr[a].Split('\t');
                    string tempString = string.Empty;

                    if (valuesEn.Length < 2)
                    {
                        tempString = valuesKr[0] + "\t" + valuesKr[1];
                        etcRowsEn[a] = tempString;
                        valuesEn = etcRowsEn[a].Split('\t');
                    }

                    if (valuesPt == null || valuesPt.Length < 2)
                    {
                        tempString = valuesEn[0] + "\t" + valuesEn[1];
                        newEtcRows[a] = tempString.TrimStart().TrimEnd();
                    }
                    else
                    {
                        newEtcRows[a] = etcRowsPt[a].TrimStart().TrimEnd();
                    }
                }
            } catch { }
            newItemRows = newItemRows.Take(newItemRows.Length - 1).ToArray();
            newEtcRows = newEtcRows.Take(newEtcRows.Length - 1).ToArray();

            File.WriteAllLines(Directory.GetCurrentDirectory() + @"\ITEM_NEW.tsv", newItemRows, Encoding.UTF8);
            File.WriteAllLines(Directory.GetCurrentDirectory() + @"\ETC_NEW.tsv", newEtcRows, Encoding.UTF8);

            MessageBox.Show("End!", this.Text);
        }
    }
}
