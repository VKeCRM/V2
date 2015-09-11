using System;
using System.IO.Pipes;
using VKeCRM.Common.Logging;

namespace VKeCRM.Common.NamedPipe
{
	public class ServerNamedPipe : NamedPipeBase<NamedPipeServerStream>, IDisposable
	{
		#region Fields
		const int MaxRetries = 3;
		const int OneSecond = 1000;
		private System.Threading.Thread messageReader;
		#endregion

		#region Properties
		protected override bool AutoFlushPipeWriter
		{
			get { return true; }
		}

		public bool IsWaiting
		{
			get; 
			set;
		}
		#endregion

		#region Constructor & Destructor
		public ServerNamedPipe(string pipeName) 
			: base(pipeName)
        {
        }

		public ServerNamedPipe(string pipeName, Logger logger)
			: base(pipeName, logger)
		{
		}

		~ServerNamedPipe()
        {
            if (Pipe != null) Pipe.Dispose();
		}
		#endregion
		
		#region Methods
		/// <summary>
		/// Creates and return an instance of NamedPipeServerStream.
		/// </summary>
		/// <returns>Returns an instance of NamedPipeServerStream</returns>
		protected override NamedPipeServerStream CreatePipe()
        {
            return new NamedPipeServerStream(PipeName, 
                       PipeDirection.InOut, 
                       1, 
                       PipeTransmissionMode.Message, 
                       PipeOptions.Asynchronous, 
                       BUFFER_SIZE, 
                       BUFFER_SIZE);
        }

		/// <summary>
		/// Method that runs on the ThreadPool for reading messages from the pipe.
		/// </summary>
		/// <param name="state"></param>
        protected override void ReadFromPipe()
        {			
			int retryCount = 0;
			messageReader = new System.Threading.Thread(new System.Threading.ThreadStart(ReadMessage));
			messageReader.Name = "MessageReader";
			try
			{
				while (Pipe != null && IsListening == true)
				{
					try
					{
						if (!Pipe.IsConnected)
						{
							Pipe.WaitForConnection();
							retryCount = 0;
						}
					}
					catch (System.IO.IOException ex)
					{
						LogError(string.Format("The named pipe, {0}, is NOT connected and encounter the following error for {1} time while waiting for connection.{2}{3}", PipeName, retryCount+1, Environment.NewLine, ex.ToString()));
						retryCount++;
						if (retryCount <= MaxRetries)
						{
							System.Threading.Thread.Sleep(OneSecond);
							continue;
						}
						else
						{
							throw ex;
						}
					}

					// sleep for 100 milliseconds to wait for message to be read from the pipe
					System.Threading.Thread.Sleep(100);

					if (messageReader.ThreadState == System.Threading.ThreadState.WaitSleepJoin &&
						!string.IsNullOrEmpty(Message))
					{
						InvokeOnReceivedMessage(Message);
						ClearMessage();
					}
					else if (messageReader.ThreadState == System.Threading.ThreadState.Unstarted)
					{
						if (Pipe.IsConnected)
						{
							messageReader.Start();
						}
					}
					else if (messageReader.ThreadState == System.Threading.ThreadState.Stopped ||
							 messageReader.ThreadState == System.Threading.ThreadState.Aborted)
					{
						messageReader = new System.Threading.Thread(new System.Threading.ThreadStart(ReadMessage));
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
			IsWaiting = false;

			try
			{
				// wait for connection if client is not yet connected
				if (!Pipe.IsConnected)
				{
					LogMessage(string.Format("The server named pipe, {0}, is not connected and is waiting for clinet to reconnect.", PipeName));
					//Pipe.WaitForConnection();
					IsWaiting = true;
					Pipe.BeginWaitForConnection(AsyncWaitForConnectionCallBack, message);
				}
				else
				{
					// wait for pipe to be read
					Pipe.WaitForPipeDrain();
					// write message to the pipe
					WriteMessage(message);
					// wait for pipe to be read
					Pipe.WaitForPipeDrain();
				}
			}
			catch (Exception ex)
			{
				IsListening = false;
				LogError(string.Format("\"{0}\":  {1}", PipeName, ex.ToString()));
				throw ex;
			}
		}		

		/// <summary>
		/// Dispose underlying resources
		/// </summary>
		public void Dispose()
		{
			if (PipeReader != null)
			{
				PipeReader.Abort();
			}

			if (messageReader != null)
			{
				messageReader.Abort();
			}			

			if (Pipe != null)
			{
				Pipe.Close();
				Pipe.Dispose();
			}
		}

		private void AsyncWaitForConnectionCallBack(IAsyncResult result)
		{
			try
			{
				
				Pipe.EndWaitForConnection(result);

				// wait for pipe to be read
				Pipe.WaitForPipeDrain();
				// write message to the pipe
				WriteMessage(result.AsyncState as string ?? string.Empty);
				// wait for pipe to be read
				Pipe.WaitForPipeDrain();

				IsWaiting = false;
			}
			catch (Exception ex)
			{
				IsListening = false;
				LogError(string.Format("\"{0}\":  {1}", PipeName, ex.ToString()));
			}
		}
		#endregion
	}
}
