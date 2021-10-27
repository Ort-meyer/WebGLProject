using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirstPersonMovement : MonoBehaviour
{

    public float m_moveSpeed;
    public float m_turnSpeed;
    public float m_jumpSpeed;

    private CharacterController m_charController;

    private bool m_rotating;
    private bool m_mouseInCenter;

    private float m_currentUpVelocity;

    // Start is called before the first frame update
    void Start()
    {
        m_charController = GetComponent<CharacterController>();
        m_rotating = true;
        m_mouseInCenter = true;
        m_currentUpVelocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        M_Movement();
        M_Rotation();
    }

    private void M_Movement()
    {
        Vector3 movement = new Vector3();
        // xz movement
        Vector3 xzforward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        if (Input.GetKey(KeyCode.W))
        {
            movement += m_moveSpeed * xzforward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += m_moveSpeed * -1 * xzforward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += m_moveSpeed * -1 * transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += m_moveSpeed * transform.right;
        }

        // Gravity and jump
        if(m_charController.isGrounded)
        {
            m_currentUpVelocity = 0;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                m_currentUpVelocity = m_jumpSpeed;
            }
        }

        m_currentUpVelocity -= 9.82f * Time.deltaTime;
        movement.y = m_currentUpVelocity;

        // Final move
        m_charController.Move(movement * Time.deltaTime);
        
    }

    private void M_Rotation()
    {
        // Toggling of rotation
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_rotating = !m_rotating;
        }
        // Toggle warp to center
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            m_mouseInCenter = !m_mouseInCenter;
        }

        if (m_mouseInCenter)
        {
            // TODO somehow
        }

        if (m_rotating)
        {

            float xFactor = (Input.mousePosition.x - Screen.width / 2) / Screen.width;
            float yFactor = (Input.mousePosition.y - Screen.height / 2) / Screen.height * -1;
            // The further the mouse is from its origin, the faster we rotate
            Vector2 mousePosDiff = new Vector2(xFactor, yFactor);
            Vector2 rotationAngles = m_turnSpeed * Time.deltaTime * mousePosDiff;

            transform.Rotate(0, rotationAngles.x, 0, Space.World);
            transform.Rotate(rotationAngles.y, 0, 0, Space.Self);
        }
    }
}
