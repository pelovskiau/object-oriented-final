using UnityEngine;

public class TextScroll : MonoBehaviour
{
    private float speed = 12f; //not too fast
    private float stopY = 938f; //should go out of frame.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Go up until it hits the top.
        if(transform.position.y<stopY)
            transform.Translate(Vector3.up*Time.deltaTime*speed);
    }
}
