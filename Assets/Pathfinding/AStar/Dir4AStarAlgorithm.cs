using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.AStar
{
    // 最简单的 四向移动矩阵
    public class Dir4AStarAlgorithm
    {
        public int[,] matrix;  // 矩阵
        public Vector2Int start ;  // 起点
        public Vector2Int end ;  // 终点
        private List<AStarNode> openList = new List<AStarNode>();  // 开放列表
        private HashSet<AStarNode> closedList = new HashSet<AStarNode>();  // 关闭列表
        private AStarNode[,] nodes;  // 存储所有节点的数组
        
        private Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, -1), // 上
            new Vector2Int(1, 0),  // 右
            new Vector2Int(0, 1),  // 下
            new Vector2Int(-1, 0), // 左
        };

        public Dir4AStarAlgorithm(int[,] matrixParam)
        {
            matrix = matrixParam;
            
            int cols = matrix.GetLength(0);
            int rows = matrix.GetLength(1);

            nodes = new AStarNode[cols, rows];
            
            // 初始化节点
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    nodes[j, i] = new AStarNode(j, i);
                    if (matrix[j, i] == 1)
                    {
                        start = new Vector2Int(j, i);
                    }

                    if (matrix[j, i] == 2)
                    {
                        end = new Vector2Int(j, i);
                    }
                }
            }
        }

        public void RunAStar()
        {
            AStarNode startNode = nodes[start.x, start.y];
            AStarNode endNode = nodes[end.x, end.y];
            
            startNode.g = 0;
            startNode.h = CalculateHeuristic(startNode, endNode);
            startNode.CalculateF();
            
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                // 获取 f 值最小的节点
                AStarNode currentFMinNode = GetNodeWithLowestF();
                
                if (currentFMinNode == endNode)
                {
                    // 找到目标，构造路径
                    TracePath(currentFMinNode);
                    return;
                }
                
                openList.Remove(currentFMinNode);
                closedList.Add(currentFMinNode);
                
                // 检查邻接节点
                foreach (var dir in directions)
                {
                    int newX = currentFMinNode.x + dir.x;
                    int newY = currentFMinNode.y + dir.y;

                    // 如果新位置不在矩阵范围内，跳过
                    if (newX < 0 || newX >= matrix.GetLength(0) || newY < 0 || newY >= matrix.GetLength(1))
                        continue;

                    // 如果是障碍，跳过
                    if (matrix[newX, newY] == 3)
                        continue;

                    AStarNode neighbor = nodes[newX, newY];

                    if (closedList.Contains(neighbor))
                        continue;

                    // 计算 g、h 和 f
                    float tentativeG = currentFMinNode.g + 1; // 假设每一步的代价为 1
                    if (tentativeG < neighbor.g)
                    {
                        neighbor.g = tentativeG;
                        neighbor.h = CalculateHeuristic(neighbor, endNode);
                        neighbor.CalculateF();
                        neighbor.parent = currentFMinNode;

                        if (!openList.Contains(neighbor))
                        {
                            openList.Add(neighbor);
                        }
                    }
                }
            }
        }
        
        // 使用曼哈顿距离作为启发式估算
        float CalculateHeuristic(AStarNode node, AStarNode targetNode)
        {
            return Mathf.Abs(node.x - targetNode.x) + Mathf.Abs(node.y - targetNode.y);
        }
        
        AStarNode GetNodeWithLowestF()
        {
            AStarNode lowestNode = openList[0];
            foreach (var node in openList)
            {
                if (node.f < lowestNode.f)
                {
                    lowestNode = node;
                }
            }
            return lowestNode;
        }
        
        void TracePath(AStarNode endNode)
        {
            AStarNode currentNode = endNode;
            while (currentNode != null)
            {
                // 在控制台打印路径
                Debug.Log($"路径: ({currentNode.x}, {currentNode.y})");
                currentNode = currentNode.parent;
            }
        }
    }
}