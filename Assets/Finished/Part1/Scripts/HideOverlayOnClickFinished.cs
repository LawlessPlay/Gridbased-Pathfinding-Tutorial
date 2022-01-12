using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace part1
{
    public class HideOverlayOnClickFinished : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            }
        }

    }
}
