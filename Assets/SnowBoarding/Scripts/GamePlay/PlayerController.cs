using System;
using System.Collections.Generic;
using UnityEngine;
using _GAME.Scripts.Controllers;
using DenkKits.GameServices.Audio.Scripts;
using DenkKits.GameServices.SaveData; // Import GameController namespace

namespace SnowBoarding.Scripts.GamePlay
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")] [SerializeField]
        float torqueAmount = 1f;

        [Header("Jump Settings")] [SerializeField]
        float jumpForce = 5f;

        [SerializeField] LayerMask groundLayer;
        [SerializeField] Transform groundCheck;
        [SerializeField] float groundCheckRadius = 0.5f;

        [Header("Physics Components")] public Rigidbody2D rb2d;


        public void RotateLeft()
        {
            rb2d.AddTorque(torqueAmount);
        }

        public void RotateRight()
        {
            rb2d.AddTorque(-torqueAmount);
        }

        public void TryJump()
        {
            if (IsGrounded())
            {
                AudioManager.Instance.PlaySfx(AudioName.Gameplay_PlayerJump);
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce); // Gán luôn vận tốc Y
            }
        }

        public bool grounded;

        private void Update()
        {
            if (IsGrounded())
            {
                if (grounded)
                {
                    return;
                }

                Debug.Log("Grounded");
                AudioManager.Instance.PlaySfx(AudioName.Gameplay_TimerRun, true);
                grounded = true;
            }
            else
            {
                if (!grounded)
                {
                    return;
                }

                AudioManager.Instance.StopSfx(AudioName.Gameplay_TimerRun);
                grounded = false;
            }
        }

        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        private bool isDone;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isDone)
            {
                return;
            }

            switch (other.tag)
            {
                case "Star":
                    GameController.Instance.PlayerHitCoin();
                    Destroy(other.gameObject);
                    break;
                case "Rock":
                    isDone = true;
                    GameController.Instance.PlayerHitObstacle();
                    break;
                case "FinishLine":
                    isDone = true;
                    GameController.Instance.PlayerHitFinishLine();
                    break;
            }
        }
    }
}