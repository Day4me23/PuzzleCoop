using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : Triggerable
{
    [Header("Platform")]
    [SerializeField] bool backtrack = false;

    [SerializeField] List<node> nodes = new List<node>();

    float percent = 0f;
    int nodeCur = 0;
    int nodeTar = 1;
    bool backtracking = false;

    private void FixedUpdate() 
    {
        if (nodes.Count < 2)
        {
            Debug.LogError("Out of bounds, add mode nodes.");
            return;
        }

        if (active) 
        {
            percent += nodes[nodeCur].GetSpeed(backtracking) * Time.deltaTime;
            transform.position = Vector3.Lerp(nodes[nodeCur].pos, nodes[nodeTar].pos, percent);
            if (percent >= 1f) StartCoroutine(NextNode());
        }
    }

    IEnumerator NextNode() 
    {
        nodeCur = nodeTar;
        yield return new WaitForSecondsRealtime(nodes[nodeCur].GetWait(backtracking));
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
        percent = 0;
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
public struct node
{
    public Vector3 pos;
    [Range(0,2)][SerializeField] float speedMain, speedBack, waitMain, WaitBack;
    public float GetSpeed(bool backtracking)
    {
        if (!backtracking) return speedMain;
        return speedBack;
    }
    public float GetWait(bool backtracking)
    {
        if (!backtracking) return waitMain;
        return WaitBack;
    }
}