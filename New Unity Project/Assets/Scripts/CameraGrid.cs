using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGrid : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)  //very bad, definitely needs to be improved
    {
        int multX = 1;
        if (collision.transform.position.x < transform.position.x)
        {
            multX = -1;
        }
        if (collision.CompareTag("Player"))
        {
            float x = (collision.gameObject.transform.position.x + 13.25f * multX);
            transform.position = new Vector3(x, 0, -10);
        }
    }
}
