using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Cruds.Infra;
using Cruds.Domain;

namespace Crud.UI
{
    internal class NinjectRepos
    {
        public static IKernel _kernel;
        public static void ComponenteModulo(INinjectModule module)
        {
            _kernel = new StandardKernel(module); 
        
        }
        public static T Resolver<T>() {

            return _kernel.Get<T>();
        }
          

    }
       public class ModuloAplicacao : NinjectModule
        {
            public override void Load()
            {
              Bind<IRepositoryCliente>().To<RepoLinqTodb>(); 
                
            }

        }
    



}
