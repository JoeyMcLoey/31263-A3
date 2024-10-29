using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public Transform connection;
    
    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update(){}

    private void OnTriggerEnter2D(Collider2D collision){
        Vector3 position = collision.transform.position;
        position.x = connection.position.x;
        position.y = connection.position.y;
        collision.transform.position = position;
    }
}
