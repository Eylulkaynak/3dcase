using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Speed ​​at which the character moves. It is not affected by gravity or jumping.")]
    public float velocity = 5f;
    [Tooltip("This value is added to the speed value while the character is sprinting.")]
    public float sprintAdittion = 3.5f;
    [Tooltip("The higher the value, the higher the character will jump.")]
    public float jumpForce = 18f;
    [Tooltip("Stay in the air. The higher the value, the longer the character floats before falling.")]
    public float jumpTime = 0.85f;
    [Space]
    [Tooltip("Force that pulls the player down. Changing this value causes all movement, jumping and falling to be changed as well.")]
    public float gravity = 9.8f;

    float jumpElapsedTime = 0;
    
    float verticalVelocity = 0f; 

    // Player states
    bool isJumping = false;
    bool isSprinting = false;
    bool isCrouching = false;

    // Inputs
    float inputHorizontal;
    float inputVertical;
    bool inputJump;
    bool inputCrouch;
    bool inputSprint;

    Animator animator;
    CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (animator == null)
            Debug.LogWarning("Hey buddy, you don't have the Animator component in your player. Without it, the animations won't work.");
    }

    void Update()
    {
        // Input checkers
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        
       
        inputJump = Input.GetKeyDown(KeyCode.Space); 
   
        inputSprint = Input.GetAxis("Fire3") == 1f || Input.GetKey(KeyCode.LeftShift); 
        inputCrouch = Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton1);

        if (inputCrouch)
            isCrouching = !isCrouching;

        if (inputJump && cc.isGrounded && !isJumping)
        {
            isJumping = true;
            jumpElapsedTime = 0;
        }

        // Animations
        if (animator != null)
        {
            // Yerdeyken Koşma/Çömelme animasyonları
            if (cc.isGrounded)
            {
                animator.SetBool("crouch", isCrouching);
                
                float minimumSpeed = 0.9f;
                
                // Animasyonlar için sadece yatay hızı alıyoruz (yukarı/aşağı hareketi yoksay)
                Vector3 horizontalVelocity = new Vector3(cc.velocity.x, 0, cc.velocity.z);
                
                animator.SetBool("run", horizontalVelocity.magnitude > minimumSpeed);

                isSprinting = horizontalVelocity.magnitude > minimumSpeed && inputSprint;
                animator.SetBool("sprint", isSprinting);
            }

            // Zıplama/Havada Olma animasyonu
            animator.SetBool("air", !cc.isGrounded);
        }

        HeadHittingDetect();
    }

    private void FixedUpdate()
    {
        // Sprinting velocity boost or crounching desacelerate
        float velocityAdittion = 0;
        if (isSprinting)
            velocityAdittion = sprintAdittion;
        if (isCrouching)
            velocityAdittion = - (velocity * 0.50f); 

        // Direction movement
        float directionX = inputHorizontal * (velocity + velocityAdittion) * Time.deltaTime;
        float directionZ = inputVertical * (velocity + velocityAdittion) * Time.deltaTime;

        // --- YENİ ZIPLAMA VE YERÇEKİMİ MANTIĞI ---
        if (cc.isGrounded && !isJumping)
        {
            // Karakter yere bastığında aşağı doğru ufak bir kuvvet uygulayarak isGrounded bool'unun bozulmasını (titremesini) engelliyoruz.
            verticalVelocity = -2f; 
        }

        if (isJumping)
        {
            // Zıplama gücünü uygula
            verticalVelocity = Mathf.SmoothStep(jumpForce, jumpForce * 0.30f, jumpElapsedTime / jumpTime);

            jumpElapsedTime += Time.deltaTime;
            if (jumpElapsedTime >= jumpTime)
            {
                isJumping = false;
            }
        }
        else
        {
            // Havada süzülüyorsa veya düşüyorsa yerçekimini üstüne ekle
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // --- Character rotation --- 
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        forward = forward * directionZ;
        right = right * directionX;

        if (directionX != 0 || directionZ != 0)
        {
            float angle = Mathf.Atan2(forward.x + right.x, forward.z + right.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }
        // --- End rotation ---

        // Dikey ve Yatay hareketleri birleştirip karaktere aktarıyoruz
        Vector3 verticalDirection = Vector3.up * verticalVelocity * Time.deltaTime;
        Vector3 horizontalDirection = forward + right;

        Vector3 moviment = verticalDirection + horizontalDirection;
        cc.Move(moviment);
    }

    void HeadHittingDetect()
    {
        float headHitDistance = 1.1f;
        Vector3 ccCenter = transform.TransformPoint(cc.center);
        float hitCalc = cc.height / 2f * headHitDistance;

        if (Physics.Raycast(ccCenter, Vector3.up, hitCalc))
        {
            jumpElapsedTime = 0;
            isJumping = false;
        }
    }
}