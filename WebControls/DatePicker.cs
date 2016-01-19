using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:DatePicker runat=server></{0}:DatePicker>")]
    public class DatePicker : Control, IPostBackDataHandler
    {
        private string plusYearString = "+543";
        private string minusYearString = "-543";

        #region Property

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string DateValue
        {
            get
            {
                string s = (String)ViewState["DateValue"];
                return ((s == null) ? String.Empty : s);
            }
            set
            {
                ViewState["DateValue"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("true")]
        [Localizable(true)]
        public bool Enabled
        {
            get
            {
                bool s = true;
                if (ViewState["Enabled"] != null)
                    s = (bool)ViewState["Enabled"];
                return s;
            }
            set
            {
                ViewState["Enabled"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string CssClass
        {
            get
            {
                string s = (String)ViewState["CssClass"];
                return ((s == null) ? String.Empty : s);
            }
            set
            {
                ViewState["CssClass"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string ImageButtonUrl
        {
            get
            {
                string s = (String)ViewState["ImageButtonUrl"];
                return ((s == null) ? String.Empty : s);
            }
            set
            {
                ViewState["ImageButtonUrl"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string ImageDividerUrl
        {
            get
            {
                string s = (String)ViewState["ImageDividerUrl"];
                return ((s == null) ? String.Empty : s);
            }
            set
            {
                ViewState["ImageDividerUrl"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string ImageDropUrl
        {
            get
            {
                string s = (String)ViewState["ImageDropUrl"];
                return ((s == null) ? String.Empty : s);
            }
            set
            {
                ViewState["ImageDropUrl"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string DateFormat
        {
            get
            {
                string s = (String)ViewState["DateFormat"];
                return ((s == null) ? String.Empty : s);
            }
            set
            {
                ViewState["DateFormat"] = "dd/mm/yyyy";
            }
        }

        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            string calendarScript = "";

            #region Style

            calendarScript += "<style>" + Environment.NewLine;
            calendarScript += "     .calendartext{width:80px;font-family: Tahoma; font-size:9pt;}" + Environment.NewLine;
            calendarScript += "</style>" + Environment.NewLine;

            #endregion

            calendarScript += "<script language=JavaScript>";
            calendarScript += "//	written	by Tan Ling	Wee	on 2 Dec 2001" + Environment.NewLine;
            calendarScript += "//	last updated 23 June 2002" + Environment.NewLine;
            calendarScript += "//	email :	fuushikaden@yahoo.com" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;

            #region Variables
            calendarScript += "var	fixedX = -1			// x position (-1 if to appear below control)" + Environment.NewLine;
            calendarScript += "var	fixedY = -1			// y position (-1 if to appear below control)" + Environment.NewLine;
            calendarScript += "var startAt = 0			// 0 - sunday ; 1 - monday" + Environment.NewLine;
            calendarScript += "var showWeekNumber = 0	// 0 - don't show; 1 - show" + Environment.NewLine;
            calendarScript += "var showToday = 1		// 0 - don't show; 1 - show"+ Environment.NewLine;
            calendarScript += "var imgDir = '' " + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "var gotoString = \"Go To Current Date\"" + Environment.NewLine;
            calendarScript += "var todayString = \"ปัจจุบัน\"" + Environment.NewLine;
            calendarScript += "var noneString = \"ไม่เลือก\"" + Environment.NewLine;
            calendarScript += "var weekString = \"Wk\"" + Environment.NewLine;
            calendarScript += "var scrollLeftMessage = \"Click to scroll to previous month. Hold mouse button to scroll automatically.\"" + Environment.NewLine;
            calendarScript += "var scrollRightMessage = \"Click to scroll to next month. Hold mouse button to scroll automatically.\"" + Environment.NewLine;
            calendarScript += "var selectMonthMessage = \"Click to select a month.\"" + Environment.NewLine;
            calendarScript += "var selectYearMessage = \"Click to select a year.\"" + Environment.NewLine;
            calendarScript += "var selectDateMessage = \"Select [date] as date.\" // do not replace [date], it will be replaced by date." + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "var	crossobj, crossMonthObj, crossYearObj, monthSelected, yearSelected, dateSelected, omonthSelected, oyearSelected, odateSelected, monthConstructed, yearConstructed, intervalID1, intervalID2, timeoutID1, timeoutID2, ctlToPlaceValue, ctlNow, dateFormat, nStartingYear" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "var	bPageLoaded=false" + Environment.NewLine;
            calendarScript += "var	ie=document.all" + Environment.NewLine;
            calendarScript += "var	dom=document.getElementById" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "var	ns4=document.layers" + Environment.NewLine;
            calendarScript += "var	today =	new	Date()" + Environment.NewLine;
            calendarScript += "var	dateNow	 = today.getDate()" + Environment.NewLine;
            calendarScript += "var	monthNow = today.getMonth()" + Environment.NewLine;
            calendarScript += "var	yearNow	 = today.getYear()" + Environment.NewLine;
            calendarScript += "var	img	= new Array()" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "var bShow = false;" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "var HolidaysCounter = 0" + Environment.NewLine;
            calendarScript += "var Holidays = new Array()" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "var	monthName =	new	Array(\"มกราคม\",\"กุมภาพันธ์\",\"มีนาคม\",\"เมษายน\",\"พฤษภาคม\",\"มิถุนายน\",\"กรกฎาคม\",\"สิงหาคม\",\"กันยายน\",\"ตุลาคม\",\"พฤศจิกายน\",\"ธันวาคม\")" + Environment.NewLine;
            //calendarScript += "var	monthName =	new	Array(\"January\",\"February\",\"March\",\"April\",\"May\",\"June\",\"July\",\"August\",\"September\",\"October\",\"November\",\"December\")" + Environment.NewLine;
            calendarScript += "var	styleAnchor=\"text-decoration:none;color:black; font-family:tahoma;\"" + Environment.NewLine;
            calendarScript += "var	styleAnchorButtom=\"text-decoration:none;color:blue; font-family:tahoma;\"" + Environment.NewLine;
            calendarScript += "var	styleLightBorder=\"border-style:solid; border-width:1px; border-color:#0000CC; background-color:#AAFFFF; \"" + Environment.NewLine;
            #endregion

            #region function "hideElement"
            calendarScript += "/* hides <select> and <applet> objects (for IE only) */" + Environment.NewLine;
            calendarScript += "function hideElement( elmID, overDiv )" + Environment.NewLine;
            calendarScript += "{" + Environment.NewLine;
            calendarScript += "    if( ie )" + Environment.NewLine;
            calendarScript += "    {" + Environment.NewLine;
            calendarScript += "        for( i = 0; i < document.all.tags( elmID ).length; i++ )" + Environment.NewLine;
            calendarScript += "        {" + Environment.NewLine;
            calendarScript += "            obj = document.all.tags( elmID )[i];" + Environment.NewLine;
            calendarScript += "            if( !obj || !obj.offsetParent )" + Environment.NewLine;
            calendarScript += "            {" + Environment.NewLine;
            calendarScript += "                continue;" + Environment.NewLine;
            calendarScript += "            }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "            // Find the element's offsetTop and offsetLeft relative to the BODY tag." + Environment.NewLine;
            calendarScript += "            objLeft   = obj.offsetLeft;" + Environment.NewLine;
            calendarScript += "            objTop    = obj.offsetTop;" + Environment.NewLine;
            calendarScript += "            objParent = obj.offsetParent;" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "            while( objParent.tagName.toUpperCase() != \"BODY\" )" + Environment.NewLine;
            calendarScript += "            {" + Environment.NewLine;
            calendarScript += "                objLeft  += objParent.offsetLeft;" + Environment.NewLine;
            calendarScript += "                objTop   += objParent.offsetTop;" + Environment.NewLine;
            calendarScript += "                objParent = objParent.offsetParent;" + Environment.NewLine;
            calendarScript += "             }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "            objHeight = obj.offsetHeight;" + Environment.NewLine;
            calendarScript += "            objWidth = obj.offsetWidth;" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "            if(( overDiv.offsetLeft + overDiv.offsetWidth ) <= objLeft );" + Environment.NewLine;
            calendarScript += "            else if(( overDiv.offsetTop + overDiv.offsetHeight ) <= objTop );" + Environment.NewLine;
            calendarScript += "            else if( overDiv.offsetTop >= ( objTop + objHeight ));" + Environment.NewLine;
            calendarScript += "            else if( overDiv.offsetLeft >= ( objLeft + objWidth ));" + Environment.NewLine;
            calendarScript += "            else" + Environment.NewLine;
            calendarScript += "            {" + Environment.NewLine;
            calendarScript += "                obj.style.visibility = \"hidden\";" + Environment.NewLine;
            calendarScript += "            }" + Environment.NewLine;
            calendarScript += "        }" + Environment.NewLine;
            calendarScript += "    }" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "showElement"
            calendarScript += "/*" + Environment.NewLine;
            calendarScript += "* unhides <select> and <applet> objects (for IE only)" + Environment.NewLine;
            calendarScript += "*/" + Environment.NewLine;
            calendarScript += "function showElement( elmID )" + Environment.NewLine;
            calendarScript += "{" + Environment.NewLine;
            calendarScript += "    if( ie )" + Environment.NewLine;
            calendarScript += "    {" + Environment.NewLine;
            calendarScript += "        for( i = 0; i < document.all.tags( elmID ).length; i++ )" + Environment.NewLine;
            calendarScript += "        {" + Environment.NewLine;
            calendarScript += "            obj = document.all.tags( elmID )[i];" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "            if( !obj || !obj.offsetParent )" + Environment.NewLine;
            calendarScript += "            {" + Environment.NewLine;
            calendarScript += "                continue;" + Environment.NewLine;
            calendarScript += "            }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "            obj.style.visibility = \"\";" + Environment.NewLine;
            calendarScript += "        }" + Environment.NewLine;
            calendarScript += "    }" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "HolidayRec"
            calendarScript += "function HolidayRec (d, m, y, desc)" + Environment.NewLine;
            calendarScript += "{" + Environment.NewLine;
            calendarScript += "    this.d = d" + Environment.NewLine;
            calendarScript += "    this.m = m" + Environment.NewLine;
            calendarScript += "    this.y = y" + Environment.NewLine;
            calendarScript += "    this.desc = desc" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "addHoliday"
            calendarScript += "function addHoliday (d, m, y, desc)" + Environment.NewLine;
            calendarScript += "{" + Environment.NewLine;
            calendarScript += "    Holidays[HolidaysCounter++] = new HolidayRec ( d, m, y, desc )" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            calendarScript += "if (dom)" + Environment.NewLine;
            calendarScript += "{" + Environment.NewLine;
            calendarScript += "document.write (\"<div onclick='bShow=true' id='calendar'	style='z-index:+999;position:absolute;visibility:hidden;'>";
            calendarScript += "<table cellpadding='0' cellspacing='0' width=\"+((showWeekNumber==1)?250:190)+\" style='border-width:1px;border-style:solid;border-color:#a0a0a0;font-family:arial; font-size:11px}' bgcolor='#ffffff'>";
            calendarScript += "<tr bgcolor='#0000aa'>";
            calendarScript += "<td><span id='caption'></span>";
            calendarScript += "</td></tr><tr><td style='padding:2px' bgcolor=#ffffff><span id='content'></span></td></tr>\")" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "if (showToday==1)" + Environment.NewLine;
            calendarScript += "{" + Environment.NewLine;
            calendarScript += "  document.write (\"<tr bgcolor=#f0f0f0 height='18px'><td style='padding:2px' align=center><span id='lblToday'></span></td></tr>\")" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "document.write (\"</table></div><div id='selectMonth' style='z-index:+999;position:absolute;visibility:hidden;'></div><div id='selectYear' style='z-index:+999;position:absolute;visibility:hidden;'></div>\");" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "if (startAt==0)" + Environment.NewLine;
            calendarScript += "{" + Environment.NewLine;
            calendarScript += "dayName = new Array	(\"อา\",\"จ\",\"อ\",\"พ\",\"พฤ\",\"ศ\",\"ส\")" + Environment.NewLine;
            //calendarScript += "dayName = new Array	(\"Sun\",\"Mon\",\"Tue\",\"Wed\",\"Thu\",\"Fri\",\"Sat\")" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "else" + Environment.NewLine;
            calendarScript += "{" + Environment.NewLine;
            calendarScript += "dayName = new Array	(\"จ\",\"อ\",\"พ\",\"พฤ\",\"ศ\",\"ส\",\"อา\")" + Environment.NewLine;
            //calendarScript += "dayName = new Array	(\"Mon\",\"Tue\",\"Wed\",\"Thu\",\"Fri\",\"Sat\",\"Sun\")" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;

            #region function "init"
            calendarScript += "function init()	{" + Environment.NewLine;
            calendarScript += "  if (!ns4)" + Environment.NewLine;
            calendarScript += "  {" + Environment.NewLine;
            calendarScript += "    if (!ie) { yearNow += 1900	}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    crossobj=(dom)?document.getElementById(\"calendar\").style : ie? document.all.calendar : document.calendar" + Environment.NewLine;
            calendarScript += "    hideCalendar()" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    crossMonthObj=(dom)?document.getElementById(\"selectMonth\").style : ie? document.all.selectMonth	: document.selectMonth" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    crossYearObj=(dom)?document.getElementById(\"selectYear\").style : ie? document.all.selectYear : document.selectYear" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    monthConstructed=false;" + Environment.NewLine;
            calendarScript += "    yearConstructed=false;" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    if (showToday==1)" + Environment.NewLine;
            calendarScript += "    {" + Environment.NewLine;
            calendarScript += "      document.getElementById(\"lblToday\").innerHTML =	\"";
            //calendarScript += "[\" + todayString + \" <a onmousemove='window.status=\\\"\"+gotoString+\"\\\"' onmouseout='window.status=\\\"\\\"' title='\"+gotoString+\"' style='\"+styleAnchor+\"' href='javascript:monthSelected=monthNow;yearSelected=yearNow;constructCalendar();'>\"+dayName[(today.getDay()-startAt==-1)?6:(today.getDay()-startAt)]+\", \" + dateNow + \" \" + monthName[monthNow].substring(0,3)	+ \"	\" +	yearNow	+ \"</a>]&nbsp";
            calendarScript += "<a onmousemove='window.status=\\\"\"+gotoString+\"\\\"' onmouseout='window.status=\\\"\\\"' title='\"+gotoString+\"' style='\"+styleAnchorButtom+\" font-size:11px;' href='#' onclick='monthSelected=monthNow;yearSelected=yearNow;constructCalendar(); return false;'>[\" + todayString + \"]</a>&nbsp&nbsp";
            calendarScript += "<a style='\"+styleAnchorButtom+\" font-size:11px;' href='#' onclick='dateSelected=0; closeCalendar(); return false;'>[\" + noneString + \"]</a>\"" + Environment.NewLine;
            calendarScript += "    }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    sHTML1=\"<table border='0' cellspacing='2' cellpadding='2' width='200px'><tr height='10px' valign='middle'>";
            calendarScript += "<td width='18px' align='center' style='font-family:arial; font-size:11px; color:#ffffff; font-weight:bold; border-style:solid;border-width:1;border-color:#3366FF;cursor:pointer' onmouseover='this.style.borderColor=\\\"#88AAFF\\\";window.status=\\\"\"+scrollLeftMessage+\"\\\"' onclick='javascript:decMonth()' onmouseout='clearInterval(intervalID1);this.style.borderColor=\\\"#3366FF\\\";window.status=\\\"\\\"' onmousedown='clearTimeout(timeoutID1);timeoutID1=setTimeout(\\\"StartDecMonth()\\\",500)'	onmouseup='clearTimeout(timeoutID1);clearInterval(intervalID1)'>\"" + Environment.NewLine;
            calendarScript += "    sHTML1+=\"<span id='spanLeft'>&nbsp<<&nbsp</span>\"" + Environment.NewLine;
            calendarScript += "    sHTML1+=\"</td><td align='center'  width='18px' style='font-family:arial; font-size:11px; color:#ffffff; font-weight:bold; border-style:solid;border-width:1;border-color:#3366FF;cursor:pointer'	onmouseover='this.style.borderColor=\\\"#88AAFF\\\";window.status=\\\"\"+scrollRightMessage+\"\\\"' onmouseout='clearInterval(intervalID1);this.style.borderColor=\\\"#3366FF\\\";window.status=\\\"\\\"' onclick='incMonth()' onmousedown='clearTimeout(timeoutID1);timeoutID1=setTimeout(\\\"StartIncMonth()\\\",500)'	onmouseup='clearTimeout(timeoutID1);clearInterval(intervalID1)'>\"" + Environment.NewLine;
            calendarScript += "    sHTML1+=\"<span id='spanRight'>&nbsp>>&nbsp</span>\"" + Environment.NewLine;
            calendarScript += "    sHTML1+=\"</td><td width='85px' style='font-family:arial; font-size:11px; color:#ffffff; font-weight:bold; " + (ImageDropUrl == "" ? "" : "background-image: url(" + ImageDropUrl + "); background-repeat: no-repeat; background-color: transparent; background-position: right center;") + "border-style:solid;border-width:1;border-color:#3366FF;cursor:pointer;'	onmouseover='this.style.borderColor=\\\"#88AAFF\\\";window.status=\\\"\"+selectMonthMessage+\"\\\"' onmouseout='this.style.borderColor=\\\"#3366FF\\\";window.status=\\\"\\\"' onclick='popUpMonth()'>\"" + Environment.NewLine;
            calendarScript += "    sHTML1+=\"<span id='spanMonth'></span>\"" + Environment.NewLine;
            calendarScript += "    sHTML1+=\"</td><td style='font-family:arial; font-size:11px; color:#ffffff; font-weight:bold; " + (ImageDropUrl == "" ? "" : "background-image: url(" + ImageDropUrl + "); background-repeat: no-repeat; background-color: transparent; background-position: right center;") + "border-style:solid;border-width:1;border-color:#3366FF;cursor:pointer' onmouseover='this.style.borderColor=\\\"#88AAFF\\\";window.status=\\\"\"+selectYearMessage+\"\\\"'	onmouseout='this.style.borderColor=\\\"#3366FF\\\";window.status=\\\"\\\"'	onclick='popUpYear()'>\"" + Environment.NewLine;
            calendarScript += "    sHTML1+=\"<span id='spanYear'></span>\"" + Environment.NewLine;
            calendarScript += "    sHTML1+=\"</td></tr></table>\"" + Environment.NewLine;

            calendarScript += "" + Environment.NewLine;
            calendarScript += "    document.getElementById(\"caption\").innerHTML  =	sHTML1" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    bPageLoaded=true" + Environment.NewLine;
            calendarScript += "  }" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "hideCalendar"
            calendarScript += "function hideCalendar()	{" + Environment.NewLine;
            calendarScript += "  crossobj.visibility=\"hidden\"" + Environment.NewLine;
            calendarScript += "  if (crossMonthObj != null){crossMonthObj.visibility=\"hidden\"}" + Environment.NewLine;
            calendarScript += "  if (crossYearObj !=	null){crossYearObj.visibility=\"hidden\"}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "  showElement( 'SELECT' );" + Environment.NewLine;
            calendarScript += "  showElement( 'APPLET' );" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "padZero"
            calendarScript += "function padZero(num) {" + Environment.NewLine;
            calendarScript += "  return (num	< 10)? '0' + num : num ;" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "condtructDate"
            calendarScript += "function constructDate(d,m,y)" + Environment.NewLine;
            calendarScript += "{" + Environment.NewLine;
            calendarScript += "  sTmp = dateFormat" + Environment.NewLine;
            calendarScript += "  sTmp = sTmp.replace(\"dd\",\"<e>\")" + Environment.NewLine;
            calendarScript += "  sTmp = sTmp.replace(\"d\",\"<d>\")" + Environment.NewLine;
            calendarScript += "  sTmp = sTmp.replace(\"<e>\",padZero(d))" + Environment.NewLine;
            calendarScript += "  sTmp = sTmp.replace(\"<d>\",d)" + Environment.NewLine;
            calendarScript += "  sTmp = sTmp.replace(\"mmm\",\"<o>\")" + Environment.NewLine;
            calendarScript += "  sTmp = sTmp.replace(\"mm\",\"<n>\")" + Environment.NewLine;
            calendarScript += "  sTmp = sTmp.replace(\"m\",\"<m>\")" + Environment.NewLine;
            calendarScript += "  sTmp = sTmp.replace(\"<m>\",m+1)" + Environment.NewLine;
            calendarScript += "  sTmp = sTmp.replace(\"<n>\",padZero(m+1))" + Environment.NewLine;
            calendarScript += "  sTmp = sTmp.replace(\"<o>\",monthName[m])" + Environment.NewLine;
            calendarScript += "  return sTmp.replace(\"yyyy\",(y" + plusYearString + "))" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "closeCalendar"
            calendarScript += "function closeCalendar() {" + Environment.NewLine;
            calendarScript += "  var	sTmp" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "  hideCalendar();" + Environment.NewLine;
            calendarScript += "  if (dateSelected == 0) " + Environment.NewLine;
            calendarScript += "    { ctlToPlaceValue.value = ''; }" + Environment.NewLine;
            calendarScript += "  else" + Environment.NewLine;
            //calendarScript += "    { ctlToPlaceValue.value = '1'; }" + Environment.NewLine;
            calendarScript += "    { ctlToPlaceValue.value =	constructDate(dateSelected,monthSelected,yearSelected) }" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "StartDecMonth"
            calendarScript += "/*** Month Pulldown	***/" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "function StartDecMonth()" + Environment.NewLine;
            calendarScript += "{" + Environment.NewLine;
            calendarScript += "  intervalID1=setInterval(\"decMonth()\",80)" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "StartIncMonth"
            calendarScript += "function StartIncMonth()" + Environment.NewLine;
            calendarScript += "{" + Environment.NewLine;
            calendarScript += "  intervalID1=setInterval(\"incMonth()\",80)" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "incMonth"
            calendarScript += "function incMonth () {" + Environment.NewLine;
            calendarScript += "  monthSelected++" + Environment.NewLine;
            calendarScript += "  if (monthSelected>11) {" + Environment.NewLine;
            calendarScript += "    monthSelected=0" + Environment.NewLine;
            calendarScript += "    yearSelected++" + Environment.NewLine;
            calendarScript += "  }" + Environment.NewLine;
            calendarScript += "  constructCalendar()" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "decMonth"
            calendarScript += "function decMonth () {" + Environment.NewLine;
            calendarScript += "  monthSelected--" + Environment.NewLine;
            calendarScript += "  if (monthSelected<0) {" + Environment.NewLine;
            calendarScript += "    monthSelected=11" + Environment.NewLine;
            calendarScript += "    yearSelected--" + Environment.NewLine;
            calendarScript += "  }" + Environment.NewLine;
            calendarScript += "  constructCalendar()" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "constructMonth"
            calendarScript += "function constructMonth() {" + Environment.NewLine;
            calendarScript += "  popDownYear()" + Environment.NewLine;
            calendarScript += "  if (!monthConstructed) {" + Environment.NewLine;
            calendarScript += "    sHTML =	\"\"" + Environment.NewLine;
            calendarScript += "    for	(i=0; i<12;	i++) {" + Environment.NewLine;
            calendarScript += "      sName =	monthName[i];" + Environment.NewLine;
            calendarScript += "      if (i==monthSelected){" + Environment.NewLine;
            calendarScript += "        sName =	\"<B>\" +	sName +	\"</B>\"" + Environment.NewLine;
            calendarScript += "      }" + Environment.NewLine;
            calendarScript += "      sHTML += \"<tr><td style='font-family:tahoma; font-size:11px' id='m\" + i + \"' onmouseover='this.style.backgroundColor=\\\"#FFCC99\\\"' onmouseout='this.style.backgroundColor=\\\"\\\"' style='cursor:pointer' onclick='monthConstructed=false;monthSelected=\" + i + \";constructCalendar();popDownMonth();event.cancelBubble=true'>&nbsp;\" + sName + \"&nbsp;</td></tr>\"" + Environment.NewLine;
            calendarScript += "    }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    document.getElementById(\"selectMonth\").innerHTML = \"<table width=92	style='border-width:1; border-style:solid; border-color:#a0a0a0;' bgcolor='#FFFFDD' cellspacing=0 onmouseover='clearTimeout(timeoutID1)'	onmouseout='clearTimeout(timeoutID1);timeoutID1=setTimeout(\\\"popDownMonth()\\\",100);event.cancelBubble=true'>\" +	sHTML +	\"</table>\"" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    monthConstructed=true" + Environment.NewLine;
            calendarScript += "  }" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "popUpMonth"
            calendarScript += "function popUpMonth() {" + Environment.NewLine;
            calendarScript += "  constructMonth()" + Environment.NewLine;
            calendarScript += "  crossMonthObj.visibility = (dom||ie)? \"visible\"	: \"show\"" + Environment.NewLine;
            calendarScript += "  crossMonthObj.left = parseInt(crossobj.left) + 55" + Environment.NewLine;
            calendarScript += "  crossMonthObj.top =	parseInt(crossobj.top) + 23" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "  hideElement( 'SELECT', document.getElementById(\"selectMonth\") );" + Environment.NewLine;
            calendarScript += "  hideElement( 'APPLET', document.getElementById(\"selectMonth\") );" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "popDownMonth"
            calendarScript += "function popDownMonth()	{" + Environment.NewLine;
            calendarScript += "  crossMonthObj.visibility= \"hidden\"" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "incYear"
            calendarScript += "/*** Year Pulldown ***/" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "function incYear() {" + Environment.NewLine;
            calendarScript += "  for	(i=0; i<7; i++){" + Environment.NewLine;
            calendarScript += "    newYear	= (i+nStartingYear)+1" + Environment.NewLine;
            calendarScript += "    if (newYear==yearSelected)" + Environment.NewLine;
            calendarScript += "      { txtYear =	\"&nbsp;<B>\"	+ (newYear" + plusYearString + ") + \"</B>&nbsp;\" }" + Environment.NewLine;
            calendarScript += "    else" + Environment.NewLine;
            calendarScript += "      { txtYear =	\"&nbsp;\" + (newYear" + plusYearString + ") + \"&nbsp;\" }" + Environment.NewLine;
            calendarScript += "    document.getElementById(\"y\"+i).innerHTML = txtYear" + Environment.NewLine;
            calendarScript += "  }" + Environment.NewLine;
            calendarScript += "  nStartingYear ++;" + Environment.NewLine;
            calendarScript += "  bShow=true" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "decYear"
            calendarScript += "function decYear() {" + Environment.NewLine;
            calendarScript += "  for	(i=0; i<7; i++){" + Environment.NewLine;
            calendarScript += "    newYear	= (i+nStartingYear)-1" + Environment.NewLine;
            calendarScript += "    if (newYear==yearSelected)" + Environment.NewLine;
            calendarScript += "      { txtYear =	\"&nbsp;<B>\"	+ (newYear" + plusYearString + ") + \"</B>&nbsp;\" }" + Environment.NewLine;
            calendarScript += "    else" + Environment.NewLine;
            calendarScript += "      { txtYear =	\"&nbsp;\" + (newYear" + plusYearString + ") + \"&nbsp;\" }" + Environment.NewLine;
            calendarScript += "    document.getElementById(\"y\"+i).innerHTML = txtYear" + Environment.NewLine;
            calendarScript += "  }" + Environment.NewLine;
            calendarScript += "  nStartingYear --;" + Environment.NewLine;
            calendarScript += "  bShow=true" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "selectYear"
            calendarScript += "function selectYear(nYear) {" + Environment.NewLine;
            calendarScript += "  yearSelected=parseInt(nYear+nStartingYear);" + Environment.NewLine;
            calendarScript += "  yearConstructed=false;" + Environment.NewLine;
            calendarScript += "  constructCalendar();" + Environment.NewLine;
            calendarScript += "  popDownYear();" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "constructYear"
            calendarScript += "function constructYear() {" + Environment.NewLine;
            calendarScript += "  popDownMonth()" + Environment.NewLine;
            calendarScript += "  sHTML =	\"\"" + Environment.NewLine;
            calendarScript += "  if (!yearConstructed) {" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    sHTML =	\"<tr><td align='center'	onmouseover='this.style.backgroundColor=\\\"#FFCC99\\\"' onmouseout='clearInterval(intervalID1);this.style.backgroundColor=\\\"\\\"' style='cursor:pointer'	onmousedown='clearInterval(intervalID1);intervalID1=setInterval(\\\"decYear()\\\",30)' onmouseup='clearInterval(intervalID1)'>-</td></tr>\"" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    j =	0" + Environment.NewLine;
            calendarScript += "    nStartingYear =	yearSelected-3" + Environment.NewLine;
            calendarScript += "    for	(i=(yearSelected-3); i<=(yearSelected+3); i++) {" + Environment.NewLine;
            calendarScript += "      sName =	i" + plusYearString + ";" + Environment.NewLine;
            calendarScript += "      if (i==yearSelected){" + Environment.NewLine;
            calendarScript += "        sName =	\"<B>\" +	sName +	\"</B>\"" + Environment.NewLine;
            calendarScript += "      }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "      sHTML += \"<tr><td style='font-family:tahoma; font-size:11px' id='y\" + j + \"' onmouseover='this.style.backgroundColor=\\\"#FFCC99\\\"' onmouseout='this.style.backgroundColor=\\\"\\\"' style='cursor:pointer' onclick='selectYear(\"+j+\");event.cancelBubble=true'>&nbsp;\" + sName + \"&nbsp;</td></tr>\"" + Environment.NewLine;
            calendarScript += "      j ++;" + Environment.NewLine;
            calendarScript += "    }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    sHTML += \"<tr><td style='font-family:tahoma; font-size:11px' align='center' onmouseover='this.style.backgroundColor=\\\"#FFCC99\\\"' onmouseout='clearInterval(intervalID2);this.style.backgroundColor=\\\"\\\"' style='cursor:pointer' onmousedown='clearInterval(intervalID2);intervalID2=setInterval(\\\"incYear()\\\",30)'	onmouseup='clearInterval(intervalID2)'>+</td></tr>\"" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    document.getElementById(\"selectYear\").innerHTML	= \"<table width=52px style='border-width:1; border-style:solid; border-color:#a0a0a0;'	bgcolor='#FFFFDD' onmouseover='clearTimeout(timeoutID2)' onmouseout='clearTimeout(timeoutID2);timeoutID2=setTimeout(\\\"popDownYear()\\\",100)' cellspacing=0>\"	+ sHTML	+ \"</table>\"" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    yearConstructed	= true" + Environment.NewLine;
            calendarScript += "  }" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "popDownYear"
            calendarScript += "function popDownYear() {" + Environment.NewLine;
            calendarScript += "  clearInterval(intervalID1)" + Environment.NewLine;
            calendarScript += "  clearTimeout(timeoutID1)" + Environment.NewLine;
            calendarScript += "  clearInterval(intervalID2)" + Environment.NewLine;
            calendarScript += "  clearTimeout(timeoutID2)" + Environment.NewLine;
            calendarScript += "  crossYearObj.visibility= \"hidden\"" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "popUpYear"
            calendarScript += "function popUpYear() {" + Environment.NewLine;
            calendarScript += "  var	leftOffset" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "  constructYear()" + Environment.NewLine;
            calendarScript += "  crossYearObj.visibility	= (dom||ie)? \"visible\" : \"show\"" + Environment.NewLine;
            calendarScript += "  crossYearObj.left = parseInt(crossobj.left) + 148" + Environment.NewLine;
            calendarScript += "  crossYearObj.top = parseInt(crossobj.top) + 23" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "WeekNbr"
            calendarScript += "/*** calendar ***/" + Environment.NewLine;
            calendarScript += "function WeekNbr(n) {" + Environment.NewLine;
            calendarScript += "  year = n.getFullYear();" + Environment.NewLine;
            calendarScript += "  month = n.getMonth() + 1;" + Environment.NewLine;
            calendarScript += "  if (startAt == 0) {" + Environment.NewLine;
            calendarScript += "    day = n.getDate() + 1;" + Environment.NewLine;
            calendarScript += "  }" + Environment.NewLine;
            calendarScript += "  else {" + Environment.NewLine;
            calendarScript += "    day = n.getDate();" + Environment.NewLine;
            calendarScript += "  }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "  a = Math.floor((14-month) / 12);" + Environment.NewLine;
            calendarScript += "  y = year + 4800 - a;" + Environment.NewLine;
            calendarScript += "  m = month + 12 * a - 3;" + Environment.NewLine;
            calendarScript += "  b = Math.floor(y/4) - Math.floor(y/100) + Math.floor(y/400);" + Environment.NewLine;
            calendarScript += "  J = day + Math.floor((153 * m + 2) / 5) + 365 * y + b - 32045;" + Environment.NewLine;
            calendarScript += "  d4 = (((J + 31741 - (J % 7)) % 146097) % 36524) % 1461;" + Environment.NewLine;
            calendarScript += "  L = Math.floor(d4 / 1460);" + Environment.NewLine;
            calendarScript += "  d1 = ((d4 - L) % 365) + L;" + Environment.NewLine;
            calendarScript += "  week = Math.floor(d1/7) + 1;" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "  return week;" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "constructCalendar"
            calendarScript += "function constructCalendar () {" + Environment.NewLine;
            calendarScript += "  var aNumDays = Array (31,0,31,30,31,30,31,31,30,31,30,31)" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "  var dateMessage" + Environment.NewLine;
            calendarScript += "  var cwidth = \"23px\"" + Environment.NewLine;
            calendarScript += "  var cheight = \"18px\"" + Environment.NewLine;
            calendarScript += "  var	startDate =	new	Date (yearSelected,monthSelected,1)" + Environment.NewLine;
            calendarScript += "  var endDate" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "  if (monthSelected==1)" + Environment.NewLine;
            calendarScript += "  {" + Environment.NewLine;
            calendarScript += "    endDate	= new Date (yearSelected,monthSelected+1,1);" + Environment.NewLine;
            calendarScript += "    endDate	= new Date (endDate	- (24*60*60*1000));" + Environment.NewLine;
            calendarScript += "    numDaysInMonth = endDate.getDate()" + Environment.NewLine;
            calendarScript += "  }" + Environment.NewLine;
            calendarScript += "  else" + Environment.NewLine;
            calendarScript += "  {" + Environment.NewLine;
            calendarScript += "    numDaysInMonth = aNumDays[monthSelected];" + Environment.NewLine;
            calendarScript += "  }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "  datePointer	= 0" + Environment.NewLine;
            calendarScript += "  dayPointer = startDate.getDay() - startAt" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "  if (dayPointer<0)" + Environment.NewLine;
            calendarScript += "  { dayPointer = 6 }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "  sHTML =	\"<table border=0 cellspacing='1px' cellpadding='1px' style='font-family:tahoma;font-size:9px;'><tr>\"" + Environment.NewLine;

            calendarScript += "" + Environment.NewLine;
            calendarScript += "  for	(i=0; i<7; i++)	{" + Environment.NewLine;
            calendarScript += "    sHTML += \"<td style='font-family:tahoma;font-size:11px; color:#000000;' width='\" + cwidth + \"' align='center'><b>\"+ dayName[i]+\"</b></td>\"" + Environment.NewLine;
            calendarScript += "  }" + Environment.NewLine;
            calendarScript += "  sHTML +=\"</tr><tr>\"" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;

            calendarScript += "  for	( var i=1; i<=dayPointer;i++ )" + Environment.NewLine;
            calendarScript += "  {" + Environment.NewLine;
            calendarScript += "    sHTML += \"<td width='\" + cwidth + \"' ></td>\"" + Environment.NewLine;
            calendarScript += "  }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "  for	( datePointer=1; datePointer<=numDaysInMonth; datePointer++ )" + Environment.NewLine;
            calendarScript += "  {" + Environment.NewLine;
            calendarScript += "    dayPointer++;" + Environment.NewLine;
            calendarScript += "    sStyle=styleAnchor" + Environment.NewLine;
            calendarScript += "    if ((datePointer==odateSelected) &&	(monthSelected==omonthSelected)	&& (yearSelected==oyearSelected))" + Environment.NewLine;
            calendarScript += "    { sStyle+=styleLightBorder }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    sHint = \"\"" + Environment.NewLine;
            calendarScript += "    for (k=0;k<HolidaysCounter;k++)" + Environment.NewLine;
            calendarScript += "    {" + Environment.NewLine;
            calendarScript += "      if ((parseInt(Holidays[k].d)==datePointer)&&(parseInt(Holidays[k].m)==(monthSelected+1)))" + Environment.NewLine;
            calendarScript += "      {" + Environment.NewLine;
            calendarScript += "        if ((parseInt(Holidays[k].y)==0)||((parseInt(Holidays[k].y)==yearSelected)&&(parseInt(Holidays[k].y)!=0)))" + Environment.NewLine;
            calendarScript += "        {" + Environment.NewLine;
            calendarScript += "          sStyle+=\"background-color:#FFDDDD;\"" + Environment.NewLine;
            calendarScript += "          sHint+= (sHint==\"\" ? Holidays[k].desc : \"\\n \" + Holidays[k].desc)" + Environment.NewLine;
            calendarScript += "        }" + Environment.NewLine;
            calendarScript += "      }" + Environment.NewLine;
            calendarScript += "    }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    var regexp= /\\\"/g" + Environment.NewLine;
            calendarScript += "    sHint=sHint.replace(regexp,\"&quot;\")" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    dateMessage = \"onmousemove='window.status=\\\"\"+selectDateMessage.replace(\"[date]\",constructDate(datePointer,monthSelected,yearSelected))+\"\\\"' onmouseout='window.status=\\\"\\\"' \"" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    sHTML += \"<td align='center' width='\" + cwidth + \"' style='\"+sStyle+\" font-size:11px;'>\"" + Environment.NewLine;
            calendarScript += "    if ((datePointer==dateNow)&&(monthSelected==monthNow)&&(yearSelected==yearNow))" + Environment.NewLine;
            calendarScript += "      { sHTML += \"<a \"+dateMessage+\" title=\\\"\" + sHint + \"\\\" style='\"+styleAnchor+\"' href='#' onclick='dateSelected=\" + datePointer + \"; closeCalendar(); return false;'><font color='#ff0000'>&nbsp;\" + datePointer + \"</font></a></td>\"}" + Environment.NewLine;
            calendarScript += "    else if	(dayPointer % 7 == (startAt * -1)+1)" + Environment.NewLine;
            calendarScript += "      { sHTML += \"<a \"+dateMessage+\" title=\\\"\" + sHint + \"\\\" style='\"+styleAnchor+\"' href='#' onclick='dateSelected=\" + datePointer + \"; closeCalendar(); return false;'><font color='#909090'>\" + datePointer + \"</font></a></td>\" }" + Environment.NewLine;
            calendarScript += "    else" + Environment.NewLine;
            calendarScript += "      { sHTML += \"<a \"+dateMessage+\" title=\\\"\" + sHint + \"\\\" style='\"+styleAnchor+\"' href='#' onclick='dateSelected=\" + datePointer + \"; closeCalendar(); return false;'>\" + datePointer + \"</a></td>\" }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "    sHTML += \"\"" + Environment.NewLine;
            calendarScript += "    if ((dayPointer+startAt) % 7 == startAt) {" + Environment.NewLine;
            calendarScript += "      sHTML += \"</tr><tr>\"" + Environment.NewLine;
            calendarScript += "      if ((showWeekNumber==1)&&(datePointer<numDaysInMonth))" + Environment.NewLine;
            calendarScript += "      {" + Environment.NewLine;
            calendarScript += "        sHTML += \"<td align=right>\" + (WeekNbr(new Date(yearSelected,monthSelected,datePointer+1))) + \"&nbsp;</td>\"" + Environment.NewLine;
            calendarScript += "      }" + Environment.NewLine;
            calendarScript += "    }" + Environment.NewLine;
            calendarScript += "  }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "  document.getElementById(\"content\").innerHTML   = sHTML" + Environment.NewLine;
            calendarScript += "  document.getElementById(\"spanMonth\").innerHTML = \"\" +	monthName[monthSelected] + \"\"" + Environment.NewLine;
            calendarScript += "  document.getElementById(\"spanYear\").innerHTML =	\"\" + (yearSelected " + plusYearString + ") + \"\"" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            #region function "popUpCalendar"
            calendarScript += "function popUpCalendar(ctl,	ctl2) {" + Environment.NewLine;
            calendarScript += "  var	format='" + (DateFormat == "" ? "dd/mm/yyyy" : DateFormat) + "' " + Environment.NewLine;
            calendarScript += "  var	leftpos=0" + Environment.NewLine;
            calendarScript += "  var	toppos=0" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "  if (bPageLoaded)" + Environment.NewLine;
            calendarScript += "  {" + Environment.NewLine;
            calendarScript += "    if ( crossobj.visibility ==	\"hidden\" ) {" + Environment.NewLine;
            calendarScript += "      ctlToPlaceValue	= ctl2" + Environment.NewLine;
            calendarScript += "      dateFormat=format;" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "      formatChar = \" \"" + Environment.NewLine;
            calendarScript += "      aFormat	= dateFormat.split(formatChar)" + Environment.NewLine;
            calendarScript += "      if (aFormat.length<3)" + Environment.NewLine;
            calendarScript += "      {" + Environment.NewLine;
            calendarScript += "        formatChar = \"/\"" + Environment.NewLine;
            calendarScript += "        aFormat	= dateFormat.split(formatChar)" + Environment.NewLine;
            calendarScript += "        if (aFormat.length<3)" + Environment.NewLine;
            calendarScript += "        {" + Environment.NewLine;
            calendarScript += "          formatChar = \".\"" + Environment.NewLine;
            calendarScript += "          aFormat	= dateFormat.split(formatChar)" + Environment.NewLine;
            calendarScript += "          if (aFormat.length<3)" + Environment.NewLine;
            calendarScript += "          {" + Environment.NewLine;
            calendarScript += "            formatChar = \"-\"" + Environment.NewLine;
            calendarScript += "            aFormat	= dateFormat.split(formatChar)" + Environment.NewLine;
            calendarScript += "            if (aFormat.length<3)" + Environment.NewLine;
            calendarScript += "            {" + Environment.NewLine;
            calendarScript += "// invalid date	format" + Environment.NewLine;
            calendarScript += "              formatChar=\"\"" + Environment.NewLine;
            calendarScript += "            }" + Environment.NewLine;
            calendarScript += "          }" + Environment.NewLine;
            calendarScript += "        }" + Environment.NewLine;
            calendarScript += "      }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "      tokensChanged =	0" + Environment.NewLine;
            calendarScript += "      if ( formatChar	!= \"\" )" + Environment.NewLine;
            calendarScript += "      {" + Environment.NewLine;
            calendarScript += "        // use user's date" + Environment.NewLine;
            calendarScript += "        aData =	ctl2.value.split(formatChar)" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "        for	(i=0;i<3;i++)" + Environment.NewLine;
            calendarScript += "        {" + Environment.NewLine;
            calendarScript += "          if ((aFormat[i]==\"d\") || (aFormat[i]==\"dd\"))" + Environment.NewLine;
            calendarScript += "          {" + Environment.NewLine;
            calendarScript += "            dateSelected = parseInt(aData[i], 10)" + Environment.NewLine;
            calendarScript += "            tokensChanged ++" + Environment.NewLine;
            calendarScript += "          }" + Environment.NewLine;
            calendarScript += "          else if	((aFormat[i]==\"m\") || (aFormat[i]==\"mm\"))" + Environment.NewLine;
            calendarScript += "          {" + Environment.NewLine;
            calendarScript += "            monthSelected =	parseInt(aData[i], 10) - 1" + Environment.NewLine;
            calendarScript += "            tokensChanged ++" + Environment.NewLine;
            calendarScript += "          }" + Environment.NewLine;
            calendarScript += "          else if	(aFormat[i]==\"yyyy\")" + Environment.NewLine;
            calendarScript += "          {" + Environment.NewLine;
            calendarScript += "            yearSelected = parseInt(aData[i], 10)" + minusYearString  + Environment.NewLine;
            calendarScript += "            tokensChanged ++" + Environment.NewLine;
            calendarScript += "          }" + Environment.NewLine;
            calendarScript += "          else if	(aFormat[i]==\"mmm\")" + Environment.NewLine;
            calendarScript += "          {" + Environment.NewLine;
            calendarScript += "            for	(j=0; j<12;	j++)" + Environment.NewLine;
            calendarScript += "            {" + Environment.NewLine;
            calendarScript += "              if (aData[i]==monthName[j])" + Environment.NewLine;
            calendarScript += "              {" + Environment.NewLine;
            calendarScript += "                monthSelected=j" + Environment.NewLine;
            calendarScript += "                tokensChanged ++" + Environment.NewLine;
            calendarScript += "              }" + Environment.NewLine;
            calendarScript += "            }" + Environment.NewLine;
            calendarScript += "          }" + Environment.NewLine;
            calendarScript += "        }" + Environment.NewLine;
            calendarScript += "      }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "      if ((tokensChanged!=3)||isNaN(dateSelected)||isNaN(monthSelected)||isNaN(yearSelected))" + Environment.NewLine;
            calendarScript += "      {" + Environment.NewLine;
            calendarScript += "        dateSelected = dateNow" + Environment.NewLine;
            calendarScript += "        monthSelected =	monthNow" + Environment.NewLine;
            calendarScript += "        yearSelected = yearNow" + Environment.NewLine;
            calendarScript += "      }" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "      odateSelected=dateSelected" + Environment.NewLine;
            calendarScript += "      omonthSelected=monthSelected" + Environment.NewLine;
            calendarScript += "      oyearSelected=yearSelected" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "      aTag = ctl" + Environment.NewLine;
            calendarScript += "      do {" + Environment.NewLine;
            calendarScript += "        aTag = aTag.offsetParent;" + Environment.NewLine;
            calendarScript += "        leftpos	+= aTag.offsetLeft;" + Environment.NewLine;
            calendarScript += "        toppos += aTag.offsetTop;" + Environment.NewLine;
            calendarScript += "      } while(aTag.tagName!=\"BODY\");" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "      crossobj.left =	fixedX==-1 ? ctl.offsetLeft	+ leftpos :	fixedX" + Environment.NewLine;
            calendarScript += "      crossobj.top = fixedY==-1 ?	ctl.offsetTop +	toppos + ctl.offsetHeight +	2 :	fixedY" + Environment.NewLine;
            calendarScript += "      constructCalendar (1, monthSelected, yearSelected);" + Environment.NewLine;
            calendarScript += "      crossobj.visibility=(dom||ie)? \"visible\" : \"show\"" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "      hideElement( 'SELECT', document.getElementById(\"calendar\") );" + Environment.NewLine;
            calendarScript += "      hideElement( 'APPLET', document.getElementById(\"calendar\") );" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "      bShow = true;" + Environment.NewLine;
            calendarScript += "    }" + Environment.NewLine;
            calendarScript += "    else" + Environment.NewLine;
            calendarScript += "    {" + Environment.NewLine;
            calendarScript += "      hideCalendar()" + Environment.NewLine;
            calendarScript += "      if (ctlNow!=ctl) {popUpCalendar(ctl, ctl2, format)}" + Environment.NewLine;
            calendarScript += "    }" + Environment.NewLine;
            calendarScript += "    ctlNow = ctl" + Environment.NewLine;
            calendarScript += "  }" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            #endregion

            calendarScript += "document.onkeypress = function hidecal1 () {" + Environment.NewLine;
            calendarScript += "  if (event.keyCode==27)" + Environment.NewLine;
            calendarScript += "  {" + Environment.NewLine;
            calendarScript += "    hideCalendar()" + Environment.NewLine;
            calendarScript += "  }" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "document.onclick = function hidecal2 () {" + Environment.NewLine;
            calendarScript += "if (!bShow)" + Environment.NewLine;
            calendarScript += "{" + Environment.NewLine;
            calendarScript += "  hideCalendar()" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "  bShow = false" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "" + Environment.NewLine;
            calendarScript += "if(ie)" + Environment.NewLine;
            calendarScript += "{" + Environment.NewLine;
            calendarScript += "  init()" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "else" + Environment.NewLine;
            calendarScript += "{" + Environment.NewLine;
            calendarScript += "  window.onload=init" + Environment.NewLine;
            calendarScript += "}" + Environment.NewLine;
            calendarScript += "</script>";

            if (!Page.ClientScript.IsStartupScriptRegistered("calendarScript"))
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "calendarScript", calendarScript);

        }

        protected override void Render(HtmlTextWriter writer)
        {
            string html = "";
            string onClick = "";
            onClick = "onclick='javascript:popUpCalendar(" + this.UniqueID + "," + this.UniqueID + ");' ";

            html += "<input style='width:80px;' class=''" + (CssClass == "" ? "calendartext" : CssClass) + "' readonly='true' type='text' name='" + this.UniqueID + "' value='" + DateValue + "'>";
            if (Enabled)
            {
                html += "&nbsp;";
                if (ImageButtonUrl == "")
                    html += "<input " + onClick + " type='button' name='" + this.UniqueID + "_btn' value='Date' align = 'AbsMiddle'>";
                else
                    html += "<img src='" + ImageButtonUrl + "' align='AbsMiddle' style='cursor: hand' " + onClick + " alt='Select Date'/>";
            }
            writer.Write(html);
        }

        #region IPostBackDataHandler Members

        public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
        {
            DateValue = postCollection[this.UniqueID];
            return false;
        }

        public void RaisePostDataChangedEvent()
        {
            
        }

        #endregion
    }
}
