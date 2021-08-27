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

        //GameManager gm;
        int haveCount = 0;//アイテム所持数
        GameObject ItemArea;//アイテム保管用スペース
        float distItem = -3;//アイテム保管後の間隔

        string itemName;

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
            //gm = GetComponent<GameManager>();
            //csg = GetComponent<CSlotGrid>();
            ItemArea = GameObject.Find("ItemArea");
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
            m_release = Input.GetButtonDown("Fire2");

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

            //if (csg.allItem[2] == null)
            //{
            //    m_catch = Input.GetButton("Fire1");
                
            //}
            //else if(csg.allItem[select.SelectNum()] != null)
            //{
            //    m_release = Input.GetButton("Fire1");
            //}

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
                        collision.gameObject.transform.position = new Vector3(ItemArea.transform.position.x + distItem, ItemArea.transform.position.y + 5, ItemArea.transform.position.z);
                        distItem += 3;
                        haveCount++;
                    }
                }
            }
            else if(collision.gameObject.tag == "Gomibako")
            {
                if (haveCount > 0 && haveCount < 3)
                {
                    if (m_release)
                    {
                        ///離した時の処理///
                    }
                }
            }
        }

        public string ItemName()
        {
            return itemName;
        }

        public bool Catch()
        {
            return m_catch;
        }

    }
}
