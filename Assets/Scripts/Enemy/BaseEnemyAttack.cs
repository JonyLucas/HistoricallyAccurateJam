using Game.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public abstract class BaseEnemyAttack : MonoBehaviour
    {
        [SerializeField]
        protected float minYDistance = 0.5f;

        [SerializeField]
        protected float attackRate = 0.5f;

        [SerializeField]
        protected SoundFx soundFx;

        protected AudioSource audioSource;
        protected GameObject player;
        protected SpriteRenderer renderer;
        protected Animator animator;
        protected Vector3 distance;

        protected bool isFacingLeft = true;
        protected bool playerAlive = true;
        protected bool isVisible = false;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            renderer = GetComponent<SpriteRenderer>();
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (isVisible)
            {
                PerformAction();
            }
        }

        protected abstract void PerformAction();

        protected void FacePlayer()
        {
            if (distance.x > 0)
            {
                isFacingLeft = true;
            }
            else
            {
                isFacingLeft = false;
            }

            renderer.flipX = isFacingLeft;
        }

        private void OnBecameInvisible()
        {
            isVisible = false;
        }

        private void OnBecameVisible()
        {
            isVisible = true;
        }

        public void PlayerDeath()
        {
            playerAlive = false;
        }
    }
}