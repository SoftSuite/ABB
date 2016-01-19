using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ABB.Flow.Sales
{
    public class UCurrency
    {
        private CultureInfo ThaiCulture = new CultureInfo("th-TH");
        private string[] Suffix = { "", "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
        private string[] NumSpeak = {"", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า"};

        #region "Property"
        public string LongBathUnit
        {
            get { return "บาท"; }
        }
        public string ShortBathUnit
        {
            get { return "บ."; }
        }
        public int DecimalLength
        {
            get { return 2; }
        }
        #endregion 

        //#region "Thai Format"

        public string ToThaiShortFormat(double InputCurrency)
        {
            return InputCurrency.ToString("C", ThaiCulture);
        }

        public string ToThaiLongFormat(double InputCurrency)
        {
            if (InputCurrency != 1 & InputCurrency != 0)
            {
                return InputCurrency + " " + LongBathUnit;
            }
            else
            {
                return InputCurrency + " " + ShortBathUnit;
            }
        }

        //public string ToThaiBahtString(double M)
        //{
        //    string S1 = "";
        //    string S2 = "";
        //    string S3 = "";
        //    System.Text.StringBuilder Result = new System.Text.StringBuilder();
        //    if ((M == 0))
        //        return ("ศูนย์บาท");
        //    SplitCurr(M, S1, S2, S3);
        //    if ((S1.Length > 0))
        //        Result.Append(Speak(S1) + "ล้าน");
        //    if ((S2.Length > 0))
        //        Result.Append(Speak(S2) + "บาท");
        //    if ((S3.Length > 0))
        //    {
        //        Result.Append(SpeakStang(S3) + "สตางค์");
        //    }
        //    else
        //    {
        //        Result.Append("ถ้วน");
        //    }
        //    return (Result.ToString);
        //} 

        //    private string Speak(string S) 
        //    { 
        //        int C; 
        //        System.Text.StringBuilder Result = new System.Text.StringBuilder(); 
        //        int L; 
        //        if ((S.Length == 0)) 
        //            return (""); 
        //        L = S.Length; 
        //        for (int i = 1; i <= L; i++) { 
        //            if ((S.Chars(i - 1) == "-")) { 
        //                Result.Append("ติดลบ"); 
        //            } 
        //            else { 
        //                C = Conversion.Val(S.Chars(i - 1)); 
        //                if (((i == L) & (C == 1))) { 
        //                    if ((L == 1)) { 
        //                        return ("หนึ่ง"); 
        //                    } 
        //                    if ((L > 1) & (S.Chars(L - 2) == "0")) { 
        //                        Result.Append("หนึ่ง"); 
        //                    } 
        //                    else { 
        //                        Result.Append("เอ็ด"); 
        //                    } 
        //                } 
        //                else if (((i == L - 1) & (C == 2))) { 
        //                    Result.Append("ยี่สิบ"); 
        //                } 
        //                else if (((i == L - 1) & (C == 1))) { 
        //                    Result.Append("สิบ"); 
        //                } 
        //                else { 
        //                    if ((C != 0)) { 
        //                        Result.Append(NumSpeak(C) + Suffix(L - i + 1)); 
        //                    } 
        //                } 
        //            } 
        //        } 
        //        return (Result.ToString()); 
        //    } 

        //    private string SpeakStang(string S) 
        //    { 
        //        int I; 
        //        int L; 
        //        int C; 
        //        System.Text.StringBuilder Result = new System.Text.StringBuilder(); 
        //        L = S.Length; 
        //        if ((L == 0)) 
        //            return (""); 
        //        if ((L == 1)) 
        //            S = S + "0"; 

        //            L = 2; 
        //        if ((L > 2)) 
        //            S = S.Substring(0, 2); 

        //            L = 2; 
        //        for (I = 1; I <= 2; I++) { 
        //            C = Conversion.Val(S.Chars(I - 1)); 
        //            if (((I == L) & (C == 1))) { 
        //                if (((int)Strings.Mid(S, 1, 1) == 0)) { 
        //                    Result.Append("หนึ่ง"); 
        //                } 
        //                else { 
        //                    Result.Append("เอ็ด"); 
        //                } 
        //            } 
        //            else if (((I == L - 1) & (C == 2))) { 
        //                Result.Append("ยี่สิบ"); 
        //            } 
        //            else if (((I == L - 1) & (C == 1))) { 
        //                Result.Append("สิบ"); 
        //            } 
        //            else { 
        //                if ((C != 0)) { 
        //                    Result.Append(NumSpeak(C) + Suffix(2 - I + 1)); 
        //                } 
        //            } 
        //        } 
        //        return (Result.ToString()); 
        //    } 

        //    private void SplitCurr(double M, ref string S1, ref string S2, ref string S3) 
        //    { 
        //        string S; 
        //        int L; 
        //        int Position; 
        //        S = (string)M; 
        //        Position = S.IndexOf(".") + 1; 
        //        if ((Position != 0)) { 
        //            //this currency have a point 
        //            S1 = S.Substring(0, Position - 1); 
        //            S3 = S.Substring(Position); 
        //            if (S3 == "00") 
        //                S3 = ""; 
        //        } 
        //        else { 
        //            S1 = S; 
        //            S3 = ""; 
        //        } 
        //        L = S1.Length; 
        //        if ((L > 6)) { 
        //            S2 = S1.Substring(L - 5 - 1); 
        //            S1 = S1.Substring(0, L - 6); 
        //        } 
        //        else { 
        //            S2 = S1; 
        //            S1 = ""; 
        //        } 
        //        if ((!Information.IsNumeric(S1))) 
        //            S1 = ""; 
        //        if ((!Information.IsNumeric(S2))) 
        //            S2 = ""; 
        //        if ((Conversion.Val(S1) == 0)) 
        //            S1 = ""; 
        //        if ((Conversion.Val(S2) == 0)) 
        //            S2 = ""; 
        //    } 
        //} 

        //#endregion 
    }
}