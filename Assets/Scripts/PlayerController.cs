using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public RoadGenerator _roadGenerator;
    [SerializeField] float _laneOffset;
    [SerializeField] int _widthRoad;
    [SerializeField] float _speedX;
    [SerializeField] float _jumpPower;
    [SerializeField] float _jumpGravity;
    [SerializeField] float _realGravity;
    [SerializeField] float _euleryY; // текущий угол поворота;
    Rigidbody rb;
    Vector3 startGamePosition;
    Vector3 startEulerAngles;

    public GameObject _left;
    public GameObject _right;
    public GameObject _up;

    bool left;
    bool right;
    bool up;
    float pointStart;
    float pointFinish;
    bool isJumping = false;
    bool flagLeft = true;
    bool flagRight = true;
    bool flagUp = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startGamePosition = transform.position;
        pointFinish = startGamePosition.x;//transform.position.x;
        startEulerAngles = transform.eulerAngles;

        showControl();
    }
    public void hideControl()
    {
        _left.SetActive(false);
        _right.SetActive(false);
        _up.SetActive(false);
    }
    public void showControl()
    {
        _left.SetActive(true);
        _right.SetActive(true);
        _up.SetActive(true);
    }
    public void Left()
    {
        if (_roadGenerator.currentSpeed > 0)
        left = true;
    }
    public void Right()
    {
        if (_roadGenerator.currentSpeed > 0)
        right = true;
    }
    public void Up()
    {
        if (_roadGenerator.currentSpeed > 0)
        up = true;
    }

    void FixedUpdate()
    {
        if (_roadGenerator.currentSpeed > 0)
        {

            if (transform.position.y < 0.5f && flagLeft && pointFinish > 0 && (Input.GetKey(KeyCode.LeftArrow) || left))
            {
                MoveHorizontal(-_speedX);
                flagLeft = false;

            }
            if (Input.GetKey(KeyCode.LeftArrow) == false) flagLeft = true;

            if (transform.position.y < 0.5f && flagRight && pointFinish < _widthRoad && (Input.GetKey(KeyCode.RightArrow) || right))
            {
                MoveHorizontal(_speedX);
                flagRight = false;
            }
            if (Input.GetKey(KeyCode.RightArrow) == false) flagRight = true;

            if (flagUp && Input.GetKey(KeyCode.UpArrow) && isJumping == false || up && isJumping == false)
            {
                flagUp = false;
                Jump();
                up = false;
            }

            if (Input.GetKey(KeyCode.UpArrow) == false) flagUp = true;

            else up = false;
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Space))
        {
            hideControl();
        }
    }

    void MoveHorizontal(float speed)
    {
        pointStart = pointFinish;
        pointFinish += Mathf.Sign(speed) * _laneOffset;

        if (Mathf.Sign(speed) < 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -_euleryY, transform.eulerAngles.z);
        }
        else if (Mathf.Sign(speed) > 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, _euleryY * 2, transform.eulerAngles.z);
        };

        transform.position = new Vector3(pointStart, transform.position.y, transform.position.z);

        StartCoroutine(MoveCoroutine(speed));
        left = false;
        right = false;
    }

    IEnumerator MoveCoroutine(float vectorX)
    {
        while (Mathf.Abs(pointStart - transform.position.x) < _laneOffset)
        {
            rb.velocity = new Vector3(vectorX, rb.velocity.y, 0);
            //выполняем ожидание FixedUpdate()
            yield return new WaitForFixedUpdate();
            // и после события

        }
        rb.velocity = Vector3.zero;
        transform.eulerAngles = startEulerAngles;
    }

    public void ResetGame()
    {
        transform.position = startGamePosition;
        GetComponent<RoadGenerator>().ResetLevel();
    }

    void Jump()
    {
        isJumping = true;
        rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        Physics.gravity = new Vector3(0, _jumpGravity, 0);
        StartCoroutine(StopJumpCoroutine());
    }

    IEnumerator StopJumpCoroutine()
    {
        do
        {
            // запускаем паузы ожидания по 20 мс
            yield return new WaitForSeconds(0.02f);
        } while (rb.velocity.y != 0);
        //после чего
        isJumping = false;
        Physics.gravity = new Vector3(0, _realGravity, 0);
        transform.eulerAngles = startEulerAngles;
    }
}
