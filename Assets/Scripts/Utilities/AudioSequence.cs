using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ru1t3rl.ChalkHunter.Data;

namespace Ru1t3rl.ChalkHunter.Utilities
{
    public class AudioSequence : MonoBehaviour
    {
        private Coroutine playSequenceCoroutine;

        // TODO: implement multiple sources for playing at the same time
        private List<AudioSource> sources = new List<AudioSource>();

        [Header("References")]
        [SerializeField] private AudioClipSequence sequence;
        [SerializeField] private AudioSource source;

        [Header("General")]
        [SerializeField] private bool playerInOrder = true;
        [SerializeField] private bool loop = true;
        [SerializeField] private bool overrideInterval = false;
        [SerializeField] private float interval = .5f;

        [Header("Random Interval")]
        [SerializeField] private bool randomInterval = false;
        [SerializeField] private float minInterval = .5f;
        [SerializeField] private float maxInterval = 1f;
        private List<AudioClip> tempClips = new List<AudioClip>();


        private void Awake()
        {
            if (source is null)
            {
                throw new System.NullReferenceException("The audio source is empty, without it the audio sequence can't play");
            }

            if (sequence.Length > 0)
                source.clip = sequence.GetClip(0);

            sources.Add(source);
        }

        public void Play()
        {
            if (playerInOrder)
            {
                if (playSequenceCoroutine is null)
                    playSequenceCoroutine = StartCoroutine(PlayInOrder());
            }
            else
            {
                if (playSequenceCoroutine is null)
                    playSequenceCoroutine = StartCoroutine(PlayInRandomOrder());
            }
        }

        public void Stop()
        {
            if (playSequenceCoroutine != null)
            {
                StopCoroutine(playSequenceCoroutine);
                playSequenceCoroutine = null;
            }
        }

        private IEnumerator PlayInOrder()
        {
            for (int i = 0; i < sequence.Length; i++)
            {
                source.PlayOneShot(sequence.GetClip(i));

                if (overrideInterval)
                {
                    if (randomInterval)
                    {
                        yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
                    }
                    else
                    {
                        yield return new WaitForSeconds(interval);
                    }
                }
                else
                {
                    yield return new WaitForSeconds(source.clip.length);
                }
            }

            if (loop)
            {
                playSequenceCoroutine = StartCoroutine(PlayInOrder());
            }
            else
            {
                playSequenceCoroutine = null;
            }
        }

        private IEnumerator PlayInRandomOrder()
        {
            tempClips = new List<AudioClip>();
            tempClips.AddRange(sequence.Sequence);

            for (int i = tempClips.Count; i-- > 0; i++)
            {
                source.clip = sequence.GetClip(i);
                source.Play();

                if (overrideInterval)
                {
                    if (randomInterval)
                    {
                        yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
                    }
                    else
                    {
                        yield return new WaitForSeconds(interval);
                    }
                }
                else
                {
                    yield return new WaitForSeconds(source.clip.length);
                }

                tempClips.RemoveAt(i);
            }

            if (loop)
            {
                playSequenceCoroutine = StartCoroutine(PlayInOrder());
            }
            else
            {
                playSequenceCoroutine = null;
            }
        }
    }
}