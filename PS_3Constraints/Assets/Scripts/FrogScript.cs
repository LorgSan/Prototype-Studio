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
    [SerializeField] float maxSpeed;

    [Header("Reference Objects")]
    [SerializeField] Transform rightEyeTarget;
    [SerializeField] Transform leftEyeTarget;
    [SerializeField] Transform rightEye;
    [SerializeField] Transform leftEye;

    FlyScript flyScript;
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
        rightEyeCollider = rightEye.GetComponent<CircleCollider2D>();
        leftEyeCollider = leftEye.GetComponent<CircleCollider2D>();
        flyScript = FlyScript.FindInstance();
        rightEyeSprite = rightEye.GetChild(0);
        leftEyeSprite = leftEye.GetChild(0);

    }

    void ControlsSetup()
    {
        controls.Gameplay.RestartScene.performed += ctx => RestartScene();
        controls.Gameplay.RightEye.performed += ctx => rightEyeMove = ctx.ReadValue<Vector2>();
        controls.Gameplay.RightEye.canceled += ctx => rightEyeMove = Vector2.zero;
        controls.Gameplay.LeftEye.performed += ctx => leftEyeMove = ctx.ReadValue<Vector2>();
        controls.Gameplay.LeftEye.canceled += ctx => leftEyeMove = Vector2.zero;
        controls.Gameplay.Catch.performed += ctx => CheckFly(); 
    }

    void Update()
    {

        EyePosCalc();
        float clampedRightX = Mathf.Clamp(leftEyeMove.x, -6, 7);
        float clampedRightY = Mathf.Clamp(leftEyeMove.y, -1f, 8);
        Vector2 mR = new Vector2(clampedRightX, clampedRightY) * Time.deltaTime * speed;

        rightEyeTarget.Translate(mR, Space.World);
        float clampedLeftX = Mathf.Clamp(rightEyeMove.x, -6, 7);
        float clampedLeftY = Mathf.Clamp(rightEyeMove.y, -1f, 8);
        Vector2 mL = new Vector2(clampedLeftX, clampedLeftY) * Time.deltaTime * speed;
        leftEyeTarget.Translate(mL, Space.World);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void CheckFly()
    {
        if (flyScript.RightEye == true && flyScript.LeftEye == true)
        {
            Debug.Log("yeah double hit");
        }
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
