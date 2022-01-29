using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Platform : Triggerable
{
    public bool backtrack = false;
    public List<Node> nodes = new List<Node>();

    float percent = 0f;
    int nodeCur = 0;
    int nodeTar = 1;
    bool backtracking = false;
    bool paused = false; 

    private void Start() => transform.position = nodes[0].pos;
    private void FixedUpdate() 
    {
        if (nodes.Count < 2)
        {
            Debug.LogError("Out of bounds, add mode nodes.");
            return;
        }

        if (active)
        {
            if (!paused) percent += nodes[nodeCur].GetSpeed(backtracking) * Time.deltaTime;
            transform.position = Vector3.Lerp(nodes[nodeCur].pos, nodes[nodeTar].pos, percent);
            if (percent >= 1f)
            {
                StartCoroutine(NextNode());
                percent = 0;
            }
        }
    }

    public float GetCircutTime()
    {
        float count = 0;
        for (int i = 0; i < nodes.Count; i++)
        {
            count += 1 / nodes[i].GetSpeed(backtrack);
            count += nodes[i].GetWait(backtrack);
            if (backtrack)
            {
                count += 1 / nodes[i].GetSpeed(!backtrack);
                count += nodes[i].GetWait(!backtrack);
            }
        }
        return count;
    }

    IEnumerator NextNode() 
    {
        nodeCur = nodeTar;
        paused = true;
        yield return new WaitForSecondsRealtime(nodes[nodeCur].GetWait(backtracking));
        paused = false;

        if (!backtrack) 
        {
            if (nodeTar == nodes.Count - 1)
                nodeTar = 0;
            else nodeTar++;
        }
        else if (backtrack) 
        {
            if (nodeTar == 0 || nodeTar == nodes.Count - 1)
                backtracking = !backtracking;

            if (backtracking)
            {
                if (nodeTar == 0)
                    nodeTar = nodeTar + 1;
                else nodeTar--;
            }
            else
            {
                if (nodeTar == nodes.Count - 1)
                    nodeTar = nodes.Count - 2;
                else nodeTar++;
            }
        }
        if (backtrack && backtracking)
                nodes[nodeCur].speedBack = Vector3.Distance(nodes[nodeCur].pos, nodes[nodeTar].pos) / (1 / nodes[nodeCur].GetSpeed(true));
        else nodes[nodeCur].speedMain = Vector3.Distance(nodes[nodeCur].pos, nodes[nodeTar].pos) / (1 / nodes[nodeCur].GetSpeed(false));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Player>())
            other.transform.parent = transform;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<Player>())
            other.transform.parent = GameObject.Find("Env").transform;
    }
}
[System.Serializable]
public class Node
{
    public Vector3 pos;
    [Header("Main")]
    [Range(0, 2)] public float timeMain;
    [Range(0, 10)] public float waitMain;
    [SerializeField] public float speedMain;
    [Header("Back")]
    [Range(0, 2)] public float timeBack;
    [Range(0, 10)] public float waitBack;
    [SerializeField] public float speedBack;
    public float GetSpeed(bool backtracking)
    {
        if (!backtracking) return timeMain;
        return timeBack;
    }
    public float GetWait(bool backtracking)
    {
        if (!backtracking) return waitMain;
        return waitBack;
    }
}
[CustomEditor(typeof(Platform))]
public class PlatformsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // add triggers
        // bold thing
        // foldouts



        Platform platform = (Platform)target;
        EditorGUILayout.LabelField("CIRCUT TIME " + platform.GetCircutTime() + " SECONDS");
        platform.backtrack = EditorGUILayout.Toggle("Backtrack", platform.backtrack);

        List<Node> list = platform.nodes;
        int size = Mathf.Max(0, EditorGUILayout.IntField("Node Count", list.Count));
        while (size > list.Count)
            list.Add(null);
        while (size < platform.nodes.Count)
            platform.nodes.RemoveAt(list.Count - 1);
        for (int i = 0; i < list.Count; i++)
        {
            EditorGUILayout.LabelField("Node " + i);
            EditorGUI.indentLevel++;

            platform.nodes[i].pos = EditorGUILayout.Vector3Field("POS",platform.nodes[i].pos);
            EditorGUILayout.Space();

            list[i].timeMain = EditorGUILayout.FloatField("Time Main", list[i].timeMain);
            list[i].waitMain = EditorGUILayout.FloatField("Time Main", list[i].waitMain);
            EditorGUILayout.LabelField("Speed: " + list[i].speedMain);

            if (platform.backtrack)
            {
                EditorGUILayout.Space();
                list[i].timeBack = EditorGUILayout.FloatField("Time Back", list[i].timeBack);
                list[i].waitBack = EditorGUILayout.FloatField("Time Back", list[i].waitBack);
                EditorGUILayout.LabelField("Speed: " + list[i].speedBack);
            }

            EditorGUILayout.Space();
            EditorGUI.indentLevel--;
        }
    }
}