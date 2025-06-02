//using System.Collections.Generic;
//using UnityEngine;

//public class MapGenerator : MonoBehaviour
//{
//    public int totalLayers = 5; // 总共5层
//    public int nodesPerLayer = 3; // 每层3个节点

//    public Dictionary<int, List<int>> GeneratePathMap()
//    {
//        Dictionary<int, List<int>> pathMap = new Dictionary<int, List<int>>();
//        int totalNodes = totalLayers * nodesPerLayer;

//        for (int layer = 0; layer < totalLayers - 1; layer++)
//        {
//            for (int i = 0; i < nodesPerLayer; i++)
//            {
//                int from = layer * nodesPerLayer + i;
//                pathMap[from] = new List<int>();

//                int nextLayerStart = (layer + 1) * nodesPerLayer;
//                List<int> candidates = new List<int> { 0, 1, 2 };
//                ShuffleList(candidates);

//                int connections = Random.Range(1, 3); // 每个节点连接1-2个下层节点
//                for (int j = 0; j < connections; j++)
//                {
//                    int to = nextLayerStart + candidates[j];
//                    pathMap[from].Add(to);
//                }
//            }
//        }

//        return pathMap;
//    }

//    public Dictionary<int, MapManager.NodeType> GenerateNodeTypes(int totalNodes)
//    {
//        Dictionary<int, MapManager.NodeType> nodeTypes = new Dictionary<int, MapManager.NodeType>();

//        for (int i = 0; i < totalNodes; i++)
//        {
//            if (i >= totalNodes - nodesPerLayer) // 最后一层是 Boss
//            {
//                nodeTypes[i] = MapManager.NodeType.Boss;
//            }
//            else
//            {
//                int r = Random.Range(0, 3);
//                if (r == 0) nodeTypes[i] = MapManager.NodeType.Battle;
//                else if (r == 1) nodeTypes[i] = MapManager.NodeType.Shop;
//                else nodeTypes[i] = MapManager.NodeType.Event;
//            }
//        }

//        return nodeTypes;
//    }

//    private void ShuffleList(List<int> list)
//    {
//        for (int i = list.Count - 1; i > 0; i--)
//        {
//            int rand = Random.Range(0, i + 1);
//            int temp = list[i];
//            list[i] = list[rand];
//            list[rand] = temp;
//        }
//    }
//}
