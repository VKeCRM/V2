using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using VKeCRM.Common.Logging;

namespace VKeCRM.Framework.ServiceModel.Exceptions
{
    public class ServiceExceptionAttribute : Attribute, IOperationBehavior, IOperationInvoker
    {
        public ServiceExceptionAttribute()
        {
        }

        public ServiceExceptionAttribute(IOperationInvoker innerOperationInvoker)
        {
            this.innerOperationInvoker = innerOperationInvoker;
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.DispatchOperation dispatchOperation)
        {
            dispatchOperation.Invoker = new ServiceExceptionAttribute(dispatchOperation.Invoker);
        }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {

            var logger = log4net.LogManager.GetLogger("logError");
            try
            {
                return innerOperationInvoker.Invoke(instance, inputs, out outputs);
            }
            catch (Exception ex)
            {
                var msg = string.Format("Instance:{0},inputs:{1}", instance.GetType().Name, inputs);
                logger.Error(msg, ex);
                throw;
            }
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
            return innerOperationInvoker.InvokeEnd(instance, out outputs, result);
        }

        public bool IsSynchronous
        {
            get { return innerOperationInvoker.IsSynchronous; }
        }
        #endregion
    }
}
