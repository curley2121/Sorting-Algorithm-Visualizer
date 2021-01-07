using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        main.label.text = "" + (transform.localScale.y * 20);
        main.lFade = 1;
        main.label.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 3, 0));
    }
}
