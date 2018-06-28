using System.ComponentModel;
using System.ServiceProcess;

namespace ServiceSalesWatcher.ControlClasses
{
    [RunInstaller(true)]
    public partial class InstallerSaleService : System.Configuration.Install.Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;
        public InstallerSaleService()
        {
            InitializeComponent();
            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "ServiceSalesWatcher";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
