﻿
namespace AppWebCrawlingForm
{
    partial class FrmDaumLogin
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
            this.gbDaumLogin = new System.Windows.Forms.GroupBox();
            this.btnGoDaum = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbDaumLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDaumLogin
            // 
            this.gbDaumLogin.Controls.Add(this.btnGoDaum);
            this.gbDaumLogin.Controls.Add(this.btnLogin);
            this.gbDaumLogin.Controls.Add(this.txtPWD);
            this.gbDaumLogin.Controls.Add(this.label2);
            this.gbDaumLogin.Controls.Add(this.txtUserID);
            this.gbDaumLogin.Controls.Add(this.label1);
            this.gbDaumLogin.Location = new System.Drawing.Point(26, 34);
            this.gbDaumLogin.Name = "gbDaumLogin";
            this.gbDaumLogin.Size = new System.Drawing.Size(322, 148);
            this.gbDaumLogin.TabIndex = 0;
            this.gbDaumLogin.TabStop = false;
            this.gbDaumLogin.Text = "다음 사이트 로그인";
            // 
            // btnGoDaum
            // 
            this.btnGoDaum.Location = new System.Drawing.Point(60, 20);
            this.btnGoDaum.Name = "btnGoDaum";
            this.btnGoDaum.Size = new System.Drawing.Size(137, 49);
            this.btnGoDaum.TabIndex = 3;
            this.btnGoDaum.Text = "다음 접속";
            this.btnGoDaum.UseVisualStyleBackColor = true;
            this.btnGoDaum.Click += new System.EventHandler(this.btnGoDaum_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(203, 75);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(96, 48);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "로그인";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPWD
            // 
            this.txtPWD.Location = new System.Drawing.Point(60, 102);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.PasswordChar = '*';
            this.txtPWD.Size = new System.Drawing.Size(137, 21);
            this.txtPWD.TabIndex = 1;
            this.txtPWD.Text = "dong8634";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "암호:";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(60, 75);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(137, 21);
            this.txtUserID.TabIndex = 1;
            this.txtUserID.Text = "dongyoul.shin@gmail.com";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID: ";
            // 
            // FrmDaumLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 202);
            this.Controls.Add(this.gbDaumLogin);
            this.Name = "FrmDaumLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "크롤링 폼";
            this.gbDaumLogin.ResumeLayout(false);
            this.gbDaumLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDaumLogin;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGoDaum;
    }
}

