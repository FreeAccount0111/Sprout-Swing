using UnityEngine;

namespace Gameplay
{
    public class ParabolaThroughPoints
    {
        public static (float a, float b, float c, Vector2 B, Vector2 Vertex)[] FindParabolas(Vector2 A, Vector2 C,
            float distance = 1f)
        {
            Vector2 M = (A + C) / 2f;
            Vector2 AC = C - A;
            Vector2 n = new Vector2(-AC.y, AC.x).normalized;

            Vector2 B1 = M + n * distance;
            Vector2 B2 = M - n * distance;

            var p1 = FitParabola(A, B1, C);
            var p2 = FitParabola(A, B2, C);

            return new (float, float, float, Vector2, Vector2)[]
            {
                (p1.a, p1.b, p1.c, B1, CalcVertex(p1.a, p1.b, p1.c)),
                (p2.a, p2.b, p2.c, B2, CalcVertex(p2.a, p2.b, p2.c))
            };
        }

        private static (float a, float b, float c) FitParabola(Vector2 A, Vector2 B, Vector2 C)
        {
            float[,] mat =
            {
                { A.x * A.x, A.x, 1 },
                { B.x * B.x, B.x, 1 },
                { C.x * C.x, C.x, 1 }
            };

            float[] rhs = { A.y, B.y, C.y };

            float det = Determinant3x3(mat);
            float detA = Determinant3x3(new float[,]
            {
                { rhs[0], mat[0, 1], mat[0, 2] },
                { rhs[1], mat[1, 1], mat[1, 2] },
                { rhs[2], mat[2, 1], mat[2, 2] }
            });
            float detB = Determinant3x3(new float[,]
            {
                { mat[0, 0], rhs[0], mat[0, 2] },
                { mat[1, 0], rhs[1], mat[1, 2] },
                { mat[2, 0], rhs[2], mat[2, 2] }
            });
            float detC = Determinant3x3(new float[,]
            {
                { mat[0, 0], mat[0, 1], rhs[0] },
                { mat[1, 0], mat[1, 1], rhs[1] },
                { mat[2, 0], mat[2, 1], rhs[2] }
            });

            return (detA / det, detB / det, detC / det);
        }

        private static float Determinant3x3(float[,] m)
        {
            return m[0, 0] * (m[1, 1] * m[2, 2] - m[1, 2] * m[2, 1])
                   - m[0, 1] * (m[1, 0] * m[2, 2] - m[1, 2] * m[2, 0])
                   + m[0, 2] * (m[1, 0] * m[2, 1] - m[1, 1] * m[2, 0]);
        }

        private static Vector2 CalcVertex(float a, float b, float c)
        {
            float xv = -b / (2 * a);
            float yv = a * xv * xv + b * xv + c;
            return new Vector2(xv, yv);
        }

        // Chọn parabol có đỉnh cao hơn
        public static (float a, float b, float c, Vector2 B, Vector2 Vertex) ChooseHigherVertex(Vector2 A, Vector2 C,
            float distance = 1f)
        {
            var parab = FindParabolas(A, C, distance);
            return parab[0].Vertex.y > parab[1].Vertex.y ? parab[0] : parab[1];
        }
        
        public static Vector3 SolveParabola(Vector2 A, Vector2 C)
        {
            Vector2 B = FindB(A, C, 1);
            // Lập hệ phương trình: M * [a,b,c]^T = Y
            float[,] M = {
                { A.x*A.x, A.x, 1 },
                { B.x*B.x, B.x, 1 },
                { C.x*C.x, C.x, 1 }
            };
            float[] Y = { A.y, B.y, C.y };

            float det = Determinant(M);
            if (Mathf.Abs(det) < 1e-6f) return Vector3.zero;

            float[,] Ma = { { Y[0], M[0,1], M[0,2] }, { Y[1], M[1,1], M[1,2] }, { Y[2], M[2,1], M[2,2] } };
            float[,] Mb = { { M[0,0], Y[0], M[0,2] }, { M[1,0], Y[1], M[1,2] }, { M[2,0], Y[2], M[2,2] } };
            float[,] Mc = { { M[0,0], M[0,1], Y[0] }, { M[1,0], M[1,1], Y[1] }, { M[2,0], M[2,1], Y[2] } };

            float a = Determinant(Ma) / det;
            float b = Determinant(Mb) / det;
            float c = Determinant(Mc) / det;

            return new Vector3(a, b, c);
        }
        
        public static Vector2 FindB(Vector2 A, Vector2 C, float d)
        {
            Vector2 mid = (A + C) / 2f;
            Vector2 dir = (C - A).y < -(C - A).y
                ? new Vector2(-(C - A).y, (C - A).x).normalized
                : new Vector2((C - A).y, -(C - A).x).normalized;
            return mid + dir * d; // có thể đổi -dir để lấy phía đối diện
        }

        public static float Determinant(float[,] m)
        {
            return
                m[0,0]*(m[1,1]*m[2,2] - m[1,2]*m[2,1]) -
                m[0,1]*(m[1,0]*m[2,2] - m[1,2]*m[2,0]) +
                m[0,2]*(m[1,0]*m[2,1] - m[1,1]*m[2,0]);
        }
    }
}
