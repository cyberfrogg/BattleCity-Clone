using Entities;
using UnityEngine;

namespace Blocks
{
    public class SteelWallBlock : BreakableBlock
    {
        public AudioClip WallAudio;
        public override void OnThingCollidedExit(Thing thing)
        {
            if (thing is Bullet && (thing as Bullet).Type == BulletType.PlayerBullet)
            {
                AudioManager.Instance.PlaySFX(WallAudio);
            }
        }
    }   
}
