using UnityEngine;

public class FoundData
{
    public FoundData(GameObject obj)
    {
        Obj = obj;
    }

    public GameObject Obj { get; private set; }

    private bool isCurrentFound = false;
    public bool IsCurrentFound()
    {
        return isCurrentFound;
    }

    private bool isPrevFound = false;

    public Vector3 Position
    {
        get { return Obj != null ? Obj.transform.position : Vector3.zero; }
    }

    public void Update(bool isFound)
    {
        isPrevFound = isCurrentFound;
        isCurrentFound = isFound;
    }

    public bool IsFound()
    {
        return isCurrentFound && !isPrevFound;
    }

    public bool IsLost()
    {
        return !isCurrentFound && isPrevFound;
    }
}