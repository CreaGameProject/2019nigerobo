using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraManagement : MonoBehaviour
{
    [SerializeField] GameObject targetObj;
    [SerializeField] private Vector3 targetPos;

    [SerializeField] private Transform targetPlayer;
    [SerializeField] List<string> coverLayerNameList;
    private int layerMask;
    public List<Renderer> rendererHitsList = new List<Renderer>();
    public Renderer[] rendererHitsPrevs;

    void Start()
    {
        targetPos = new Vector3(targetObj.transform.position.x, 0, targetObj.transform.position.z);

        layerMask = 0;
        foreach(string layerName in coverLayerNameList)
        {
            layerMask |= 1 << LayerMask.NameToLayer(layerName);
        }
    }

    void Update()
    {
        transform.position += new Vector3(targetObj.transform.position.x, 0, targetObj.transform.position.z) - targetPos;
        targetPos = new Vector3(targetObj.transform.position.x, 0, targetObj.transform.position.z);

        float mouseInputX = Input.GetAxis("Mouse X");

        transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 100f);


        Vector3 differnce = (targetPlayer.transform.position - this.transform.position);
        Vector3 direction = differnce.normalized;
        Ray ray = new Ray(this.transform.position, direction);

        // 前回の結果を退避してから、Raycast して今回の遮蔽物のリストを取得する
        RaycastHit[] hits = Physics.RaycastAll(ray, differnce.magnitude, layerMask);


        rendererHitsPrevs = rendererHitsList.ToArray();
        rendererHitsList.Clear();
        // 遮蔽物は一時的にすべて描画機能を無効にする。
        foreach (RaycastHit hit in hits)
        {
            // 遮蔽物が被写体の場合は例外とする
            if (hit.collider.gameObject == targetPlayer)
            {
                continue;
            }

            // 遮蔽物の Renderer コンポーネントを無効にする
            Renderer _renderer = hit.collider.gameObject.GetComponent<Renderer>();
            if (_renderer != null)
            {
                rendererHitsList.Add(_renderer);
                _renderer.enabled = false;
            }
        }

        // 前回まで対象で、今回対象でなくなったものは、表示を元に戻す。
        foreach (Renderer _renderer in rendererHitsPrevs.Except(rendererHitsList))
        {
            // 遮蔽物でなくなった Renderer コンポーネントを有効にする
            if (_renderer != null)
            {
                _renderer.enabled = true;
            }
        }
    }
}
