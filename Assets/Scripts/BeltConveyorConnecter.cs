using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyorConnecter : MonoBehaviour
{
    // �I�u�W�F�N�g�̊p�x�� 90 or 45 ����
    [Header("�p�x")]
    [SerializeField] private float m_angleTick = 90.0f;

    [Header("�q���ӂ��̃x���g�R���x�A")]
    [SerializeField] private BeltConveyor m_fromBelt = null;
    [SerializeField] private BeltConveyor m_toBelt = null;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // ���N���b�N

        // �}�E�X�|�C���^��Ray
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);



    }

    // �x���g�R���x�A���q���邩�m�F
    public bool CheckConnectPrevious()
    {
        if (m_fromBelt)
        {
            return false;
        }
        return true;
    }
    public bool CheckConnectNext()
    {
        if (m_toBelt)
        {
            return false;
        }
        return true;
    }


    // ���������ĂȂ���ԂŃN���b�N�����Ƃ��͑I��

    // �����Ȃ��Ƃ�����N���b�N������I������

    // �x���g�R���x�A�������Ă����ԂŃN���b�N�����炻�̈ʒu�Ƀx���g�R���x�A��u��

}