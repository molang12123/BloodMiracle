using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MapManager : MonoBehaviour
{
    public enum NodeType { Battle, Shop, Event, Boss }
    private Dictionary<int, NodeType> nodeTypes = new Dictionary<int, NodeType>();

    public List<Button> nodeButtons; // ���нڵ㰴ť��˳������һ�£�

    private HashSet<int> visitedNodes = new HashSet<int>(); // �ѷ��ʽڵ�
    private int currentNode; // ��ǰ�ڵ�������Start �и�ֵ

    public GameObject linePrefab; // Ԥ���壬�� LineRenderer ���
    private List<GameObject> lineObjects = new List<GameObject>();

    void Start()
    {
        currentNode = GameData.Instance.currentNodeIndex;
        SetupNodeTypes();
        HighlightNode(currentNode); // ������ǰ�ڵ�
        SetupButtonEvents(); // ���ð�ť����߼�
        UpdateInteractableNodes(); // ֻ���������ڽڵ�
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
            Debug.Log("��ת���ڵ㣺" + index);
            currentNode = index;
            GameData.Instance.currentNodeIndex = index;
            HighlightNode(index);
            UpdateInteractableNodes();

            SelectedCharacter.Instance.characterData = PlayerData.selectedCharacter;

            // ���ݽڵ����ͼ��ض�Ӧ�ĳ���
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
                Debug.LogWarning("�ýڵ�û�����Ͷ��壡");
            }
        }
        else
        {
            Debug.Log("������ӵ�ǰ�ڵ����������");
        }
    }

    void HighlightNode(int index)
    {
        visitedNodes.Add(index); // ���Ϊ�ѷ���

        for (int i = 0; i < nodeButtons.Count; i++)
        {
            // ��ȡ��ť�������������ʾ����
            TextMeshProUGUI label = nodeButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (nodeTypes.ContainsKey(i))
                label.text = nodeTypes[i].ToString();

            Image btnImage = nodeButtons[i].GetComponent<Image>();

            if (i == index)
            {
                btnImage.color = Color.green; // ��ǰ�ڵ�
            }
            else if (visitedNodes.Contains(i))
            {
                btnImage.color = Color.gray; // �ѷ���
            }
            else
            {
                // δ���ʣ�������������ɫ
                if (nodeTypes.ContainsKey(i))
                {
                    switch (nodeTypes[i])
                    {
                        case NodeType.Battle:
                            btnImage.color = new Color(1f, 0.6f, 0.6f); // ����
                            break;
                        case NodeType.Shop:
                            btnImage.color = new Color(0.6f, 0.8f, 1f); // ����
                            break;
                        case NodeType.Event:
                            btnImage.color = new Color(1f, 1f, 0.6f); // ����
                            break;
                        case NodeType.Boss:
                            btnImage.color = new Color(0.8f, 0.6f, 1f); // ����
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
            // ֻ�е�ǰ�ڵ�����ת��Ŀ��ڵ���Ե��
            if (GameData.Instance.pathMap.ContainsKey(currentNode) && GameData.Instance.pathMap[currentNode].Contains(i))
                nodeButtons[i].interactable = true;
            else
                nodeButtons[i].interactable = false;
        }
        // �رյ�ǰ�ڵ�����Ҳ�����ǽ���״̬
        nodeButtons[currentNode].interactable = false;
    }
    
    void DrawLinesBetweenNodes()
    {
        // ��������ߣ�����������ɣ�
        foreach (GameObject line in lineObjects)
        {
            Destroy(line);
        }
        lineObjects.Clear();

        // ���� pathMap ��ÿ������
        foreach (var pair in GameData.Instance.pathMap)
        {
            int from = pair.Key;
            foreach (int to in pair.Value)
            {
                // Ϊ�����ظ����ƣ�˫�����ӣ���ֻ��һ�Σ���ֻ�� from < to ���ߣ�
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
