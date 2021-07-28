using System;
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
        bool m_catch;
        bool m_release;

        string itemName;

        CSlotGrid csg;
        ItemSelect select;
        //GameManager gm;
        //int count;

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
            csg = GetComponent<CSlotGrid>();
            select = GetComponent<ItemSelect>();
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
            m_catch = Input.GetButtonDown("Fire1");
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

            if (collision.gameObject.tag == "Item")//アイテムを取得したときの処理
            {
                if (m_catch == true)
                {
                    itemName = collision.gameObject.name;
                    //Debug.Log(itemName);
                    //count--;
                    Destroy(collision.gameObject, 0.1f);
                }
            }

        }

        public string GetItemName()
        {
            return itemName;
        }
        //public int ItemCount()
        //{
        //    return count;
        //}
    }
}
