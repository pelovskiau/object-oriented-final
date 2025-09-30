using UnityEngine;

public class TextScroll : MonoBehaviour
{
    private float speed = 12f;
    private float stopY = 938f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<stopY)
            transform.Translate(Vector3.up*Time.deltaTime*speed);
    }
}
