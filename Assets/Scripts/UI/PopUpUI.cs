using UnityEngine;
using System.Collections;

public class PopUpUI : MonoBehaviour
{
	public static PopUpUI instance;

	public Camera UICamera;
	public GameObject PU_Saved;
	public GameObject PU_Loaded;

	void Awake()
	{
		instance = this;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(1))
			CreatePU_Saved();
	}

	public void CreatePU_Saved()
	{

		Instantiate(PU_Saved, MousePosToWorld(), Quaternion.identity);
	}

	public void CreatePU_Loaded()
	{

		Instantiate(PU_Loaded, MousePosToWorld(), Quaternion.identity);
	}


	Vector3 MousePosToWorld()
	{
		Vector3 worldPos = UICamera.ScreenToWorldPoint(Input.mousePosition);
		//		Vector3 worldPos = UICamera.ViewportToWorldPoint(Input.mousePosition);
		worldPos.z = 1;

		return worldPos;
	}
}
