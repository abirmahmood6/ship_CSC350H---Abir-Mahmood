// Abir Mahmood
// CSC-350H
// Dr. Hao Tang
// Ship.cs

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Spacecraft : MonoBehaviour
{
    // Cache Rigidbody 2D component for efficient access.
    private Rigidbody2D rb2d;
    // Direction of movement.
    private Vector2 direction;


    // Thrust force constant.
    private float thrustForce = 1f;

    // Store collider radius.
    private float shipColliderRadius;

    // Speed of rotation.
    private const float rotationSpeed = 50f;


    // Start is called before the first frame update
    void Start()
    {
        // Assigning the Rigidbody 2D component attached to the spacecraft to this field.
        rb2d = GetComponent<Rigidbody2D>();
        // Retrieving and storing the radius of the collider.
        shipColliderRadius = GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        float rotateInput = Input.GetAxis("Rotate");
        // Calculate rotation amount and apply rotation.
        float rotationAmount = rotationSpeed * Time.deltaTime;
        if (rotateInput < 0)
        {
            rotationAmount *= -1;
        }


        transform.Rotate(Vector3.forward, rotationAmount);
        // Assigning direction of thrust as same as rotation direction.
        direction = CalculateDynamicThrustDirection();
    }


    // Used to interact with physics.
    private void FixedUpdate()
    {
        float thrustInput = Input.GetAxis("Thrust");
        if (thrustInput > 0f)
        {
            rb2d.AddForce(direction * thrustForce, ForceMode2D.Force);
        }
    }

    // Keep the spacecraft on the screen.
    void OnBecameInvisible()
    {
        KeepOnScreen();
    }


    void KeepOnScreen()
    {
        Vector2 position = transform.position;
        // If spacecraft exits the right side, it appears on the left side.
        if (position.x + shipColliderRadius > ScreenUtils.ScreenRight)
        {
            position.x = ScreenUtils.ScreenLeft + shipColliderRadius;
            transform.position = position;
            Debug.Log("Entering the left side of the screen: " + transform.position);
        }

        // If spacecraft exits the left side, it appears on the right side.
        if (position.x - shipColliderRadius < ScreenUtils.ScreenLeft)
        {
            position.x = ScreenUtils.ScreenRight + shipColliderRadius;
            transform.position = position;
            Debug.Log("Entering the right side of the screen: " + transform.position);
        }


        // If spacecraft exits the top side, it appears on the bottom side.
        if (position.y + shipColliderRadius > ScreenUtils.ScreenTop)
        {
            position.y = ScreenUtils.ScreenBottom + shipColliderRadius;
            transform.position = position;
            Debug.Log("Entering the bottom side of the screen: " + transform.position);
        }
        // If spacecraft exits the bottom side, it appears on the top side.
        if (position.y - shipColliderRadius < ScreenUtils.ScreenBottom)
        {
            position.y = ScreenUtils.ScreenTop + shipColliderRadius;
            transform.position = position;
            Debug.Log("Entering the top side of the screen: " + transform.position);
        }
    }

    Vector2 CalculateDynamicThrustDirection()
    {
        // Get the spacecraft's rotation around the Z axis.
        float spacecraftRotationZ = transform.eulerAngles.z;


        // Convert the angle from degrees to radians.
        float spacecraftRotationRadians = spacecraftRotationZ * Mathf.Deg2Rad;


        // Determine the new X and Y components of the direction vector.
        float directionX = Mathf.Cos(spacecraftRotationRadians);
        float directionY = Mathf.Sin(spacecraftRotationRadians);

        // Return the calculated direction as a Vector2.
        return new Vector2(directionX, directionY);
    }
}
