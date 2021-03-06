﻿using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DocumentServices.Interceptors;
using DocumentServices.Repository;

namespace DocumentServices.Installers
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(Classes.FromThisAssembly().Where(type => typeof (IInterceptor).IsAssignableFrom(type)).LifestyleTransient());
            container.Register(
                Classes.FromThisAssembly()
                       .Where(type => typeof (IRepository).IsAssignableFrom(type))
                       .WithServiceDefaultInterfaces()
                       .Configure(c => c.LifestyleTransient().Interceptors<AspectInterceptor>()));
        }
    }
}