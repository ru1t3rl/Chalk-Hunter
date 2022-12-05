using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ru1t3rl.ChalkHunter.Utilities
{
    public class LookAt : MonoBehaviour
    {
        [Tooltip("By default/when empty the object will look at the camera")]
        [SerializeField] private Transform target;

        void FixedUpdate()
        {
            transform.LookAt(new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z));
        }
    }
}