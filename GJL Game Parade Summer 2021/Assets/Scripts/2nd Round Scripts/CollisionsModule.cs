using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toiper
{
    namespace Player
    {
        public class CollisionsModule : MonoBehaviour
        {
            [SerializeField] NewCurrentData current;
            [SerializeField] NewGameData data;

            [SerializeField] GameEvent eSteppedOnTrampoline;
            [SerializeField] GameEvent eReachedExit;
            int trampolineLayer => data.trampolineLayerInt;
            int exitLayer => data.exitLayerInt;


            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.gameObject.layer == trampolineLayer)
                {
                    if (current.state != NewCurrentData.States.Swinging || current.state != NewCurrentData.States.Done)
                        eSteppedOnTrampoline.Raise();
                }
                else if (collision.gameObject.layer == exitLayer)
                    eReachedExit.Raise();
            }
        }
    }
}
