using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Misc.Editor
{
    public abstract class PaginationWindow<T> : EditorWindow
    {
        // The position on of the scrolling viewport
        private Vector2 _scrollPosition = Vector2.zero;

        #region Page

        public int PageSize { get; protected set; }
        public int MaxPages { get; private set; }

        public int Page {
            get => _page;
            private set {
                _page = value;
                if (_page >= MaxPages) _page = MaxPages - 1;
                if (_page < 0) _page = 0;
                _scrollPosition = Vector2.zero;
            }
        }

        private int _page;

        #endregion

        protected PaginationWindow(int pageSize = 50) {
            PageSize = pageSize;
        }

        protected abstract IList<T> Elements();

        protected virtual void OnGUI() {
            GUILayout.BeginVertical();
            var elements = Elements();
            Header();
            Navigation(elements);
            InternalRender(elements);
            Footer();
            GUILayout.EndVertical();
        }

        protected virtual void Header() { }

        protected virtual void Navigation(IList<T> elements) {
            var count = elements.Count;
            MaxPages = (count + PageSize - 1) / PageSize;

            GUILayout.BeginHorizontal();
            var exists = count > 0;
            GUI.enabled = exists && Page > 0;
            if (GUILayout.Button("⇤")) Page = 0;
            if (GUILayout.Button("←")) Page -= 1;
            GUI.enabled = true;
            var pages = exists ? $"{Page + 1}/{MaxPages} ({count})" : "-/- (-)";
            GUILayout.Label($"{pages}", GUILayout.ExpandWidth(false));
            GUI.enabled = exists && Page < MaxPages - 1;
            if (GUILayout.Button("→")) Page += 1;
            if (GUILayout.Button("⇥")) Page = MaxPages - 1;
            GUI.enabled = true;
            GUILayout.EndHorizontal();
        }

        private void InternalRender(IEnumerable<T> elements) {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition,
                GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            var page = elements.Skip(Page * PageSize).Take(PageSize).ToList();
            if (page.Count > 0)
                Render(page);
            else
                RenderEmpty();
            GUILayout.EndScrollView();
        }

        protected virtual void Render(IEnumerable<T> page) {
            foreach (var element in page) RenderElement(element);
        }

        protected abstract void RenderElement(T element);

        protected virtual void RenderEmpty() {
            GUILayout.BeginHorizontal();
            GUILayout.Label("No elements");
            GUILayout.EndHorizontal();
        }

        protected virtual void Footer() { }
    }
}