using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;


public class PlayerSprites : MonoBehaviour
{
    [SerializeField] SpriteResolver spriteResolver;
    [SerializeField] CurrentData current;

    private void Update()
    {
        spriteResolver.SetCategoryAndLabel("Toiper", $"Head_{current.abilitiesUsed}");
    }

}
