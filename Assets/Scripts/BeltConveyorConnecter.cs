using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyorConnecter : MonoBehaviour
{
    // オブジェクトの角度は 90 or 45 刻み
    [Header("角度")]
    [SerializeField] private float m_angleTick = 90.0f;

    [Header("繋ぐふたつのベルトコンベア")]
    [SerializeField] private BeltConveyor m_fromBelt = null;
    [SerializeField] private BeltConveyor m_toBelt = null;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // 左クリック

        // マウスポインタのRay
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);



    }

    // ベルトコンベアを繋げるか確認
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


    // 何も持ってない状態でクリックしたときは選択

    // 何もないところをクリックしたら選択解除

    // ベルトコンベアを持っている状態でクリックしたらその位置にベルトコンベアを置く

}