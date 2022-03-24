using Entities;
using UnityEngine;

namespace Blocks
{
    /// <summary>
    /// Breakable part of block
    /// </summary>
    public class BreakableBlockPart : Thing
    {
        public BreakableBlock Self;
        public AudioClip BrickBreakAudio;

        public override void OnThingCollidedEnter(Thing thing)
        {
            base.OnThingCollidedEnter(thing);

            if (thing is Bullet  && (thing as Bullet).Type == BulletType.PlayerBullet)
            {
                AudioManager.Instance.PlaySFX(BrickBreakAudio);
            }

            if (thing is Bullet)
            {
                
                Self.OnBlockPartBreak(this, thing);
            }

            if (!Self.gameObject.CompareTag("dummy") && thing.gameObject.CompareTag("dummy"))
            {
                AudioManager.Instance.PlaySFX(BrickBreakAudio);
                Self.gameObject.SetActive(false);
                //Destroy(Self.gameObject);
            }
        }
    }
}
