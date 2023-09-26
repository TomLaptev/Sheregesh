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
    bool flagLeft = true;
    bool flagRight = true;
    bool flagUp =  true;
    bool allowUp = true;
    bool flagTime = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startGamePosition = transform.position;
        pointFinish = startGamePosition.x; //transform.position.x;
        startEulerAngles = transform.eulerAngles;
        
        if (!Progress.Instance.isDesktop)
        {
           showControl(); 
        }
        Physics.gravity = new Vector3(0, _realGravity, 0);
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
        if (_roadGenerator.currentSpeed > 0 && allowUp)
            up = true;
            allowUp = false;
            Invoke("AllowUp", 0.5f);
    }

     void AllowUp()
    {
        if (_roadGenerator.currentSpeed == 0)
            allowUp = true;
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

            if (transform.position.y < 0.5f && flagRight && pointFinish < _widthRoad && (Input.GetKey(KeyCode.RightArrow) || right))
            {
                MoveHorizontal(_speedX);
                flagRight = false;
            }

            if (flagUp && Input.GetKey(KeyCode.UpArrow) && transform.position.y < 0.1f || up && transform.position.y < 0.1f)
            {
                flagUp = false;
                up = false;
                Jump();                
            }

             if (Input.GetKey(KeyCode.UpArrow) == false) flagUp = true;

        }            
        if (flagTime)
        {
            flagTime = false;
            Invoke("CorrectPosition", 0.5f);
        }

    }

    void CorrectPosition()
    {
        if (_roadGenerator.currentSpeed > 0 && Mathf.Abs(pointFinish - transform.position.x) > 0.5f)
        {
            transform.position = new Vector3(pointFinish, transform.position.y, transform.position.z);
        }
        flagTime = true;
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
        yield return new WaitForSeconds(0.02f);
        flagLeft = true;
        flagRight = true;
    }

    public void ResetGame()
    {
        transform.position = startGamePosition;
        GetComponent<RoadGenerator>().ResetLevel();
    }

    void Jump()
    {
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
        Physics.gravity = new Vector3(0, _realGravity, 0);
        allowUp = true;
        transform.eulerAngles = startEulerAngles;
    }

}
