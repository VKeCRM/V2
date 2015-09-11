using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using VKeCRM.Framework.Data;

namespace VKeCRM.Framework.ServiceModel
{
	public class NHibernateTransaction : Attribute, IOperationBehavior, IOperationInvoker
	{
		public NHibernateTransaction()
		{
		}

		public NHibernateTransaction(IOperationInvoker innerOperationInvoker)
		{
			this.innerOperationInvoker = innerOperationInvoker;
		}

		public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
			dispatchOperation.Invoker = new NHibernateTransaction(dispatchOperation.Invoker);
		}

		public object Invoke(object instance, object[] inputs, out object[] outputs)
		{
			//Begin the transaction.
			NHibernateSessionManager.Instance.BeginTransaction();

			return innerOperationInvoker.Invoke(instance, inputs, out outputs);
		}

		
		
		#region default empty method for IOperationBehavior
		public void Validate(OperationDescription operationDescription)
		{
			
		}

		public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
			
		}

		public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{

		}
		#endregion

		

		#region Simply delegate to innerOperationInvoker for IOperationInvoker
		IOperationInvoker innerOperationInvoker;

		public object[] AllocateInputs()
		{
			return innerOperationInvoker.AllocateInputs();
		}

		public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
		{
			return innerOperationInvoker.InvokeBegin(instance, inputs, callback, state);
		}

		public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
		{
			return innerOperationInvoker.InvokeEnd(instance,out outputs, result);
		}

		public bool IsSynchronous
		{
			get { return innerOperationInvoker.IsSynchronous; }
		}
		#endregion
	}
}
