using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace cross_cutting_concern_attributes.Queries.Handlers
{
    public class InstrumentationQueryHandlerDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        private readonly IRequestHandler<TRequest, TResponse> _innerHandler;
        private readonly bool _canLog;
        private readonly ILogger<InstrumentationQueryHandlerDecorator<TRequest, TResponse>> _logger;

        public InstrumentationQueryHandlerDecorator(IRequestHandler<TRequest, TResponse> innerHandler, 
            ILogger<InstrumentationQueryHandlerDecorator<TRequest, TResponse>> logger)
        {
            _innerHandler = innerHandler ?? throw new ArgumentNullException(nameof(innerHandler));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var handlerType = _innerHandler.GetType();

            var handlerTypeInfo = handlerType.GetTypeInfo();
            var customAttrib = handlerTypeInfo.GetCustomAttribute<InstrumentationAttribute>();
            _canLog = (null != customAttrib);
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if(null == request)
                throw new ArgumentNullException(nameof(request));

            Stopwatch sw = null;
            if (_canLog)
            {
                var msg = $"execution of {_innerHandler.GetType()} started";
                _logger.LogInformation(msg, request);

                sw = new Stopwatch();
                sw.Start();
            }

            var result = await _innerHandler.Handle(request, cancellationToken);

            if (_canLog)
            {
                sw.Stop();
                var msg = $"execution of {_innerHandler.GetType()} completed, took {sw.Elapsed}";
                _logger.LogInformation(msg, request);
            }

            return result;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class InstrumentationAttribute : Attribute
    {
    }
}