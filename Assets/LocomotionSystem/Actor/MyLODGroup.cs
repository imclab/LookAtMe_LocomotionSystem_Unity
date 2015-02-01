using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;

public class MyLOD
{
	public float		_Percentage;
	public SkinnedMeshRenderer _SMR = null;
	public int _Id;
	bool _IsShown;
	public float _Min;

	public MyLOD(int id, SkinnedMeshRenderer SMR, float min, float percentage)
	{
		_Min = min;
		_Id = id;
		_Percentage = percentage;
		_SMR = SMR;
		_IsShown = false;
		Hide();
	}
	public void Show()
	{
		_IsShown = true;
		//if(_SMR)
		//	_SMR.enabled = true;
	}
	public void Hide()
	{
		_IsShown = false;
		//if(_SMR)
		//	_SMR.enabled = false;
	}
	public bool IsShown()
	{
		return _IsShown;
	}
	public bool Update(float current_ratio)
	{
		if((_Percentage<current_ratio) && (current_ratio<_Min))
		{
			Show();
			return true;
		}
		else
		{
			Hide();
			return false;
		}
	}
}

public class MyLODGroup : MonoBehaviour
{
	[HideInInspector] public int NumberOfLOD;
	[HideInInspector] public List<MyLOD> LODVector = null;
	
	Transform _Head;
	Transform _Hip;
	float heightInPixel;
	float ratio;
	int _iCurrentLOD;
	ObjectInterface _Inter;

	GameObject [] ListOfGO;

	// Use this for initialization
	public MyLODGroup(ObjectInterface inter, Transform Head, Transform Hip)
	{
		Init(inter, Head, Hip);
	}

	public void Init(ObjectInterface inter, Transform Head, Transform Hip)
	{
		if(LODVector==null)
		{
			LODVector = new List<MyLOD>();
			//LODVector.Add(new MyLOD("0", null, 100.0f));
		}
		_iCurrentLOD = 0;
		_Inter = inter;
		_Hip = Hip;
		_Head = Head;
	}
	// Update is called once per frame
	public void Update ()
	{
		return;
		Vector2 screenPosHead = Camera.main.WorldToScreenPoint(_Head.position);
		Vector2 screenPosBip = Camera.main.WorldToScreenPoint(_Hip.position);
		heightInPixel = Mathf.Abs(screenPosHead.y-screenPosBip.y)*2;
		//widthInPixel = 50*heightInPixel/180;
		//AreaInPixel = widthInPixel*heightInPixel;
		//FullArea = Screen.width*Screen.height;
		//ratio = AreaInPixel*100/FullArea;
		ratio = heightInPixel*100/Screen.height;

		bool Hide = false;
		for(int i=0 ; i<LODVector.Count ; ++i)
		{
			bool IsSelected = LODVector[i].Update(ratio);
			if(IsSelected && (_iCurrentLOD!=i))
			{
				_iCurrentLOD = i;
				_Inter.OnLODChange(_iCurrentLOD);
			}
		}
	}
	public void UpdateLodlist()
	{
		/*if(NumberOfLOD == 0)
		{
			NumberOfLOD = 1;
		}
		if(LODVector.Count<NumberOfLOD)
		{
			// Extend Vector Size
			int iSize = NumberOfLOD-LODVector.Count;
			if( (LODVector.Count==1) && (LODVector[0]._Percentage == 100.0f))
			{
				LODVector[0]._Percentage = 50.0f;
				//else
				//{
			//		LODVector[LODVector.Count-1]._Percentage = LODVector[LODVector.Count-2]._Percentage + 
			//			(LODVector[LODVector.Count-1]._Percentage+LODVector[LODVector.Count-2]._Percentage)/2.0f;
			//	}
			}
			float delta = 100.0f-LODVector[LODVector.Count-1]._Percentage;
			delta = delta/iSize;
			for(int i=0 ; i<iSize ; ++i)
			{
				LODVector.Add(new MyLOD((LODVector.Count-1).ToString() , null, 100.0f));

				if((LODVector.Count>2) && (LODVector[LODVector.Count-2]._Percentage == 100.0f))
				{
					LODVector[LODVector.Count-2]._Percentage = LODVector[LODVector.Count-1]._Percentage - 
						(LODVector[LODVector.Count-1]._Percentage-LODVector[LODVector.Count-3]._Percentage)/2.0f;
				}

				//LODVector[LODVector.Count-2]._Percentage = LODVector[LODVector.Count-2]._Percentage/2.0f;
			}
		}
		else if((LODVector.Count>NumberOfLOD) && (NumberOfLOD!=0))
		{
			int nbToDelete = LODVector.Count-NumberOfLOD;

			// Extend Vector Size
			LODVector.RemoveRange(LODVector.Count-nbToDelete, nbToDelete);

		}*/
	}
}

