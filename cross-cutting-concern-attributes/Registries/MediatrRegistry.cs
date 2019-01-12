using cross_cutting_concern_attributes.Queries;
using cross_cutting_concern_attributes.Queries.Handlers;
using MediatR;
using StructureMap;
using StructureMap.Pipeline;

namespace cross_cutting_concern_attributes.Registries
{
    public class MediatrRegistry : Registry
    {
        public MediatrRegistry()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType(typeof(ValuesArchive)); 
                scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>));
                scanner.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
            });

            For(typeof(IRequestHandler<,>)).DecorateAllWith(typeof(InstrumentationQueryHandlerDecorator<,>));

            For<IMediator>().LifecycleIs<TransientLifecycle>().Use<Mediator>();
            For<ServiceFactory>().Use<ServiceFactory>(ctx => ctx.GetInstance);
        }
    }
}