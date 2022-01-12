using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOverlayOnClickFinished : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
        }
    }

}
