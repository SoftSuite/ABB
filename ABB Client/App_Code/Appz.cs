using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ABB.Data;
using ABB.Flow;
using ABB.Flow.Common;

namespace ABBClient
{
    public class Appz
    {
        private static UserData _UserData;
        private static DataTable _sysConfig;

        public static UserData CurrentUserData
        {
            get
            { 
                if (_UserData == null) { _UserData = new UserData(); }
                return _UserData;
            }
            set
            {
                _UserData = value;
            }
        }

        public static void FormatDataGridView(DataGridView zDataGridView, bool AllowUserToAddRows, bool AllowUserToDeleteRows, bool Readonly)
        {
            DataGridViewCellStyle ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle();
            ColumnHeadersDefaultCellStyle.Font = new Font("Ms Sans Serif", 8, FontStyle.Bold);
            ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(218)))), ((int)(((byte)(169)))));
            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            DataGridViewCellStyle AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle();
            //AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(233)))), ((int)(((byte)(203)))));

            DataGridViewCellStyle RowHeadersDefaultCellStyle = new DataGridViewCellStyle();
            RowHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(218)))), ((int)(((byte)(169)))));

            zDataGridView.AllowUserToAddRows = AllowUserToAddRows;
            zDataGridView.AllowUserToDeleteRows = AllowUserToDeleteRows;
            zDataGridView.ReadOnly = Readonly;
            zDataGridView.AutoGenerateColumns = false;
            zDataGridView.AllowUserToOrderColumns = false;
            zDataGridView.AllowUserToResizeColumns = true;
            zDataGridView.AllowUserToResizeRows = true;
            zDataGridView.BackgroundColor = System.Drawing.Color.Silver;
            zDataGridView.ColumnHeadersDefaultCellStyle = ColumnHeadersDefaultCellStyle;
            zDataGridView.AlternatingRowsDefaultCellStyle =AlternatingRowsDefaultCellStyle;
            zDataGridView.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(218)))), ((int)(((byte)(255)))));
            zDataGridView.RowHeadersDefaultCellStyle = RowHeadersDefaultCellStyle;
            zDataGridView.EnableHeadersVisualStyles = false;
            zDataGridView.MultiSelect = false;
            zDataGridView.RowHeadersWidth = 21;
            zDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            zDataGridView.EditMode = DataGridViewEditMode.EditOnKeystroke;

        }

        public static void BuildStatusCombo(ComboBox zCombo)
        {
            DataTable dt = new DataTable("STATUS");
            dt.Columns.Add("RANK");
            dt.Columns.Add("NAME");
            DataRow dRow = dt.NewRow();
            dRow["RANK"] = Constz.Requisition.Status.Waiting.Rank;
            dRow["NAME"] = Constz.Requisition.Status.Waiting.Name;
            dt.Rows.Add(dRow);
            dRow = dt.NewRow();
            dRow["RANK"] = Constz.Requisition.Status.Approved.Rank;
            dRow["NAME"] = Constz.Requisition.Status.Approved.Name;
            dt.Rows.Add(dRow);
            dRow = dt.NewRow();
            dRow["RANK"] = Constz.Requisition.Status.Void.Rank;
            dRow["NAME"] = Constz.Requisition.Status.Void.Name;
            dt.Rows.Add(dRow);

            zCombo.DataSource = dt;
            zCombo.DisplayMember = "NAME";
            zCombo.ValueMember = "RANK";
        }

        public static void BuildCombo(ComboBox zCombo, string TableName, string TextFiledName, string ValueFieldName, string OrderFieldName, string WhereString)
        {
            BuildCombo(zCombo, TableName, TextFiledName, ValueFieldName, OrderFieldName, WhereString, null, null);
        }

        public static void BuildCombo(ComboBox zCombo, string TableName, string TextFiledName, string ValueFieldName, string OrderFieldName, string WhereString, string BlankText, string BlankValue)
        {
            ComboSourceFlow cmbFlow = new ComboSourceFlow();
            DataTable dt = cmbFlow.GetSource(TableName, TextFiledName, ValueFieldName, OrderFieldName, WhereString);
            if (dt != null)
            {
                //zCombo.Items.Clear(); UPDATE BY NANG
                if (BlankText != null || BlankValue != null)
                {
                    DataRow dRow = dt.NewRow();
                    dRow[TextFiledName] = BlankText;
                    dRow[ValueFieldName] = BlankValue;
                    dt.Rows.InsertAt(dRow, 0);
                }

                zCombo.DataSource = dt;
                zCombo.DisplayMember = TextFiledName;
                zCombo.ValueMember = ValueFieldName;
            }
        }

        public static void BuildComboDistinct(ComboBox zCombo, string TableName, string TextFiledName, string ValueFieldName, string OrderFieldName, string WhereString)
        {
            BuildComboDistinct(zCombo, TableName, TextFiledName, ValueFieldName, OrderFieldName, WhereString, null, null);
        }

        public static void BuildComboDistinct(ComboBox zCombo, string TableName, string TextFiledName, string ValueFieldName, string OrderFieldName, string WhereString, string BlankText, string BlankValue)
        {
            ComboSourceFlow cmbFlow = new ComboSourceFlow();
            DataTable dt = cmbFlow.GetSourceDistinct(TableName, TextFiledName, ValueFieldName, OrderFieldName, WhereString);
            if (dt != null)
            {
                zCombo.Items.Clear();
                if (BlankText != null || BlankValue != null)
                {
                    DataRow dRow = dt.NewRow();
                    dRow[TextFiledName] = BlankText;
                    dRow[ValueFieldName] = BlankValue;
                    dt.Rows.InsertAt(dRow, 0);
                }

                zCombo.DataSource = dt;
                zCombo.DisplayMember = TextFiledName;
                zCombo.ValueMember = ValueFieldName;

            }
        }

        public static DialogResult OpenInformationDialog(string text)
        {
            return MessageBox.Show(text, "ผลการทำรายการ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult OpenQuestionDialog(string text)
        {
            return MessageBox.Show(text, "ยืนยันการทำรายการ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        public static DialogResult OpenErrorDialog(string text)
        {
            return MessageBox.Show(text, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult OpenWarningDialog(string text)
        {
            return MessageBox.Show("ไม่สามารถทำรายการได้" + Environment.NewLine + Environment.NewLine + text, "คำเตือน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
        }

        public static void SetSysConfig()
        {
            SysConfigFlow flow = new SysConfigFlow();
            if (_sysConfig == null) _sysConfig = new DataTable();
            _sysConfig = flow.GetDataList();
            DataColumn[] pk = new DataColumn[1];
            pk[0] = _sysConfig.Columns["CONFIGNAME"];
            _sysConfig.PrimaryKey = pk;
        }

        public static string GetSysConfig(string configName)
        {
            string configValue = "";
            if (_sysConfig != null)
            {
                if (_sysConfig.Rows.Count >0)
                {
                    if (_sysConfig.Rows.Contains(configName))
                    {
                        DataRow dRow = _sysConfig.Rows.Find(configName);
                        if (dRow != null) configValue = dRow["CONFIGVALUE"].ToString();
                    }
                }
            }
            return configValue;
        }

        partial class DataGridViewImageCellBlank : DataGridViewImageCell
        {
            public DataGridViewImageCellBlank() : base()
            {
            }
            
            public DataGridViewImageCellBlank(bool valueIsIcon) : base(valueIsIcon)
            {
            }

            public override object DefaultNewRowValue
            {
                get { return ABBClient.Properties.Resources.view; }
            }
        }

        public static void SetDataGridViewImageColumnCellStyle(DataGridViewImageColumn zImageColumn)
        {
            zImageColumn.CellTemplate = new Appz.DataGridViewImageCellBlank(false);
            zImageColumn.DefaultCellStyle.NullValue = ABBClient.Properties.Resources.view;
        }

        public static void BuildRequisitionStatusCombo(ComboBox zCombo)
        {
            DataTable dt = new DataTable("STATUS");
            
            DataRow dRow = dt.NewRow();
            dt.Columns.Add("CODE");
            dt.Columns.Add("NAME");

            ////dt.Rows.Add(dRow);
            ////dRow = dt.NewRow();
            dRow["CODE"] = Constz.Requisition.Status.AllStatus.Code;
            dRow["NAME"] = Constz.Requisition.Status.AllStatus.Name;

            dt.Rows.Add(dRow);
            dRow = dt.NewRow();
            dRow["CODE"] = Constz.Requisition.Status.Waiting.Code;
            dRow["NAME"] = Constz.Requisition.Status.Waiting.Name;

            dt.Rows.Add(dRow);
            dRow = dt.NewRow();
            dRow["CODE"] = Constz.Requisition.Status.Approved.Code;
            dRow["NAME"] = Constz.Requisition.Status.Approved.Name;

            dt.Rows.Add(dRow);
            dRow = dt.NewRow();
            dRow["CODE"] = Constz.Requisition.Status.Void.Code;
            dRow["NAME"] = Constz.Requisition.Status.Void.Name;


            dt.Rows.Add(dRow);
            dRow = dt.NewRow();
            dRow["CODE"] = Constz.Requisition.Status.Finish.Code;
            dRow["NAME"] = Constz.Requisition.Status.Finish.Name;
            dt.Rows.Add(dRow);

            zCombo.DataSource = dt;
            zCombo.DisplayMember = "NAME";
            zCombo.ValueMember = "CODE";
        }

        public static string GetStatusName(string code)
        {
            string ret = "";
            switch (code)
            {
                case Constz.Requisition.Status.Approved.Code:
                    ret = Constz.Requisition.Status.Approved.Name;
                    break;

                case Constz.Requisition.Status.Finish.Code:
                    ret = Constz.Requisition.Status.Finish.Name;
                    break;

                case Constz.Requisition.Status.QC.Code:
                    ret = Constz.Requisition.Status.QC.Name;
                    break;

                case Constz.Requisition.Status.Reserve.Code:
                    ret = Constz.Requisition.Status.Reserve.Name;
                    break;

                case Constz.Requisition.Status.Void.Code:
                    ret = Constz.Requisition.Status.Void.Name;
                    break;

                case Constz.Requisition.Status.Waiting.Code:
                    ret = Constz.Requisition.Status.Waiting.Name;
                    break;

                case Constz.Requisition.Status.RW.Code:
                    ret = Constz.Requisition.Status.RW.Name;
                    break;

                case Constz.Requisition.Status.XRay.Code:
                    ret = Constz.Requisition.Status.XRay.Name;
                    break;

                case Constz.Requisition.Status.QS.Code:
                    ret = Constz.Requisition.Status.QS.Name;
                    break;
            }
            return ret;
        }
    }
}
