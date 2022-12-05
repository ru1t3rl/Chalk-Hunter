
using Ru1t3rl.ChalkHunter.Enums;
using UnityEngine;

namespace Ru1t3rl.ChalkHunter.Data
{
    [System.Serializable]
    public class KeyBinding
    {
        public Action action;
        public KeyCode[] key;
    }
}