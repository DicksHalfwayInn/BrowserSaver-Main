using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFAppBSUI.ViewModels
{
    public class ShellTabbedViewModel : Conductor<IScreen>.Collection.OneActive
    {
        int count = 1;

        public void OpenTab()
        {
            ActivateItem(new TabTypeTestViewModel { DisplayName = "Tabjhkjhgjhgkjhgkkjhg " + count++ });
        }

        public void CloseItem(object dataContext)
        {
            ScreenExtensions.CloseItem(this, dataContext);
            
        }
    }
}
