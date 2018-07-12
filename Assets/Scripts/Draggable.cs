using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	
	public Transform parentToReturnTo = null;
	public Transform placeholderParent = null;

	GameObject placeholder = null;
    private bool notDragging;

	public void OnBeginDrag(PointerEventData eventData) {
        //Debug.Log ("OnBeginDrag");

        if (this.transform.parent && this.transform.parent.transform.parent.GetComponent<ScrollRect>())
        {
            float angle = Mathf.Atan2(Mathf.Abs(eventData.delta.y), Mathf.Abs(eventData.delta.x)) * Mathf.Rad2Deg;
            if (angle < 45)
            {
                notDragging = true;
                this.transform.parent.transform.parent.GetComponent<ScrollRect>().OnBeginDrag(eventData);
                return;
            }
        }

        placeholder = new GameObject();
		placeholder.transform.SetParent( this.transform.parent );
		LayoutElement le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;

		placeholder.transform.SetSiblingIndex( this.transform.GetSiblingIndex() );
		
		parentToReturnTo = this.transform.parent;
		placeholderParent = parentToReturnTo;
		this.transform.SetParent( this.transform.parent.parent.parent );
		
		GetComponent<CanvasGroup>().blocksRaycasts = false;


	}
	
	public void OnDrag(PointerEventData eventData) {
		//Debug.Log ("OnDrag");

        if (notDragging)
        {
            this.transform.parent.transform.parent.GetComponent<ScrollRect>().OnDrag(eventData);
            return;
        }
		
		this.transform.position = eventData.position;

		if(placeholder.transform.parent != placeholderParent)
			placeholder.transform.SetParent(placeholderParent);

		int newSiblingIndex = placeholderParent.childCount;

		for(int i=0; i < placeholderParent.childCount; i++) {
			if(this.transform.position.x < placeholderParent.GetChild(i).position.x) {

				newSiblingIndex = i;

				if(placeholder.transform.GetSiblingIndex() < newSiblingIndex)
					newSiblingIndex--;

				break;
			}
		}

		placeholder.transform.SetSiblingIndex(newSiblingIndex);

	}
	
	public void OnEndDrag(PointerEventData eventData) {
        if (notDragging)
        {
            notDragging = false;
            this.transform.parent.transform.parent.GetComponent<ScrollRect>().OnEndDrag(eventData);
            return;
        }

        //Debug.Log ("OnEndDrag");
		this.transform.SetParent( parentToReturnTo );
		this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex() );
		GetComponent<CanvasGroup>().blocksRaycasts = true;

		Destroy(placeholder);
	}
	
	
	
}
