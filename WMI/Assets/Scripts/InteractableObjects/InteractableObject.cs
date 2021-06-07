using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


namespace WMI
{
    public class InteractableObject : MonoBehaviourPun
    {
        protected bool enabledState = true;

        public virtual void PerformAction() { }

        public void EnableInteraction()
        {
            enabledState = true;
        }

        public void DisableInteraction()
        {
            enabledState = false;
        }

        public bool IsEnabled()
        {
            return enabledState;
        }

    }
}

