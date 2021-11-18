using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_Sheets : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public int storyID;
		public int charaID;
		public string name;
		public string sentence;
	}
}

