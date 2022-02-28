using Game.ScriptableObjects.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class TimedEnableObject : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _objectsToEnable;

        public void EnableObjects()
        {
            _objectsToEnable.ForEach(obj => obj.SetActive(true));
        }
    }
}