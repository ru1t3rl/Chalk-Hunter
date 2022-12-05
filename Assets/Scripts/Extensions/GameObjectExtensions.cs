using UnityEngine;

namespace Ru1t3rl.ChalkHunter.Extensions
{
    public static class GameObjectExtensions
    {
        public static bool CompareTag(this GameObject gameobject, string[] tags)
        {
            foreach (string tag in tags)
            {
                if (gameobject.CompareTag(tag))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CompareTag(this Collider gameobject, string[] tags)
        {
            foreach (string tag in tags)
            {
                if (gameobject.CompareTag(tag))
                {
                    return true;
                }
            }

            return false;
        }
    }
}