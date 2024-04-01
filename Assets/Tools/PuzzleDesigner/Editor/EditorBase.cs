using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ConstellationEditor
{
    public class EditorBase : EditorWindow
    {
        [MenuItem("Tools/Constellation Designer")]
        public static void ShowMyEditor()
        {
            // This method is called when the user selects the menu item in the Editor
            EditorWindow wnd = GetWindow<EditorBase>();
            wnd.titleContent = new GUIContent("Constellation Designer");
        }

        public void CreateGUI()
        {
            rootVisualElement.Add(new Label("Hello Sylvie"));
        }
    }
}