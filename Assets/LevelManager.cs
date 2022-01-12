using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    public static LevelManager Instance { get { return _instance; } }

    public Dictionary<Vector2Int, GameObject> map;

    public GameObject characterPrefab;
    private GameObject character;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        map = new Dictionary<Vector2Int, GameObject>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(character == null)
            {
               var tile = GetClickedOnTile();
                if(tile.HasValue)
                    SetInitialCharacterPosition(tile.Value.collider);
            }
        }
    }

    public void SetMap(Dictionary<Vector2Int, GameObject> map)
    {
        this.map = map;
    }

    public void SetInitialCharacterPosition(Collider2D tileAtMousePos)
    {
        character = Instantiate(characterPrefab);
        character.transform.position = tileAtMousePos.transform.position;
        character.GetComponent<SpriteRenderer>().sortingOrder = tileAtMousePos.transform.GetComponent<SpriteRenderer>().sortingOrder+1;
    }

    public RaycastHit2D? GetClickedOnTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

        if (hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }

        return null;
    }

    public void RenderOverlay(int range, Vector2Int location)
    {
        List<Vector2Int> mapTiles = new List<Vector2Int>();
        mapTiles = map.Keys.ToList();

        List<Vector2Int> inRangeTiles = new List<Vector2Int>();
        inRangeTiles.Add(location);

        int reach = 0;
        foreach (var tile in inRangeTiles)
        {
            if(reach < range)
            {
                //get neightbours

            }
        }
    }
}
