using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace finished1
{
    public class OverlayTile : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            }
        }
    }
}
