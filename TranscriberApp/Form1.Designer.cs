namespace TranscriberApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnTranscribe = new Button();
            txtResults = new TextBox();
            btnSaveToFile = new Button();
            SuspendLayout();
            // 
            // btnTranscribe
            // 
            btnTranscribe.Location = new Point(38, 21);
            btnTranscribe.Name = "btnTranscribe";
            btnTranscribe.Size = new Size(167, 23);
            btnTranscribe.TabIndex = 0;
            btnTranscribe.Text = "Select Audio to Transcribe";
            btnTranscribe.UseVisualStyleBackColor = true;
            btnTranscribe.Click += btnTranscribe_Click;
            // 
            // txtResults
            // 
            txtResults.Location = new Point(38, 63);
            txtResults.Multiline = true;
            txtResults.Name = "txtResults";
            txtResults.ShortcutsEnabled = false;
            txtResults.Size = new Size(734, 355);
            txtResults.TabIndex = 1;
            txtResults.TextChanged += txtResults_TextChanged;
            // 
            // btnSaveToFile
            // 
            btnSaveToFile.Location = new Point(38, 437);
            btnSaveToFile.Name = "btnSaveToFile";
            btnSaveToFile.Size = new Size(75, 23);
            btnSaveToFile.TabIndex = 2;
            btnSaveToFile.Text = "Save to File";
            btnSaveToFile.UseVisualStyleBackColor = true;
            btnSaveToFile.Click += btnSaveToFile_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 483);
            Controls.Add(btnSaveToFile);
            Controls.Add(txtResults);
            Controls.Add(btnTranscribe);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnTranscribe;
        private TextBox txtResults;
        private Button btnSaveToFile;
    }
}
