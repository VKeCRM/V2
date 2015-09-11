//-----------------------------------------------------------------------
// <copyright file="DesignByContract.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace VKeCRM.Common.Utility
{
    /// <summary>
    /// <para>
    /// Design By Contract Checks.
    /// Each method generates an exception or
    /// a trace assertion statement if the contract is broken.
    /// </para>
    /// </summary>
    /// <remarks>
    /// This example shows how to call the Require method.
    /// <code>
    /// public void Test(int x)
    /// {
    /// 	try
    /// 	{
    ///			Check.Require(x > 1, "x must be > 1");
    ///		}
    ///		catch (System.Exception ex)
    ///		{
    ///			Console.WriteLine(ex.ToString());
    ///		}
    ///	}
    /// </code>
    /// You can direct output to a Trace listener. For example, you could insert
    /// <code>
    /// Trace.Listeners.Clear();
    /// Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
    /// </code>
    /// or direct output to a file or the Event Log.
    /// (Note: For ASP.NET clients use the Listeners collection
    /// of the Debug, not the Trace, object and, for a Release build, only exception-handling
    /// is possible.)
    /// </remarks>
    public sealed class Check
    {
        #region Interface

        /// <summary>
        /// Precondition check - should run regardless of preprocessor directives.
        /// </summary>
        /// <param name="assertion">A boolean value to indicate assertion settings</param>
        /// <param name="message">Exception message</param>
        public static void Require(bool assertion, string message)
        {
            if (UseExceptions)
            {
                if (!assertion)
                {
                    throw new PreconditionException(message);
                }
            }
            else
            {
                Assert(assertion, "Precondition: " + message);
            }
        }

        /// <summary>
        /// Precondition check - should run regardless of preprocessor directives.
        /// </summary>
        /// <param name="assertion">A boolean value to indicate assertion settings</param>
        /// <param name="message">Exception message</param>
        /// <param name="inner">Inner exception</param>
        public static void Require(bool assertion, string message, Exception inner)
        {
            if (UseExceptions)
            {
                if (!assertion)
                {
                    throw new PreconditionException(message, inner);
                }
            }
            else
            {
                TraceAssert(assertion, "Precondition: " + message);
            }
        }

        /// <summary>
        /// Precondition check - should run regardless of preprocessor directives.
        /// </summary>
        /// <param name="assertion">A boolean value to indicate assertion settings</param>
        public static void Require(bool assertion)
        {
            if (UseExceptions)
            {
                if (!assertion)
                {
                    throw new PreconditionException("Precondition failed.");
                }
            }
            else
            {
                TraceAssert(assertion, "Precondition failed.");
            }
        }

        /// <summary>
        /// Postcondition check.
        /// </summary>
        /// <param name="assertion">A boolean value to indicate assertion settings</param>
        /// <param name="message">Exception message</param>
        public static void Ensure(bool assertion, string message)
        {
            if (UseExceptions)
            {
                if (!assertion)
                {
                    throw new PostconditionException(message);
                }
            }
            else
            {
                TraceAssert(assertion, "Postcondition: " + message);
            }
        }

		/// <summary>
		/// Postcondition check.
		/// </summary>
		public static void Ensure(bool assertion, Exception inner)
		{
			if (UseExceptions)
			{
				if (!assertion)
				{
					throw inner;
				}
			}
			else
			{
				TraceAssert(assertion, "Postcondition: " + inner.Message);
			}
		}

		/// <summary>
		/// Postcondition check.
		/// </summary>
		public static void Ensure(bool assertion, Type type, params object[] p)
		{
			if (UseExceptions)
			{
				if (!assertion)
				{
					throw (Exception)Activator.CreateInstance(type, p);
				}
			}
			else
			{
				TraceAssert(assertion, "Postcondition: " + type.GetType().ToString());
			}
		}

		/// <summary>
        /// Postcondition check.
        /// </summary>
        public static void Ensure(bool assertion, string message, Exception inner)
        {
            if (UseExceptions)
            {
                if (!assertion)
                {
                    throw new PostconditionException(message, inner);
                }
            }
            else
            {
                TraceAssert(assertion, "Postcondition: " + message);
            }
        }

        /// <summary>
        /// Postcondition check.
        /// </summary>
        public static void Ensure(bool assertion)
        {
            if (UseExceptions)
            {
                if (!assertion)
                {
                    throw new PostconditionException("Postcondition failed.");
                }
            }
            else
            {
                TraceAssert(assertion, "Postcondition failed.");
            }
        }

        /// <summary>
        /// Invariant check.
        /// </summary>
        public static void Invariant(bool assertion, string message)
        {
            if (UseExceptions)
            {
                if (!assertion)
                {
                    throw new InvariantException(message);
                }
            }
            else
            {
                TraceAssert(assertion, "Invariant: " + message);
            }
        }

        /// <summary>
        /// Invariant check.
        /// </summary>
        public static void Invariant(bool assertion, string message, Exception inner)
        {
            if (UseExceptions)
            {
                if (!assertion)
                {
                    throw new InvariantException(message, inner);
                }
            }
            else
            {
                TraceAssert(assertion, "Invariant: " + message);
            }
        }

        /// <summary>
        /// Invariant check.
        /// </summary>
        public static void Invariant(bool assertion)
        {
            if (UseExceptions)
            {
                if (!assertion)
                {
                    throw new InvariantException("Invariant failed.");
                }
            }
            else
            {
                TraceAssert(assertion, "Invariant failed.");
            }
        }

        /// <summary>
        /// Assertion check.
        /// </summary>
        public static void Assert(bool assertion, string message)
        {
            if (UseExceptions)
            {
                if (!assertion)
                {
                    throw new AssertionException(message);
                }
            }
            else
            {
                TraceAssert(assertion, "Assertion: " + message);
            }
        }

        /// <summary>
        /// Assertion check.
        /// </summary>
        public static void Assert(bool assertion, string message, Exception inner)
        {
            if (UseExceptions)
            {
                if (!assertion)
                {
                    throw new AssertionException(message, inner);
                }
            }
            else
            {
                TraceAssert(assertion, "Assertion: " + message);
            }
        }

        /// <summary>
        /// Assertion check.
        /// </summary>
        public static void Assert(bool assertion)
        {
            if (UseExceptions)
            {
                if (!assertion)
                {
                    throw new AssertionException("Assertion failed.");
                }
            }
            else
            {
                TraceAssert(assertion, "Assertion failed.");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether you wish to use Trace Assert statements instead of exception handling. 
        /// (The Check class uses exception handling by default.)
        /// </summary>
        public static bool UseAssertions
        {
            get
            {
                return useAssertions;
            }
            set
            {
                useAssertions = value;
            }
        }

        #endregion // Interface

        #region Implementation

        /// <summary>
        /// Prevents a default instance of the Check class from being created
        /// </summary>
        private Check() 
        { 
        }

        /// <summary>
        /// Gets a value indicating whether exception handling is being used?
        /// </summary>
        private static bool UseExceptions
        {
            get
            {
                return !useAssertions;
            }
        }

        /// <summary>
        /// Are trace assertion statements being used? Default is to use exception handling.
        /// </summary>
        private static bool useAssertions = false;

        #endregion // Implementation

        #region Private help methods
        private static void TraceAssert(bool assertion, string message)
        {
            System.Diagnostics.Trace.Assert(assertion, message);
        }
        #endregion
    } // End Check

    #region Exceptions

    /// <summary>
    /// <para>
    /// Exception raised when a contract is broken.
    /// Catch this exception type if you wish to differentiate between 
    /// any DesignByContract exception and other runtime exceptions.
    /// </para>
    /// </summary>
    public class DesignByContractException : ApplicationException
    {
        /// <summary>
        /// Prevents a default instance of the DesignByContractException class from being created
        /// </summary>
        protected DesignByContractException() 
        { 
        }

        /// <summary>
        /// Prevents a default instance of the DesignByContractException class from being created
        /// </summary>
        /// <param name="message">Exception message</param>
        protected DesignByContractException(string message) : base(message) 
        { 
        }

        /// <summary>
        /// Prevents a default instance of the DesignByContractException class from being created
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="inner">Inner exception</param>
        protected DesignByContractException(string message, Exception inner) : base(message, inner) 
        { 
        }
    }

    /// <summary>
    /// Exception raised when a precondition fails.
    /// </summary>
    public class PreconditionException : DesignByContractException
    {
        /// <summary>
        /// Initializes a new instance of the PreconditionException class.
        /// </summary>
        public PreconditionException() 
        {
        }

        /// <summary>
        /// Initializes a new instance of the PreconditionException class.
        /// </summary>
        /// <param name="message">Exception message</param>
        public PreconditionException(string message) : base(message) 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the PreconditionException class.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="inner">Inner exception</param>
        public PreconditionException(string message, Exception inner) : base(message, inner) 
        { 
        }
    }

    /// <summary>
    /// Exception raised when a postcondition fails.
    /// </summary>
    public class PostconditionException : DesignByContractException
    {
        /// <summary>
        /// Initializes a new instance of the PostconditionException class.
        /// </summary>
        public PostconditionException() 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the PostconditionException class.
        /// </summary>
        /// <param name="message">Exception message</param>
        public PostconditionException(string message) : base(message) 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the PostconditionException class.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="inner">Inner exception</param>
        public PostconditionException(string message, Exception inner) : base(message, inner) 
        { 
        }
    }

    /// <summary>
    /// Exception raised when an invariant fails.
    /// </summary>
    public class InvariantException : DesignByContractException
    {
        /// <summary>
        /// Initializes a new instance of the InvariantException class.
        /// </summary>
        public InvariantException() 
        { 
        }
        /// <summary>
        /// Initializes a new instance of the InvariantException class.
        /// </summary>
        public InvariantException(string message) : base(message) 
        { 
        }
        /// <summary>
        /// Initializes a new instance of the InvariantException class.
        /// </summary>
        public InvariantException(string message, Exception inner) : base(message, inner) 
        { 
        }
    }

    /// <summary>
    /// Exception raised when an assertion fails.
    /// </summary>
    public class AssertionException : DesignByContractException
    {
        /// <summary>
        /// Initializes a new instance of the AssertionException class.
        /// </summary>
        public AssertionException() 
        {
        }
        /// <summary>
        /// Initializes a new instance of the AssertionException class.
        /// </summary>
        public AssertionException(string message) : base(message) 
        {
        }
        /// <summary>
        /// Initializes a new instance of the AssertionException class.
        /// </summary>
        public AssertionException(string message, Exception inner) : base(message, inner) 
        { 
        }
    }

    #endregion // Exception classes
}
