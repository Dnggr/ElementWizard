using UnityEngine;
using UnityEngine.InputSystem; // This is for the new Input System, but we won't use it in this example

public class PlayerMovement : MonoBehaviour
{
    // This is a VARIABLE — it stores a value we can change
    // "public" means we can see and edit it in Unity's Inspector
    // "float" means it's a decimal number
    public float moveSpeed = 5f;

    // This stores a reference to the Rigidbody2D component
    // "private" means only this script can use it
    private Rigidbody2D rb;
    void Start()
    {
        // GetComponent finds the Rigidbody2D attached to this same GameObject
        // We store it in "rb" so we can use it every frame without searching again
        rb = GetComponent<Rigidbody2D>();
        //"Hey Unity, go find the Rigidbody2D on this object and link it to the nickname rb. From now on, whenever I give an order to rb, I’m actually talking to that Rigidbody2D."
    }

    void Update()
    {
        // Input.GetAxisRaw reads keyboard input
        // "Horizontal" = A/D keys or Left/Right arrows  (-1, 0, or 1)
        // "Vertical"   = W/S keys or Up/Down arrows     (-1, 0, or 1)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


       // Vector2 is just an (X, Y) direction
        // We multiply by moveSpeed so it moves faster or slower
        Vector2 movement = new Vector2(moveX, moveY) * moveSpeed;


        // We set the rigidbody's velocity instead of moving the Transform directly
        // This works with physics properly
        rb.linearVelocity = movement;
    }
}
