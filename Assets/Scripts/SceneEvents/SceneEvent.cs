using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneEvents
{
    /// <summary>
    /// Base class of Scene events. Used in events
    /// </summary>
    public class SceneEvent : Thing
    {
        /// <summary>
        /// Event trigger call
        /// </summary>
        public virtual void TriggerEvent()
        {

        }
    }
}
