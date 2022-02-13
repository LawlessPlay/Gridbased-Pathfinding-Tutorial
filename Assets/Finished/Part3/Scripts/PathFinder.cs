using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace finished3
{
    public class PathFinder
    {
        private Dictionary<Vector2Int, OverlayTile> searchableTiles;

        public List<OverlayTile> FindPath(OverlayTile start, OverlayTile end, List<OverlayTile> inRangeTiles)
        {
            searchableTiles = new Dictionary<Vector2Int, OverlayTile>();

            List<OverlayTile> openList = new List<OverlayTile>();
            HashSet<OverlayTile> closedList = new HashSet<OverlayTile>();

            if (inRangeTiles.Count > 0)
            {
                foreach (var item in inRangeTiles)
                {
                    searchableTiles.Add(item.grid2DLocation, MapManager.Instance.map[item.grid2DLocation]);
                }
            }
            else
            {
                searchableTiles = MapManager.Instance.map;
            }

            openList.Add(start);

            while (openList.Count > 0)
            {
                OverlayTile currentOverlayTile = openList.OrderBy(x => x.F).First();

                openList.Remove(currentOverlayTile);
                closedList.Add(currentOverlayTile);

                if (currentOverlayTile == end)
                {
                    return GetFinishedList(start, end);
                }

                foreach (var tile in GetNeightbourOverlayTiles(currentOverlayTile))
                {
                    if (tile.isBlocked || closedList.Contains(tile) || Mathf.Abs(currentOverlayTile.transform.position.z - tile.transform.position.z) > 1)
                    {
                        continue;
                    }

                    tile.G = GetManhattenDistance(start, tile);
                    tile.H = GetManhattenDistance(end, tile);

                    tile.Previous = currentOverlayTile;


                    if (!openList.Contains(tile))
                    {
                        openList.Add(tile);
                    }
                }
            }

            return new List<OverlayTile>();
        }

        private List<OverlayTile> GetFinishedList(OverlayTile start, OverlayTile end)
        {
            List<OverlayTile> finishedList = new List<OverlayTile>();
            OverlayTile currentTile = end;

            while (currentTile != start)
            {
                finishedList.Add(currentTile);
                currentTile = currentTile.Previous;
            }

            finishedList.Reverse();

            return finishedList;
        }

        private int GetManhattenDistance(OverlayTile start, OverlayTile tile)
        {
            return Mathf.Abs(start.gridLocation.x - tile.gridLocation.x) + Mathf.Abs(start.gridLocation.y - tile.gridLocation.y);
        }

        private List<OverlayTile> GetNeightbourOverlayTiles(OverlayTile currentOverlayTile)
        {
            var map = MapManager.Instance.map;

            List<OverlayTile> neighbours = new List<OverlayTile>();

            //right
            Vector2Int locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x + 1,
                currentOverlayTile.gridLocation.y
            );

            if (searchableTiles.ContainsKey(locationToCheck))
            {
                neighbours.Add(searchableTiles[locationToCheck]);
            }

            //left
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x - 1,
                currentOverlayTile.gridLocation.y
            );

            if (searchableTiles.ContainsKey(locationToCheck))
            {
                neighbours.Add(searchableTiles[locationToCheck]);
            }

            //top
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x,
                currentOverlayTile.gridLocation.y + 1
            );

            if (searchableTiles.ContainsKey(locationToCheck))
            {
                neighbours.Add(searchableTiles[locationToCheck]);
            }

            //bottom
            locationToCheck = new Vector2Int(
                currentOverlayTile.gridLocation.x,
                currentOverlayTile.gridLocation.y - 1
            );

            if (searchableTiles.ContainsKey(locationToCheck))
            {
                neighbours.Add(searchableTiles[locationToCheck]);
            }

            return neighbours;
        }

      
    }
}