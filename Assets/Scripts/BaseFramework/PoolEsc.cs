using System.Collections.Generic;
using UnityEngine;

public class PoolEsc : InstanceBaseAuto_Mono<PoolEsc>
{
    public List<string> ListEsc = new();        

    private void Start()
    {
        Hot.CenterEvent_.AddEventListener<KeyCode>(E_InputKeyEvent.KeyDown.ToString(), (key) =>
        {
            if (key == KeyCode.Escape)
            {
                HideTop();
            }
        });
    }

    /// <summary>
    /// �������м���PoolEsc����岢ִ������Ӧ��Esc�¼�
    /// </summary>
    public void HideAllAndInvokeEvent()
    {
        int tempCount = ListEsc.Count;
        for (int i = 0; i < tempCount; i++)
            HideTop();
    }

    /// <summary>
    /// ֻ���������м���PoolEsc����嵫��ִ������Ӧ��Esc�¼�
    /// </summary>
    public void HideAllOnly()
    {
        int tempCount = ListEsc.Count;

        for (int i = 0; i < tempCount; i++)
        {
            if (ListEsc.Count > 0)
            {
                //������ж���Ϊ�˷�ֹ������PoolEsc��û�м���MgrUI��DicPanel�е����
                //��ִ������Hot.MgrUI_.GetPanel(ListEsc[^1]).gameObject���߼�ʱ�Ŀ����ñ���
                if (Hot.MgrUI_.ContainPanel(ListEsc[^1]))
                    PoolBuffer.GetInstance().
                        Push(false, Hot.MgrUI_.GetPanel(ListEsc[^1]).gameObject, ListEsc[^1]);

                PoolNowPanel.GetInstance().ListNowPanel.Remove(ListEsc[^1]);

                ListEsc.Remove(ListEsc[^1]);
            }
        }
    }

    /// <summary>
    /// ������PoolEsc���˵����
    /// </summary>
    public void HideTop()
    {
        if (ListEsc.Count > 0)
        {
            Hot.CenterEvent_.EventTrigger("Esc" + ListEsc[^1]);

            //������ж���Ϊ�˷�ֹ������PoolEsc��û�м���MgrUI��DicPanel�е����
            //��ִ������Hot.MgrUI_.GetPanel(ListEsc[^1]).gameObject���߼�ʱ�Ŀ����ñ���
            if (Hot.MgrUI_.ContainPanel(ListEsc[^1]))
            {
                PoolBuffer.GetInstance().Push(false, Hot.MgrUI_.GetPanel(ListEsc[^1]).gameObject, ListEsc[^1]);
            }

            PoolNowPanel.GetInstance().ListNowPanel.Remove(ListEsc[^1]);

            ListEsc.Remove(ListEsc[^1]);
        }               
    }
}
