using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    /// <summary>
    /// Base class of all moveable objects
    /// </summary>
    public class Entity : Thing
    {
        [HideInInspector] public EntityAI AI;

        public override void Update()
        {
            base.Update();

            AI.UpdateAI();
        }
    }
}