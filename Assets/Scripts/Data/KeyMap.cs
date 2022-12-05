using System.Linq;
using UnityEngine;
using Ru1t3rl.ChalkHunter.Enums;
using Ru1t3rl.ChalkHunter.Extensions;

namespace Ru1t3rl.ChalkHunter.Data
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
}