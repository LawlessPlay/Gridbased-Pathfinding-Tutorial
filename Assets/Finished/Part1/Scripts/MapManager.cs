using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace finished1
{
    public class MapManager : MonoBehaviour
    {
        private static MapManager _instance;
        public static MapManager Instance { get { return _instance; } }

        public GameObject overlayPrefab;
        public GameObject overlayContainer;

        public float littleBump;

        public Dictionary<Vector2Int, GameObject> map;

        private void Awake()
        {
            if(_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else
            {
                _instance = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            littleBump = 0.0003f;
            var tileMap = gameObject.GetComponentInChildren<Tilemap>();
            map = new Dictionary<Vector2Int, GameObject>();

            BoundsInt bounds = tileMap.cellBounds;

            for (int z = bounds.max.z; z > bounds.min.z; z--)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    for (int x = bounds.min.x; x < bounds.max.x; x++)
                    {
                        var tileLocation = new Vector3Int(x, y, z);
                        var tileKey = new Vector2Int(x, y);
                        if (tileMap.HasTile(tileLocation) && !map.ContainsKey(tileKey))
                        {
                            var overlayTile = Instantiate(overlayPrefab, overlayContainer.transform);
                            var cellWorldPosition = tileMap.GetCellCenterWorld(tileLocation);
                            overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z+1);
                            overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder;
                            map.Add(tileKey, overlayTile);
                        }
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
