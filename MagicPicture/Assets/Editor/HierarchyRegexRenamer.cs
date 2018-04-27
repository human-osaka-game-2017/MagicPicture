namespace HierarchyRegexRenamer
{
    using UnityEngine;
    using UnityEditor;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class HierarchyRegexRenamer : EditorWindow
    {
        [SerializeField] string pattern = ""; // Regexパターン
        [SerializeField] string replacement = ""; // 置換文字列

        // Hierarchy上で選択しているオブジェクトをリネームする
        void DoRename()
        {
            var gameObjects = Selection.gameObjects.Where(go => !AssetDatabase.IsMainAsset(go)).ToArray(); // リネーム対象のGameObject

            // Undoに登録
            Undo.RecordObjects(gameObjects, "Regex Rename");

            // 名前を変える
            foreach (var go in gameObjects)
            {
                go.name = Regex.Replace(go.name, this.pattern, this.replacement);
            }
        }

        // EditorWindowの描画処理
        void OnGUI()
        {
            EditorGUILayout.LabelField("Hierarchy上で選択しているオブジェクトをリネームします");
            GUILayout.Space(2f);

            this.pattern = EditorGUILayout.TextField("Regex", this.pattern);
            this.replacement = EditorGUILayout.TextField("Replacement", this.replacement);

            EditorGUI.BeginDisabledGroup(Selection.gameObjects.Length == 0);

            // ボタンを表示
            if (GUILayout.Button("リネーム"))
            {
                this.DoRename();
            }

            EditorGUI.EndDisabledGroup();
        }

        // ウィンドウを開く
        [MenuItem("EditorWindow/Hierarchy Regex Renamer")]
        static void Open()
        {
            GetWindow<HierarchyRegexRenamer>();
        }
    }
}