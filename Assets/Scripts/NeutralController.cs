using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralController : MonoBehaviour
{
    public int currentIndex;    // 敵　中立　味方
    [SerializeField] private Material[] materials = new Material[3];
    
    private bool isColored;
    [SerializeField] private float velocity;
    private Rigidbody rigidbody;
    private float time;
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
        rigidbody.AddForce(transform.forward * velocity);
        if(changedInterval > 0f) changedInterval -= Time.deltaTime;
        time -= Time.deltaTime;
        if (time <= 0) ResetColor();

        if (transform.position.y < 0f)
        {
            transform.position = new Vector3(Random.Range(-4, 4), 5, Random.Range(-4, 4));
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
            time = 30f;
        }
    }

    private void ResetColor()
    {
        currentIndex = 1;
        gameObject.GetComponent<Renderer>().material = materials[currentIndex];
        isColored = false;
        time = 30f;
    }
}
