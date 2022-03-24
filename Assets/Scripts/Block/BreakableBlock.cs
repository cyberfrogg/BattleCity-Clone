namespace Blocks
{
    /// <summary>
    /// Base class of Block, but breakable
    /// </summary>
    public class BreakableBlock : Block
    {
        /// <summary>
        /// Called on Block break
        /// </summary>
        /// <param name="part">broken part</param>
        /// <param name="collidedThing">thing that breaks part</param>
        public virtual void OnBlockPartBreak(BreakableBlockPart part, Thing collidedThing)
        {
            part.gameObject.SetActive(false);
            //Destroy(part.gameObject);
        }

        public void OnBlockPartBreak(BreakSteel part, Thing collidedThing)
        {
            part.gameObject.SetActive(false);
        }
    }
}
