using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Misc
{
    /// <summary>
    ///     https://forum.unity.com/threads/scrollview-using-controller-arrowkeys.1008121/
    /// </summary>
    [RequireComponent(typeof(ScrollRect))]
    public class AutoScrollRect : MonoBehaviour
    {
        [Tooltip("Sets the speed to move the scrollbar")] [Min(0.001f)] [SerializeField]
        private float scrollSpeed = 80f;

        [Tooltip("Set as Template Object via (Your Dropdown Button > Template)")] [SerializeField]
        private ScrollRect templateScrollRect;

        [Tooltip("Set as Template Viewport Object via (Your Dropdown Button > Template > Viewport)")] [SerializeField]
        private RectTransform templateViewportTransform;

        [Tooltip("Set as Template Content Object via (Your Dropdown Button > Template > Viewport > Content)")]
        [SerializeField]
        private RectTransform contentRectTransform;

        private void Update() {
            UpdateScrollToSelected();
        }

        private void UpdateScrollToSelected() {
            // Get the current selected option from the event system.
            var selected = EventSystem.current.currentSelectedGameObject;

            if (selected == null) return;
            // depending how deap you button is nested you need to use `parent.parent.parent....`
            // if (selected.transform.parent != contentRectTransform.transform) return;
            var selectedRect = selected.transform.parent.parent.parent;
            if (selectedRect.parent != contentRectTransform.transform) return;
            // var selectedRectTransform = selected.GetComponent<RectTransform>();
            var selectedRectTransform = selectedRect.GetComponent<RectTransform>();

            // caching
            var contentRectTransformRect = contentRectTransform.rect;
            var selectedRectTransformRect = selectedRectTransform.rect;
            var viewportRectTransformRect = templateViewportTransform.rect;

            // Math stuff
            var selectedDifference = templateViewportTransform.localPosition - selectedRectTransform.localPosition;
            var contentHeightDifference = contentRectTransformRect.height - viewportRectTransformRect.height;

            var selectedPosition = contentRectTransformRect.height - selectedDifference.y;
            var currentScrollRectPosition = templateScrollRect.normalizedPosition.y * contentHeightDifference;
            var above = currentScrollRectPosition + viewportRectTransformRect.height;
            var below = currentScrollRectPosition + selectedRectTransformRect.height;

            // Check if selected option is out of bounds.
            if (selectedPosition > above) {
                var step = selectedPosition - above;
                var newY = currentScrollRectPosition + step;
                var newNormalizedY = newY / contentHeightDifference;
                templateScrollRect.normalizedPosition = Vector2.Lerp(templateScrollRect.normalizedPosition,
                    new Vector2(0, newNormalizedY), scrollSpeed * Time.deltaTime);
            }
            else if (selectedPosition < below) {
                var step = selectedPosition - below;
                var newY = currentScrollRectPosition + step;
                var newNormalizedY = newY / contentHeightDifference;
                templateScrollRect.normalizedPosition = Vector2.Lerp(templateScrollRect.normalizedPosition,
                    new Vector2(0, newNormalizedY), scrollSpeed * Time.deltaTime);
            }
        }
    }
}