using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject motherNeutral;

    private int childCount;
    private Rigidbody rigidbody;

    private Vector3 targetPosition;
    private Vector3 targetDirection;
    private bool isTargetDefined;

    private float interval;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        childCount = motherNeutral.gameObject.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Judgement.isStart)
        {
            if (!isTargetDefined || interval < 0f)
            {
                var target = UnityEngine.Random.Range(0, childCount + 1);
                // Debug.Log("Next destination is " + target);
                if (target < childCount)
                {
                    targetPosition = motherNeutral.transform.GetChild(target).transform.position;
                    targetDirection = targetPosition - transform.position;
                }
                else
                {
                    targetPosition = player.transform.position;
                    targetDirection = targetPosition - transform.position;
                    targetDirection.y = 0f;
                }
                isTargetDefined = true;
                interval = 10f;
            }
            else
            {
                if (Vector3.Distance(targetPosition, transform.position) < 0.5f)
                {
                    isTargetDefined = false;
                }
                rigidbody.AddForce(targetDirection);
            }

            interval -= Time.deltaTime;
            if (transform.position.y < 0f)
            {
                transform.position = new Vector3(UnityEngine.Random.Range(-10, 10), 3, UnityEngine.Random.Range(-10, 10));
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Neutral"))
        {
            var neutralController = other.gameObject.GetComponent<NeutralController>();
            neutralController.UpdateColor(-1);
        }
        isTargetDefined = false;
    }
}