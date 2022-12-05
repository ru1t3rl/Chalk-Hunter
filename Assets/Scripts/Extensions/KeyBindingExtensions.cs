using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ru1t3rl.ChalkHunter.Data;
using Ru1t3rl.ChalkHunter.Enums;

namespace Ru1t3rl.ChalkHunter.Extensions
{
    public static class KeyBindingExtensions
    {
        public static bool IsDown(this KeyBinding keyBinding)
        {
            foreach (KeyCode keyCode in keyBinding.key)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsHeld(this KeyBinding keyBinding)
        {
            foreach (KeyCode keyCode in keyBinding.key)
            {
                if (Input.GetKey(keyCode))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsDown(this KeyBinding[] keyBindings, Action action)
        {
            foreach (KeyBinding keyBinding in keyBindings)
            {
                if (keyBinding.action == action)
                {
                    return keyBinding.IsDown();
                }
            }

            throw new System.Exception("KeyBinding not found");
        }

        public static bool IsHeld(this KeyBinding[] keyBindings, Action action)
        {
            foreach (KeyBinding keyBinding in keyBindings)
            {
                if (keyBinding.action == action)
                {
                    return keyBinding.IsHeld();
                }
            }

            throw new System.Exception("KeyBinding not found");
        }
    }
}