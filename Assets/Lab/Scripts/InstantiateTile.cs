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
        // �C�x���g��ϐ��Ƃ��ĕۑ�
        var currentEvent = Event.current;

        if (currentEvent.type != EventType.MouseUp)
        {
            return;
        }

        // �}�E�X�̍��N���b�N�̂Ƃ�true
        if (_flag && (currentEvent.type == EventType.MouseUp) && currentEvent.button == 0)
        {
            // ���W�̃Y�����N���Ȃ��悤�Ƀ}�E�X�̃|�W�V�������擾
            _mousePosition = new Vector3(currentEvent.mousePosition.x,
                Camera.current.pixelHeight - currentEvent.mousePosition.y,
                0);
            //Debug.Log(_mousePosition);
            Create();
            //Debug.Log(currentEvent.type);

            // ���C�L���X�g���΂�
            //if (Physics.Raycast(Camera.current.ScreenPointToRay(mousePosition), out var hit))
            //{
            //    Debug.Log(hit.point);
            //}
        }
    }

    public static void Create()
    {
        //�V�����Q�[���I�u�W�F�N�g���쐬�A���̎���Undo�ɋL�^
        GameObject newGameObject = new GameObject("New GameObject") ;
        //newGameObject.transform.position = _mousePosition;
        Undo.RegisterCreatedObjectUndo(_gameObject, "Create New GameObject");

        //�J���Ă���V�[����S�ĕۑ�(�O������Undo�ɋL�^���Ă��Ȃ������͕ۑ�����Ȃ�)
        EditorSceneManager.SaveOpenScenes();
    }
}
