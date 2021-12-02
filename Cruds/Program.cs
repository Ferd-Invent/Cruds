using Crud;
using Crud.UI;
using Cruds.Infra;
using LinqToDB.Data;

namespace Cruds
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            NinjectRepos.ComponenteModulo(new ModuloAplicacao());
            ApplicationConfiguration.Initialize();
            DataConnection.DefaultSettings = new MySettings();

            Application.Run(NinjectRepos.Resolver<FormMain>());
            
        }
    }
}