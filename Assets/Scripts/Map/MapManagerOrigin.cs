using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MapManager : MonoBehaviour
{
    public enum NodeType { Battle, Shop, Event, Boss }
    private Dictionary<int, NodeType> nodeTypes = new Dictionary<int, NodeType>();

    public List<Button> nodeButtons; // 所有节点按钮（顺序与编号一致）

    private HashSet<int> visitedNodes = new HashSet<int>(); // 已访问节点
    private int currentNode; // 当前节点索引，Start 中赋值

    public GameObject linePrefab; // 预制体，带 LineRenderer 组件
    private List<GameObject> lineObjects = new List<GameObject>();

    void Start()
    {
        currentNode = GameData.Instance.currentNodeIndex;
        SetupNodeTypes();
        HighlightNode(currentNode); // 高亮当前节点
        SetupButtonEvents(); // 设置按钮点击逻辑
        UpdateInteractableNodes(); // 只允许点击相邻节点
        DrawLinesBetweenNodes();
    }

    void SetupNodeTypes()
    {
        nodeTypes[0] = NodeType.Battle;
        nodeTypes[1] = NodeType.Battle;
        nodeTypes[2] = NodeType.Battle;
        nodeTypes[3] = NodeType.Shop;
        nodeTypes[4] = NodeType.Event;
        nodeTypes[5] = NodeType.Event;
        nodeTypes[6] = NodeType.Battle;
        nodeTypes[7] = NodeType.Battle;
        nodeTypes[8] = NodeType.Shop;
        nodeTypes[9] = NodeType.Battle;
        nodeTypes[10] = NodeType.Battle;
        nodeTypes[11] = NodeType.Boss;
    }

    
    void SetupButtonEvents()
    {
        for (int i = 0; i < nodeButtons.Count; i++)
        {
            int index = i;
            nodeButtons[i].onClick.AddListener(() => OnNodeClicked(index));
        }
    }

    void OnNodeClicked(int index)
    {
        if (GameData.Instance.pathMap.ContainsKey(currentNode) && GameData.Instance.pathMap[currentNode].Contains(index))
        {
            Debug.Log("跳转到节点：" + index);
            currentNode = index;
            GameData.Instance.currentNodeIndex = index;
            HighlightNode(index);
            UpdateInteractableNodes();

            SelectedCharacter.Instance.characterData = PlayerData.selectedCharacter;

            // 根据节点类型加载对应的场景
            if (nodeTypes.ContainsKey(index))
            {
                NodeType type = nodeTypes[index];
                switch (type)
                {
                    case NodeType.Battle:
                        SceneManager.LoadScene("BattleScene");
                        break;
                    case NodeType.Shop:
                        SceneManager.LoadScene("ShopScene");
                        break;
                    case NodeType.Event:
                        SceneManager.LoadScene("EventScene");
                        break;
                    case NodeType.Boss:
                        SceneManager.LoadScene("BossScene");
                        break;
                }
            }
            else
            {
                Debug.LogWarning("该节点没有类型定义！");
            }
        }
        else
        {
            Debug.Log("不允许从当前节点跳到这个点");
        }
    }

    void HighlightNode(int index)
    {
        visitedNodes.Add(index); // 标记为已访问

        for (int i = 0; i < nodeButtons.Count; i++)
        {
            // 获取按钮的文字组件并显示类型
            TextMeshProUGUI label = nodeButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (nodeTypes.ContainsKey(i))
                label.text = nodeTypes[i].ToString();

            Image btnImage = nodeButtons[i].GetComponent<Image>();

            if (i == index)
            {
                btnImage.color = Color.green; // 当前节点
            }
            else if (visitedNodes.Contains(i))
            {
                btnImage.color = Color.gray; // 已访问
            }
            else
            {
                // 未访问，根据类型设颜色
                if (nodeTypes.ContainsKey(i))
                {
                    switch (nodeTypes[i])
                    {
                        case NodeType.Battle:
                            btnImage.color = new Color(1f, 0.6f, 0.6f); // 淡红
                            break;
                        case NodeType.Shop:
                            btnImage.color = new Color(0.6f, 0.8f, 1f); // 淡蓝
                            break;
                        case NodeType.Event:
                            btnImage.color = new Color(1f, 1f, 0.6f); // 淡黄
                            break;
                        case NodeType.Boss:
                            btnImage.color = new Color(0.8f, 0.6f, 1f); // 淡紫
                            break;
                    }
                }
                else
                {
                    btnImage.color = Color.white;
                }
            }
        }
    }


    void UpdateInteractableNodes()
    {
        for (int i = 0; i < nodeButtons.Count; i++)
        {
            // 只有当前节点能跳转的目标节点可以点击
            if (GameData.Instance.pathMap.ContainsKey(currentNode) && GameData.Instance.pathMap[currentNode].Contains(i))
                nodeButtons[i].interactable = true;
            else
                nodeButtons[i].interactable = false;
        }
        // 关闭当前节点自身也可以是交互状态
        nodeButtons[currentNode].interactable = false;
    }
    
    void DrawLinesBetweenNodes()
    {
        // 清除旧连线（如果重新生成）
        foreach (GameObject line in lineObjects)
        {
            Destroy(line);
        }
        lineObjects.Clear();

        // 遍历 pathMap 中每对连接
        foreach (var pair in GameData.Instance.pathMap)
        {
            int from = pair.Key;
            foreach (int to in pair.Value)
            {
                // 为避免重复绘制（双向连接），只画一次（如只画 from < to 的线）
                if (from < to)
                {
                    Vector3 startPos = nodeButtons[from].transform.position;
                    Vector3 endPos = nodeButtons[to].transform.position;

                    GameObject newLine = Instantiate(linePrefab, transform);
                    LineRenderer lr = newLine.GetComponent<LineRenderer>();
                    lr.positionCount = 2;
                    lr.SetPosition(0, startPos);
                    lr.SetPosition(1, endPos);

                    lineObjects.Add(newLine);
                }
            }
        }
    }

}
