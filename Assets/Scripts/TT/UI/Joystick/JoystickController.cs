using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TT
{
    [RequireComponent(typeof(UnityEngine.UI.Image))]
    public class JoystickController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region Manager Joystick
        static Dictionary<string, JoystickController> _joysticks = new Dictionary<string, JoystickController>();

        public static void AddJoystick(JoystickController joystick, bool destroyIfExists = true)
        {
            if (joystick == null)
            {
                Debug.Log("Joystick cannot be null");
                return;
            }

            if (_joysticks.ContainsKey(joystick.JoysickID))
            {
                Debug.Log(string.Format("Joystick ID: {0} exists!", joystick.JoysickID));
                if (destroyIfExists)
                {
                    Destroy(joystick.gameObject);
                    Debug.Log("Has destroyed Joystick ID: " + joystick.JoysickID);
                }
            }
            else
            {
                _joysticks.Add(joystick._joysickID, joystick);
            }
        }

        public static void RemoveJoystick(JoystickController joystick)
        {
            _joysticks.Remove(joystick._joysickID);
        }

        public static JoystickController GetJoystick(string nameType)
        {
            if (_joysticks.ContainsKey(nameType)) return _joysticks[nameType];
            return null;
        }
        #endregion

        #region Joystick Events
        public enum JoystickEvent { JoyBeginDrag, JoyDrag, JoyEndDrag, }
        protected ObserverEvents<JoystickEvent, JoystickController> _events = new ObserverEvents<JoystickEvent, JoystickController>();
        public ObserverEvents<JoystickEvent, JoystickController> Events => _events;
        #endregion

        [Header("References")]
        [SerializeField] protected Image _joystick;
        [SerializeField] protected Transform _handle;

        [Header("Informations")]
        [SerializeField] protected string _joysickID;
        protected Vector2 _originPos;
        protected Vector2 _direction;
        protected float _radius;

        #region Get Method
        public virtual float Radius => _radius;
        public virtual string JoysickID => _joysickID;
        public virtual Vector3 Direction => _direction;
        public virtual bool IsControl => !_direction.Equals(Vector3.zero);
        #endregion

        protected virtual void Awake()
        {
            JoystickController.AddJoystick(this);
        }

        protected virtual void Start()
        {
            _originPos = _joystick.transform.position;
            RectTransform joyRect = _joystick.GetComponent<RectTransform>();

            RectTransform rectCanvas = FindRectOfCanvasInParent(transform.parent);
            float canvasRectLocalScale = rectCanvas ? rectCanvas.localScale.x : 1f;

            _radius = (joyRect.rect.width / 2f) * canvasRectLocalScale;
        }

        protected virtual void OnDestroy()
        {
            JoystickController.RemoveJoystick(this);
        }

        protected virtual RectTransform FindRectOfCanvasInParent(Transform inParent)
        {
            if (inParent == null) return null;

            Canvas canvas = inParent.GetComponent<Canvas>();
            if (canvas != null) return canvas.GetComponent<RectTransform>();

            return FindRectOfCanvasInParent(inParent.parent);
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            _joystick.transform.position = eventData.position;
            _events.Notify(JoystickEvent.JoyBeginDrag, this);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            Vector2 realDirection = Vector2.ClampMagnitude(eventData.position - (Vector2)_joystick.transform.position, _radius);

            _direction = realDirection.normalized;
            _handle.position = (Vector2)_joystick.transform.position + realDirection;
            _events.Notify(JoystickEvent.JoyDrag, this);
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            _joystick.transform.position = _originPos;
            _handle.localPosition = Vector2.zero;
            _events.Notify(JoystickEvent.JoyEndDrag, this);
            _direction = Vector2.zero;
        }
    }
}
