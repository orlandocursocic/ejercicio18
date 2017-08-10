using ejercicio18.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Web.Http;
using Unity.WebApi;
using System;
using System.Collections.Generic;
using ejercicio18.Models;

namespace ejercicio18
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.AddNewExtension<Interception>();
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IPersonasService, PersonasService>(
                    new Interceptor<InterfaceInterceptor>(),
                    new InterceptionBehavior<DBInterceptor>());
            container.RegisterType<IPersonasRepository, PersonasRepository>();


            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }

        public class DBInterceptor : IInterceptionBehavior
        {
            public IMethodReturn Invoke(IMethodInvocation input,
              GetNextInterceptionBehaviorDelegate getNext)
            {
                IMethodReturn result;
                if (ApplicationDbContext.applicationDbContext == null)
                {
                    using (var context = new ApplicationDbContext())
                    {
                        ApplicationDbContext.applicationDbContext = context;
                        using (var dbContextTransaction = context.Database.BeginTransaction())
                        {
                            try
                            {

                                result = getNext()(input, getNext);


                                if (result.Exception != null)
                                {
                                    throw result.Exception;
                                }
                                context.SaveChanges();

                                dbContextTransaction.Commit();
                            }
                            catch (Exception e)
                            {
                                dbContextTransaction.Rollback();
                                throw new Exception("He hecho rollback de la transacción", e);
                            }
                        }
                    }
                    ApplicationDbContext.applicationDbContext = null;
                }
                else
                {

                    result = getNext()(input, getNext);


                    if (result.Exception != null)
                    {
                        throw result.Exception;
                    }
                }
                return result;
            }

            public IEnumerable<Type> GetRequiredInterfaces()
            {
                return Type.EmptyTypes;
            }

            public bool WillExecute
            {
                get { return true; }
            }

            private void WriteLog(string message)
            {

            }
        }
    }