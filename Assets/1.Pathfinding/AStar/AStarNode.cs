namespace Pathfinding.AStar
{
    public class AStarNode
    {
        public int x, y;
        public float g, h, f;
        public AStarNode parent;
        
        public AStarNode(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.g = float.MaxValue;
            this.h = 0;
            this.f = float.MaxValue;
            this.parent = null;
        }
        
        public void CalculateF()
        {
            f = g + h;
        }
    }
}
