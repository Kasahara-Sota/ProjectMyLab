using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class InstantiateTile : EditorWindow
{
    private static bool _flag;
    private static GameObject _gameObject;
    private static Vector3 _mousePosition;
    [MenuItem("Tools/KurokumaSoft/Change Texture Max Size")]
    public static void ShowWindow()
    {
        GetWindow<InstantiateTile>("Change Texture Max Size");
    }

    //public void OnEnable()
    //{
    //    SceneView.duringSceneGui += OnSceneViewClick;
    //    //Debug.Log("Enable");
    //}

    public void OnGUI()
    {
        _flag = EditorGUILayout.Toggle("PositionSetting", _flag);

        if (_flag)
        {
            SceneView.duringSceneGui -= OnSceneViewClick;
            SceneView.duringSceneGui += OnSceneViewClick;
        }
        else
        {
            SceneView.duringSceneGui -= OnSceneViewClick;
        }

        _gameObject = (GameObject)EditorGUILayout.ObjectField("Select Prefab", _gameObject, typeof(GameObject), false);
    }
    public void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneViewClick;
        //Debug.Log("Disable");
    }

    public static void OnSceneViewClick(SceneView scene)
    {
        // イベントを変数として保存
        var currentEvent = Event.current;

        if (currentEvent.type != EventType.MouseUp)
        {
            return;
        }

        // マウスの左クリックのときtrue
        if (_flag && (currentEvent.type == EventType.MouseUp) && currentEvent.button == 0)
        {
            // 座標のズレが起きないようにマウスのポジションを取得
            _mousePosition = new Vector3(currentEvent.mousePosition.x,
                Camera.current.pixelHeight - currentEvent.mousePosition.y,
                0);
            //Debug.Log(_mousePosition);
            Create();
            //Debug.Log(currentEvent.type);

            // レイキャストを飛ばす
            //if (Physics.Raycast(Camera.current.ScreenPointToRay(mousePosition), out var hit))
            //{
            //    Debug.Log(hit.point);
            //}
        }
    }

    public static void Create()
    {
        //新しいゲームオブジェクトを作成、その事をUndoに記録
        GameObject newGameObject = new GameObject("New GameObject") ;
        //newGameObject.transform.position = _mousePosition;
        Undo.RegisterCreatedObjectUndo(_gameObject, "Create New GameObject");

        //開いているシーンを全て保存(前もってUndoに記録していない部分は保存されない)
        EditorSceneManager.SaveOpenScenes();
    }
}
