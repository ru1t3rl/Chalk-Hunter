using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ru1t3rl.Events;
using Ru1t3rl.Events.Args;

namespace Ru1t3rl.ChalkHunter.Utilities
{
    public class Screenshake : MonoBehaviour
    {
        private Coroutine shakeRoutine;

        private void Awake()
        {
            EventManager.Instance.AddListener("Screenshake", Shake);
        }

        public void Shake(System.EventArgs args)
        {
            ShakeArguments shakeArgs = (ShakeArguments)args;

            if (shakeRoutine is not null)
            {
                StopCoroutine(shakeRoutine);
            }

            shakeRoutine = StartCoroutine(ShakeRoutine(
                shakeArgs.duration,
                shakeArgs.magnitude,
                shakeArgs.interval
            ));
        }

        private IEnumerator ShakeRoutine(float duration, float magnitude, float interval)
        {
            float timeToStop = Time.time + duration;

            do
            {
                transform.position = new Vector3(
                    Random.Range(-1f, 1f) * magnitude,
                    Random.Range(-1f, 1f) * magnitude,
                    0
                );
                yield return new WaitForSeconds(interval);
            } while (Time.time < timeToStop);

            transform.position = Vector3.zero;
            shakeRoutine = null;
        }
    }
}