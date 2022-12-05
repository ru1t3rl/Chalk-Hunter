using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ru1t3rl.ChalkHunter.Data;

namespace Ru1t3rl.ChalkHunter.Behaviours
{
    /// <summary>
    /// The plan was to make a more advanced inventory for currency and throwables
    /// But due to some problems and the limited time, I decided to focus on more important mechanics
    /// and created this collection manager
    /// </summary>
    public class ThrowableCollection : MonoBehaviour
    {
        [SerializeField] private KeyMap keyMap;
        [SerializeField] List<Throwable> throwables = new List<Throwable>();
        public int Count => throwables.Count;

        [SerializeField] private Player.PlayerBehaviour playerBehaviour;

        public void Update()
        {
            if (keyMap.IsKeyDown(Enums.Action.Throw))
            {
                Throw();
            }
        }

        public void Throw()
        {
            if (throwables.Count > 0)
            {
                Throwable throwable = throwables[0];
                throwables.RemoveAt(0);
                throwable.transform.position = playerBehaviour.transform.position;
                throwable.Throw(Vector3.right * (playerBehaviour.Direction < 0 ? -1 : 1));
            }
        }
    }
}
