using UnityEngine;
using System.Collections.Generic;

public class Finder : MonoBehaviour
{
    public event System.Action<GameObject> onFound = (obj) => { };
    public event System.Action<GameObject> onLost = (obj) => { };

    [SerializeField, Range(0.0f, 360.0f)]
    private float searchAngle = 0.0f;
    public float SearchAngle
    {
        get { return searchAngle; }
    }

    private SphereCollider sphereCollider = null;
    private List<FoundData> foundList = new List<FoundData>();
    public List<FoundData> FoundList
    {
        get { return foundList; }
    }

    public float SearchRadius
    {
        get
        {
            if (sphereCollider == null)
            {
                sphereCollider = GetComponent<SphereCollider>();
            }
            return sphereCollider != null ? sphereCollider.radius : 0.0f;
        }
    }

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnDisable()
    {
        foundList.Clear();
    }

    private void Update()
    {
        UpdateFoundObject();
    }

    private void UpdateFoundObject()
    {
        foreach (var foundData in foundList)
        {
            GameObject targetObject = foundData.Obj;
            if (targetObject == null)
            {
                continue;
            }

            bool isFound = CheckFoundObject(targetObject);
            foundData.Update(isFound);

            if (foundData.IsFound())
            {
                onFound(targetObject);
            }
            else if (foundData.IsLost())
            {
                onLost(targetObject);
            }
        }
    }

    private bool CheckFoundObject(GameObject target)
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 myPosition = transform.position;
        //目の位置に補正
        myPosition.y += 1;

        Vector3 myPositionXZ = Vector3.Scale(myPosition, new Vector3(1.0f, 0.0f, 1.0f));
        Vector3 targetPositionXZ = Vector3.Scale(targetPosition, new Vector3(1.0f, 0.0f, 1.0f));

        Vector3 toTargetFlatDir = (targetPositionXZ - myPositionXZ).normalized;
        Vector3 myForward = transform.forward;
        if (!IsWithinRangeAngle(myForward, toTargetFlatDir, Mathf.Cos(searchAngle * 0.5f * Mathf.Deg2Rad)))
        {
            return false;
        }

        Vector3 toTargetDir = (targetPosition - myPosition).normalized;

        if (!IsHitRay(myPosition, toTargetDir, target))
        {
            return false;
        }

        return true;
    }

    private bool IsWithinRangeAngle(Vector3 forwardDir, Vector3 toTargetDir, float cosTheta)
    {
        // 方向ベクトルが無い場合、同位置にあるものだと判断
        if (toTargetDir.sqrMagnitude <= Mathf.Epsilon)
        {
            return true;
        }

        float dot = Vector3.Dot(forwardDir, toTargetDir);
        return dot >= cosTheta;
    }

    private bool IsHitRay(Vector3 fromPosition, Vector3 toTargetDir, GameObject target)
    {
        // 方向ベクトルが無い場合は、同位置にあるものだと判断する。
        if (toTargetDir.sqrMagnitude <= Mathf.Epsilon)
        {
            return true;
        }

        RaycastHit onHitRay;
        if (!Physics.Raycast(fromPosition, toTargetDir, out onHitRay, SearchRadius))
        {
            return false;
        }

        if (onHitRay.transform.gameObject != target)
        {
            return false;
        }

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject enterObject = other.gameObject;

        // 念のため多重登録されないようにする。
        if (foundList.Find(value => value.Obj == enterObject) == null)
        {
            foundList.Add(new FoundData(enterObject));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject exitObject = other.gameObject;

        var foundData = foundList.Find(value => value.Obj == exitObject);
        if (foundData == null)
        {
            return;
        }

        if (foundData.IsCurrentFound())
        {
            onLost(foundData.Obj);
        }

        foundList.Remove(foundData);
    }


    public class FoundData
    {
        public FoundData(GameObject argObj)
        {
            Obj = argObj;
        }

        public GameObject Obj { get; private set; }

        private bool isCurrentFound/* { get; private set; }*/ = false;
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
}