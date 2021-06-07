using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WMI
{
    public class ActivateForm : InteractableObject
    {
        [SerializeField] private GameObject form;

        public override void PerformAction()
        {
            form.SetActive(true);
        }

    }
}