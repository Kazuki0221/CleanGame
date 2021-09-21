using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(PlayerCharacter))]
    public class PlayerControl : MonoBehaviour
    {
        private PlayerCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        bool m_catch;//拾う
        bool m_release;//放す

        int haveCount = 0;//アイテム所持数

        public string itemName = null;//アイテム名
        GameController gameController;

        CSlotGrid cSlotGrid;
        ItemSelect itemSelect;
        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<PlayerCharacter>();
            cSlotGrid = GameObject.Find("SlotGrid").GetComponent<CSlotGrid>();
            itemSelect = GameObject.Find("SelectArea").GetComponent<ItemSelect>();
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);
            m_catch = Input.GetButton("Fire1");
            m_release = Input.GetButton("Fire2");

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }
#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;


#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump, m_catch, m_release);
            m_Jump = false;
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.tag == "Item")
            {
                if (haveCount < 3)
                {
                    if (m_catch)
                    {
                        itemName = collision.gameObject.name;
                        Destroy(collision.gameObject);
                        if (itemName != null)
                        {
                            haveCount++;
                            gameController.itemCount--;
                        }
                    }
                }
            }
        }

        private void OnTriggerStay(Collider collision)
        {
            if (collision.gameObject.tag == "Gomibako")
            {
                Vector3 gPos = collision.gameObject.transform.position;
                gPos.y += 5;
                if (haveCount > 0 && haveCount < 3)
                {
                    if (m_release && cSlotGrid.ReleseItem(itemSelect.num) != null)
                    {
                        Instantiate(cSlotGrid.ReleseItem(itemSelect.num).ItemObj, gPos, cSlotGrid.ReleseItem(itemSelect.num).ItemObj.transform.rotation);
                        haveCount--;
                    }
                }
            }
        }

        public bool Catch()
        {
            return m_catch;
        }
        public bool Release()
        {
            return m_release;
        }

    }
}
