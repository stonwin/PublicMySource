namespace KAPTData
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnRetrieve = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.sccArea = new DevExpress.XtraEditors.SplitContainerControl();
            this.xgList = new SiS.Framework.Extension.Xtra.XtraControls.GridControl();
            this.xgListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rtxtTelNo = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.controlNavigationBar1 = new SiS.Framework.Extension.Xtra.CompositeControls.ControlNavigationBar();
            this.lueDong = new DevExpress.XtraEditors.LookUpEdit();
            this.lueGuGun = new DevExpress.XtraEditors.LookUpEdit();
            this.lueSiDo = new DevExpress.XtraEditors.LookUpEdit();
            this.cbYear = new SiS.Framework.Extension.Xtra.XtraControls.ComboBoxEdit();
            this.lblYear = new SiS.Framework.Extension.Xtra.XtraControls.LabelControl();
            this.cbeMonth = new SiS.Framework.Extension.Xtra.XtraControls.ComboBoxEdit();
            this.panelControl2 = new SiS.Framework.Extension.Xtra.XtraControls.PanelControl();
            this.btnGugunAll = new SiS.Framework.Extension.Xtra.XtraControls.SimpleButton();
            this.textEdit1 = new SiS.Framework.Extension.Xtra.XtraControls.TextEdit();
            this.chkCopyOption = new SiS.Framework.Extension.Xtra.XtraControls.CheckEdit();
            this.btnClear = new SiS.Framework.Extension.Xtra.XtraControls.SimpleButton();
            this.xgKAPT = new SiS.Framework.Extension.Xtra.XtraControls.GridControl();
            this.xgKAPTView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new SiS.Framework.Extension.Xtra.XtraControls.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.sccArea)).BeginInit();
            this.sccArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtTelNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueGuGun.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSiDo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCopyOption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgKAPT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgKAPTView)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(981, 36);
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
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(0, 0);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "A42370103,A42370102";
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point(669, 32);
            this.btnRetrieve.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(75, 29);
            this.btnRetrieve.TabIndex = 5;
            this.btnRetrieve.Text = "조회";
            this.btnRetrieve.UseVisualStyleBackColor = true;
            this.btnRetrieve.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(961, 5);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "A42370102";
            this.textBox1.Visible = false;
            // 
            // sccArea
            // 
            this.sccArea.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.sccArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sccArea.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.sccArea.Horizontal = false;
            this.sccArea.Location = new System.Drawing.Point(0, 197);
            this.sccArea.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sccArea.Name = "sccArea";
            this.sccArea.Panel1.Controls.Add(this.xgList);
            this.sccArea.Panel1.Controls.Add(this.controlNavigationBar1);
            this.sccArea.Panel1.Text = "Panel1";
            this.sccArea.Panel2.Controls.Add(this.richTextBox1);
            this.sccArea.Panel2.Text = "Panel2";
            this.sccArea.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            this.sccArea.Size = new System.Drawing.Size(1204, 484);
            this.sccArea.SplitterPosition = 211;
            this.sccArea.TabIndex = 6;
            // 
            // xgList
            // 
            this.xgList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgList.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xgList.EnableExportMenu = true;
            this.xgList.IsViewRowNumber = true;
            this.xgList.IsViewRowState = false;
            this.xgList.Location = new System.Drawing.Point(0, 28);
            this.xgList.MainView = this.xgListView;
            this.xgList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xgList.Name = "xgList";
            this.xgList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtTelNo});
            this.xgList.Size = new System.Drawing.Size(1204, 456);
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
            this.xgListView.OptionsBehavior.Editable = false;
            this.xgListView.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.True;
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
            this.xgListView.OptionsSelection.MultiSelect = true;
            this.xgListView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.xgListView.OptionsView.BestFitMaxRowCount = 200;
            this.xgListView.OptionsView.ColumnAutoWidth = false;
            this.xgListView.OptionsView.EnableAppearanceEvenRow = true;
            this.xgListView.OptionsView.ShowGroupPanel = false;
            this.xgListView.RowHeight = 29;
            // 
            // rtxtTelNo
            // 
            this.rtxtTelNo.AutoHeight = false;
            this.rtxtTelNo.Mask.EditMask = "(999) 9000-0000";
            this.rtxtTelNo.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            this.rtxtTelNo.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtTelNo.Name = "rtxtTelNo";
            // 
            // controlNavigationBar1
            // 
            this.controlNavigationBar1.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.controlNavigationBar1.CausesValidation = false;
            this.controlNavigationBar1.CurrentPosition = 0;
            this.controlNavigationBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlNavigationBar1.EnableAppend = false;
            this.controlNavigationBar1.EnableCancelEdit = false;
            this.controlNavigationBar1.Location = new System.Drawing.Point(0, 0);
            this.controlNavigationBar1.MaximumSize = new System.Drawing.Size(1000000, 32);
            this.controlNavigationBar1.MinimumSize = new System.Drawing.Size(130, 28);
            this.controlNavigationBar1.Name = "controlNavigationBar1";
            this.controlNavigationBar1.NavigatableControl = this.xgList;
            this.controlNavigationBar1.Padding = new System.Windows.Forms.Padding(2);
            this.controlNavigationBar1.RecordFont = new System.Drawing.Font("맑은 고딕", 9F);
            this.controlNavigationBar1.RecordForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.controlNavigationBar1.Size = new System.Drawing.Size(1204, 28);
            this.controlNavigationBar1.TabIndex = 2;
            this.controlNavigationBar1.TabStop = false;
            this.controlNavigationBar1.TitleFont = new System.Drawing.Font("맑은 고딕", 9F);
            // 
            // lueDong
            // 
            this.lueDong.Location = new System.Drawing.Point(418, 36);
            this.lueDong.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lueDong.Name = "lueDong";
            this.lueDong.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lueDong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDong.Properties.NullText = "동 선택";
            this.lueDong.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            this.lueDong.Size = new System.Drawing.Size(188, 22);
            this.lueDong.TabIndex = 4;
            this.lueDong.EditValueChanged += new System.EventHandler(this.lueDong_EditValueChanged);
            // 
            // lueGuGun
            // 
            this.lueGuGun.Location = new System.Drawing.Point(210, 36);
            this.lueGuGun.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lueGuGun.Name = "lueGuGun";
            this.lueGuGun.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lueGuGun.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueGuGun.Properties.NullText = "구군 선택";
            this.lueGuGun.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            this.lueGuGun.Size = new System.Drawing.Size(188, 22);
            this.lueGuGun.TabIndex = 3;
            this.lueGuGun.EditValueChanged += new System.EventHandler(this.lueGuGun_EditValueChanged);
            // 
            // lueSiDo
            // 
            this.lueSiDo.Location = new System.Drawing.Point(5, 36);
            this.lueSiDo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lueSiDo.Name = "lueSiDo";
            this.lueSiDo.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lueSiDo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueSiDo.Properties.NullText = "시도 선택";
            this.lueSiDo.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            this.lueSiDo.Size = new System.Drawing.Size(188, 22);
            this.lueSiDo.TabIndex = 2;
            this.lueSiDo.EditValueChanged += new System.EventHandler(this.lueSiDo_EditValueChanged);
            // 
            // cbYear
            // 
            this.cbYear.Location = new System.Drawing.Point(65, 5);
            this.cbYear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbYear.Name = "cbYear";
            this.cbYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbYear.Size = new System.Drawing.Size(104, 22);
            this.cbYear.TabIndex = 0;
            this.cbYear.SelectedIndexChanged += new System.EventHandler(this.cbYear_SelectedIndexChanged);
            this.cbYear.EditValueChanged += new System.EventHandler(this.cbYear_EditValueChanged);
            // 
            // lblYear
            // 
            this.lblYear.Location = new System.Drawing.Point(5, 8);
            this.lblYear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(48, 15);
            this.lblYear.TabIndex = 8;
            this.lblYear.Text = "발생년월";
            // 
            // cbeMonth
            // 
            this.cbeMonth.Location = new System.Drawing.Point(188, 5);
            this.cbeMonth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbeMonth.Name = "cbeMonth";
            this.cbeMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbeMonth.Size = new System.Drawing.Size(87, 22);
            this.cbeMonth.TabIndex = 1;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnGugunAll);
            this.panelControl2.Controls.Add(this.textEdit1);
            this.panelControl2.Controls.Add(this.chkCopyOption);
            this.panelControl2.Controls.Add(this.btnClear);
            this.panelControl2.Controls.Add(this.xgKAPT);
            this.panelControl2.Controls.Add(this.lueDong);
            this.panelControl2.Controls.Add(this.textBox1);
            this.panelControl2.Controls.Add(this.lueGuGun);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.lblYear);
            this.panelControl2.Controls.Add(this.lueSiDo);
            this.panelControl2.Controls.Add(this.button1);
            this.panelControl2.Controls.Add(this.cbeMonth);
            this.panelControl2.Controls.Add(this.btnRetrieve);
            this.panelControl2.Controls.Add(this.cbYear);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1204, 197);
            this.panelControl2.TabIndex = 9;
            // 
            // btnGugunAll
            // 
            this.btnGugunAll.Location = new System.Drawing.Point(210, 71);
            this.btnGugunAll.Name = "btnGugunAll";
            this.btnGugunAll.Size = new System.Drawing.Size(188, 29);
            this.btnGugunAll.TabIndex = 13;
            this.btnGugunAll.Text = "현재 구군 모든 단지 가져오기";
            this.btnGugunAll.Click += new System.EventHandler(this.btnGugunAll_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = "1000";
            this.textEdit1.Location = new System.Drawing.Point(669, 68);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(75, 22);
            this.textEdit1.TabIndex = 12;
            // 
            // chkCopyOption
            // 
            this.chkCopyOption.Location = new System.Drawing.Point(86, 166);
            this.chkCopyOption.Name = "chkCopyOption";
            this.chkCopyOption.Properties.Caption = "복사시 칼럼 해더 제외";
            this.chkCopyOption.Size = new System.Drawing.Size(150, 20);
            this.chkCopyOption.TabIndex = 11;
            this.chkCopyOption.CheckedChanged += new System.EventHandler(this.chkCopyOption_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(5, 161);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 29);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "초기화";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // xgKAPT
            // 
            this.xgKAPT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgKAPT.IsViewRowNumber = true;
            this.xgKAPT.IsViewRowState = false;
            this.xgKAPT.Location = new System.Drawing.Point(750, 5);
            this.xgKAPT.MainView = this.xgKAPTView;
            this.xgKAPT.Name = "xgKAPT";
            this.xgKAPT.Size = new System.Drawing.Size(449, 185);
            this.xgKAPT.TabIndex = 9;
            this.xgKAPT.UseLookAndFeel = true;
            this.xgKAPT.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.xgKAPTView});
            // 
            // xgKAPTView
            // 
            this.xgKAPTView.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.xgKAPTView.Appearance.EvenRow.Options.UseBackColor = true;
            this.xgKAPTView.Appearance.FocusedCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xgKAPTView.Appearance.FocusedCell.Options.UseForeColor = true;
            this.xgKAPTView.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(168)))), ((int)(((byte)(201)))));
            this.xgKAPTView.Appearance.FocusedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xgKAPTView.Appearance.FocusedRow.Options.UseBackColor = true;
            this.xgKAPTView.Appearance.FocusedRow.Options.UseForeColor = true;
            this.xgKAPTView.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xgKAPTView.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.xgKAPTView.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.xgKAPTView.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xgKAPTView.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.xgKAPTView.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.xgKAPTView.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.xgKAPTView.Appearance.Row.Options.UseBackColor = true;
            this.xgKAPTView.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(168)))), ((int)(((byte)(201)))));
            this.xgKAPTView.Appearance.SelectedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xgKAPTView.Appearance.SelectedRow.Options.UseBackColor = true;
            this.xgKAPTView.Appearance.SelectedRow.Options.UseForeColor = true;
            this.xgKAPTView.GridControl = this.xgKAPT;
            this.xgKAPTView.HorzScrollStep = 15;
            this.xgKAPTView.IndicatorWidth = 50;
            this.xgKAPTView.Name = "xgKAPTView";
            this.xgKAPTView.OptionsBehavior.Editable = false;
            this.xgKAPTView.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xgKAPTView.OptionsCustomization.AllowFilter = false;
            this.xgKAPTView.OptionsCustomization.AllowQuickHideColumns = false;
            this.xgKAPTView.OptionsDetail.EnableMasterViewMode = false;
            this.xgKAPTView.OptionsFind.AllowFindPanel = false;
            this.xgKAPTView.OptionsLayout.StoreAllOptions = true;
            this.xgKAPTView.OptionsLayout.StoreAppearance = true;
            this.xgKAPTView.OptionsMenu.EnableColumnMenu = false;
            this.xgKAPTView.OptionsMenu.EnableFooterMenu = false;
            this.xgKAPTView.OptionsMenu.EnableGroupPanelMenu = false;
            this.xgKAPTView.OptionsNavigation.AutoFocusNewRow = true;
            this.xgKAPTView.OptionsView.BestFitMaxRowCount = 200;
            this.xgKAPTView.OptionsView.ColumnAutoWidth = false;
            this.xgKAPTView.OptionsView.EnableAppearanceEvenRow = true;
            this.xgKAPTView.OptionsView.ShowGroupPanel = false;
            this.xgKAPTView.OptionsView.ShowViewCaption = true;
            this.xgKAPTView.RowHeight = 23;
            this.xgKAPTView.ViewCaption = "조회 단지";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(404, 97);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(340, 15);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "조회간격(1000-->1초): 일괄조회 건수가 많은 경우 시간 늘릴것";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 681);
            this.Controls.Add(this.sccArea);
            this.Controls.Add(this.panelControl2);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("FrmMain.IconOptions.SvgImage")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmMain";
            this.Text = "단지 정보 조회 (V.1.1)";
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.sccArea)).EndInit();
            this.sccArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtTelNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueGuGun.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSiDo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCopyOption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgKAPT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgKAPTView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnRetrieve;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraEditors.SplitContainerControl sccArea;
        private DevExpress.XtraEditors.LookUpEdit lueSiDo;
        private DevExpress.XtraEditors.LookUpEdit lueGuGun;
        private DevExpress.XtraEditors.LookUpEdit lueDong;
        private SiS.Framework.Extension.Xtra.XtraControls.GridControl xgList;
        private DevExpress.XtraGrid.Views.Grid.GridView xgListView;
        private SiS.Framework.Extension.Xtra.XtraControls.ComboBoxEdit cbYear;
        private SiS.Framework.Extension.Xtra.XtraControls.LabelControl lblYear;
        private SiS.Framework.Extension.Xtra.XtraControls.ComboBoxEdit cbeMonth;
        private SiS.Framework.Extension.Xtra.XtraControls.PanelControl panelControl2;
        private SiS.Framework.Extension.Xtra.XtraControls.GridControl xgKAPT;
        private DevExpress.XtraGrid.Views.Grid.GridView xgKAPTView;
        private SiS.Framework.Extension.Xtra.XtraControls.SimpleButton btnClear;
        private SiS.Framework.Extension.Xtra.CompositeControls.ControlNavigationBar controlNavigationBar1;
        private SiS.Framework.Extension.Xtra.XtraControls.CheckEdit chkCopyOption;
        private SiS.Framework.Extension.Xtra.XtraControls.TextEdit textEdit1;
        private SiS.Framework.Extension.Xtra.XtraControls.LabelControl labelControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtTelNo;
        private SiS.Framework.Extension.Xtra.XtraControls.SimpleButton btnGugunAll;
    }
}

