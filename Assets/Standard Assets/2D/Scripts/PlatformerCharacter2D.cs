using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [Range(0,1)] [SerializeField] private float m_AirControlTime = 0.1f;
        [SerializeField] public int airJumpsAllowed = 0;
        [Range(0,1000)] [SerializeField] public float m_JumpMult = 0.5f;
        [SerializeField] public float wallTouchRadius = 0.2f;
        public LayerMask whatIsWall;     
        public float jumpPushForce = 10f;
        bool touchingWallLeft = false;
        bool touchingWallRight = false;
        
        private int AirJumpCount = 0;
        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .01f; // Radius of the overlap circle to determine if grounded
        public bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private bool can_AirControl = true;
        private float ac_Timer = 0f;
        public Transform m_LeftCheck;
        public Transform m_RightCheck;
        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_LeftCheck = transform.Find("LeftCheck");
            m_RightCheck = transform.Find("RightCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            m_Grounded = (Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround).Length != 0);
            touchingWallLeft = (Physics2D.OverlapCircleAll(m_LeftCheck.position, wallTouchRadius, whatIsWall).Length != 0);
            touchingWallRight = (Physics2D.OverlapCircleAll(m_RightCheck.position, wallTouchRadius, whatIsWall).Length != 0);
            
            
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
             
              if (touchingWallLeft || touchingWallRight)
              {
                  AirJumpCount = 0;
              }
            
            if (m_Grounded)
            {
                can_AirControl = true; 
                ac_Timer = 0;
                AirJumpCount = 0;
            }
            else
            {
                ac_Timer += Time.fixedDeltaTime;
                if (ac_Timer > m_AirControlTime)
                {
                    can_AirControl = false; 
                }
            }
        }


        public void Move(int m, bool crouch, bool jump)
        {
            float move = (float)m;
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }
            
            if (crouch)
            {
                // TODO: enable drop through platforms
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);
            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl || can_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
            
            if (touchingWallLeft && jump)
            {
                m_Rigidbody2D.AddForce(new Vector2(jumpPushForce,m_JumpForce));
            }
            else if (touchingWallRight && jump)
            {  
                m_Rigidbody2D.AddForce(new Vector2(-jumpPushForce,m_JumpForce));
            }
            if (!m_Grounded && jump && (AirJumpCount < airJumpsAllowed))
            {
                m_Rigidbody2D.AddForce(new Vector2((float)move * m_JumpMult, m_JumpForce));
                AirJumpCount += 1;
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
