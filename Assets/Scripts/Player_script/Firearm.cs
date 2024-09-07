using System;
using UnityEngine;

namespace Assets.Scripts.Player_script
{
    [Serializable]
    public class Firearm
    {
        public Transform Transform;
        public SpriteRenderer Renderer;
        public Animator Animator;
        public Transform FireMuzzle;
        public Vector2 FireMuzzlePosition;
        public bool Detached; // https://github.com/hippogamesunity/PixelHeroesHub/wiki/FAQ#what-is-a-detached-firearm
    }
}