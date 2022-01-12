using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MouseControllerFinished : MonoBehaviour
{
    public GameObject cursor;

    RaycastHit2D hit;

    void LateUpdate()
    {
        var hitValue = LevelManager.Instance.GetClickedOnTile();
        if (hitValue.HasValue)
        {
            hit = hitValue.Value;
            cursor.transform.position = hit.collider.transform.position;
            cursor.gameObject.GetComponent<SpriteRenderer>().sortingOrder = hit.collider.transform.GetComponent<SpriteRenderer>().sortingOrder;
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider != null)
                {
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                }
            }
        }
    }
}
