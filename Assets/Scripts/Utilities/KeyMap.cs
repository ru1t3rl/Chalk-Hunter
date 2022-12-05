using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ru1t3rl.ChalkHunter.Enums;
using Ru1t3rl.ChalkHunter.Extensions;

namespace Ru1t3rl.ChalkHunter.Utilities
{

    [CreateAssetMenu(fileName = "KeyMap", menuName = "Chalk-Hunter/KeyMap", order = 0)]
    public class KeyMap : ScriptableObject
    {
        public KeyBinding[] keyBindings;

        public bool IsKeyDown(Action action)
        {
            return keyBindings.IsDown(action);
        }

        public bool IsKey(Action action)
        {
            return keyBindings.IsHeld(action);
        }

        public KeyBinding GetBinding(Action action)
        {
            return keyBindings.First(binding => binding.action == action);
        }
    }

    [System.Serializable]
    public class KeyBinding
    {
        public Action action;
        public KeyCode[] key;
    }
}