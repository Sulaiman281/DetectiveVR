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
            head.transform.GetChild(0).position = head.data.constrainedObject.position;
            head.transform.GetChild(0).rotation = head.data.constrainedObject.rotation;
            
            // adding IK left hand constraint into left hand
            {
                var leftHand = rigObj.transform.GetChild(1).GetComponent<TwoBoneIKConstraint>();
                leftHand.data.tip = transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0)
                    .GetChild(0).GetChild(0);
                leftHand.data.mid = transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0)
                    .GetChild(0);
                leftHand.data.root = transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0);

                // aligning the target and hint position with the bones
                leftHand.transform.GetChild(0).position = leftHand.data.tip.position;
                leftHand.transform.GetChild(0).rotation = leftHand.data.tip.rotation;
                leftHand.transform.GetChild(1).position = leftHand.data.mid.position;
                leftHand.transform.GetChild(1).rotation = leftHand.data.mid.rotation;
            }

            // adding IK right hand constraint into left hand
            {
                var rightHand = rigObj.transform.GetChild(2).GetComponent<TwoBoneIKConstraint>();

                rightHand.data.tip = transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0)
                    .GetChild(0).GetChild(0);
                rightHand.data.mid = transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0)
                    .GetChild(0);
                rightHand.data.root = transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0);

                // aligning the target and hint position with the bones
                rightHand.transform.GetChild(0).position = rightHand.data.tip.position;
                rightHand.transform.GetChild(0).rotation = rightHand.data.tip.rotation;
                rightHand.transform.GetChild(1).position = rightHand.data.mid.position;
                rightHand.transform.GetChild(1).rotation = rightHand.data.mid.rotation;
            }
            // adding IK left leg constraint
            {
                var leftLeg = rigObj.transform.GetChild(3).GetComponent<TwoBoneIKConstraint>();

                leftLeg.data.tip = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0);
                leftLeg.data.mid = transform.GetChild(1).GetChild(0).GetChild(0);
                leftLeg.data.root = transform.GetChild(1).GetChild(0);
                
                // aligning the target and hint position with the bones
                leftLeg.transform.GetChild(0).position = leftLeg.data.tip.position;
                leftLeg.transform.GetChild(0).rotation = leftLeg.data.tip.rotation;
                leftLeg.transform.GetChild(1).position = leftLeg.data.mid.position;
                leftLeg.transform.GetChild(1).rotation = leftLeg.data.mid.rotation;
            }
            // adding IK right leg constraint
            {
                var rightLeg = rigObj.transform.GetChild(4).GetComponent<TwoBoneIKConstraint>();

                rightLeg.data.tip = transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0);
                rightLeg.data.mid = transform.GetChild(1).GetChild(1).GetChild(0);
                rightLeg.data.root = transform.GetChild(1).GetChild(1);

                // aligning the target and hint position with the bones
                rightLeg.transform.GetChild(0).position = rightLeg.data.tip.position;
                rightLeg.transform.GetChild(0).rotation = rightLeg.data.tip.rotation;
                rightLeg.transform.GetChild(1).position = rightLeg.data.mid.position;
                rightLeg.transform.GetChild(1).rotation = rightLeg.data.mid.rotation;
            }

            Debug.Log("Generated Head, LeftHand, and RightHand Animation Rig Constraints");
            Debug.Log($"Rig is Ready? {rigBuilder.Build()}");
        }
    }
}
