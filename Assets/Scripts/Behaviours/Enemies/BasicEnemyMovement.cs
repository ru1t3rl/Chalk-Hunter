using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ru1t3rl.ChalkHunter.Enums;

namespace Ru1t3rl.ChalkHunter.Behaviours.Enemies
{
    public class BasicEnemyMovement : MonoBehaviour
    {
        [SerializeField] private EnemyTypes enemyType;

        [Header("Patrol")]
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private float patrolSpeed;
        [SerializeField] private float patrolWaitTime = 1f;
        [SerializeField] private float patrolDestinationTolerance = .5f;
        [SerializeField] private int patrolIndex = 0;
        private Coroutine waitForPatrol;

        [Header("Hunt")]
        [SerializeField] private bool patrolOnIdle = false;
        [SerializeField] private float huntSpeed;
        [SerializeField] private float huntDistance;

        private Transform target;

        protected virtual void Awake()
        {
            target = enemyType switch
            {
                EnemyTypes.Static => null,
                EnemyTypes.Petrol => patrolPoints[patrolIndex],
                EnemyTypes.Hunt => patrolOnIdle ? patrolPoints[patrolIndex] : GameObject.FindGameObjectWithTag("Player").transform,
                _ => null
            };
        }

        protected virtual void Update()
        {
            switch (enemyType)
            {
                case EnemyTypes.Static:
                    Static();
                    break;
                case EnemyTypes.Petrol:
                    break;
                case EnemyTypes.Hunt:
                    break;
            }
        }

        protected virtual void Static()
        {
            // Do nothing
        }

        protected virtual void Patrol()
        {
            if (Vector3.Distance(transform.position, target.position) < patrolDestinationTolerance)
            {
                if (waitForPatrol is null)
                {
                    StartCoroutine(WaitForPatrol());
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);
            }
        }

        private IEnumerator WaitForPatrol()
        {
            yield return new WaitForSeconds(patrolWaitTime);
            waitForPatrol = null;
        }

        protected virtual void Hunt()
        {
            if (Vector3.Distance(transform.position, target.position) < huntDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, huntSpeed * Time.deltaTime);
            }
            else if (patrolOnIdle)
            {
                Patrol();
            }
        }

        private void UpdateVisualDirection()
        {
            if (target is null)
                return;

            transform.localScale = new Vector3(
                (target.position.x < transform.position.x) ? 
                    Mathf.Min(-transform.localScale.x, transform.localScale.x) : 
                    Mathf.Max(-transform.localScale.x, transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }
}