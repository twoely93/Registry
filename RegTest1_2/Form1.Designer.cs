namespace RegTest1_2
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.ClassID = new System.Windows.Forms.Button();
            this.RegLogViewer = new System.Windows.Forms.ListBox();
            this.InstanceID = new System.Windows.Forms.Button();
            this.VIPI = new System.Windows.Forms.Button();
            this.USERNAME = new System.Windows.Forms.Button();
            this.ConnectT = new System.Windows.Forms.Button();
            this.LOG_SAVE = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ClassID
            // 
            this.ClassID.Location = new System.Drawing.Point(12, 12);
            this.ClassID.Name = "ClassID";
            this.ClassID.Size = new System.Drawing.Size(197, 23);
            this.ClassID.TabIndex = 0;
            this.ClassID.Text = "장치클래스ID";
            this.ClassID.UseVisualStyleBackColor = true;
            this.ClassID.Click += new System.EventHandler(this.ClassID_Click);
            // 
            // RegLogViewer
            // 
            this.RegLogViewer.FormattingEnabled = true;
            this.RegLogViewer.ItemHeight = 12;
            this.RegLogViewer.Location = new System.Drawing.Point(215, 12);
            this.RegLogViewer.Name = "RegLogViewer";
            this.RegLogViewer.Size = new System.Drawing.Size(1139, 136);
            this.RegLogViewer.TabIndex = 1;
            // 
            // InstanceID
            // 
            this.InstanceID.Location = new System.Drawing.Point(12, 41);
            this.InstanceID.Name = "InstanceID";
            this.InstanceID.Size = new System.Drawing.Size(197, 23);
            this.InstanceID.TabIndex = 2;
            this.InstanceID.Text = "고유인스턴스ID";
            this.InstanceID.UseVisualStyleBackColor = true;
            this.InstanceID.Click += new System.EventHandler(this.InstanceID_Click);
            // 
            // VIPI
            // 
            this.VIPI.Location = new System.Drawing.Point(12, 70);
            this.VIPI.Name = "VIPI";
            this.VIPI.Size = new System.Drawing.Size(197, 23);
            this.VIPI.TabIndex = 3;
            this.VIPI.Text = "제조사ID/제품ID";
            this.VIPI.UseVisualStyleBackColor = true;
            this.VIPI.Click += new System.EventHandler(this.VIPI_Click);
            // 
            // USERNAME
            // 
            this.USERNAME.Location = new System.Drawing.Point(12, 99);
            this.USERNAME.Name = "USERNAME";
            this.USERNAME.Size = new System.Drawing.Size(197, 23);
            this.USERNAME.TabIndex = 4;
            this.USERNAME.Text = "사용자명/볼륨GUID";
            this.USERNAME.UseVisualStyleBackColor = true;
            this.USERNAME.Click += new System.EventHandler(this.USERNAME_Click);
            // 
            // ConnectT
            // 
            this.ConnectT.Location = new System.Drawing.Point(12, 128);
            this.ConnectT.Name = "ConnectT";
            this.ConnectT.Size = new System.Drawing.Size(197, 23);
            this.ConnectT.TabIndex = 5;
            this.ConnectT.Text = "최초연결시각";
            this.ConnectT.UseVisualStyleBackColor = true;
            this.ConnectT.Click += new System.EventHandler(this.ConnectT_Click);
            // 
            // LOG_SAVE
            // 
            this.LOG_SAVE.Location = new System.Drawing.Point(215, 154);
            this.LOG_SAVE.Name = "LOG_SAVE";
            this.LOG_SAVE.Size = new System.Drawing.Size(159, 23);
            this.LOG_SAVE.TabIndex = 6;
            this.LOG_SAVE.Text = "로그파일저장";
            this.LOG_SAVE.UseVisualStyleBackColor = true;
            this.LOG_SAVE.Click += new System.EventHandler(this.LOG_SAVE_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 187);
            this.Controls.Add(this.LOG_SAVE);
            this.Controls.Add(this.ConnectT);
            this.Controls.Add(this.USERNAME);
            this.Controls.Add(this.VIPI);
            this.Controls.Add(this.InstanceID);
            this.Controls.Add(this.RegLogViewer);
            this.Controls.Add(this.ClassID);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ClassID;
        private System.Windows.Forms.ListBox RegLogViewer;
        private System.Windows.Forms.Button InstanceID;
        private System.Windows.Forms.Button VIPI;
        private System.Windows.Forms.Button USERNAME;
        private System.Windows.Forms.Button ConnectT;
        private System.Windows.Forms.Button LOG_SAVE;
    }
}

