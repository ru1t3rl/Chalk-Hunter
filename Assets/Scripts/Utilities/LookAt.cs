using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ru1t3rl.ChalkHunter.Utilities
{
    public class LookAt : MonoBehaviour
    {
        void FixedUpdate()
        {
            transform.LookAt(new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z));
        }
    }
}