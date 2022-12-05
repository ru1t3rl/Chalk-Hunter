using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ru1t3rl.ChalkHunter.Data;
using Ru1t3rl.Utilities;

namespace Ru1t3rl.ChalkHunter.Utilities
{
    public class MessageUI : UnitySingleton<MessageUI>
    {
        [SerializeField] private MessageIcon[] icons;

        [Header("Object References")]
        [SerializeField] private Image iconObject;
        [SerializeField] private TextMeshProUGUI textObject;

        [Header("Animation")]
        [SerializeField] private bool animateMessage = true;
        [SerializeField] private float characterInterval = .1f;

        private Queue<Message> queuedMessages = new Queue<Message>();
        private float timeToHide = 0f;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Time.time < timeToHide)
                return;

            if (queuedMessages.Count <= 0)
            {
                gameObject.SetActive(false);
                return;
            }

            ShowMessage(queuedMessages.Dequeue());
        }

        public void ShowMessage(Message message, bool forceShow = false)
        {
            if (Time.time < timeToHide)
            {
                queuedMessages.Enqueue(message);
                return;
            }

            gameObject.SetActive(true);
            SetIcon(message.messageType);

            if (animateMessage)
            {
                StartCoroutine(AnimateMessage(message));
                timeToHide = Time.time + message.duration + characterInterval * message.message.Length;
            }
            else
            {
                textObject.text = message.message;
                timeToHide = Time.time + message.duration;
            }
        }

        private void SetIcon(MessageType type)
        {
            for (int i = 0; i < icons.Length; i++)
            {
                if (icons[i].messageType == type)
                {
                    iconObject.sprite = icons[i].icon;
                    return;
                }
            }
        }

        private IEnumerator AnimateMessage(Message message)
        {
            textObject.text = string.Empty;
            for (int i = 0; i < message.message.Length; i++)
            {
                textObject.text += message.message[i];
                yield return new WaitForSeconds(characterInterval);
            }
        }
    }
}