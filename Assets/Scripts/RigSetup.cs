using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigSetup : MonoBehaviour
{
    private void OnValidate()
    {
        if(!transform.TryGetComponent<RigBuilder>(out var rigBuilder))
        {
            var rigPrefab = Resources.Load<GameObject>("Prefab/Rig");
            rigBuilder = transform.AddComponent<RigBuilder>();
            // setup the rig
            var rigObj = Instantiate(rigPrefab, rigBuilder.transform).GetComponent<Rig>();
            rigBuilder.layers.Add(new RigLayer(rigObj));
            rigObj.gameObject.name = "Rig";
            
            // adding IK constraint into head
            var head = rigObj.transform.GetChild(0).GetComponent<MultiAimConstraint>();
            head.data.constrainedObject = transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0);
            
            // adding IK left hand constraint into left hand
            var leftHand = rigObj.transform.GetChild(1).GetComponent<TwoBoneIKConstraint>();
            leftHand.data.tip = transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
            leftHand.data.mid = transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
            leftHand.data.root = transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
                
            // adding IK right hand constraint into left hand
            var rightHand =rigObj.transform.GetChild(2).GetComponent<TwoBoneIKConstraint>();
            
            rightHand.data.tip = transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0);
            rightHand.data.mid = transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0);
            rightHand.data.root = transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0);
            
            Debug.Log("Generated Head, LeftHand, and RightHand Animation Rig Constraints");
            Debug.Log($"Rig is Ready? {rigBuilder.Build()}");
        }
    }
}
