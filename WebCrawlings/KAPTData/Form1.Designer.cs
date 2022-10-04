namespace KAPTData
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
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.xgList = new SiS.Framework.Extension.Xtra.XtraControls.GridControl();
            this.xgListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lueDong = new DevExpress.XtraEditors.LookUpEdit();
            this.lueGuGun = new DevExpress.XtraEditors.LookUpEdit();
            this.lueSiDo = new DevExpress.XtraEditors.LookUpEdit();
            this.cbYear = new SiS.Framework.Extension.Xtra.XtraControls.ComboBoxEdit();
            this.lblYear = new SiS.Framework.Extension.Xtra.XtraControls.LabelControl();
            this.cbeMonth = new SiS.Framework.Extension.Xtra.XtraControls.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueGuGun.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSiDo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeMonth.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(713, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 69);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1049, 133);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "A42370103,A42370102";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 15);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 29);
            this.button2.TabIndex = 1;
            this.button2.Text = "조회";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(607, 18);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "A42370102";
            this.textBox1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "아파트 코드 목록( \',\'로 나열)";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(12, 51);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.xgList);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1049, 615);
            this.splitContainerControl1.SplitterPosition = 202;
            this.splitContainerControl1.TabIndex = 6;
            // 
            // xgList
            // 
            this.xgList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgList.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xgList.EnableExportMenu = true;
            this.xgList.IsViewRowNumber = true;
            this.xgList.IsViewRowState = false;
            this.xgList.Location = new System.Drawing.Point(0, 0);
            this.xgList.MainView = this.xgListView;
            this.xgList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xgList.Name = "xgList";
            this.xgList.Size = new System.Drawing.Size(1049, 403);
            this.xgList.TabIndex = 1;
            this.xgList.UseLookAndFeel = true;
            this.xgList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.xgListView});
            // 
            // xgListView
            // 
            this.xgListView.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.xgListView.Appearance.EvenRow.Options.UseBackColor = true;
            this.xgListView.Appearance.FocusedCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xgListView.Appearance.FocusedCell.Options.UseForeColor = true;
            this.xgListView.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(168)))), ((int)(((byte)(201)))));
            this.xgListView.Appearance.FocusedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xgListView.Appearance.FocusedRow.Options.UseBackColor = true;
            this.xgListView.Appearance.FocusedRow.Options.UseForeColor = true;
            this.xgListView.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xgListView.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.xgListView.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.xgListView.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xgListView.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.xgListView.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.xgListView.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.xgListView.Appearance.Row.Options.UseBackColor = true;
            this.xgListView.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(168)))), ((int)(((byte)(201)))));
            this.xgListView.Appearance.SelectedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xgListView.Appearance.SelectedRow.Options.UseBackColor = true;
            this.xgListView.Appearance.SelectedRow.Options.UseForeColor = true;
            this.xgListView.DetailHeight = 437;
            this.xgListView.GridControl = this.xgList;
            this.xgListView.HorzScrollStep = 15;
            this.xgListView.IndicatorWidth = 50;
            this.xgListView.Name = "xgListView";
            this.xgListView.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xgListView.OptionsCustomization.AllowFilter = false;
            this.xgListView.OptionsCustomization.AllowQuickHideColumns = false;
            this.xgListView.OptionsDetail.EnableMasterViewMode = false;
            this.xgListView.OptionsFind.AllowFindPanel = false;
            this.xgListView.OptionsLayout.StoreAllOptions = true;
            this.xgListView.OptionsLayout.StoreAppearance = true;
            this.xgListView.OptionsMenu.EnableColumnMenu = false;
            this.xgListView.OptionsMenu.EnableFooterMenu = false;
            this.xgListView.OptionsMenu.EnableGroupPanelMenu = false;
            this.xgListView.OptionsNavigation.AutoFocusNewRow = true;
            this.xgListView.OptionsView.BestFitMaxRowCount = 200;
            this.xgListView.OptionsView.ColumnAutoWidth = false;
            this.xgListView.OptionsView.EnableAppearanceEvenRow = true;
            this.xgListView.OptionsView.ShowGroupPanel = false;
            this.xgListView.RowHeight = 29;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lueDong);
            this.panelControl1.Controls.Add(this.lueGuGun);
            this.panelControl1.Controls.Add(this.lueSiDo);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1049, 69);
            this.panelControl1.TabIndex = 6;
            // 
            // lueDong
            // 
            this.lueDong.Location = new System.Drawing.Point(440, 36);
            this.lueDong.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lueDong.Name = "lueDong";
            this.lueDong.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lueDong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDong.Properties.NullText = "동 선택";
            this.lueDong.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            this.lueDong.Size = new System.Drawing.Size(188, 22);
            this.lueDong.TabIndex = 2;
            this.lueDong.EditValueChanged += new System.EventHandler(this.lueDong_EditValueChanged);
            // 
            // lueGuGun
            // 
            this.lueGuGun.Location = new System.Drawing.Point(232, 36);
            this.lueGuGun.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lueGuGun.Name = "lueGuGun";
            this.lueGuGun.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lueGuGun.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueGuGun.Properties.NullText = "구군 선택";
            this.lueGuGun.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            this.lueGuGun.Size = new System.Drawing.Size(188, 22);
            this.lueGuGun.TabIndex = 1;
            this.lueGuGun.EditValueChanged += new System.EventHandler(this.lueGuGun_EditValueChanged);
            // 
            // lueSiDo
            // 
            this.lueSiDo.Location = new System.Drawing.Point(16, 36);
            this.lueSiDo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lueSiDo.Name = "lueSiDo";
            this.lueSiDo.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lueSiDo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueSiDo.Properties.NullText = "시도 선택";
            this.lueSiDo.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            this.lueSiDo.Size = new System.Drawing.Size(188, 22);
            this.lueSiDo.TabIndex = 0;
            this.lueSiDo.EditValueChanged += new System.EventHandler(this.lueSiDo_EditValueChanged);
            // 
            // cbYear
            // 
            this.cbYear.Location = new System.Drawing.Point(244, 19);
            this.cbYear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbYear.Name = "cbYear";
            this.cbYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbYear.Size = new System.Drawing.Size(87, 22);
            this.cbYear.TabIndex = 7;
            // 
            // lblYear
            // 
            this.lblYear.Location = new System.Drawing.Point(190, 22);
            this.lblYear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(48, 15);
            this.lblYear.TabIndex = 8;
            this.lblYear.Text = "발생년월";
            // 
            // cbeMonth
            // 
            this.cbeMonth.Location = new System.Drawing.Point(337, 19);
            this.cbeMonth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbeMonth.Name = "cbeMonth";
            this.cbeMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbeMonth.Size = new System.Drawing.Size(87, 22);
            this.cbeMonth.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 681);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.cbeMonth);
            this.Controls.Add(this.cbYear);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueGuGun.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSiDo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeMonth.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit lueSiDo;
        private DevExpress.XtraEditors.LookUpEdit lueGuGun;
        private DevExpress.XtraEditors.LookUpEdit lueDong;
        private SiS.Framework.Extension.Xtra.XtraControls.GridControl xgList;
        private DevExpress.XtraGrid.Views.Grid.GridView xgListView;
        private SiS.Framework.Extension.Xtra.XtraControls.ComboBoxEdit cbYear;
        private SiS.Framework.Extension.Xtra.XtraControls.LabelControl lblYear;
        private SiS.Framework.Extension.Xtra.XtraControls.ComboBoxEdit cbeMonth;
    }
}

