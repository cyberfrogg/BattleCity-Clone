using Entities;

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

            if (!Self.gameObject.CompareTag("dummy") && thing.gameObject.CompareTag("dummy"))
            {
                Self.gameObject.SetActive(false);
                //Destroy(Self.gameObject);
            }
        }
    }
}
