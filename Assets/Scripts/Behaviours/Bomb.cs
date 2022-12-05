using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ru1t3rl.Events;
using Ru1t3rl.Events.Args;
using Ru1t3rl.ChalkHunter.Behaviours.Enemies;

namespace Ru1t3rl.ChalkHunter.Behaviours
{
    [RequireComponent(typeof(ArealAttack))]
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float fuseTime = 3f;
        private ArealAttack arealAttack;

        private float shakeDuration = 1f;
        private float shakeMagnitude = 0.5f;
        private float shakeInterval = 0.05f;

        private void Awake()
        {
            arealAttack = GetComponent<ArealAttack>();
        }

        public void Detonate()
        {
            StartCoroutine(Explode());
        }

        private IEnumerator Explode()
        {
            yield return new WaitForSeconds(fuseTime);

            EventManager.Instance.Invoke("Screenshake", new ShakeArguments(
                shakeDuration,
                shakeMagnitude,
                shakeInterval
            ));

            arealAttack.Attack();
            gameObject.SetActive(false);
        }
    }
}