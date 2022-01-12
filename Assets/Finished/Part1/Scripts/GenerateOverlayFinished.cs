using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace part1
{
    public class GenerateOverlayFinished : MonoBehaviour
    {
        public GameObject overlayPrefab;
        public GameObject overlayContainer;

        // Start is called before the first frame update
        void Start()
        {
            var tileMaps = gameObject.transform.GetComponentsInChildren<Tilemap>().OrderByDescending(x => x.GetComponent<TilemapRenderer>().sortingOrder);
            var map = new Dictionary<Vector2Int, GameObject>();

            foreach (var tm in tileMaps)
            {
                BoundsInt bounds = tm.cellBounds;

                for (int z = bounds.max.z; z > bounds.min.z; z--)
                {
                    for (int y = bounds.min.y; y < bounds.max.y; y++)
                    {
                        for (int x = bounds.min.x; x < bounds.max.x; x++)
                        {
                            if (tm.HasTile(new Vector3Int(x, y, z)))
                            {
                                if (!map.ContainsKey(new Vector2Int(x, y)))
                                {
                                    var overlayTile = Instantiate(overlayPrefab, overlayContainer.transform);
                                    overlayTile.transform.position = tm.GetCellCenterWorld(new Vector3Int(x, y, z));
                                    overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tm.GetComponent<TilemapRenderer>().sortingOrder + 1;

                                    map.Add(new Vector2Int(x, y), overlayTile);
                                }
                            }
                        }
                    }
                }
            }

            LevelManager.Instance.SetMap(map);
        }
    }
}
