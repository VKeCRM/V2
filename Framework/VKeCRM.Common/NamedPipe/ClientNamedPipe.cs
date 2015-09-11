using System;
using System.Threading;
using System.IO.Pipes;
using VKeCRM.Common.Logging;

namespace VKeCRM.Common.NamedPipe
{
	public class ClientNamedPipe : NamedPipeBase<NamedPipeClientStream>, IDisposable
	{
		#region Fields
		private const int OneSecond = 1000;
		private const int connectionTimeout = 60 * OneSecond; // 1 minute
		private const int sleepingTime = OneSecond;
		#endregion

		#region Constructor
		public ClientNamedPipe(string pipeName)
			: base(pipeName)
        {
		}

		public ClientNamedPipe(string pipeName, Logger logger)
			: base(pipeName, logger)
		{

		}
		#endregion

		#region Properties
		protected override bool AutoFlushPipeWriter
		{
			get { return true; }
		}
		#endregion		

		#region Methods
		protected override NamedPipeClientStream CreatePipe()
		{
			NamedPipeClientStream stream = new NamedPipeClientStream(".",
																	 PipeName,
																	 PipeDirection.InOut,
																	 PipeOptions.Asynchronous);
			stream.Connect(connectionTimeout);
			stream.ReadMode = PipeTransmissionMode.Message;
			return stream;
		}

		/// <summary>
		/// Method that runs on the ThreadPool for reading messages from the pipe.
		/// </summary>
		/// <param name="state"></param>
		protected override void ReadFromPipe()
        {			
			int timeElapsed = 0;
			System.Threading.Thread messageReader = new System.Threading.Thread(new System.Threading.ThreadStart(ReadMessage));
			messageReader.Name = "MessageReader";

			try
			{
				while (Pipe != null && IsListening == true)
				{
					if (Pipe.IsConnected == true)
					{
						// reset time elapsed
						timeElapsed = 0;

						// sleep for 100 milliseconds to wait for message to be read from the pipe
						System.Threading.Thread.Sleep(100);

						if (messageReader.ThreadState == System.Threading.ThreadState.WaitSleepJoin && !string.IsNullOrEmpty(Message))
						{
							InvokeOnReceivedMessage(Message);
							ClearMessage();
						}
						else if (messageReader.ThreadState == System.Threading.ThreadState.Unstarted)
						{
							messageReader.Start();
						}
						else if (messageReader.ThreadState == System.Threading.ThreadState.Stopped ||
							 messageReader.ThreadState == System.Threading.ThreadState.Aborted)
						{
							messageReader = new System.Threading.Thread(new System.Threading.ThreadStart(ReadMessage));
						}
					}
					else
					{
						Thread.Sleep(sleepingTime);
						timeElapsed += sleepingTime;
						if (timeElapsed >= connectionTimeout)
						{
							TimeoutException timeoutException = new TimeoutException("Client Name Pipe Connection Timeout.");
							IsListening = false;
							_pipeReadExceptions.Add(timeoutException);
							LogError(timeoutException.ToString());
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				IsListening = false;
				_pipeReadExceptions.Add(ex);
				LogError(ex.ToString());
			}
		}

		/// <summary>
		/// Writes message to pipe
		/// </summary>
		/// <param name="message">The message to be written to the pipe.</param>
		public override void WriteToPipe(string message)
		{
			try
			{
				// wait for connection if client is not yet connected
				if (!Pipe.IsConnected)
				{
					LogMessage("The client named pipe is not connected and is trying to reconnect for one minute.");
					Pipe.Connect(connectionTimeout);
				}
				// wait for pipe to be read
				Pipe.WaitForPipeDrain();
				// write message to the pipe
				WriteMessage(message);
				// wait for pipe to be read
				Pipe.WaitForPipeDrain();
			}
			catch (Exception ex)
			{
				IsListening = false;
				LogError(ex.ToString());
				throw ex;
			}
		}

		/// <summary>
		/// Dispose underlying resources
		/// </summary>
		public void Dispose()
		{
			Pipe.Close();
			Pipe.Dispose();
		}
		#endregion
	}
}
