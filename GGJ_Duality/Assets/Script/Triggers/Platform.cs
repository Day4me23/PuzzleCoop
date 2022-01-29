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
    float elapsedTime = 0;

    static string holder = "SMOKE WEED EVERYDAY";

    private void Start() 
    {
        if (GameObject.Find(holder) == null)
        {
            GameObject temp = new GameObject(holder);
            temp.transform.position = Vector3.zero;
        }

        transform.position = nodes[0].pos; 
    }
    private void FixedUpdate() 
    {
        if (nodes.Count < 2)
        {
            Debug.LogError("Out of bounds, add mode nodes.");
            return;
        }

        if (active)
        {
            //if (!paused) percent += nodes[nodeCur].GetSpeed(backtracking) * Time.deltaTime;
            if (!paused)
            {
                elapsedTime += Time.deltaTime;
                percent = elapsedTime / nodes[nodeCur].GetSpeed(backtracking);
            }
            transform.position = Vector3.Lerp(nodes[nodeCur].pos, nodes[nodeTar].pos, percent);
            if (percent >= 1f)
            {
                elapsedTime = 0;
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
            count += nodes[i].GetSpeed(backtrack);
            count += nodes[i].GetWait(backtrack);
            if (backtrack)
            {
                count += nodes[i].GetSpeed(!backtrack);
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
        if (other.transform.GetComponent<PlayerMovement>())
            other.transform.parent = transform;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<PlayerMovement>())
            other.transform.parent = GameObject.Find(holder).transform;
    }
}
[System.Serializable]
public class Node
{
    public Vector3 pos;
    [Header("Main")]
    [Range(0, 20)] public float timeMain;
    [Range(0, 20)] public float waitMain;
    [SerializeField] public float speedMain;
    [Header("Back")]
    [Range(0, 20)] public float timeBack;
    [Range(0, 20)] public float waitBack;
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
[System.Serializable]
public class PlatformsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // add triggers
        base.OnInspectorGUI();

        Platform platform = (Platform)target;
        EditorGUILayout.LabelField("CIRCUT TIME " + platform.GetCircutTime() + " SECONDS", EditorStyles.boldLabel);
    }
}