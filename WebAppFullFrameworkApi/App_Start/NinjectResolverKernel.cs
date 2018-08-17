using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using WebAppFullFrameworkApi.Models;

namespace WebAppFullFrameworkApi
{
    public static class NinjectResolverKernel
    {
        public static IKernel Load()
        {
            IKernel kernel = new StandardKernel();
            SetAuthContext(kernel);
            SetGlobal(kernel);
            return kernel;
        }

        private static void SetAuthContext(IKernel kernel)
        {            
            kernel.Bind(typeof(AuthContext)).ToSelf().InThreadScope();
            kernel.Bind(typeof(IUserStore<IdentityUser>)).To(typeof(UserStore<IdentityUser>)).InThreadScope();
            kernel.Bind(typeof(UserManager<IdentityUser>)).ToSelf().InThreadScope();
        }

        private static void SetGlobal(IKernel kernel)
        {
            kernel.Bind<IAuthRepository>().To<AuthRepository>().InThreadScope();
            kernel.Bind<DatabaseContext>().ToSelf().InThreadScope();
        }
    }
}