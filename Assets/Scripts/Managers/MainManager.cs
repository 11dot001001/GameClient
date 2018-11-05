﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Managers
{
    public static class MainManager
    {
        private static MainFormController _mainForm;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainManager"/> class.
        /// </summary>
        public static void Initialize(MainFormController mainFormController) => _mainForm = mainFormController;

        public static void SetActivePanel(MainFormPanel mainFormPanel) => _mainForm.ActivePanel = mainFormPanel;

        public static void TryToConnect() => Main.Start();
    }
}
