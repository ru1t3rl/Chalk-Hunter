using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Ru1t3rl.ChalkHunter.Data
{
    [CreateAssetMenu(fileName = "AudioClipSequence", menuName = "Chalk-Hunter/AudioClipSequence", order = 0)]
    public class AudioClipSequence : ScriptableObject
    {
        [SerializeField] private AudioClip[] sequence;
        public AudioClip[] Sequence => sequence;

        public AudioClip GetClip(int index)
        {
            return sequence[index];
        }

        public int Length => sequence.Length;
    }
}