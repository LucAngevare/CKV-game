using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorFunc : MonoBehaviour
{
    public GameObject dropdown;
    public GameObject toggle;
    public GameObject player;
    public GameObject goodBuilding;
    public GameObject badBuilding;
    public Camera playerCam;
    public Camera houseCam;
    public GameObject brokenBuilding;
    public GameObject badBuildingPrefab;
    private GameObject door;
    private Rigidbody rigidBody;
    private bool sleeping;
    private GameObject CloneBrokenBuilding;
    private bool broken;
    private bool isStationary;
    private HingeJoint doorHinge;
    private Rigidbody hingeBody;
    List<Vector3> locations = new List<Vector3>(14);
    List<string> floors = new List<string>(14);

    // Start is called before the first frame update
    void Start()
    {
        door = GameObject.Find("CKV_obj_door (1)");
        broken = false;
        isStationary = false;
        rigidBody = goodBuilding.AddComponent<Rigidbody>();
        rigidBody.Sleep();
        rigidBody.mass = 20000.0f;
        Physics.gravity = new Vector3(0, -2.0f, 0);
        goodBuilding.SetActive(false);
        houseCam.enabled = false;
        playerCam.enabled = true;
        toggle.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        dropdown.GetComponent<UnityEngine.UI.Dropdown>().ClearOptions();

        for (int i = 0; i < 14; i++)
        {
            floors.Add($"Verdieping {i}");
        }
        floors[0] = "Begane grond";
        dropdown.GetComponent<UnityEngine.UI.Dropdown>().AddOptions(floors);

        locations.Add(new Vector3(9.8f, 3.8f, -10));
        locations.Add(new Vector3(8.0f, 8.0f, -10));
        locations.Add(new Vector3(8.2f, 9.7f, -10));
        locations.Add(new Vector3(9.8f, 11.9f, -10));
        locations.Add(new Vector3(9.8f, 14.2f, -10));
        locations.Add(new Vector3(10.0f, 16.4f, -10));
        locations.Add(new Vector3(9.7f, 18.7f, -10));
        locations.Add(new Vector3(10.0f, 21.0f, -10));
        locations.Add(new Vector3(10.0f, 23.3f, -10));
        locations.Add(new Vector3(10.0f, 25.5f, -10));
        locations.Add(new Vector3(9.2f, 27.8f, -10));
        locations.Add(new Vector3(9.2f, 30.1f, -10));
        locations.Add(new Vector3(9.2f, 32.3f, -10));
        locations.Add(new Vector3(11.0f, 34.6f, -10));
    }

    // Update is called once per frame
    void Update()
    {
        toggle.GetComponent<UnityEngine.UI.Toggle>().onValueChanged.AddListener(delegate
        {
            easterEgg(toggle.GetComponent<UnityEngine.UI.Toggle>());
        });
        dropdown.GetComponent<UnityEngine.UI.Dropdown>().onValueChanged.AddListener(delegate
        {
            teleportPlayer(dropdown.GetComponent<UnityEngine.UI.Dropdown>());
        });

        try
        {
            if (goodBuilding.transform.position.y <= 40 && !broken)
            {
                broken = true;
                CloneBrokenBuilding = Instantiate(brokenBuilding, new Vector3(0.0f, 0.0f, 29.2f), Quaternion.Euler(0, 0, 0));
                brokenBuilding.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                Destroy(badBuilding);
                Destroy(GameObject.Find("CKV_obj_door (2)"));
            }
            if (goodBuilding.transform.position.y <= 2.5f && !isStationary)
            {
                door = GameObject.Find("CKV_obj_door (1)");
                Debug.Log("done");
                isStationary = true;
                Destroy(CloneBrokenBuilding);
                goodBuilding.transform.position = new Vector3(0, -22.64f, 13.2f);
                goodBuilding.transform.rotation = Quaternion.Euler(0, 0, 0);
                Destroy(rigidBody);
                doorHinge = door.AddComponent<HingeJoint>();
                JointLimits limits = doorHinge.limits;
                limits.min = -90;
                limits.max = 0;
                limits.bounciness = 0.1f;
                limits.bounceMinVelocity = 0.2f;
                doorHinge.limits = limits;
                doorHinge.useLimits = true;
                doorHinge.anchor = new Vector3(-9.56f, -0.99f, 0);
                doorHinge.axis = new Vector3(-0.23f, 90, 0);
                doorHinge.autoConfigureConnectedAnchor = true;

                hingeBody = door.AddComponent<Rigidbody>();
            }
        } catch {}
    }

    void easterEgg(Toggle toggle)
    {
        goodBuilding.SetActive(toggle.isOn);

        if (toggle.isOn)
        {
            rigidBody.WakeUp();
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, rigidBody.velocity.z);
        } else
        {
            Destroy(goodBuilding);
            Instantiate(badBuildingPrefab, new Vector3(0.0f, 0.0f, 29.2f), Quaternion.Euler(0, 0, 0));
        }
    }

    void teleportPlayer(Dropdown floor)
    {
        var vector = locations[floor.value];

        player.transform.position = vector;
    }
}
