using System;
using System.Text;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Collections.Generic;
using VKeCRM.Common.Logging;

namespace VKeCRM.Common.NamedPipe
{
	public abstract class NamedPipeBase<T> where T : PipeStream
	{
		#region Private Fields
		/// <summary>
		/// The sream writer to write message to the pipe stream.
		/// </summary>
		private StreamWriter _pipeWriter;
		/// <summary>
		/// The message builder used to build the string read from the pipe.
		/// </summary>
		StringBuilder _messageBuilder = new StringBuilder();
		/// <summary>
		/// The message read from the pipe.
		/// </summary>
		private string _message = string.Empty;
		/// <summary>
		/// The thread that constantly reads from the pipe.
		/// </summary>
		private Thread _pipeReader;
		/// <summary>
		/// Holds list of exceptions occurred during the pipe reader thread reading the pipe.
		/// </summary>
		protected List<Exception> _pipeReadExceptions;
		/// <summary>
		/// Holds the logger instance provided from the caller class for error and debug logging.
		/// </summary>
		private Logger _namedPipeLogger;
		#endregion

		#region Constant
		/// <summary>
		/// The buffer size for inbound and outbound of the pipe stream
		/// </summary>
		protected const int BUFFER_SIZE = 4096;
		#endregion 

		#region Events
		/// <summary>
        /// This event fires when a message is received from the pipe.
        /// </summary>
        public event EventHandler<ReceivedMessageEventArgs> OnReceivedMessage;
		#endregion

		#region Properties
		/// <summary>
		/// A flag indicts whether the pipe is listening
		/// </summary>
		public bool IsListening
		{
			get;
			set;
		}

		/// <summary>
		/// The instance of PipeStream.
		/// </summary>
		protected T Pipe
		{
			get;
			set;
		}

		/// <summary>
		/// The name of the pipe
		/// </summary>
		protected string PipeName
		{
			get;
			private set;
		}

		/// <summary>
		/// The message read from the pipe
		/// </summary>
		protected string Message
		{
			get
			{
				return _message;
			}
		}

		/// <summary>
		/// The thread that constantly reads from the pipe.
		/// </summary>
		protected Thread PipeReader
		{
			get
			{
				return _pipeReader;
			}
		}
		#endregion

		#region Abstract Properties
		/// <summary>
		/// Should calls to write to the stream through PipeWriter automatically call Flush().
		/// </summary>
		protected abstract bool AutoFlushPipeWriter
		{
			get;
		}
		#endregion

		#region Constuctor
		/// <summary>
		/// NamePipeBase constructor
		/// </summary>
		/// <param name="pipeName">the name of pipe to be created</param>
		internal NamedPipeBase(string pipeName) 
			: this(pipeName, null)
		{			
		}

		/// <summary>
		/// NamePipeBase constructor
		/// </summary>
		/// <param name="pipeName">the name of pipe to be created</param>
		/// <param name="logger">The logger instance provided from the caller class for error and debug logging.</param>
		internal NamedPipeBase(string pipeName, Logger logger)
        {
            if (String.IsNullOrEmpty(pipeName))
            {
                throw new ArgumentNullException("pipeName", "Argument, pipeName, cannot be null.");
            }
            PipeName = pipeName;
			_namedPipeLogger = logger;
			_pipeReadExceptions = new List<Exception>();
		}
		#endregion

		#region Public Methods
		/// <summary>
        /// Create the named pipe stream and starts the reader thread.
        /// </summary>
        public void Start()
        {
			try
			{
				Pipe = CreatePipe();
				//ThreadPool.QueueUserWorkItem(new WaitCallback(ReadFromPipe));
				_pipeReader = new Thread(new System.Threading.ThreadStart(ReadFromPipe));
				_pipeReader.Name = "PipeReader";
				_pipeReader.Start();
				IsListening = true;
			}
			catch (Exception ex)
			{
				IsListening = false;
				LogError(ex.ToString());
				throw ex;
			}
        }

        /// <summary>
        /// Requests the reader thread stop and disposes the named pipe.
        /// </summary>
        public void Stop()
        {
            IsListening = false;
            Pipe.Close();
            Pipe.Dispose();
		}

		/// <summary>
		/// Monitors pipe reader thread and restart thread if the thread is suspended or stoped.
		/// </summary>
		public Exception[] MonitorPipeReader()
		{
			Exception[] pipeReadExceptions = new Exception[_pipeReadExceptions.Count];
			_pipeReadExceptions.CopyTo(pipeReadExceptions);

			try
			{
				if (_pipeReader != null)
				{
					if (_pipeReader.ThreadState == ThreadState.Unstarted)
					{
						_pipeReader.Start();
					}
					else if (_pipeReader.ThreadState == ThreadState.Stopped ||
							 _pipeReader.ThreadState == ThreadState.Aborted ||
							 _pipeReader.ThreadState == ThreadState.Suspended)
					{
						_pipeReader = new System.Threading.Thread(new System.Threading.ThreadStart(ReadFromPipe));
					}
				}
				else
				{
					_pipeReader = new System.Threading.Thread(new System.Threading.ThreadStart(ReadFromPipe));
				}

				_pipeReadExceptions.Clear();				
			}
			catch (Exception ex)
			{
				IsListening = false;
				_pipeReadExceptions.Add(ex);
				pipeReadExceptions = new Exception[_pipeReadExceptions.Count];
				_pipeReadExceptions.CopyTo(pipeReadExceptions);
				LogError(ex.ToString());
			}

			return pipeReadExceptions;
		}
		#endregion

		#region Proptected Methods
		/// <summary>
		/// Reads a message from the pipe.
		/// </summary>
		protected void ReadMessage()
		{
			int numberOfBytes = 0;
			Decoder decoder = Encoding.UTF8.GetDecoder();
			byte[] buffer = new byte[BUFFER_SIZE];
			char[] chars = new char[BUFFER_SIZE];

			try
			{
				if (IsListening)
				{
					do
					{
						numberOfBytes = Pipe.Read(buffer, 0, buffer.Length);
						int numberOfChars = decoder.GetCharCount(buffer, 0, numberOfBytes);
						decoder.GetChars(buffer, 0, numberOfBytes, chars, 0, false);
						_messageBuilder.Append(chars, 0, numberOfChars);
						_message = _messageBuilder.ToString();
					} while (numberOfBytes > 0);
				}
			}
			catch (Exception ex)
			{				
				_pipeReadExceptions.Add(ex);
				LogError(ex.ToString());
			}
		}
		
		/// <summary>
		/// Fire the OnReceivedMessage event.
		/// </summary>
		/// <param name="message">The message sent with the event.</param>
		protected void InvokeOnReceivedMessage(string message)
		{
			if (OnReceivedMessage != null)
			{
				OnReceivedMessage(this, new ReceivedMessageEventArgs(message));
			}
		}

		/// <summary>
		/// Clears out the message read from the pipe
		/// </summary>
		protected void ClearMessage()
		{
			_message = string.Empty;
			_messageBuilder.Remove(0, _messageBuilder.Length);
		}

		/// <summary>
		/// Write a message to the pipe if the pipe is connected.
		/// </summary>
		/// <param name="message"></param>
		protected void WriteMessage(string message)
		{
			try
			{
				if (Pipe.IsConnected == true && Pipe.CanWrite == true)
				{
					if (_pipeWriter == null)
					{
						_pipeWriter = new StreamWriter(Pipe);

						_pipeWriter.AutoFlush = AutoFlushPipeWriter;
					}

					// write a message to the pipe's stream.
					_pipeWriter.Write(message);
				}
			}
			catch (Exception ex)
			{
				IsListening = false;
				LogError(ex.ToString());
				throw ex;
			}
		}

		protected void LogError(string errorMessage)
		{
			if (_namedPipeLogger != null)
			{
				_namedPipeLogger.Error(errorMessage);
				_namedPipeLogger.Debug(errorMessage);
			}
		}

		protected void LogMessage(string message)
		{
			if (_namedPipeLogger != null)
			{
				_namedPipeLogger.Debug(message);
			}
		}
		#endregion

		#region Private Method

		#endregion
		
		#region Abstract Methods
		/// <summary>
		/// Creates an instance of T.
		/// </summary>
		/// <returns>an instance of T which inherites from PipeStream.</returns>
		protected abstract T CreatePipe();		

		/// <summary>
		/// Method that runs on the ThreadPool for reading messages from the pipe.
		/// </summary>
		/// <param name="state"></param>
		protected abstract void ReadFromPipe();

		/// <summary>
		/// Writes message to pipe
		/// </summary>
		/// <param name="message">The message to be written to the pipe.</param>
		public abstract void WriteToPipe(string message);
		#endregion
        
	}
}
