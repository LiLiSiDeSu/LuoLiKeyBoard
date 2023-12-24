using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : InstanceBaseAuto_Mono<Start>
{
    private void Awake()
    {
        Hot.MgrUI_.CreatePanel<PanelKeyBoard>(true, "/PanelKeyBoard",
        (panel) =>
        {
            Hot.MgrData_.Init();
        });

        Application.runInBackground = true;

        Destroy(gameObject);
    }
}
