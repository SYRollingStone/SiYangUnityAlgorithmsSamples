using UnityEngine;

namespace Pathfinding.AStar
{
    public class BlockController : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        
        // Config：0是空地，1是起点，2是终点，3是障碍，
        // runtime:4待选择,5通路
        public int BlockType = 0;

        public void SetBlockType(int type)
        {
            BlockType = type;

            switch (BlockType)
            {
                case 0:
                    spriteRenderer.color = AStarSimpleManager.normalBlockColor;
                    break;
                case 1:
                    spriteRenderer.color = AStarSimpleManager.startPointColor;
                    break;
                case 2:
                    spriteRenderer.color = AStarSimpleManager.endPointColor;
                    break;
                case 3:
                    spriteRenderer.color = AStarSimpleManager.obstaclePointColor;
                    break;
                case 4:
                    spriteRenderer.color = AStarSimpleManager.findPointColor;
                    break;
                case 5:
                    spriteRenderer.color = AStarSimpleManager.resultColor;
                    break;
                default:
                    spriteRenderer.color = AStarSimpleManager.normalBlockColor;
                    break;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
