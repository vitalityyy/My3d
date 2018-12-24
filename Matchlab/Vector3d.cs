using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3d
{
   public class Vector3d
    {
        public double dx;
        public double dy;
        public double dz;

        public Vector3d(double dx, double dy, double dz)
        {
            this.dx = dx;
            this.dy = dy;
            this.dz = dz;
        }
        public static Vector3d operator +(Vector3d v0, Vector3d v1)
        {
            return new Vector3d(v0.dx + v1.dx, v0.dy + v1.dy, v0.dz + v1.dz);
        }
        
        public static Vector3d operator -(Vector3d v0, Vector3d v1)
        {
            return new Vector3d(v0.dx - v1.dx, v0.dy - v1.dy, v0.dz - v1.dz);
        }

        public static Vector3d operator *(Vector3d v0, Vector3d v1)
        {
            return new Vector3d(v0.dx * v1.dx, v0.dy * v1.dy, v0.dz * v1.dz);
        }

        public static Vector3d operator /(Vector3d v0, Vector3d v1)
        {
            return new Vector3d(v0.dx / v1.dx, v0.dy / v1.dy, v0.dz / v1.dz);
        }
    }
}





