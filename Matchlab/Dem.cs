using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3d
{
    /// <summary>
    /// 地形数据
    /// </summary>
    
     public class Dem
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="ncols">列数</param>
        /// <param name="nrows">行数</param>
        public Dem(int ncols,int nrows)
        {
            this.ncols = ncols;
            this.nrows = nrows;
            this.terrainMap = new float[this.ncols, this.nrows];
        }
            /// <summary>
            /// 列数
            /// </summary>
            public int ncols;
            /// <summary>
            /// 行数
            /// </summary>
            public int nrows;
            /// <summary>
            /// 起点经纬坐标
            /// </summary>
            public double xllcorner;
            public double yllcorner;
            /// <summary>
            /// 单元尺寸
            /// </summary>
            public double cellsize;
            /// <summary>
            /// 未定义数据
            /// </summary>
            public float nodataValue;
            /// <summary>
            /// 地形数据
            /// </summary>
            public float[,] terrainMap;
            /// <summary>
            /// 最大值
            /// </summary>
            public float maxValue;
            /// <summary>
            /// 最小值
            /// </summary>
            public float minValue;
        }
    }

