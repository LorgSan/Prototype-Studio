using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovementScript : MonoBehaviour
{
    [SerializeField] private float RotateSpeed = 5f;
    [SerializeField]  private float Radius = 0.1f;

    private Vector2 _centre;
    private float _angle;
    bool DoCircle = false;
    bool DoLine = false;
    public bool allowedToMove = true;
    Vector3 currentTarget;
    [SerializeField] float Speed;
    [SerializeField] AudioClip slap;
    [SerializeField] Sprite killSprite;
    SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        DirectMovementSetup();
    }

    private void Update()
    {
        if (allowedToMove == true)
        {
            if (DoCircle == true)
                {
                    CircleMovement();
                }
                if (DoLine == true)
                {
                    DirectMovement(currentTarget);
                }
        }

    }

    public void KillFly()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        sr.sprite = killSprite;
        audioSource.Stop();
        audioSource.PlayOneShot(slap);
    }

    void CircleMovement()
    {
        _angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        transform.position = _centre + offset;
    }

    void DirectMovement(Vector3 target)
    {
        float scaleSpeed = Speed * Time.deltaTime;
        float angle = Mathf.Atan2(target.y - transform.position.y, target.x -transform.position.x ) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1000f * Time.deltaTime);
        Vector3 newVector = Vector3.MoveTowards(transform.position, target, scaleSpeed);
        transform.position = newVector;
        if (transform.position == target)
        {
            StartCoroutine("Circle");
            DoLine = false;
        }
    }

    void DirectMovementSetup()
    {
        float x = Random.Range(-6.5f, 6.5f);
        float y = Random.Range(-1.5f, 3.6f);
        Vector3 newDirection = new Vector3(x, y, 0);
        currentTarget = newDirection;
        DoLine = true;
    }

    IEnumerator Circle()
    {
        _centre = transform.position;
        
        float time = Random.Range(1f, 4f);
        DoCircle = true;
        yield return new WaitForSeconds(time);
        DoCircle = false;
        DirectMovementSetup();
    }

}
