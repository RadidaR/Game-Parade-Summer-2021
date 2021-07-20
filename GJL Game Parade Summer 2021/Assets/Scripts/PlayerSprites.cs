using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Toiper
{
    namespace Player
    {
        public class PlayerSprites : MonoBehaviour
        {
            [HideInInspector][SerializeField] SpriteResolver spriteResolver;
            [SerializeField] NewCurrentData current;

            private void Awake()
            {
                spriteResolver = GetComponentInChildren<SpriteResolver>();
            }
            private void Update()
            {
                spriteResolver.SetCategoryAndLabel("Toiper", $"Head_{current.abilitiesUsed}");
            }

        }
    }
}
