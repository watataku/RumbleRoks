using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
        // var position = player.position;
        // var tan = Mathf.Atan(position.z / position.x) * 180 / PI;
        // transform.rotation = Quaternion.Euler(45,tan,0);
    }
}
