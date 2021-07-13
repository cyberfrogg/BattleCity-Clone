using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Blocks
{
    /// <summary>
    /// Breakable part of block
    /// </summary>
    public class BreakableBlockPart : Thing
    {
        public BreakableBlock Self;

        public override void OnThingCollidedEnter(Thing thing)
        {
            base.OnThingCollidedEnter(thing);

            if (thing is Bullet)
            {
                Self.OnBlockPartBreak(this, thing);
            }
        }
    }
}
