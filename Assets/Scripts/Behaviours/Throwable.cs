using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ru1t3rl.ChalkHunter.Data;

namespace Ru1t3rl.ChalkHunter.Behaviours
{
    public class Throwable : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private KeyMap keyMap;
        [SerializeField] private new Rigidbody rigidbody;

        [Header("Settings")]
        [SerializeField] private float throwForce = 10f;
        public UnityEvent onThrow;

        private void Awake()
        {
            rigidbody ??= GetComponent<Rigidbody>();
        }

        public void Throw(Vector3 direction)
        {
            onThrow?.Invoke();
            rigidbody?.AddForce(direction.normalized * throwForce, ForceMode.Impulse);
        }
    }
}