using System;
using UnityEngine;


namespace Entities
{
    /// <summary>
    /// Base class of Entity AI
    /// </summary>
    [Serializable]
    public class EntityAI
    {
        [HideInInspector] public Entity Self;

        /// <summary>
        /// Initializing AI
        /// </summary>
        /// <param name="entity">Self entity</param>
        public virtual void Init(Entity entity)
        {
            Self = entity;
        }

        /// <summary>
        /// Update translator. Updatets AI every frame
        /// </summary>
        public virtual void UpdateAI()
        {

        }

        
    }
}
