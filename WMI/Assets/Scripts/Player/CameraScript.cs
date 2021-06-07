using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WMI
{
    public class CameraScript : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;

        public void SetCameraTarget(Transform playerTransform)
        {
            target = playerTransform;
        }
        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);
        }
    }
}