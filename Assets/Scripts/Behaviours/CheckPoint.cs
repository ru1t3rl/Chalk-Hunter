using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ru1t3rl.ChalkHunter.Data;
using Ru1t3rl.ChalkHunter.Extensions;
using Ru1t3rl.ChalkHunter.Utilities;

namespace Ru1t3rl.ChalkHunter.Behaviours
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private Message message;
        [SerializeField] private string[] colliderTags;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(colliderTags))
            {
                MessageUI.Instance.ShowMessage(message);
            }

            gameObject.SetActive(false);
        }
    }
}