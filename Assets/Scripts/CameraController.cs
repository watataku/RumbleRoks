using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 cameraPosition;
    private float movableRange;
    [SerializeField] private Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
       cameraPosition = transform.position;
       offset = cameraPosition - player.transform.position;
       movableRange = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = player.transform.position + offset;
        if (pos.x > cameraPosition.x + movableRange) pos.x = cameraPosition.x + movableRange;
        else if (pos.x < cameraPosition.x - movableRange) pos.x = cameraPosition.x - movableRange;
        if (pos.z > cameraPosition.z + movableRange) pos.z = cameraPosition.z + movableRange;
        else if (pos.z < cameraPosition.z - movableRange) pos.z = cameraPosition.z - movableRange;
        
        transform.position = pos;
        
        // var position = player.position;
        // var tan = Mathf.Atan(position.z / position.x) * 180 / PI;
        // transform.rotation = Quaternion.Euler(45,tan,0);
    }
}
