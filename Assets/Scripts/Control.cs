using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public UnityEngine.UI.Text time, health , gameoverText;
    public UnityEngine.UI.Button tryButton;
    private Rigidbody rb;
    public float speed = 1.5f;
    float timeCount = 20;
    int healthCount = 3;
    bool gameCont = true;
    void Start()
    {
        time.text = (int)timeCount + "";
        health.text = healthCount + "";
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (timeCount>0 && gameCont)
        {
            timeCount -= Time.deltaTime;
            time.text = (int)timeCount + "";
        }
        if (timeCount<=0)
        {
            gameCont = false;
        }
    }

    private void FixedUpdate()
    {
        if (gameCont)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            rb.AddForce(new Vector3(-vertical, 0, horizontal) * speed);
        }
        else if(!gameCont)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameoverText.gameObject.SetActive(true);
            tryButton.gameObject.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        string objName = collision.gameObject.name;
        if (objName.Equals("exitPoint"))
        {
            gameoverText.gameObject.SetActive(true);
            gameoverText.text = "Level Up!";
        }
        else if(!objName.Equals("mazeFloor") && !objName.Equals("floor"))
        {
            healthCount -= 1;
            health.text = healthCount + "";
            if (healthCount <= 0 )
            {
                gameCont = false;
            }
        }
    }
}
