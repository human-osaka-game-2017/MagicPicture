using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour {

    private GameObject player = null;
    private List<FoundData> foundList = new List<FoundData>();
    public List<FoundData> FoundList
    {
        get { return foundList; }
    }

    void Start()
    {
        this.player = GameObject.Find("Player");
    }

    void Update()
    {
        RaycastHit[] hitColliders = null;
        Vector3 vec = (this.player.transform.position - this.transform.position);
        hitColliders = Physics.RaycastAll(
            this.transform.position,
            vec.normalized,
            vec.magnitude,
            int.MaxValue ^ LayerMask.NameToLayer("player"));

        foreach (var hitCollider in hitColliders)
        {
            GameObject hitObj = hitCollider.collider.gameObject;
            //多重登録防止
            if (foundList.Find(value => value.Obj == hitObj) == null)
            {
                FoundData foundData = new FoundData(hitObj);
                foundData.Update(true);
                foundList.Add(foundData);
            }
        }
        
    }
}