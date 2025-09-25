using UnityEngine;

public interface ISelector
{
    GameObject[] SelectTarget(GameObject go, int tag);
}
