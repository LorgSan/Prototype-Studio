using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrogScript : MonoBehaviour
{
    PlayerControls controls;
    Vector2 rightEyeMove;
    Vector2 leftEyeMove;
    [SerializeField] float speed;
    [SerializeField] float accelerationRight;
    [SerializeField] float accelerationLeft;
    [SerializeField] float maxSpeed;
    bool isMovingRight;
    bool isMovingLeft;
    Transform rightEye;
    Transform leftEye;
    Rigidbody2D rbRight;
    Rigidbody2D rbLeft;

    void Awake()
    {
        controls = new PlayerControls();
        ControlsSetup();
        rightEye = transform.GetChild(0);
        leftEye = transform.GetChild(1);
        rbRight = rightEye.GetComponent<Rigidbody2D>();
        rbLeft = leftEye.GetComponent<Rigidbody2D>();
    }

    void ControlsSetup()
    {
        controls.Gameplay.RestartScene.performed += ctx => RestartScene();
        controls.Gameplay.RightEye.performed += ctx => rightEyeMove = ctx.ReadValue<Vector2>();
        controls.Gameplay.RightEye.performed += ctx => isMovingRight = true;
        controls.Gameplay.RightEye.canceled += ctx => rightEyeMove = Vector2.zero;
        controls.Gameplay.RightEye.canceled += ctx => isMovingRight = false;
        controls.Gameplay.LeftEye.performed += ctx => leftEyeMove = ctx.ReadValue<Vector2>();
        controls.Gameplay.LeftEye.performed += ctx => isMovingLeft = true;
        controls.Gameplay.LeftEye.canceled += ctx => leftEyeMove = Vector2.zero;
        controls.Gameplay.LeftEye.canceled += ctx => isMovingLeft = false;
    }

    void Update()
    {
        Vector2 mR = new Vector2(rightEyeMove.x, rightEyeMove.y) * accelerationRight * Time.deltaTime * speed;
        if (accelerationRight < maxSpeed) 
        {
            accelerationRight += 1;
        }

        rightEye.Translate(mR, Space.World);
        Vector2 mL = new Vector2(leftEyeMove.x, rightEyeMove.y) * accelerationLeft * Time.deltaTime * speed;
        if (accelerationLeft < maxSpeed)
        {
            accelerationLeft += 1;
        }
        leftEye.Translate(mL, Space.World);
    }

    void FixedUpdate()
    {
        if (isMovingRight == true)
        {
            rbRight.velocity = new Vector2(rightEyeMove.x, rightEyeMove.y);  
        }
        if (isMovingLeft == true)
        {
            rbLeft.velocity = new Vector2(leftEyeMove.x, leftEyeMove.y);
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Scene restarted");
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
