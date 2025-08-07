using System;
using Ball;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Player
{
    public class CelData
    {
        public int x, y;
        public Vector2Int Pos;
        public FlowerController Flower;
        public BallController ball;

        public CelData(int x, int y, Vector2Int pos)
        {
            this.x = x;
            this.y = y;
            this.Pos = pos;
        }

        public void AddWater()
        {
            if (Flower != null)
            {
                Flower.AddWater();
            }
        }

        public void RemoveWater()
        {
            
        }
    }
    
    public class MapController : MonoBehaviour
    {
        public static MapController Instance;

        [SerializeField] private int width, height;
        [SerializeField] private Transform posOrigin;
        [SerializeField] private CelData[,] cellMap;
        
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private HoleController holeController;

        private void Awake()
        {
            Instance = this;
            Initialized();
        }

        private void Initialized()
        {
            cellMap = new CelData[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var cellPos = tilemap.WorldToCell(posOrigin.position + i * Vector3.up + j * Vector3.right);
                    cellMap[i, j] = new CelData(i, j, (Vector2Int)cellPos);
                }
            }
        }

        public CelData GetCellByWorldPosition(Vector3 worldPosition)
        {
            var cellPos = tilemap.WorldToCell(worldPosition);
            var cellOrigin = tilemap.WorldToCell(posOrigin.position);
            return cellMap[cellPos.y - cellOrigin.y, cellPos.x - cellOrigin.x];
        }

        public Vector3 GetPosCellByPosWorld(Vector3 posWorld)
        {
            return tilemap.CellToWorld(tilemap.WorldToCell(posWorld)) + new Vector3(0.5f, 0.5f, tilemap.CellToWorld(tilemap.WorldToCell(posWorld)).y);
        }

        public Vector3Int GetPosCell(Vector3 posWorld)
        {
            return tilemap.WorldToCell(posWorld);
        }

        public bool CheckHole(Vector3 worldPosition)
        {
            if (tilemap.WorldToCell(worldPosition) == tilemap.WorldToCell(holeController.transform.position))
            {
                SpawnNewHole();
                return true;
            }
            else
                return false;
        }

        public (Vector2, Vector2) GetSymmetryPoint(Vector2 origin, Vector2 target)
        {
            Vector3Int posCellOrigin = tilemap.WorldToCell(origin);
            Vector3Int posCellTarget = tilemap.WorldToCell(target);

            Vector3Int posOutOrigin = posCellOrigin;
            Vector3Int posOutTarget = posCellTarget;
            
            if (target.x > 13)
            {
                //12
                posOutOrigin = new Vector3Int(25 - posCellOrigin.x, posOutOrigin.y, 0);
                posOutTarget = new Vector3Int(25 - posCellTarget.x, posOutTarget.y, 0);
            }

            if (target.x < -13)
            {
                //-13
                posOutOrigin = new Vector3Int(-27 - posCellOrigin.x, posOutOrigin.y, 0);
                posOutTarget = new Vector3Int(-27 - posCellTarget.x, posOutTarget.y, 0);
            }

            if (target.y > 9)
            {
                //8
                posOutOrigin = new Vector3Int(posCellOrigin.x, 17 - posOutOrigin.y, 0);
                posOutTarget = new Vector3Int(posCellTarget.x, 17 - posOutTarget.y, 0);
            }

            if (target.y < -9)
            {
                //-9
                posOutOrigin = new Vector3Int(posCellOrigin.x, -19 - posOutOrigin.y, 0);
                posOutTarget = new Vector3Int(posCellTarget.x, -19 - posOutTarget.y, 0);
            }

            return (tilemap.CellToWorld(posOutOrigin) + new Vector3(0.5f, 0.5f, 0),
                tilemap.CellToWorld(posOutTarget) + new Vector3(0.5f, 0.5f, 0));
        }

        private void SpawnNewHole()
        {
            holeController.PlayAnimationWin();
        }
    }
}
