using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralController : MonoBehaviour
{
    public int currentIndex;    // 敵　中立　味方
    [SerializeField] private Material[] materials = new Material[3];

    private bool isColored;
    private bool isFar;
    [SerializeField] private float velocity;
    private Rigidbody rigidbody;
    private float resetTime;
    private float changedInterval;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 1;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Judgement.isStart)
        {
            if (Vector3.Distance(transform.position, Vector3.zero) < 3.0f)
            {
                rigidbody.AddForce(new Vector3(Random.Range(-5, 5), 0, Random.Range(-5,5)), ForceMode.Impulse);
            }
                    
            if(changedInterval > 0f) changedInterval -= Time.deltaTime;
            resetTime -= Time.deltaTime;
            if (resetTime <= 0) ResetColor();
            
            if (transform.position.y < 0f)
            {
                transform.position = new Vector3(Random.Range(-10, 10), 3, Random.Range(-10, 10));
            }
        }
        
    }

    public void UpdateColor(int add)
    {
        if (changedInterval <= 0f)
        {
            if(add < 0 && currentIndex > 0 || add > 0 && currentIndex < 2) currentIndex += add;
            gameObject.GetComponent<Renderer>().material = materials[currentIndex];
            isColored = true;
            changedInterval = 5f;
            resetTime = 30f;
        }
    }

    private void ResetColor()
    {
        currentIndex = 1;
        gameObject.GetComponent<Renderer>().material = materials[currentIndex];
        isColored = false;
        resetTime = 30f;
    }
}
