namespace ToS_Data
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.downloadData = new System.Windows.Forms.Button();
            this.tableGen = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableName = new System.Windows.Forms.TextBox();
            this.importDataTable = new System.Windows.Forms.Button();
            this.maps = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.anchor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // downloadData
            // 
            this.downloadData.Location = new System.Drawing.Point(12, 12);
            this.downloadData.Name = "downloadData";
            this.downloadData.Size = new System.Drawing.Size(127, 54);
            this.downloadData.TabIndex = 3;
            this.downloadData.Text = "Download data and merge";
            this.downloadData.UseVisualStyleBackColor = true;
            this.downloadData.Click += new System.EventHandler(this.downloadData_Click);
            // 
            // tableGen
            // 
            this.tableGen.Location = new System.Drawing.Point(12, 100);
            this.tableGen.Name = "tableGen";
            this.tableGen.Size = new System.Drawing.Size(127, 43);
            this.tableGen.TabIndex = 2;
            this.tableGen.Text = "Generate Table";
            this.tableGen.UseVisualStyleBackColor = true;
            this.tableGen.Click += new System.EventHandler(this.tableGen_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "IES File | *.ies";
            // 
            // tableName
            // 
            this.tableName.Location = new System.Drawing.Point(206, 22);
            this.tableName.Name = "tableName";
            this.tableName.Size = new System.Drawing.Size(152, 22);
            this.tableName.TabIndex = 0;
            // 
            // importDataTable
            // 
            this.importDataTable.Location = new System.Drawing.Point(364, 11);
            this.importDataTable.Name = "importDataTable";
            this.importDataTable.Size = new System.Drawing.Size(119, 44);
            this.importDataTable.TabIndex = 1;
            this.importDataTable.Text = "Import data on table";
            this.importDataTable.UseVisualStyleBackColor = true;
            this.importDataTable.Click += new System.EventHandler(this.importDataTable_Click);
            // 
            // maps
            // 
            this.maps.Location = new System.Drawing.Point(364, 100);
            this.maps.Name = "maps";
            this.maps.Size = new System.Drawing.Size(119, 43);
            this.maps.TabIndex = 4;
            this.maps.Text = "Load Maps GenType";
            this.maps.UseVisualStyleBackColor = true;
            this.maps.Click += new System.EventHandler(this.maps_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.SelectedPath = "C:\\Users\\emanu\\Downloads\\IPFUnpacker-master\\Nova pasta";
            // 
            // anchor
            // 
            this.anchor.Location = new System.Drawing.Point(247, 100);
            this.anchor.Name = "anchor";
            this.anchor.Size = new System.Drawing.Size(111, 43);
            this.anchor.TabIndex = 5;
            this.anchor.Text = "Load Maps Anchor";
            this.anchor.UseVisualStyleBackColor = true;
            this.anchor.Click += new System.EventHandler(this.anchor_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 154);
            this.Controls.Add(this.anchor);
            this.Controls.Add(this.maps);
            this.Controls.Add(this.importDataTable);
            this.Controls.Add(this.tableName);
            this.Controls.Add(this.tableGen);
            this.Controls.Add(this.downloadData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "ToS Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button downloadData;
        private System.Windows.Forms.Button tableGen;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox tableName;
        private System.Windows.Forms.Button importDataTable;
        private System.Windows.Forms.Button maps;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button anchor;
    }
}

