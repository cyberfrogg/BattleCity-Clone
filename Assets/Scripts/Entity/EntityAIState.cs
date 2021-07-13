using System;
using UnityEngine.Events;
using UnityEngine;

namespace Entities
{
    /// <summary>
    /// Base struct class of Entity Statemachine
    /// </summary>
    [Serializable]
    public class EntityAIState
    {
        public bool IsActive { private set; get; }
        [HideInInspector] public Entity Self;

        [SerializeField] private UnityEvent onStateStop;

        /// <summary>
        /// Initializing self
        /// </summary>
        /// <param name="self"></param>
        public virtual void Init(Entity self)
        {
            Self = self;
        }

        /// <summary>
        /// Runs state
        /// </summary>
        /// <param name="callbackevent">Calling on state end</param>
        public virtual void RunState(UnityAction callbackevent)
        {
            IsActive = true;
            onStateStop.AddListener(callbackevent);
        }

        /// <summary>
        /// Called every frame AI update
        /// </summary>
        public virtual void UpdateState()
        {

        }

        /// <summary>
        /// Stops state. Call on end point of state
        /// </summary>
        public virtual void StopState()
        {
            IsActive = false;
            onStateStop.Invoke();
            onStateStop.RemoveAllListeners();
        }
    }
}
