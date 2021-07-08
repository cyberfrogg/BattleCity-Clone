using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blocks
{
    /// <summary>
    /// Base class of Block, but breakable
    /// </summary>
    public class BreakableBlock : Block
    {
        public virtual void OnBlockPartBreak(BreakableBlockPart part, Thing collidedThing)
        {
            Destroy(part.gameObject);
        }
    }
}
