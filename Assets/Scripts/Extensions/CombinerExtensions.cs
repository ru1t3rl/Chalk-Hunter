using System.Collections.Generic;
using UnityEngine;

namespace Ru1t3rl.ChalkHunter.Extensions
{
    public static class CombinerExtensions
    {
        public static bool Contains(this MeshRenderer[] array, MeshRenderer renderer)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == renderer)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool EqualMaterials(this List<MeshRenderer> array, MeshRenderer renderer)
        {

            for (int i = 0; i < array.Count; i++)
            {
                if (array[i].sharedMaterials.Length == renderer.sharedMaterials.Length)
                {
                    bool equal = true;
                    for (int j = 0; j < array[i].sharedMaterials.Length; j++)
                    {
                        if (array[i].sharedMaterials[j] != renderer.sharedMaterials[j])
                        {
                            equal = false;
                            break;
                        }
                    }

                    if (equal)
                        return true;
                }
            }

            return false;
        }

        public static bool IsExclusion(this Transform transform, Transform[] array)
        {
            for (int iTransform = 0; iTransform < array.Length; iTransform++)
            {
                do
                {
                    if (array[iTransform] == transform)
                    {
                        return true;
                    }

                    if (transform == null || transform.parent == null)
                        transform = null;
                    else
                        transform = transform.parent;
                } while (transform != null);
            }

            return false;
        }
    }
}