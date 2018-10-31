using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Managers
{
    public static class MainManager
    {
        private static MainFormController mainForm;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainManager"/> class.
        /// </summary>
        public static void Initialize(MainFormController mainFormController)
        {
            mainForm = mainFormController;
        }

        public static void SetActivePanel(MainFormPanel mainFormPanel)
        {
            mainForm.ActivePanel = mainFormPanel;
        }

        public static void TryToConnect()
        {
            Main.Start();
        }
    }
}
