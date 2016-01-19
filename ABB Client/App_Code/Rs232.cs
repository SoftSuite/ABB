using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ABBClient.App_Code
{
    public class Rs232
    {
        #region Enums

        //This enumeration provides Data Parity values.
        public enum DataParity
        {
            Parity_None = 0,
            Pariti_Odd,
            Parity_Even,
            Parity_Mark,
        }
        
        // This enumeration provides Data Stop Bit values.
        // It is set to begin with a one, so that the enumeration values
        // match the actual values.
        public enum DataStopBit
        {
            StopBit_1 = 1, 
            StopBit_2
        }

        // This enumeration contains values used to purge the various buffers.
        public enum PurgeBuffers
        {
            RXAbort = 0xf2,
            RXClear = 0xf8,
            TxAbort = 0xf1,
            TxClear = 0xf4
        }

        // This enumeration provides values for the lines sent to the Comm Port
        public enum Lines
        {
            SetRts = 3,
            ClearRts = 4,
            SetDtr = 5,
            ClearDtr = 6,
            ResetDev = 7, // Reset device if possible
            SetBreak = 8, // Set the device break line.
            ClearBreak = 9 // Clear the device break line.
        }

        // This enumeration provides values for the Modem Status, since
        // we'll be communicating primarily with a modem.
        // Note that the Flags() attribute is set to allow for a bitwise
        // combination of values.
        [Flags()]
        public enum ModemStatusBits
        {
            ClearToSendOn = 0xf10,
            DataSetReadyOn = 0xf20,
            RingIndicatorOn = 0xf40,
            CarrierDetect = 0xf80
        }

        // This enumeration provides values for the Working mode
        public enum Mode
        {
            NonOverlapped,
            Overlapped
        }

        // This enumeration provides values for the Comm Masks used.
        // Note that the Flags() attribute is set to allow for a bitwise
        // combination of values.
        [Flags()]
        public enum EventMasks
        {
            RxChar = 0xf1,
            RXFlag = 0xf2,
            TxBufferEmpty = 0xf4,
            ClearToSend = 0xf8,
            DataSetReady = 0xf10,
            ReceiveLine = 0xf20,
            Break = 0xf40,
            StatusError = 0xf80,
            Ring = 0xf100
        }

        #endregion

        //Declare the necessary class variables, and their initial values.		
        private int mhRS = -1;   // Handle to Com Port									
        private int miPort = 1;   // Default is COM1	
        private int miTimeout = 70;   // Timeout in ms
        private int miBaudRate = 9600;
        private DataParity meParity = 0;
        private DataStopBit meStopBit = 0;
        private int miDataBit = 8;
        private int miBufferSize = 512; // Buffers size default to 512 bytes
        private byte[] mabtRxBuf; // Receive buffer
        private Mode meMode; //Class working mode
        private bool mbWaitOnRead;
        private bool mbWaitOnWrite;
        private bool mbWriteErr;
        private Overlapped muOverlapped;
        private Overlapped muOverlappedW;
        private Overlapped muOverlappedE;
        private byte[] mabtTmpTxBuf; // Temporary buffer used by Async Tx
        private Thread moThreadTx;
        private Thread moThreadRx;
        private int miTmpBytes2Read;
        private EventMasks meMask;

        #region Constants

        private const int PURGE_RXABORT = 0xf2;
        private const int PURGE_RXCLEAR = 0xf8;
        private const int PURGE_TXABORT = 0xf1;
        private const int PURGE_TXCLEAR = 0xf4;
        private const Int64 GENERIC_READ = 0xf80000000;
        private const Int64 GENERIC_WRITE = 0xf40000000;
        private const int OPEN_EXISTING = 3;
        private const int INVALID_HANDLE_VALUE = -1;
        private const int IO_BUFFER_SIZE = 1024;
        private const Int64 FILE_FLAG_OVERLAPPED = 0xf40000000;
        private const int ERROR_IO_PENDING = 997;
        private const int WAIT_OBJECT_0 = 0;
        private const int ERROR_IO_INCOMPLETE = 996;
        private const int WAIT_TIMEOUT = 0xf102;  // &H102&
        private const Int64 INFINITE = 0xfFFFFFFFF;

        #endregion

        #region Properties

        //public int BaudRate
        //{
        //    get { return miBaudRate; }
        //    set { miBaudRate = value; }
        //}
        //public int BufferSize
        //{
        //    get { return miBufferSize; }
        //    set { miBufferSize = value; }
        //}
        //public int DataBit
        //{
        //    get { return miDataBit; }
        //    set { miDataBit = value; }
        //}
        //public bool Dtr
        //{
        //    set
        //    {
        //        if (mhRS != -1)
        //        {
        //            if (value)
        //                EscapeCommFunction(mhRS, Lines.SetDtr);
        //            else
        //                EscapeCommFunction(mhRS, Lines.ClearDtr);
        //        }
        //    }
        //}
        //public byte[] InputStream
        //{
        //    get { return mabtRxBuf; }
        //}
        //public string InputStreamString
        //{
        //    get
        //    {
        //        ASCIIEncoding oEncoder = new ASCIIEncoding();
        //        return oEncoder.GetString(this.InputStream);
        //    }
        //}
        //public bool IsOpen
        //{
        //    get { return (mhRS != -1); }
        //}
        //public ModemStatusBits ModemStatus
        //{
        //    get
        //    {
        //        if (mhRS == -1)
        //            throw new ApplicationException("Please initialize and open port before using this method");
        //        else
        //        {
        //            int lpModemStatus;
        //            if (!GetCommModemStatus(mhRS, lpModemStatus))
        //                throw new ApplicationException("Unable to get modem status");
        //            else
        //                return (ModemStatusBits)lpModemStatus;
        //        }
        //    }
        //}
        //public DataParity Parity
        //{
        //    get { return meParity; }
        //    set { meParity = value; }
        //}
        //public int Port
        //{
        //    get { return miPort; }
        //    set { miPort = value; }
        //}
        //public bool Rts
        //{
        //    set
        //    {
        //        if (mhRS != -1)
        //        {
        //            if (value)
        //                EscapeCommFunction(mhRS, Lines.SetRts);
        //            else
        //                EscapeCommFunction(mhRS, Lines.ClearRts);
        //        }
        //    }
        //}
        //public DataStopBit StopBit
        //{
        //    get { return meStopBit; }
        //    set { meStopBit = value; }
        //}
        //public int Timeout
        //{
        //    get { return miTimeout; }
        //    set
        //    {
        //        miTimeout = Convert.ToInt16(value == 0 ? 500 : value);
        //        pSetTimeout();
        //    }
        //}
        //public Mode WorkingMode
        //{
        //    get { return meMode; }
        //    set { meMode = value; }
        //}

        #endregion

        #region Structures

        //This is the DCB structure used by the calls to the Windows API.
        [StructLayout(LayoutKind.Sequential, Pack=1)]
        private struct DCB
        {
            public int DCBlength;
            public int BaudRate;
            public int Bits1;
            public Int16 wReserved;
            public Int16 XonLim;
            public Int16 XoffLim;
            public byte ByteSize;
            public byte Parity;
            public byte StopBits;
            public byte XonChar;
            public byte XoffChar;
            public byte ErrorChar;
            public Int16 wReserved2;
        }

        // This is the CommTimeOuts structure used by the calls to the Windows API.
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct CommTimeOuts
        {
            public int ReadIntervalTimeout;
            public int ReadTotalTimeoutMultiplier;
            public int ReadTotalTimeoutConstant;
            public int WriteTotalTimeoutConstant;
        }

        // This is the CommConfig structure used by the calls to the Windows API.
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct CommConfig
        {
            public int dwSize;
            public Int16 wVersion;
            public Int16 wReserved;
            public DCB dcbx;
            public int dwProviderSubType;
            public int dwProviderOffset;
            public int dwProviderSize;
            public byte wcProviderData; 
        }

        // This is the OverLapped structure used by the calls to the Windows API.
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Overlapped
        {
            public int Internal;
            public int InternalHigh;
            public int Offset;
            public int OffsetHigh;
            public int hEvent;
        }

        #endregion

        #region Exceptions

        // This class defines a customized channel exception. This exception is raised when a NACK is raised.
        public class CIOChannelException : ApplicationException
        {
            public CIOChannelException(string message)
                : base(message)
            {
            }
            public CIOChannelException(string message, Exception innerException)
                : base(message, innerException)
            {
            }
        }

        // This class defines a customized timeout exception.
        public class IOTimeoutException : CIOChannelException
        {
            public IOTimeoutException(string message)
                : base(message)
            {
            }
            public IOTimeoutException(string message, Exception innerException)
                : base(message, innerException)
            {
            }
        }

        #endregion

        #region Events

        public delegate void DataReceivedHandler(Rs232 souce, byte[] DataBuffer);
        public delegate void TxCompletedHandler(Rs232 source);
        public delegate void CommEventHandler(Rs232 source, EventMasks mask);
        DataReceivedHandler DataReceived;
        TxCompletedHandler TxCompleted;
        CommEventHandler CommEvent;

        #endregion

        #region Win32API

        // The following functions are the required Win32 functions needed to make communication with the Comm Port possible.

        [DllImport("kernel32.dll")]
        private static extern int BuildCommDCB(string lpDef, DCB lpDCB);

        [DllImport("kernel32.dll")]
        private static extern int ClearCommError(int hFile, int lpErrors, int l);

        [DllImport("kernel32.dll")]
        private static extern int CloseHandle(int hObject);

        [DllImport("kernel32.dll")]
        private static extern int CreateEvent(int lpEventAttributes, int bManualReset, int bInitialState, [MarshalAs(UnmanagedType.LPStr)] string lpName);

        [DllImport("kernel32.dll")]
        private static extern int CreateFile([MarshalAs(UnmanagedType.LPStr)] string lpFileName, int dwDesiredAccess, int dwShareMode, int lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);

        [DllImport("kernel32.dll")]
        private static extern bool EscapeCommFunction(int hFile, Int64 ifunc);

        [DllImport("kernel32.dll")]
        private static extern int FormatMessage(int dwFlags, int lpSource, int dwMessageId, int dwLanguageId, [MarshalAs(UnmanagedType.LPStr)] string lpBuffer, int nSize, int Arguments);

        [DllImport("kernel32.dll")]
        private static extern int FormatMessageA(int dwFlags, int lpSource, int dwMessageId, int dwLanguageId, StringBuilder lpBuffer, int nSize, int Arguments);

        [DllImport("kernel32.dll")]
        private static extern bool GetCommModemStatus(int hFile, int lpModemStatus);

        [DllImport("kernel32.dll")]
        private static extern int GetCommState(int hCommDev, DCB lpDCB);

        [DllImport("kernel32.dll")]
        private static extern int GetCommTimeouts(int hFile, CommTimeOuts lpCommTimeouts);

        [DllImport("kernel32.dll")]
        private static extern int GetLastError();

        [DllImport("kernel32.dll")]
        private static extern int GetOverlappedResult(int hFile, Overlapped lpOverlapped, int lpNumberOfBytesTransferred, int bWait);

        [DllImport("kernel32.dll")]
        private static extern int PurgeComm(int hFile, int dwFlags);

        [DllImport("kernel32.dll")]
        private static extern int ReadFile(int hFile, byte[] Buffer, int nNumberOfBytesToRead, int lpNumberOfBytesRead, Overlapped lpOverlapped);

        [DllImport("kernel32.dll")]
        private static extern int SetCommTimeouts(int hFile, CommTimeOuts lpCommTimeouts);

        [DllImport("kernel32.dll")]
        private static extern int SetCommState(int hCommDev, DCB lpDCB);

        [DllImport("kernel32.dll")]
        private static extern int SetupComm(int hFile, int dwInQueue, int dwOutQueue);

        [DllImport("kernel32.dll")]
        private static extern int SetCommMask(int hFile, int lpEvtMask);

        [DllImport("kernel32.dll")]
        private static extern int WaitCommEvent(int hFile, EventMasks Mask, Overlapped lpOverlap);

        [DllImport("kernel32.dll")]
        private static extern int WaitForSingleObject(int hHandle, int dwMilliseconds);

        [DllImport("kernel32.dll")]
        private static extern int WriteFile(int hFile, byte[] Buffer, int nNumberOfBytesToWrite, int lpNumberOfBytesWritten, Overlapped lpOverlapped);

        #endregion

        #region Methods

        // This function translates an API error code to text.
        private string pErr2Text(int lCode)
        {
            StringBuilder sRtrnCode = new StringBuilder(256);
            int lRet = FormatMessage(0xf1000, 0, lCode, 0, sRtrnCode.ToString(), 256, 0);
            if (lRet > 0)
                return sRtrnCode.ToString();
            else
                return "Error not found.";
        }

        // This subroutine handles overlapped reads.
        private int pHandleOverlappedRead(int Bytes2Read)
        {
            int iReadChars, iRc, iRes, iLastErr;
            iReadChars = 0;
            muOverlapped.hEvent = CreateEvent(0, 1, 0, null);
            if (muOverlapped.hEvent == 0)
                throw new ApplicationException("Error creating event for overlapped read.");
            else
            {
                // Ovellaped reading
                if (!mbWaitOnRead)
                {
                    mabtRxBuf = new byte[Bytes2Read - 1];
                    iRc = ReadFile(mhRS, mabtRxBuf, Bytes2Read, iReadChars, muOverlapped);
                    if (iRc == 0)
                    {
                        iLastErr = GetLastError();
                        if (iLastErr != ERROR_IO_PENDING)
                            throw new ApplicationException("Overlapped Read Error: " + pErr2Text(iLastErr));
                        else
                            DataReceived(this, mabtRxBuf); // Read completed successfully
                    }
                }
            }
            // Wait for operation to be completed
            if (mbWaitOnRead)
            {
                iRes = WaitForSingleObject(muOverlapped.hEvent, miTimeout);
                switch (iRes)
                {
                    case WAIT_OBJECT_0: // Object signaled,operation completed
                        if (GetOverlappedResult(mhRS, muOverlapped, iReadChars, 0) == 0)
                        {
                            // Operation error
                            iLastErr = GetLastError();
                            if (iLastErr == ERROR_IO_INCOMPLETE)
                                throw new ApplicationException("Read operation incomplete");
                            else
                                throw new ApplicationException("Read operation error " + iLastErr.ToString());
                        }
                        else
                        {
                            // Operation completed
                            DataReceived(this, mabtRxBuf);
                        }
                        break;

                    case WAIT_TIMEOUT :
                        throw new ApplicationException("Timout error");

                    default :
                        throw new ApplicationException("Overlapped read error");
                }
            }
            return 0;
        }

        // This function returns an integer specifying the number of bytes ead from the Comm Port. It accepts a parameter specifying the number of desired bytes to read.
        public int Read(int Bytes2Read)
        {
            int iReadChars = 0;
            int iRc;
            // If Bytes2Read not specified uses Buffersize
            if (Bytes2Read == 0) Bytes2Read = miBufferSize;
            if (mhRS == -1)
                throw new ApplicationException("Please initialize and open port before using this method");
            else
            {
                // Get bytes from port
                try
                {
                    // Purge buffers
                    // PurgeComm(mhRS, PURGE_RXCLEAR Or PURGE_TXCLEAR)
                    // Creates an event for overlapped operations
                    if (meMode == Mode.Overlapped)
                        pHandleOverlappedRead(Bytes2Read);
                    else
                    {
                        // Non overlapped mode
                        mabtRxBuf = new byte[Bytes2Read - 1];
                        iRc = ReadFile(mhRS, mabtRxBuf, Bytes2Read, iReadChars, new Overlapped());
                        if (iRc == 0)
                            throw new ApplicationException("ReadFile error " + iRc.ToString());
                        else
                        {
                            mbWaitOnRead = true;
                            return iReadChars;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Read Error: " + ex.Message, ex);
                }
            }
            return iReadChars;
        }

        // This subroutine handles overlapped writes.
        private bool pHandleOverlappedWrite(byte[] Buffer)
        {
            bool bErr = true;
            int iBytesWritten, iRc, iLastErr, iRes;
            iBytesWritten = 0;
            muOverlappedW.hEvent = CreateEvent(-1, 1, 0, null);
            if (muOverlappedW.hEvent == 0)
                throw new ApplicationException("Error creating event for overlapped write.");
            else
            {
                PurgeComm(mhRS, ((PURGE_RXCLEAR == 1) || (PURGE_TXCLEAR == 1) ? 1 : 0));
                mbWaitOnRead = true;
                iRc = WriteFile(mhRS, Buffer, Buffer.Length, iBytesWritten, muOverlappedW);
            }
            return bErr;
        }

        // This subroutine writes the passed array of bytes to the Comm Port to be written.
        public void Write(byte[] Buffer)
        {
            int iBytesWritten, iRc;
            if (mhRS == -1)
                throw new ApplicationException("Please initialize and open port before using this method");
            else
            {
                // Transmit data to COM Port
                try
                {
                    if (meMode == Mode.Overlapped)
                    {
                        //if (phandler
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message, ex);
                }
            }
        }

        // This subroutine invokes a thread to perform an asynchronous read.
        // This routine should not be called directly, but is used by the class.
        public void _R()
        {
            int iRet = Read(miTmpBytes2Read);
        }

        #endregion

        /*

#Region "Methods"
        
    ' This subroutine handles overlapped writes.
    Private Function pHandleOverlappedWrite(ByVal Buffer() As Byte) As Boolean
        Dim iBytesWritten, iRc, iLastErr, iRes As Integer, bErr As Boolean
        muOverlappedW.hEvent = CreateEvent(Nothing, 1, 0, Nothing)
        If muOverlappedW.hEvent = 0 Then
            ' Can't create event
            Throw New ApplicationException( _
                "")
        Else
            ' Overllaped write
            PurgeComm(mhRS, PURGE_RXCLEAR Or PURGE_TXCLEAR)
            mbWaitOnRead = True
            iRc = WriteFile(mhRS, Buffer, Buffer.Length, _
                iBytesWritten, muOverlappedW)
            If iRc = 0 Then
                iLastErr = GetLastError()
                If iLastErr <> ERROR_IO_PENDING Then
                    Throw New ArgumentException("Overlapped Read Error: " & _
                        pErr2Text(iLastErr))
                Else
                    ' Write is pending
                    iRes = WaitForSingleObject(muOverlappedW.hEvent, INFINITE)
                    Select Case iRes
                        Case WAIT_OBJECT_0
                            ' Object signaled,operation completed
                            If GetOverlappedResult(mhRS, muOverlappedW, _
                                iBytesWritten, 0) = 0 Then

                                bErr = True
                            Else
                                ' Notifies Async tx completion,stops thread
                                mbWaitOnRead = False
                                RaiseEvent TxCompleted(Me)
                            End If
                    End Select
                End If
            Else
                ' Wait operation completed immediatly
                bErr = False
            End If
        End If
        CloseHandle(muOverlappedW.hEvent)
        Return bErr
    End Function

    ' This subroutine writes the passed array of bytes to the 
    '   Comm Port to be written.
    Public Overloads Sub Write(ByVal Buffer As Byte())
        Dim iBytesWritten, iRc As Integer

        If mhRS = -1 Then
            Throw New ApplicationException( _
                "Please initialize and open port before using this method")
        Else
            ' Transmit data to COM Port
            Try
                If meMode = Mode.Overlapped Then
                    ' Overlapped write
                    If pHandleOverlappedWrite(Buffer) Then
                        Throw New ApplicationException( _
                            "Error in overllapped write")
                    End If
                Else
                    ' Clears IO buffers
                    PurgeComm(mhRS, PURGE_RXCLEAR Or PURGE_TXCLEAR)
                    iRc = WriteFile(mhRS, Buffer, Buffer.Length, _
                        iBytesWritten, Nothing)
                    If iRc = 0 Then
                        Throw New ApplicationException( _
                            "Write Error - Bytes Written " & _
                            iBytesWritten.ToString & " of " & _
                            Buffer.Length.ToString)
                    End If
                End If
            Catch Ex As Exception
                Throw
            End Try
        End If
    End Sub

    ' This subroutine writes the passed string to the 
    '   Comm Port to be written.
    Public Overloads Sub Write(ByVal Buffer As String)
        Dim oEncoder As New System.Text.ASCIIEncoding()
        Dim aByte() As Byte = oEncoder.GetBytes(Buffer)
        Me.Write(aByte)
    End Sub

    ' This subroutine invokes a thread to perform an asynchronous write.
    '   This routine should not be called directly, but is used
    '   by the class.
    Public Sub _W()
        Write(mabtTmpTxBuf)
    End Sub

    ' This subroutine uses another thread to read from the Comm Port. It 
    '   raises RxCompleted when done. It reads an integer.
    Public Overloads Sub AsyncRead(ByVal Bytes2Read As Integer)
        If meMode <> Mode.Overlapped Then Throw New ApplicationException( _
            "Async Methods allowed only when WorkingMode=Overlapped")
        miTmpBytes2Read = Bytes2Read
        moThreadTx = New Thread(AddressOf _R)
        moThreadTx.Start()
    End Sub

    ' This subroutine uses another thread to write to the Comm Port. It 
    '   raises TxCompleted when done. It writes an array of bytes.
    Public Overloads Sub AsyncWrite(ByVal Buffer() As Byte)
        If meMode <> Mode.Overlapped Then Throw New ApplicationException( _
            "Async Methods allowed only when WorkingMode=Overlapped")
        If mbWaitOnWrite = True Then Throw New ApplicationException( _
            "Unable to send message because of pending transmission.")
        mabtTmpTxBuf = Buffer
        moThreadTx = New Thread(AddressOf _W)
        moThreadTx.Start()
    End Sub

    ' This subroutine uses another thread to write to the Comm Port. It 
    '   raises TxCompleted when done. It writes a string.
    Public Overloads Sub AsyncWrite(ByVal Buffer As String)
        Dim oEncoder As New System.Text.ASCIIEncoding()
        Dim aByte() As Byte = oEncoder.GetBytes(Buffer)
        Me.AsyncWrite(aByte)
    End Sub

    ' This function takes the ModemStatusBits and returns a boolean value
    '   signifying whether the Modem is active.
    Public Function CheckLineStatus(ByVal Line As ModemStatusBits) As Boolean
        Return Convert.ToBoolean(ModemStatus And Line)
    End Function

    ' This subroutine clears the input buffer.
    Public Sub ClearInputBuffer()
        If Not mhRS = -1 Then
            PurgeComm(mhRS, PURGE_RXCLEAR)
        End If
    End Sub

    ' This subroutine closes the Comm Port.
    Public Sub Close()
        If mhRS <> -1 Then
            CloseHandle(mhRS)
            mhRS = -1
        End If
    End Sub

    ' This subroutine opens and initializes the Comm Port
    Public Overloads Sub Open()
        ' Get Dcb block,Update with current data
        Dim uDcb As DCB, iRc As Integer
        ' Set working mode
        Dim iMode As Integer = Convert.ToInt32(IIf(meMode = Mode.Overlapped, _
            FILE_FLAG_OVERLAPPED, 0))
        ' Initializes Com Port
        If miPort > 0 Then
            Try
                ' Creates a COM Port stream handle 
                mhRS = CreateFile("COM" & miPort.ToString, _
                GENERIC_READ Or GENERIC_WRITE, 0, 0, _
                OPEN_EXISTING, iMode, 0)
                If mhRS <> -1 Then
                    ' Clear all comunication errors
                    Dim lpErrCode As Integer
                    iRc = ClearCommError(mhRS, lpErrCode, 0&)
                    ' Clears I/O buffers
                    iRc = PurgeComm(mhRS, PurgeBuffers.RXClear Or _
                        PurgeBuffers.TxClear)
                    ' Gets COM Settings
                    iRc = GetCommState(mhRS, uDcb)
                    ' Updates COM Settings
                    Dim sParity As String = "NOEM"
                    sParity = sParity.Substring(meParity, 1)
                    ' Set DCB State
                    Dim sDCBState As String = String.Format( _
                        "baud={0} parity={1} data={2} stop={3}", _
                        miBaudRate, sParity, miDataBit, CInt(meStopBit))
                    iRc = BuildCommDCB(sDCBState, uDcb)
                    iRc = SetCommState(mhRS, uDcb)
                    If iRc = 0 Then
                        Dim sErrTxt As String = pErr2Text(GetLastError())
                        Throw New CIOChannelException( _
                            "Unable to set COM state0" & sErrTxt)
                    End If
                    ' Setup Buffers (Rx,Tx)
                    iRc = SetupComm(mhRS, miBufferSize, miBufferSize)
                    ' Set Timeouts
                    pSetTimeout()
                Else
                    ' Raise Initialization problems
                    Throw New CIOChannelException( _
                        "Unable to open COM" & miPort.ToString)
                End If
            Catch Ex As Exception
                ' Generica error
                Throw New CIOChannelException(Ex.Message, Ex)
            End Try
        Else
            ' Port not defined, cannot open
            Throw New ApplicationException("COM Port not defined, " + _
                "use Port property to set it before invoking InitPort")
        End If
    End Sub

    ' This subroutine opens and initializes the Comm Port (overloaded
    '   to support parameters).
    Public Overloads Sub Open(ByVal Port As Integer, _
        ByVal BaudRate As Integer, ByVal DataBit As Integer, _
        ByVal Parity As DataParity, ByVal StopBit As DataStopBit, _
        ByVal BufferSize As Integer)

        Me.Port = Port
        Me.BaudRate = BaudRate
        Me.DataBit = DataBit
        Me.Parity = Parity
        Me.StopBit = StopBit
        Me.BufferSize = BufferSize
        Open()
    End Sub

    ' This subroutine sets the Comm Port timeouts.
    Private Sub pSetTimeout()
        Dim uCtm As COMMTIMEOUTS
        ' Set ComTimeout
        If mhRS = -1 Then
            Exit Sub
        Else
            ' Changes setup on the fly
            With uCtm
                .ReadIntervalTimeout = 0
                .ReadTotalTimeoutMultiplier = 0
                .ReadTotalTimeoutConstant = miTimeout
                .WriteTotalTimeoutMultiplier = 10
                .WriteTotalTimeoutConstant = 100
            End With
            SetCommTimeouts(mhRS, uCtm)
        End If
    End Sub

#End Region
        */
    }
}
