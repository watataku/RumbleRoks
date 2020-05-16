using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velocity;
    
    [SerializeField] private Material myMaterial;
    [SerializeField] private Material hurtedMaterial;
    
    private Rigidbody rigidbody;
    private bool isMovable;


    // Start is called before the first frame update
    void Start()
    {
        isMovable = true;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Judgement.isStart && isMovable)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rigidbody.AddForce(new Vector3(0, 0, velocity));
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                rigidbody.AddForce(new Vector3(0, 0, -velocity));
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                rigidbody.AddForce(new Vector3(velocity, 0, 0));
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rigidbody.AddForce(new Vector3(-velocity, 0, 0));
            }

            if (transform.position.y < 0f)
            {
                transform.position = new Vector3(UnityEngine.Random.Range(-4, 4), 5, UnityEngine.Random.Range(-4, 4));
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Neutral"))
        {
            var neutralController = other.gameObject.GetComponent<NeutralController>();
            neutralController.UpdateColor(1);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine("Damaged");
        }
    }

    private IEnumerator Damaged()
    {
        Renderer renderer = GetComponent<Renderer>();
        isMovable = false;
        renderer.material = hurtedMaterial; 
        yield return new WaitForSeconds(3);
        renderer.material = myMaterial;
        isMovable = true;
    }
}
