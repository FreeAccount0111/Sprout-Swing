using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Player
{
    public class MapController : MonoBehaviour
    {
        public static MapController Instance;
        
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private HoleController holeController;

        private void Awake()
        {
            Instance = this;
        }

        public Vector3 GetPosCellByPosWorld(Vector3 posWorld)
        {
            return tilemap.CellToWorld(tilemap.WorldToCell(posWorld)) + new Vector3(0.5f, 0.5f, 0);
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
