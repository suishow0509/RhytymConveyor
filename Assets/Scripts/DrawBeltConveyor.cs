using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DrawBeltConveyor : MonoBehaviour
{
	[System.Serializable]
	public struct BeltPath
	{
		public GameObject obj;
		public Vector3 vector;
	}

	[Header("ベルトコンベアの通り道")]
	[SerializeField] private List<BeltPath> m_path = new();


	private void Awake()
	{
        //Debug.Log("awake");
	}

	// Start is called before the first frame update
	void Start()
    {
        //Debug.Log("start");
    }

	private void OnValidate()
	{
		//int count = 0;
		//for (int i = 0; i <  BeltPath path in m_path)
		//{
  //          if (path.obj == null)
  //          {
		//		GameObject obj = new();
		//		obj.transform.parent = transform;
		//		obj.name = "path" + count;
		//		path.obj = obj;
  //          }
		//	count++;
  //      }
	}

	// Update is called once per frame
	void Update()
    {
        //Debug.Log("update");
    }
}
