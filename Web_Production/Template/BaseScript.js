// JScript File

function SelectAllCheckboxes(spanChk){

   // Added as ASPX uses SPAN for checkbox
   var oItem = spanChk.children;
   var theBox= (spanChk.type=="checkbox") ? 
        spanChk : spanChk.children.item[0];
   xState=theBox.checked;
   elm=theBox.form.elements;

   for(i=0;i<elm.length;i++)
     if(elm[i].type=="checkbox" && 
              elm[i].id!=theBox.id)
     {
       //elm[i].click();
       if(elm[i].checked!=xState)
         elm[i].click();
       //elm[i].checked=xState;
     }
 }
 
function ChkDbl(ctl)
{
	zz = window.event.keyCode;
	if ( zz < 48 || zz > 57)  {
		if (zz == 46) {
			if (ctl.value.indexOf(".", 0) >= 0)
				window.event.keyCode = 0;
		}
		else window.event.keyCode = 0;
	}
}

function ChkInt(ctl)
{
	zz = window.event.keyCode;

	if ( zz < 48 || zz > 57)  {
		window.event.keyCode = 0;
	}

}

function ChkMinusInt(ctl)
{
	zz = window.event.keyCode;

	if ( zz < 48 || zz > 57 )  {
		if (zz == 45) {
			if (ctl.value.indexOf("-", 0) >= 0)
				window.event.keyCode = 0;
		}
		else
			window.event.keyCode = 0;
	}
}

function ChkMinusDbl(ctl)
{
	zz = window.event.keyCode;

	if ( zz < 48 || zz > 57)  {
		if (zz == 46) {
			if (ctl.value.indexOf(".", 0) >= 0)
				window.event.keyCode = 0;
		}
		else {
			if (zz == 45) {
				if (ctl.value.indexOf("-", 0) >= 0)
					window.event.keyCode = 0;
			}
			else
				window.event.keyCode = 0;
		}			
	}
}


function valDbl(ctlz)
{
	ctlz.value = formatDbl(ctlz.value);
	ctlz.value = ClearMinus(ctlz.value);
	ctlz.value = AddComma(ctlz.value, ctlz.value.length - 3);
}

function valDbl3(ctlz)
{
	ctlz.value = formatDbl3(ctlz.value);
	ctlz.value = ClearMinus(ctlz.value);
	ctlz.value = AddComma(ctlz.value, ctlz.value.length - 4);
}

function valDbl4(ctlz)
{
	ctlz.value = formatDbl4(ctlz.value);
	ctlz.value = ClearMinus(ctlz.value);
	ctlz.value = AddComma(ctlz.value, ctlz.value.length - 5);
}

function valDbl5(ctlz)
{
	ctlz.value = formatDbl5(ctlz.value);
	ctlz.value = ClearMinus(ctlz.value);
	ctlz.value = AddComma(ctlz.value, ctlz.value.length - 6);
}

function valMDbl(ctlz)
{
	ctlz.value = formatDbl(ctlz.value);
	ctlz.value = AddComma(ctlz.value, ctlz.value.length - 3);
}

function valInt(ctlz)
{
	ctlz.value = formatInt(ctlz.value);
	ctlz.value = ClearMinus(ctlz.value);
	ctlz.value = AddComma(ctlz.value, ctlz.value.length);
}

function valMInt(ctlz)
{
	ctlz.value = formatInt(ctlz.value);
	ctlz.value = AddComma(ctlz.value, ctlz.value.length);
}

function valYear(ctlz)
{
	ctlz.value = formatYear(ctlz.value);
}

function formatDbl(valIn)
{
	var temp = valIn;
	if ( isNaN( parseFloat(temp) ) ) {
		temp = 0;
	}
	var temp = "" + Math.round(  parseFloat(temp) * 100 );
	if (temp == 0)
		return '0.00';
	else {
		if ( parseFloat(temp) < 0) {
			temp = temp.substring(1, temp.length);
			var i = temp.length;
			while (i < 3)
			{
				temp = "0" + temp;
				i = i+1;
			}
			i = i-2;
			temp = "-" + temp.substring(0,i) + "." + temp.substring(i, temp.length);
			
		}
		else {
			var i = temp.length;
			while (i < 3)
			{
				temp = "0" + temp;
				i = i+1;
			}
			i = i-2;
			temp = temp.substring(0,i) + "." + temp.substring(i,temp.length);  
		}
			return temp;
	}
}
function formatDbl4(valIn)
{
	var temp = valIn;
	if ( isNaN( parseFloat(temp) ) ) {
		temp = 0;
	}
	var temp = "" + Math.round(  parseFloat(temp) * 10000 );
	if (temp == 0)
		return '0.0000';
	else {
		var i = temp.length;
		while (i < 5)
		{
			temp = "0" + temp;
			i = i+1;
		}
		i = i-4;
		temp = temp.substring(0,i) + "." + temp.substring(i,temp.length);  

		return temp;
	}
}
function formatDbl5(valIn)
{
	var temp = valIn;
	if ( isNaN( parseFloat(temp) ) ) {
		temp = 0;
	}
	var temp = "" + Math.round(  parseFloat(temp) * 100000 );
	if (temp == 0)
		return '0.00000';
	else {
		var i = temp.length;
		while (i < 6)
		{
			temp = "0" + temp;
			i = i+1;
		}
		i = i-5;
		temp = temp.substring(0,i) + "." + temp.substring(i,temp.length);  

		return temp;
	}
}
function formatDbl10(valIn)
{
	var temp = valIn;
	if ( isNaN( parseFloat(temp) ) ) {
		temp = 0;
	}
	var temp = "" + Math.round(  parseFloat(temp) * 10000000000 );
	if (temp == 0)
		return '0.0000000000';
	else {
		var i = temp.length;
		while (i < 11)
		{
			temp = "0" + temp;
			i = i+1;
		}
		i = i-10;
		temp = temp.substring(0,i) + "." + temp.substring(i,temp.length);  

		return temp;
	}
}
function formatDbl3(valIn)
{
	var temp = valIn;
	if ( isNaN( parseFloat(temp) ) ) {
		temp = 0;
	}
	var temp = "" + Math.round(  parseFloat(temp) * 1000 );
	if (temp == 0)
		return '0.000';
	else {
		var i = temp.length;
		while (i < 4)
		{
			temp = "0" + temp;
			i = i+1;
		}
		i = i-3;
		temp = temp.substring(0,i) + "." + temp.substring(i,temp.length);  

		return temp;
	}
}
function formatInt(valIn)
{
	var temp = valIn;
	if (isNaN( parseInt(temp) ) ) {
		temp = 0;
	}
	return temp;
}

function formatYear(valIn)
{
	var temp = valIn;
	if ( isNaN( parseInt(temp) ) ) {
		temp = "";
	}
	return temp;
}

function AddComma(valIn, posStart)
{
	if (parseFloat(valIn) < 0.00) 
		j = 4;
	else
		j = 3;
		
	var i = posStart;
	//i = 0;              this line for block addcomma to not working...
	var temp = valIn;
	while (i > j) {
		i = i - 3;
		temp = temp.substring(0,i) + "," + temp.substring(i, temp.length);
	}
	return temp;
}

function ClearComma(valIn)
{
	temp = valIn;
	while (temp.indexOf(",", 0) != -1)
		temp = temp.replace(",", "");
	
	return temp;

}

function ClearMinus(valIn)
{
	temp = valIn;
	while (temp.indexOf("-", 0) != -1)
		temp = temp.replace("-", "");
		
	return temp;
}

function prepareNum(ctlz)
{
	ctlz.value = ClearComma(ctlz.value);
	ctlz.select();
}

function valIntValue(ctlz, valuez)
{
	if (isNaN( parseInt(ctlz.value) ) ) {
		ctlz.value = valuez;
	}
}

function FormatDate(ctl)
{
	zz = window.event.keyCode;
	if ( zz < 47 || zz > 57)  {
		window.event.keyCode = 0;
	}
	else if ( zz == 47 )
	{
		if( ctl.value.length == 1 )
			ctl.value = '0' + ctl.value;
		else if ( ctl.value.length == 2 || ctl.value.length == 5)
			{
				ctl.value = ctl.value + '/';
				window.event.keyCode = 0;
			}
		else if ( ctl.value.length == 4)
			{
				ctl.value = ctl.value.substr(0,3) + '0' + ctl.value.substr(3,1) + '/';
				window.event.keyCode = 0;
			}
		else 
			window.event.keyCode = 0;
	}
	else
	{
		if ( ctl.value.length == 2 || ctl.value.length == 5)
			ctl.value = ctl.value + '/';
	}
}

function ClickPosPopUp(popURL, popNAME, popWIDTH, popHEIGHT, exOption) {
		var hz, wz, lz, tz, mw, mh;
		wz = popWIDTH;
		hz = popHEIGHT;
		lz = event.screenX - (wz / 2);
		tz = event.screenY - (hz / 2);
		mw = screen.availWidth -  (wz + 50);
		mh = screen.availHeight - (hz + 50);
		
		if (lz < mw && tz < mh)
		{
			lz = (lz < 0 ? 0 : lz);
			tz = (hz < 0 ? 0 : tz);
		}
		else if( lz < mw )
		{
			tz = mh;
			lz = (lz < 0 ? 0 : lz);
			tz = (hz < 0 ? 0 : tz);
		}
		else if ( tz < mh )
		{
			lz = mw;
			lz = (lz < 0 ? 0 : lz);
			tz = (hz < 0 ? 0 : tz);
		}
		else {
			lz = mw;
			tz = mh;
			lz = (lz < 0 ? 0 : lz);
			tz = (hz < 0 ? 0 : tz);

		}
		window.open(popURL, popNAME, 'height=' + hz + ',width=' + wz + ',left=' + lz + ',top=' + tz + ( exOption == '' ? '' : ',' ) + exOption);
}

function CenterPopUp(popURL, popNAME, popWIDTH, popHEIGHT, exOption) {
		var hz, wz, lz, tz, mw, mh;
		hz = popHEIGHT;
		wz = popWIDTH;
		mw = (screen.availWidth/2) -  (wz/2);
		mh = (screen.availHeight/2) - (hz/2);

		lz = mw;
		tz = mh;
		lz = (lz < 0 ? 0 : lz);
		tz = (hz < 0 ? 0 : tz);

		//alert(hz + ' ' + wz + ' ' + lz + ' ' + tz + ' ' + mw + ' ' + mh + ' ' + screen.availHeight);
		window.open(popURL, popNAME, 'height=' + hz + ',width=' + wz + ',left=' + lz + ',top=' + tz + ( exOption == '' ? '' : ',' ) + exOption);
		}
		
		
function SetVisible(objName, val)
{
	if ( val == 1 ) {
		document.getElementById(objName).style.display = "block";
		}
	else
		document.getElementById(objName).style.display = "none";
		
}		

function CheckBoxDisable( chkBox, arrCtl )
{
	for (i=0; i< arrCtl.length ; i++) {
		document.getElementById(arrCtl[i]).disabled = !(chkBox.checked);
	}
}

function CheckBoxEnable( chkBox, arrCtl )
{
	for (i=0; i< arrCtl.length ; i++) {
		document.getElementById(arrCtl[i]).disabled = (chkBox.checked);
	}
}

///////////////////////////////////////////////////////////////////////////
function sumDbl(arrCtl, targetid)
{
	var tmp = 0;
	for (i=0;i < arrCtl.length;i++) {
		zz = formatDbl(ClearComma(document.getElementById(arrCtl[i]).value));
		tmp = parseFloat(tmp) + parseFloat(zz);

	}
	ret = formatDbl(tmp);
	ret = AddComma(ret, ret.length - 3);
	document.getElementById(targetid).value = ret;

}

function sumInt(arrCtl, targetid)
{
	var tmp = 0;
	for (i=0;i < arrCtl.length;i++) {
		zz = formatInt(ClearComma(document.getElementById(arrCtl[i]).value));
		tmp = parseInt(tmp) + parseInt(zz);

	}
	ret = formatDbl(tmp);
	ret = AddComma(ret, ret.length - 3);
	document.getElementById(targetid).value = ret.replace(".00", "");

}

function CheckLength30(source, arguments)
{
	var strtmp = trim(arguments.Value);
	if( strtmp.length >= 30 )
         arguments.IsValid=true;
    else
         arguments.IsValid=false;
}

function trim(str) 
{
	var start=0;
	var end=str.length;
	var i;

	for(i =0;i<str.length;i++){
		if(str.charCodeAt(i) == 9 || str.charCodeAt(i) == 32 || str.charCodeAt(i) == 10 || str.charCodeAt(i) == 13 )
			start = i+1;	
		else
			break;
	}
	
	for(i = str.length-1;i>0;i--){
		if((str.charCodeAt(i) == 9 || str.charCodeAt(i) == 32 || str.charCodeAt(i) == 10 || str.charCodeAt(i) == 13) && (start < (str.length-1)))
			end = i;	
		else
			break;
	}
	
	return (str.substring(start,end));
}

function CheckPercent(tha,fore,per1,per2,per3,per4,txtInput){
	var tol=0.00;
	tol = parseFloat(formatDbl(ClearComma(document.getElementById(per1).value))) +
		  parseFloat(formatDbl(ClearComma(document.getElementById(per2).value))) +
		  parseFloat(formatDbl(ClearComma(document.getElementById(per3).value))) +
		  parseFloat(formatDbl(ClearComma(document.getElementById(per4).value)));

	if(tol > 100){
		document.getElementById(txtInput).value = 100.00 - (parseFloat(tol) - parseFloat(formatDbl(ClearComma(document.getElementById(txtInput).value))));
		tol = 100.00;
	}

	document.getElementById(tha).value = formatDbl(100.00 - parseFloat(tol));
	document.getElementById(fore).value = formatDbl(tol);
}

function CalPercent(target, arrCtl, result)
{
	var tmpSrc = 0.0;
	var tmpArr = 0.0;
	tmpSrc = formatDbl( ClearComma(document.getElementById(target).value) );
	for (i=0; i< arrCtl.length ; i++) {
		zz = formatDbl( ClearComma(document.getElementById(arrCtl[i]).value) );
		tmpArr = parseFloat(tmpArr) + parseFloat(zz);
	}
	
	if (tmpArr > 0) {
		zret = ( tmpSrc * 100.0 ) / tmpArr
	}
	else
		zret = 0;
		
	ret = formatDbl( zret );
	
	document.getElementById(result).value = ret;

}

function difPercent(target, arrCtl)
{
	var tmpSrc = 0.0;
	var tmpArr = 0.0;
	tmpSrc = formatDbl( ClearComma(document.getElementById(target).value) );
	for (i=0; i< arrCtl.length ; i++) {
		zz = formatDbl( ClearComma(document.getElementById(arrCtl[i]).value) );
		tmpArr = parseFloat(tmpArr) + parseFloat(zz);
	}
	ret = formatDbl(100 - tmpArr);
	
	document.getElementById(target).value = ret;

}

function diffsumDbl(firstid, arrCtl, targetid)
{
	var tmpSrc = 0;
	var tmpArr = 0;
	tmpSrc = formatDbl( ClearComma(document.getElementById(firstid).value) );
	for (i=0; i< arrCtl.length ; i++) {
		zz = formatDbl( ClearComma(document.getElementById(arrCtl[i]).value) );
		tmpArr = parseFloat(tmpArr) + parseFloat(zz);
	}
	
	ret = formatDbl( parseFloat(tmpSrc) - parseFloat(tmpArr) );
	ret = AddComma(ret, ret.length - 3);
	
	document.getElementById(targetid).value = ret;

}
function ChkDisabled( ctlz, zid)
{
	if (ctlz.checked)
		document.getElementById(zid).disabled = '';
	else
		document.getElementById(zid).disabled = 'disabled';

}
function ChkDisabled2(ctlz,zid){
		
	if (ctlz.checked){
		for(i=0;i<zid.length;++i){
			document.getElementById(zid[i]).disabled = '';
		}
	}else{
		for(i=0;i<zid.length;++i){
			document.getElementById(zid[i]).disabled = 'disabled';
		}
	}
}
// คลิ้ก ปุ่มเรดิโอ นี้ แล้วต้องการให้ control ตัวไหน ใช้งานได้
function ChkEnabledRDB(ctlz,enableid){
	if (ctlz.checked){
		document.getElementById(enableid).disabled = '';
	}
}
// คลิ้ก ปุ่มเรดิโอ นี้ แล้วต้องการให้ control ตัวไหน ใช้งานไม่ได้ 
function ChkDisabledRDB(ctlz,disableid){
	if (ctlz.checked){
		document.getElementById(disableid).disabled = 'disabled';
	}
}
// คลิ้ก ปุ่มเรดิโอ นี้ แล้วต้องการให้ กลุ่ม control (หลายตัว) ใช้งานไม่ได้
function ChkDisabledRDB2(ctlz,disableid){
	if (ctlz.checked){
		for(i=0;i<disableid.length;++i){
			document.getElementById(disableid[i]).disabled = 'disabled';
		}
	}
}
// คลิ้ก ปุ่มเรดิโอ นี้ แล้วต้องการให้ control (ตัวเดียว) ใช้งานได้ กลุ่ม control (หลายตัว) ใช้งานไม่ได้ 
function ChkDisabledRDB3(ctlz,zid,disableid){
		
	if (ctlz.checked){
		document.getElementById(zid).disabled = '';
		
		for(i=0;i<disableid.length;++i){
			document.getElementById(disableid[i]).disabled = 'disabled';
		}
	}
}
// คลิ้ก ปุ่มเรดิโอ นี้ แล้วต้องการให้กลุ่ม control (หลายตัว) ใช้งานได้ กลุ่ม control (หลายตัว) ใช้งานไม่ได้ 
function ChkDisabledRDB4(ctlz,zid,disableid){
	if (ctlz.checked){
	
		for(i=0;i<zid.length;++i){
			document.getElementById(zid[i]).disabled = '';
		}
		for(i=0;i<disableid.length;++i){
			document.getElementById(disableid[i]).disabled = 'disabled';
		}
	}
}
// คลิ้ก ปุ่มเรดิโอ นี้ แล้วต้องการให้กลุ่ม control (หลายตัว) ใช้งานได้ กลุ่ม control (หลายตัว) ใช้งานไม่ได้ และ กำหนด classname 
function ChkDisabledRDB5(ctlz,zid,disableid){
    
        if (ctlz.checked){
                if (zid != null) {
                    for(i=0;i<zid.length;++i){
					    ctlzz = document.getElementById(zid[i])
					    ctlzz.className = ctlzz.className.toLowerCase().replace("-View","");
					    ctlzz.readonly = '';                
                    }
                }
                if (disableid != null)
                    for(i=0;i<disableid.length;++i){
					    ctlzz = document.getElementById(zid[i])
					    ctlzz.className = ctlzz.className.toLowerCase().replace("-View","") + "-View";
					    ctlzz.readonly = 'readonly';                
                    } 
        }
}

// Create By : chOK
// ใช้ในกรณีที่ เลือกคอมโบตัวเดียว แล้วเซ็ตให้คอมโบหลายตัวมีค่าเหมือนกับตัวแรกที่เลือก
function setSameValueMultiCMB(sourceID, arrTargetID )
{
	var val;
	val = 	document.getElementById(sourceID).value;
	for(i=0; i<arrTargetID.length; i++){
		document.getElementById(arrTargetID[i]).value = val;
	}
}

function setSameValueMultiCMBMain(sourceID, arrTargetID, arrSubId )
{
	var val;
	val = 	document.getElementById(sourceID).value;
	for(i=0; i<arrTargetID.length; i++){
		document.getElementById(arrTargetID[i]).value = val;
		setComboSubNatrueactivity(document.getElementById( arrTargetID[i] ),arrSubId[i]);
	}
}

function CheckDisabled(ctl, subChkId, arrId){
	if( ctl.checked ){
		for(j=0; j<arrId.length; j++)
			document.getElementById(arrId[j]).disabled = !document.getElementById(subChkId).checked;
	}
}
function CheckVisible(ctl, subChkId, arrId){
	if( ctl.checked ){
		if( document.getElementById(subChkId).checked )
			vis = 'block';
		else
			vis = 'none';
			
		for(j=0; j<arrId.length; j++)
			document.getElementById(arrId[j]).style.display = vis;
	}
}
function ChkVisible(ctl, arrId)
{
	if( ctl.checked )
		vis = 'block';
	else
		vis = 'none';
		
	for(j=0; j<arrId.length; j++)
		document.getElementById(arrId[j]).style.display = vis;
}
// Create By : chOK
// ในในกรณีที่จะเอาข้อมูลจากหน้า popup มาใส่ในหน้า ที่เรียก popup นั้นขึ้นมา 
// โดยที่ newLine ถ้ามีค่าเท่ากับ 'y' หรือ 'Y' คือจะขั้นบันทัดใหม่ให้กับข้อมูลแต่ละ Control ในหน้า popup
function setTextBoxOpenerFromLable(arrCtl, targetid, newLine)
{
	var tmp = "";
	for ( i=0; i < arrCtl.length; i++){
		tmp = tmp + document.getElementById(arrCtl[i]).innerText;
		if ( newLine != null && newLine.toLowerCase() == 'y' && i < (arrCtl.length - 1))
			tmp = tmp + '\n';
	}
	if( newLine != null && newLine.toLowerCase() == 'y' && opener.window.document.getElementById(targetid).value != "" )
		tmp = '\n' + tmp;
	opener.window.document.getElementById(targetid).value = opener.window.document.getElementById(targetid).value + tmp;
}

// Create By : chOK
function setHSC( hidTargetId, txtTargetId, hidLoid, lblCode, lblNameTh, lblNameEn )
{
	opener.window.document.getElementById(hidTargetId).value = document.getElementById(hidLoid).value;
	opener.window.document.getElementById(txtTargetId).value = document.getElementById(lblCode).innerText + ':' + document.getElementById(lblNameTh).innerText + "/" + document.getElementById(lblNameEn).innerText;
	opener.window.focus();
	window.close(true);
}
var popUpWin2=0;
function popUpNewWindow(URLStr,zwidth,zheight)
{
  if(popUpWin2)
  {
    if(!popUpWin2.closed) popUpWin2.close();
  }
  popUpWin2 = open(URLStr,'popUpWin2','width=' + zwidth + 'px, height=' + zheight + 'px, status=no,scrollbars=yes,left=' + ((screen.availWidth/2)-(zwidth/2)) + ' , top=' + ((screen.availHeight/2)-(zheight/2)))
  popUpWin2.focus();
}

function OpenNewModalDialog(url, width, height, sc , rz  ) 
{
    sc = (sc == 'undefined' ? 'yes' : sc);
    rz = (rz == 'undefined' ? 'no' : rz);
    var left = (screen.availWidth - width)/2;
	var top = (screen.availHeight - height)/2;
	if (window.showModalDialog)
	{
		var dialogArguments = new Object();
		var _R = window.showModalDialog(url, dialogArguments, "dialogWidth=" + width +"px;dialogHeight=" + height + "px;scroll=" + sc + ";status=no;");
		return _R;
	}		
	else	//NS			
	{  	
		var left = (screen.width-width)/2;
		var top = (screen.height-height)/2;
 		winHandle = window.open(url, ID, "modal,toolbar=false,location=false,directories=false,status=false,menubar=false,scrollbars=" + sc + ",resizable=" + rz + ",left="+left+",top="+top+",width="+width+",height="+height);
		winHandle.focus();
		return 'test';
	}
	return false;
}

function setValueMulCMB( arrCmbId, arrValId)
{
	if( (arrCmbId.length == arrValId.length) && ( arrCmbId.length > 0 ) )
	{
		for( i=0 ; i<arrCmbId.length ;i++ )
		{
			document.getElementById( arrCmbId[i] ).value = document.getElementById( arrValId[i] ).value;
		}
	}
}
function setText(objName,val)
{	
	objName.value = val;
}
function ChkDisabled3(ctlz,zid,disabled){
				
	if (document.getElementById(ctlz).checked){
		for(i=0;i<zid.length;++i){
			document.getElementById(zid[i]).disabled = disabled;
		}
	} else{
		if (disabled == '')
		{
			for(i=0;i<zid.length;++i){
				document.getElementById(zid[i]).disabled = 'disabled';
			}
		}
	}
	
}
function setTextBoxOpenerFromLable(arrCtl, targetid, newLine,seperator)
{	
	var tmp = "";
	var i = 0;	
	for ( i=0; i < arrCtl.length; i++){		
		tmp = tmp + document.getElementById(arrCtl[i]).innerText;
		if ( newLine != null && newLine.toLowerCase() == 'y' && i < (arrCtl.length - 1))
			tmp = tmp + '\n';
		else if (newLine != null && newLine.toLowerCase() == 'n' && i < (arrCtl.length - 1))
			tmp = tmp + seperator;
	}
	if( newLine != null && newLine.toLowerCase() == 'y' && opener.window.document.getElementById(targetid).value != "" )
		tmp = '\n' + tmp;
	else if( newLine != null && newLine.toLowerCase() == 'n' && opener.window.document.getElementById(targetid).value != "" )
		tmp = seperator + tmp;
	opener.window.document.getElementById(targetid).value = opener.window.document.getElementById(targetid).value + tmp;	
}

function setTextBoxOpenerFromLable(arrCtl, targetid, newLine, seperator, openerjoin)
{	
	var tmp = "";
	var i = 0;	
	for ( i=0; i < arrCtl.length; i++){		
		tmp = tmp + document.getElementById(arrCtl[i]).innerText;
		if ( newLine != null && newLine.toLowerCase() == 'y' && i < (arrCtl.length - 1))
			tmp = tmp + '\n';
		else if (newLine != null && newLine.toLowerCase() == 'n' && i < (arrCtl.length - 1))
			tmp = tmp + seperator;
	}
	if( newLine != null && newLine.toLowerCase() == 'y' && opener.window.document.getElementById(targetid).value != "" )
		tmp = '\n' + tmp;
	else if( newLine != null && newLine.toLowerCase() == 'n' && opener.window.document.getElementById(targetid).value != "" && openerjoin.toLowerCase() == 'y')
		tmp = seperator + tmp;
		
	if( openerjoin.toLowerCase() == 'y' )
		opener.window.document.getElementById(targetid).value = opener.window.document.getElementById(targetid).value + tmp;	
	else
		opener.window.document.getElementById(targetid).value = tmp;	
}

function CheckBoxStatus( mainCtl, arrCtl ) {
	var tmp = 0;
	for (i=0;i < arrCtl.length;i++) {
		if (!document.getElementById(arrCtl[i]).disabled)
		document.getElementById(arrCtl[i]).checked = mainCtl.checked;
	}
}

// zid เป็น TextBox
function ChkDisabled4(ctlz,zid,disabled){
			
	var obj ;	
	if (document.getElementById(ctlz).checked){
		for(i=0;i<zid.length;++i){			
			obj = document.getElementById(zid[i]);			
			if (disabled == '')
			{
				obj.className = obj.className.toLowerCase().replace("View","");
				obj.readOnly = false;
			}else{
				
				obj.className = obj.className.toLowerCase().replace("View","") + "View";
				obj.readOnly = true;				
			}
			
		}
	} else{
		if (disabled == '')
		{
			for(i=0;i<zid.length;++i){
				obj = document.getElementById(zid[i]);				
				obj.className = obj.className.toLowerCase().replace("Vew","") + "View";
				obj.readOnly = true;				
			}
		}
	}
	
}

function ChkDisabled4z(ctlz,zid,disabled){
			
	var obj ;	
	if (ctlz.checked){
		for(i=0;i<zid.length;++i){			
			obj = document.getElementById(zid[i]);			
			if (disabled == '')
			{
				obj.className = obj.className.toLowerCase().replace("view","");
				obj.readOnly = false;
				obj.focus();
			}else{
				
				obj.className = obj.className.toLowerCase().replace("view","") + "View";
				obj.readOnly = true;				
				obj.value = '';
			}
			
		}
	} else{
		if (disabled == '')
		{
			for(i=0;i<zid.length;++i){
				obj = document.getElementById(zid[i]);				
				obj.className = obj.className.toLowerCase().replace("Vew","") + "View";
				obj.readOnly = true;				
			}
		}
	}
	
}

function SuperPopUp(popURL, popNAME, popHEIGHT, popWIDTH) {
		var hz, wz, lz, tz, mw, mh;
		hz = popHEIGHT;
		wz = popWIDTH;
		lz = event.screenX - (wz / 2);
		tz = event.screenY - (hz / 2);
		mw = screen.availWidth -  (wz + 50);
		mh = screen.availHeight - (hz + 50);
		
		if (lz < mw && tz < mh)
		{
			lz = (lz < 0 ? 0 : lz);
			tz = (hz < 0 ? 0 : tz);
		}
		else if( lz < mw )
		{
			tz = mh;
			lz = (lz < 0 ? 0 : lz);
			tz = (hz < 0 ? 0 : tz);
		}
		else if ( tz < mh )
		{
			lz = mw;
			lz = (lz < 0 ? 0 : lz);
			tz = (hz < 0 ? 0 : tz);
		}
		else {
			lz = mw;
			tz = mh;
			lz = (lz < 0 ? 0 : lz);
			tz = (hz < 0 ? 0 : tz);

		}
		//alert(hz + ' ' + wz + ' ' + lz + ' ' + tz + ' ' + mw + ' ' + mh + ' ' + screen.availHeight);
		window.open(popURL, popNAME, 'height=' + hz + ',width=' + wz + ',left=' + lz + ',top=' + tz + ' ');
		}
		
		function SuperCenterPopUp(popURL, popNAME, popHEIGHT, popWIDTH) {
		var hz, wz, lz, tz, mw, mh;
		hz = popHEIGHT;
		wz = popWIDTH;
		mw = (screen.availWidth/2) -  (wz/2);
		mh = (screen.availHeight/2) - (hz/2);

		lz = mw;
		tz = mh;
		lz = (lz < 0 ? 0 : lz);
		tz = (hz < 0 ? 0 : tz);

		//alert(hz + ' ' + wz + ' ' + lz + ' ' + tz + ' ' + mw + ' ' + mh + ' ' + screen.availHeight);
		window.open(popURL, popNAME, 'height=' + hz + ',width=' + wz + ',left=' + lz + ',top=' + tz + ' ');
		}

function CheckDocCode(numRow) {
	var i, ret;
	
	ret = true;
	for (i=1;i<=numRow;i++) {
		zChk = document.getElementById('subChk' + i);
		zCode = document.getElementById('txtNum' + i);
		if (zChk.checked && trim(zCode.value) == '') 
			ret = false;		
	}
	
	return ret;
}

function CheckDocCodeNO (numRow) {
	var i, ret;

 	ret = true;
	for (i=1;i<=numRow;i++) {
		zChk = document.getElementById('subChk' + i);
		zCode = document.getElementById('txtNum' + i);

		if (zChk.checked && trim(zCode.value).length != 6)
			ret = false;
	}
	return ret;
}

function CheckDocTick (numRow) {
	var i, cnt, ret;
	
	ret = true;
	cnt = 0;
	for (i=1; i<=numRow; i++) {
		zChk = document.getElementById('subChk' + i);
		if (zChk.checked) 
			cnt = cnt + 1;		
	}
	if (numRow > 0 && cnt == 0)
		ret = false
		
	return ret;
}

var popUpWin3=0;
function popUpNewWindowMain(URLStr,zwidth,zheight)
{
  if(popUpWin3)
  {
    if(!popUpWin3.closed) popUpWin3.close();
  }
  popUpWin3 = open(URLStr,'popUpWin3','width=' + zwidth + 'px, height=' + zheight + 'px, status=no,scrollbars=yes,left=' + ((screen.availWidth/2)-(zwidth/2)) + ' , top=' + ((screen.availHeight/2)-(zheight/2)))
  popUpWin3.focus();
}


function replaceAll( str, from, to ) {
    var idx = str.indexOf( from );


    while ( idx > -1 ) {
        str = str.replace( from, to ); 
        idx = str.indexOf( from );
    }

    return str;
}
//เมื่อเช็คcheckbox อันหนึ่งให้อีกหลายตัวเช็คด้วย
function ChkOther(ctlz,zid){
	if(ctlz.checked)
	{
		for(i=0;i<zid.length;++i){
			document.getElementById(zid[i]).checked = true;
		}
	}
	else
	{
	for(i=0;i<zid.length;++i){
			document.getElementById(zid[i]).checked = false;
		}
	}
}

// somZa
function setTxtReadOnly(ctl,flag)
{
	var className = ctl.className;
	className = className.toLowerCase().replace("-View","");
	if (flag)
	{
		// set text box to readonly
		ctl.readOnly = true;
		ctl.className = className+'-View' ;
	}
	else
	{
		// set text box to not readonly
		ctl.readOnly = false;
		ctl.className = className;
	}
}

function popUpWindow(URLStr,popname,zwidth,zheight)
{
  if(popUpWin2)
  {
    if(!popUpWin2.closed) popUpWin2.close();
  }
  popUpWin2 = open(URLStr,'popname','width=' + zwidth + 'px, height=' + zheight + 'px, status=no,scrollbars=yes,left=' + ((screen.availWidth/2)-(zwidth/2)) + ' , top=' + ((screen.availHeight/2)-(zheight/2)))
  popUpWin2.focus();
}

function chkAllBox(cmb, name_f, name_b)
{
    var i = 2;
    var itxt = '00' + i;
    //alert(name_f + twonum(i) + name_b);
    while ( document.getElementById(name_f + itxt.substring(itxt.length - 2, itxt.length) + name_b) != null)
    {
        zctl = document.getElementById(name_f + itxt.substring(itxt.length - 2, itxt.length) + name_b)
        if (zctl.disabled == '') zctl.checked = cmb.checked;
        i++;
        itxt = '00' + i;
    }
}
 
