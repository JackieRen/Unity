using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineToolTest : MonoBehaviour {
    
	void Start () {
#if Test1
        Debug.Log("Test1");
#elif Test2
        Debug.Log("Test2");
#endif
    }

}
