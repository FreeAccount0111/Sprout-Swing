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

        private void SpawnNewHole()
        {
            holeController.PlayAnimationWin();
        }
    }
}
