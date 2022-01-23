using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : Triggerable {
    [Header("Platform")]
    [SerializeField] float speed = 1.0f;
    [SerializeField] bool backtrack = false;

    [SerializeField] List<Vector3> nodes = new List<Vector3>();

    float percent = 0f;
    int nodeCur = 0;
    int nodeTar = 1;
    bool backtracking = false;

    private void FixedUpdate() {
        if (nodes.Count < 2) {
            Debug.LogError("Out of bounds, add mode nodes.");
            return;
        }

        if (active) {
            percent += speed * Time.deltaTime;
            transform.position = Vector3.Lerp(nodes[nodeCur], nodes[nodeTar], percent);

            if (percent >= 1f) {
                NextNode();
                percent = 0;
            }
        }
    }

    void NextNode() {
        nodeCur = nodeTar;
        if (!backtrack) {
            if (nodeTar == nodes.Count - 1)
                nodeTar = 0;
            else nodeTar++;
        }
        else if (backtrack) {
            if (nodeTar == 0 || nodeTar == nodes.Count - 1)
                backtracking = !backtracking;

            if (backtracking) {
                if (nodeTar == 0)
                    nodeTar = nodeTar + 1;
                else nodeTar--;
            }
            else {
                if (nodeTar == nodes.Count - 1)
                    nodeTar = nodes.Count - 2;
                else nodeTar++;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.GetComponent<Player>())
            other.transform.parent = transform;
    }
    private void OnTriggerExit(Collider other) {
        if (other.transform.GetComponent<Player>())
            other.transform.parent = GameObject.Find("Env").transform;
    }
}
