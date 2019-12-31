namespace MediaConverter2
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.canselButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.processingListBox = new System.Windows.Forms.ListBox();
            this.finishedListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // canselButton
            // 
            resources.ApplyResources(this.canselButton, "canselButton");
            this.canselButton.Name = "canselButton";
            this.canselButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            resources.ApplyResources(this.SaveButton, "SaveButton");
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ContinueButton
            // 
            resources.ApplyResources(this.ContinueButton, "ContinueButton");
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.UseVisualStyleBackColor = true;
            // 
            // PauseButton
            // 
            resources.ApplyResources(this.PauseButton, "PauseButton");
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // processingListBox
            // 
            this.processingListBox.AllowDrop = true;
            this.processingListBox.FormattingEnabled = true;
            resources.ApplyResources(this.processingListBox, "processingListBox");
            this.processingListBox.Name = "processingListBox";
            this.processingListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.processingListBox_DragDrop);
            // 
            // finishedListBox
            // 
            this.finishedListBox.FormattingEnabled = true;
            resources.ApplyResources(this.finishedListBox, "finishedListBox");
            this.finishedListBox.Name = "finishedListBox";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.finishedListBox);
            this.Controls.Add(this.processingListBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.canselButton);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button canselButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox processingListBox;
        private System.Windows.Forms.ListBox finishedListBox;
    }
}

