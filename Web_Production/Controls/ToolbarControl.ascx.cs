using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Controls_ToolbarControl : System.Web.UI.UserControl
{
    #region Initialize
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        SetButtonText();
        SetMouseAction();
    }
    #endregion

    public event EventHandler NewClick;
    public event EventHandler DeleteClick;
    public event EventHandler EditClick;
    public event EventHandler SaveClick;
    public event EventHandler CancelClick;
    public event EventHandler BackClick;
    public event EventHandler SubmitClick;
    public event EventHandler ReturnClick;
    public event EventHandler PrintClick;

    #region Visible Property

    public bool BtnNewShow
    {
        get { return lnkNew.Visible; }
        set { lnkNew.Visible = value; }
    }

    public bool BtnDeleteShow
    {
        get { return lnkDelete.Visible; }
        set { lnkDelete.Visible = value; }
    }

    public bool BtnEditShow
    {
        get { return lnkEdit.Visible; }
        set { lnkEdit.Visible = value; }
    }

    public bool BtnSaveShow
    {
        get { return lnkSave.Visible; }
        set { lnkSave.Visible = value; }
    }

    public bool BtnCancelShow
    {
        get { return lnkCancel.Visible; }
        set { lnkCancel.Visible = value; }
    }

    public bool BtnBackShow
    {
        get { return lnkBack.Visible; }
        set { lnkBack.Visible = value; }
    }

    public bool BtnSubmitShow
    {
        get { return lnkSubmit.Visible; }
        set { lnkSubmit.Visible = value; }
    }

    public bool BtnReturnShow
    {
        get { return lnkReturn.Visible; }
        set { lnkReturn.Visible = value; }
    }

    public bool BtnPrintShow
    {
        get { return lnkPrint.Visible; }
        set { lnkPrint.Visible = value; }
    }

    #endregion

    #region Image Path constant

    private string ImageFolder
    {
        get
        {
            return Request.ApplicationPath + "/Images/";
        }
    }

    private string img_new { get { return ImageFolder + "icn_new.gif"; } }
    private string img_delete { get { return ImageFolder + "icn_delete.gif"; } }
    private string img_edit { get { return ImageFolder + "icn_edit.gif"; } }
    private string img_save { get { return ImageFolder + "icn_save.gif"; } }
    private string img_cancel { get { return ImageFolder + "icn_cancel.gif"; } }
    private string img_back { get { return ImageFolder + "icn_back.gif"; } }
    private string img_submit { get { return ImageFolder + "icn_submit.gif"; } }
    private string img_return { get { return ImageFolder + "icn_return.gif"; } }
    private string img_print { get { return ImageFolder + "icn_print.gif"; } }

    #endregion

    #region Button Text
    private string text_New = "เพิ่ม";
    private string text_Delete = "ลบ";
    private string text_Edit = "แก้ไข";
    private string text_Save = "บันทึก";
    private string text_Cancel = "ยกเลิก";
    private string text_Back = "กลับหน้ารายการ";
    private string text_Submit = "ส่งข้อมูล";
    private string text_Return = "ส่งเรื่องคืน";
    private string text_Print = "พิมพ์รายงาน";
    #endregion

    #region Button Events Handle

    protected void lnkNew_Click(object sender, EventArgs e)
    {
        if (NewClick != null) NewClick(this, e);
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        if (DeleteClick != null) DeleteClick(this, e);
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        if (EditClick != null) EditClick(this, e);
    }
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        if (SaveClick != null) SaveClick(this, e);
    }
    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        if (CancelClick != null) CancelClick(this, e);
    }
    protected void lnkBack_Click(object sender, EventArgs e)
    {
        if (BackClick != null) BackClick(this, e);
    }
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        if (SubmitClick != null) SubmitClick(this, e);
    }
    protected void lnkReturn_Click(object sender, EventArgs e)
    {
        if (ReturnClick != null) ReturnClick(this, e);
    }
    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        if (PrintClick != null) PrintClick(this, e);
    }

    #endregion

    #region Client Click Property
    public string ClientClickNew
    {
        get { return lnkNew.OnClientClick; }
        set { lnkNew.OnClientClick = value; }
    }

    public string ClientClickEdit
    {
        get { return lnkEdit.OnClientClick; }
        set { lnkEdit.OnClientClick = value; }
    }

    public string ClientClickDelete
    {
        get { return lnkDelete.OnClientClick; }
        set { lnkDelete.OnClientClick = value; }
    }

    public string ClientClickSave
    {
        get { return lnkSave.OnClientClick; }
        set { lnkSave.OnClientClick = value; }
    }

    public string ClientClickCancel
    {
        get { return lnkCancel.OnClientClick; }
        set { lnkCancel.OnClientClick = value; }
    }

    public string ClientClickBack
    {
        get { return lnkBack.OnClientClick; }
        set { lnkBack.OnClientClick = value; }
    }

    public string ClientClickSubmit
    {
        get { return lnkSubmit.OnClientClick; }
        set { lnkSubmit.OnClientClick = value; }
    }

    public string ClientClickReturn
    {
        get { return lnkReturn.OnClientClick; }
        set { lnkReturn.OnClientClick = value; }
    }

    public string ClientClickPrint
    {
        get { return lnkPrint.OnClientClick; }
        set { lnkPrint.OnClientClick = value; }
    }
    #endregion

    #region Button Name Property
    public string NameBtnNew
    {
        get { return text_New; }
        set { text_New = value; }
    }

    public string NameBtnDelete
    {
        get { return text_Delete; }
        set { text_Delete = value; }
    }

    public string NameBtnEdit
    {
        get { return text_Edit; }
        set { text_Edit = value; }
    }

    public string NameBtnSave
    {
        get { return text_Save; }
        set { text_Save = value; }
    }

    public string NameBtnCancel
    {
        get { return text_Cancel; }
        set { text_Cancel = value; }
    }

    public string NameBtnBack
    {
        get { return text_Back; }
        set { text_Back = value; }
    }

    public string NameBtnSubmit
    {
        get { return text_Submit; }
        set { text_Submit = value; }
    }

    public string NameBtnReturn
    {
        get { return text_Return; }
        set { text_Return = value; }
    }

    public string NameBtnPrint
    {
        get { return text_Print; }
        set { text_Print = value; }
    }

    #endregion

    #region Mouse Action
    private const string zMouseOver = "this.className='toolbarbuttonhover'";
    private const string zMoveOut = "this.className='toolbarbutton'";
    private void SetMouseAction()
    {
        lnkNew.Attributes.Add("OnMouseOver", zMouseOver);
        lnkNew.Attributes.Add("OnMouseOut", zMoveOut);
        lnkDelete.Attributes.Add("OnMouseOver", zMouseOver);
        lnkDelete.Attributes.Add("OnMouseOut", zMoveOut);
        lnkEdit.Attributes.Add("OnMouseOver", zMouseOver);
        lnkEdit.Attributes.Add("OnMouseOut", zMoveOut);
        lnkSave.Attributes.Add("OnMouseOver", zMouseOver);
        lnkSave.Attributes.Add("OnMouseOut", zMoveOut);
        lnkCancel.Attributes.Add("OnMouseOver", zMouseOver);
        lnkCancel.Attributes.Add("OnMouseOut", zMoveOut);
        lnkBack.Attributes.Add("OnMouseOver", zMouseOver);
        lnkBack.Attributes.Add("OnMouseOut", zMoveOut);
        lnkSubmit.Attributes.Add("OnMouseOver", zMouseOver);
        lnkSubmit.Attributes.Add("OnMouseOut", zMoveOut);
        lnkReturn.Attributes.Add("OnMouseOver", zMouseOver);
        lnkReturn.Attributes.Add("OnMouseOut", zMoveOut);
        lnkPrint.Attributes.Add("OnMouseOver", zMouseOver);
        lnkPrint.Attributes.Add("OnMouseOut", zMoveOut);
    }

    #endregion

    public void SetButtonText()
    {
        lnkNew.Text = makeImgStr(img_new) + text_New; ;
        lnkDelete.Text = makeImgStr(img_delete) + text_Delete;
        lnkEdit.Text = makeImgStr(img_edit) + text_Edit;
        lnkSave.Text = makeImgStr(img_save) + text_Save;
        lnkCancel.Text = makeImgStr(img_cancel) + text_Cancel;
        lnkBack.Text = makeImgStr(img_back) + text_Back;
        lnkSubmit.Text = makeImgStr(img_submit) + text_Submit;
        lnkReturn.Text = makeImgStr(img_return) + text_Return;
        lnkPrint.Text = makeImgStr(img_print) + text_Print;
    }

    private string makeImgStr(string imgPath)
    {
        return "<img src='" + imgPath + "' border='0' align='AbsMiddle'> ";
    }

}
