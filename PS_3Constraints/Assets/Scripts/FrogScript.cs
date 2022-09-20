using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrogScript : MonoBehaviour
{
    PlayerControls controls;
    Vector2 rightEyeMove;
    Vector2 leftEyeMove;

    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float accelerationRight;
    [SerializeField] float accelerationLeft;
    [SerializeField] float maxSpeed;
    bool isMovingRight;
    bool isMovingLeft;

    [Header("Reference Objects")]
    [SerializeField] Transform rightEyeTarget;
    [SerializeField] Transform leftEyeTarget;
    Rigidbody2D rbRight;
    Rigidbody2D rbLeft;
    [SerializeField] Transform rightEye;
    [SerializeField] Transform leftEye;
    CircleCollider2D rightEyeCollider;
    CircleCollider2D leftEyeCollider;
    Vector3 rightEyePoint;
    Vector3 leftEyePoint;
    Vector3 rightTargetPos;
    Vector3 leftTargetPos;
    Transform rightEyeSprite;
    Transform leftEyeSprite;

    void Awake()
    {
        controls = new PlayerControls();
        ControlsSetup();
        rbRight = rightEyeTarget.GetComponent<Rigidbody2D>();
        rbLeft = leftEyeTarget.GetComponent<Rigidbody2D>();
        rightEyeCollider = rightEye.GetComponent<CircleCollider2D>();
        leftEyeCollider = leftEye.GetComponent<CircleCollider2D>();
        rightEyeSprite = rightEye.GetChild(0);
        leftEyeSprite = leftEye.GetChild(0);

    }

    void ControlsSetup()
    {
        controls.Gameplay.RestartScene.performed += ctx => RestartScene();
        controls.Gameplay.RightEye.performed += ctx => rightEyeMove = ctx.ReadValue<Vector2>();
        controls.Gameplay.RightEye.performed += ctx => isMovingRight = true;
        controls.Gameplay.RightEye.canceled += ctx => rightEyeMove = Vector2.zero;
        controls.Gameplay.RightEye.canceled += ctx => isMovingRight = false;
        controls.Gameplay.RightEye.canceled += ctx => accelerationRight = 1;
        controls.Gameplay.LeftEye.performed += ctx => leftEyeMove = ctx.ReadValue<Vector2>();
        controls.Gameplay.LeftEye.performed += ctx => isMovingLeft = true;
        controls.Gameplay.LeftEye.canceled += ctx => leftEyeMove = Vector2.zero;
        controls.Gameplay.LeftEye.canceled += ctx => isMovingLeft = false;
         controls.Gameplay.LeftEye.canceled += ctx => accelerationLeft = 1;
    }

    void Update()
    {

        EyePosCalc();
        //Vector2 mR = new Vector2(Mathf.Clamp(rightEyeMove.x, -6, 7), Mathf.Clamp(rightEyeMove.y, -1f, 8)) * accelerationRight * Time.deltaTime * speed;
        float clampedRightX = Mathf.Clamp(leftEyeMove.x, -6, 7);
        float clampedRightY = Mathf.Clamp(leftEyeMove.y, -1f, 8);
        Vector2 mR = new Vector2(clampedRightX, clampedRightY) * accelerationRight * Time.deltaTime * speed;
        if (accelerationRight < maxSpeed) 
        {
            accelerationRight += 0.1f;
        }

        rightEyeTarget.Translate(mR, Space.World);
        //Vector2 mL = new Vector2(Mathf.Clamp(leftEyeMove.x, -6, 7), Mathf.Clamp(rightEyeMove.y, -1f, 8)) * accelerationLeft * Time.deltaTime * speed;
        float clampedLeftX = Mathf.Clamp(rightEyeMove.x, -6, 7);
        float clampedLeftY = Mathf.Clamp(rightEyeMove.y, -1f, 8);
        Vector2 mL = new Vector2(clampedLeftX, clampedLeftY) * accelerationLeft * Time.deltaTime * speed;
        if (accelerationLeft < maxSpeed)
        {
            accelerationLeft += 0.1f;
        }
        leftEyeTarget.Translate(mL, Space.World);
    }

    void FixedUpdate()
    {
        // if (isMovingRight == true)
        // {
        //     rbRight.velocity = new Vector2(rightEyeMove.x, rightEyeMove.y);  
        // }
        // if (isMovingLeft == true)
        // {
        //     rbLeft.velocity = new Vector2(leftEyeMove.x, leftEyeMove.y);
        // }
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

    void EyePosCalc()   
    {
        Vector3 rightTargetPos = rightEyeTarget.position;
        Vector3 leftTargetPos = leftEyeTarget.position;
        rightEyePoint = rightEyeCollider.ClosestPoint(rightTargetPos);
        leftEyePoint = leftEyeCollider.ClosestPoint(leftTargetPos);
        leftEyeSprite.position = leftEyePoint;
        rightEyeSprite.position = rightEyePoint;
    }
}
