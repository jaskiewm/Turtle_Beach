using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // UI references
    [SerializeField] UIManager uiManager;

    // Movement
    public float moveSpeed; // movement speed
    public float dashMultiplier = 3; // dash multiplies the speed for a short duration this is the multiplier
    public bool canDash = true;
    public InputActions playerControls;

    public Rigidbody rb; // using rigidbody to move character
    Vector3 moveDirection = Vector3.zero;
    private InputAction move;
    private InputAction dash;

    private void Awake()
    {
        playerControls = new InputActions();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        dash = playerControls.Player.Dash;
        dash.Enable();
        dash.performed += Dash;
    }

    private void OnDisable()
    {
        move.Disable();
        dash.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveDirection.x, 0, moveDirection.y); // quirk of using a vector2 for movedirection
        rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
    }

    void Dash(InputAction.CallbackContext context)
    {
        if (canDash)
        {
            moveSpeed *= dashMultiplier;
            canDash = false;
            StartCoroutine(timeToDash());
        }
    }
    private IEnumerator timeToDash()
    {
        yield return new WaitForSeconds(0.25f);
        moveSpeed /= dashMultiplier;
        uiManager.startSliderCooldown();
        yield return new WaitForSeconds(5f);

        canDash = true;
    }

    //public void Start()
    //{
    //    player = GetComponent<Rigidbody>();
    //    Cursor.lockState = CursorLockMode.Locked;
    //}

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    movePlayer();
    //    if (Input.GetButtonDown("Jump") && canDash==true)
    //    {
    //        Dash();
    //    }
    //}


    //public void onMove(InputAction.CallbackContext context)
    //{
    //    playerMove = context.ReadValue<Vector2>();
    //}

    //public void movePlayer()
    //{
    //    // Where we want the character to move
    //    Vector3 movement = new Vector3(playerMove.x, 0, playerMove.y);

    //    if (movement != Vector3.zero)
    //    {
    //        // Where we want the character to look
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
    //    }

    //    // Character move to that point
    //    transform.Translate(movement * speed * Time.deltaTime, Space.World);
    //}
}