using Assets.Scripts.Forms_Controller.Message_Box_Form;
using Assets.Scripts.Forms_Controller.Startup_Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Authentication : MonoBehaviour
{
    public StartupFormController startupForm;

    private void Start()
    {
        AuthenticationManager.Initialize(startupForm);
    }
}
