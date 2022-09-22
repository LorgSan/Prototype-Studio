using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FrogScript : GenericSingletonClass<FlyScript>
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
    CircleCollider2D rightEyeCollider;
    CircleCollider2D leftEyeCollider;
    Vector3 rightEyePoint;
    Vector3 leftEyePoint;
    Vector3 rightTargetPos;
    Vector3 leftTargetPos;
    Transform rightEyeSprite;
    Transform leftEyeSprite;

    [SerializeField] Text scoreText;
    float score;
    public FlyScript fly;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    [Header("Sprite Reference")]
    SpriteRenderer sr;
    Sprite normalSprite;
    [SerializeField] Sprite catchSprite;
    [SerializeField] AudioClip ribbit;
    [SerializeField] TongueScript tongue;
 
    public override void Awake()
    {
        base.Awake();
        controls = new PlayerControls();
        ControlsSetup();
        rightEyeCollider = rightEye.GetComponent<CircleCollider2D>();
        leftEyeCollider = leftEye.GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        normalSprite = sr.sprite;
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

        private void Start()
    {
        var spriteSize = rightEyeTarget.GetComponent<SpriteRenderer>().bounds.size.x * .5f; // Working with a simple box here, adapt to you necessity

        var cam = Camera.main;// Camera component to get their size, if this change in runtime make sure to update values
        var camHeight = cam.orthographicSize;
        var camWidth = cam.orthographicSize * cam.aspect;

        yMin = -camHeight + spriteSize; // lower bound
        yMax = camHeight - spriteSize; // upper bound
        
        xMin = -camWidth + spriteSize; // left bound
        xMax = camWidth - spriteSize; // right bound 
    }

    void Update()
    {

        EyePosCalc();
        float clampedRightX = Mathf.Clamp(leftEyeMove.x, xMin, xMax);
        float clampedRightY = Mathf.Clamp(leftEyeMove.y, yMin, yMax);
        Vector2 mR = new Vector2(clampedRightX, clampedRightY) * Time.deltaTime * speed;

        rightEyeTarget.Translate(mR, Space.World);
        float clampedLeftX = Mathf.Clamp(rightEyeMove.x, xMin, xMax);
        float clampedLeftY = Mathf.Clamp(rightEyeMove.y, yMin, yMax);
        Vector2 mL = new Vector2(clampedLeftX, clampedLeftY) * Time.deltaTime * speed;
        leftEyeTarget.Translate(mL, Space.World);
    }

    void RestartScene()
    {
        Destroy(GameManager.Instance);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void CheckFly()
    {
        if(fly.RightEye == true && fly.LeftEye == true)
        {
            StartCoroutine("SpriteChange");
            GameManager.Instance.highScore++;
            score++;
            scoreText.text = score.ToString();
        }
    }

    IEnumerator SpriteChange()
    {
        sr.sprite = catchSprite;
        FlyMovementScript flyMove = fly.gameObject.GetComponent<FlyMovementScript>();
        flyMove.allowedToMove = false;
        flyMove.KillFly();
        tongue.catchHappened = true;
        yield return new WaitForSeconds(0.5f);
        sr.sprite = normalSprite;
        SpawnNewFly();
    }

    void SpawnNewFly()
    {
        GameObject newFly = Instantiate(Resources.Load("Fly") as GameObject);
        int RandomDecide = Random.Range (0, 3);
        switch (RandomDecide)
        {
            case 0:
                    newFly.transform.position = new Vector2 (-7.5f, Random.Range(-1.5f, 3.6f));
                break;
            case 1:
                    newFly.transform.position = new Vector2 (7.5f, Random.Range(-1.5f, 3.6f));
                break;
            case 2:
                    newFly.transform.position = new Vector2 (Random.Range(-7.5f, 7.5f), 5.5f);
                break;
        }
        FlyScript oldFly = fly;
        fly = newFly.GetComponent<FlyScript>();
        GetComponent<AudioSource>().PlayOneShot(ribbit);
        tongue.target = newFly.transform;
        tongue.catchHappened = false;
        tongue.ResetTongue();
        Destroy(oldFly.gameObject);
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
