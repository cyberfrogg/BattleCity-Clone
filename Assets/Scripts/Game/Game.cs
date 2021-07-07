using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Game : Thing
    {
        public static Game Instance;

        public Map Map;

        public override void Awake()
        {
            base.Awake();

            Instance = this;
        }
    }
}
