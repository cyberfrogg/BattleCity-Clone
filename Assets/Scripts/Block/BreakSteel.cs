
using Entities;

namespace Blocks
{
    public class BreakSteel : Thing
    {
        public BreakableBlock Self;

        public override void OnThingCollidedEnter(Thing thing)
        {
            base.OnThingCollidedEnter(thing);

            if (thing is Bullet && thing.gameObject.GetComponent<Bullet>().CanDestroySteel && !this.gameObject.CompareTag("barrier"))
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
