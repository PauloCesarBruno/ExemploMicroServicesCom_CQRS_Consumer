using Application.Commands;
using Application.Profiles;
using Azul.Framework.Context;
using Azul.Framework.Events.Extensions;
using Azul.Framework.IoC;
using Domain.Repositories;
using Infrastructure.Data.Profiles;
using Infrastructure.Data.Repositories;
using Infrastructure.Subscribers.Profiles;
using Infrastructure.Subscribers.Subscribers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.Reflection;

namespace Infrastructure.IoC
{
    /// <summary>
    /// Representa a classe de configuração da injeção de dependências da aplicação.
    /// </summary>
    public sealed class Bootstrapper : BaseDependencyInjectionBootstrap
    {

        #region Override
        /// <summary>
        /// Realiza a injeção de todas as dependências necessárias para a aplicação
        /// </summary>
        /// <param name="container"><see cref="Container"/> representando a instância do container contendo as dependências injetadas.</param>
        public Bootstrapper(IServiceCollection services)
        {
            var container = new Container();

            container.Options.DefaultLifestyle = Lifestyle.Scoped;
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSimpleInjector(container);

            Inject(container);

            container.Register<IHttpContextAccessor, HttpContextAccessor>(Lifestyle.Singleton);
            container.RegisterSingleton<IContextFactory>(() => new ContextFactory(container));
            container.Register<IContext, ScopeContext>(Lifestyle.Scoped);

            var mapperAssemblies = new Assembly[]
            {
                  typeof(PartNumberSubscriberProfiles).Assembly,
                  typeof(PartNumberCommandProfiles).Assembly,
                  typeof(PartNumberEntityProfile).Assembly,
                  typeof(PartNumberQuantitySubscriberProfiles).Assembly,
                  typeof(PartNumberQuantityCommandProfiles).Assembly,
                  typeof(PartNumberQuantityEntityProfile).Assembly
            };

            var mediatorAssemblies = new Assembly[]
            {
                  typeof(PartNumberCommandHandler).Assembly,
                  typeof(PartNumberQuantityCommandHandler).Assembly
            };

            InjectAutoMapper(container, mapperAssemblies);
            InjectMediator(container, mediatorAssemblies);

            InjectSubscribers(container);
            InjectRepositories(container);

            services.BuildServiceProvider(true).UseSimpleInjector(container);
            container.Verify();

            container.StartPublishersAndSubscribers();
        }
        #endregion

        #region Methods
        private void InjectSubscribers(Container container)
        {
            container.RegisterSubscribers(typeof(PartNumberSubscriber).Assembly);
        }

        private void InjectRepositories(Container container)
        {
            //container.RegisterInstance<IPartNumberRepository>(new PartNumberRepository(tableName: "PartNumber", connectionId: "Uniformes"));
            //container.RegisterInstance<IPartNumberQuantityRepository>(new PartNumberQuantityRepository(tableName: "PartNumberQuantity", connectionId: "Uniformes"));
            container.Register<IPartNumberRepository, PartNumberRepository>(Lifestyle.Scoped);
            container.Register<IPartNumberQuantityRepository, PartNumberQuantityRepository>(Lifestyle.Scoped);
        }
        #endregion
    }
}
