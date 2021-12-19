using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WMI
{
    public class StartingRoomScript : InteractableObject
    {
        private BoxCollider2D trigger;
        [SerializeField] private List<GameObject> doors = new List<GameObject>();

        private void Start()
        {
            trigger = this.GetComponent<BoxCollider2D>();
        }

        public override void PerformAction()
        {
            foreach (GameObject door in doors)
            {
                door.gameObject.GetComponent<DoorScript>().OpenTheDoor();
            }
            this.DisableInteractionForAll();
            trigger.enabled = false;
        }

    }

}
