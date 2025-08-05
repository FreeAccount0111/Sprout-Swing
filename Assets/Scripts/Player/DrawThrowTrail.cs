using System;
using UnityEngine;

namespace Player
{
    public class DrawThrowTrail : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private LineRenderer line1;

        public void UpdateLine(float a,float b,float c,Vector2 start,Vector2 end)
        {
            int amount = Mathf.RoundToInt(Vector2.Distance(start, end)) + 2;
            lineRenderer.positionCount = amount + 1;

            for (int i = 0; i < amount + 1; i++)
            {
                float x = Mathf.Lerp(start.x, end.x, (float)i / amount);
                float y = a * (x - b) * (x - b) + c;

                lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            }

            Vector2 dir = (end - start).y < -(end - start).y
                ? new Vector2(-(end - start).y, (end - start).x).normalized
                : new Vector2((end - start).y, -(end - start).x).normalized;
            line1.SetPosition(0, Vector2.Lerp(start, end, 0.5f));
            line1.SetPosition(1,
                Vector2.Lerp(start, end, 0.5f) + new Vector2(end.y - start.y, start.x - end.x).normalized);
        }
    }
}
