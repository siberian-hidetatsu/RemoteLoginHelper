namespace RemoteLoginHelper
{
	partial class MainForm
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
			if ( disposing && (components != null) )
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.textBoxUserID = new System.Windows.Forms.TextBox();
			this.textBoxPassword = new System.Windows.Forms.TextBox();
			this.textBoxLogMessage = new System.Windows.Forms.TextBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxInterval = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.labelWindowTitle = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBoxUserID
			// 
			this.textBoxUserID.Location = new System.Drawing.Point(66, 26);
			this.textBoxUserID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBoxUserID.Name = "textBoxUserID";
			this.textBoxUserID.Size = new System.Drawing.Size(76, 19);
			this.textBoxUserID.TabIndex = 3;
			// 
			// textBoxPassword
			// 
			this.textBoxPassword.Location = new System.Drawing.Point(66, 48);
			this.textBoxPassword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.Size = new System.Drawing.Size(76, 19);
			this.textBoxPassword.TabIndex = 5;
			// 
			// textBoxLogMessage
			// 
			this.textBoxLogMessage.Location = new System.Drawing.Point(13, 74);
			this.textBoxLogMessage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBoxLogMessage.Multiline = true;
			this.textBoxLogMessage.Name = "textBoxLogMessage";
			this.textBoxLogMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxLogMessage.Size = new System.Drawing.Size(263, 78);
			this.textBoxLogMessage.TabIndex = 8;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 30);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "ユーザID";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 51);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "パスワード";
			// 
			// textBoxInterval
			// 
			this.textBoxInterval.Location = new System.Drawing.Point(200, 45);
			this.textBoxInterval.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBoxInterval.Name = "textBoxInterval";
			this.textBoxInterval.Size = new System.Drawing.Size(76, 19);
			this.textBoxInterval.TabIndex = 7;
			this.textBoxInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(197, 26);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "インターバル(ms)";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 12);
			this.label4.TabIndex = 0;
			this.label4.Text = "ウィンドウ";
			// 
			// labelWindowTitle
			// 
			this.labelWindowTitle.AutoSize = true;
			this.labelWindowTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelWindowTitle.Location = new System.Drawing.Point(66, 9);
			this.labelWindowTitle.Name = "labelWindowTitle";
			this.labelWindowTitle.Size = new System.Drawing.Size(62, 14);
			this.labelWindowTitle.TabIndex = 1;
			this.labelWindowTitle.Text = "ウィンドウ名";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(287, 163);
			this.Controls.Add(this.labelWindowTitle);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxLogMessage);
			this.Controls.Add(this.textBoxInterval);
			this.Controls.Add(this.textBoxPassword);
			this.Controls.Add(this.textBoxUserID);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Remote Login Helper";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxUserID;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.TextBox textBoxLogMessage;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxInterval;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label labelWindowTitle;
	}
}

