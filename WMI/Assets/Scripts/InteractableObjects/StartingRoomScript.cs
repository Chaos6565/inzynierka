using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WMI
{
    public class StartingRoomScript : InteractableObject
    {
        [SerializeField] private List<GameObject> doors = new List<GameObject>();

        public override void PerformAction()
        {
            foreach (GameObject door in doors)
            {
                door.gameObject.GetComponent<DoorScript>().OpenTheDoor();
            }
        }

    }

}
