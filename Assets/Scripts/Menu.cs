using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Menu : MonoBehaviour
    {
        public MenuFormController menuFormPrefab;

        public void Start()
        {
            MenuManager.Initialize(menuFormPrefab);
        }
    }
}
