using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float border;
    [SerializeField] private float speed;
    private Transform tr;
    private int height;
    private bool direction;
    private bool play = true;

    private Transform cameraTr;

    private List<GameObject> list = new List<GameObject>();

    private void Start()
    {
        list.Add(Instantiate(prefab, new Vector3(0, -1, 0), Quaternion.identity));
        list.Add(Instantiate(prefab));
        tr = list[1].GetComponent<Transform>();
        cameraTr = Camera.main.transform;   
    }

    private void Update()
    {
        if (play)
        {
            if (direction)
            {
                tr.position = Vector3.MoveTowards(tr.position, new Vector3(border, height, 0), Time.deltaTime * speed);
                if (tr.position.x == border) { direction = false; }
            }
            else
            {
                tr.position = Vector3.MoveTowards(tr.position, new Vector3(-border, height, 0), Time.deltaTime * speed);
                if (tr.position.x == -border) { direction = true; }   
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Mathf.Abs(tr.position.x - list[list.Count -2].transform.position.x) > 0.75f)
            {
                play = false;
                for (int i = 1; i < list.Count; i++)
                {
                    list[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
            }
            else
            {
                height++;
                list.Add(Instantiate(prefab, new Vector3(0, height, 0), Quaternion.identity));
                tr = list[list.Count - 1].transform;
            }
        }

        cameraTr.position = Vector3.MoveTowards(cameraTr.position, new Vector3(0, height + 1, -4.5f), Time.deltaTime * speed);
    }
}
