using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
	public static List<CVRef> activeRefs = new List<CVRef>();
	public static int hatless;
	public Sprite rectSprite;

	public AudioClip successClip;
	public AudioSource audioSource;

	public Transform hatSlot;
	public SpriteRenderer headBorder;
	public string CVTitle;

	public LayerMask CVlayermask;

	public Transform topLeft;
	public Transform bottomRight;

	private float CVTickTimer;

	private int CVTimer;

	public List<GameObject> hats = new List<GameObject>();

    private NavMeshAgent refAgent;

	private bool isMovingToTarget;
	private Vector3 currentTarget;

	public bool hasHat;

	float timeNearTarget;

	public class CVRef
	{
		public Vector2 topLeft;
		public Vector2 bottomRight;
		public string title = "";
		public NPC parent;
		public Image personalRect;

		public CVRef(NPC parent, Sprite rectSprite)
		{
			this.parent = parent;
			
			GameObject imageParent = new GameObject("CV Rect");
			imageParent.transform.SetParent(DroneController.GUIRef.transform);

			personalRect = imageParent.AddComponent<Image>();
			personalRect.sprite = rectSprite;
			personalRect.type = Image.Type.Sliced;

			personalRect.rectTransform.sizeDelta = Vector2.zero;
		}
	}

	private CVRef CVInfoBody;
	private CVRef CVInfoHead;

	private Vector2 savedTopLeft;
	private Vector2 savedBottomRight;

	private Camera droneCam;

	private void Start()
	{
		CVInfoBody = new CVRef(this, rectSprite);
		CVInfoHead = new CVRef(this, rectSprite);

		Invoke("RealStart", 0.1f);
	}

	private void RealStart()
	{
		headBorder.color = Color.red;

		refAgent = GetComponent<NavMeshAgent>();

		CVInfoBody.title = CVTitle;

		if (hasHat)
		{
			PutOnRandomHat();
			CVInfoHead.title = "Has hat";
		}
		else
		{
			hatless++;
			Debug.Log(hatless);
			CVInfoHead.title = "HATLESS!";
		}

		CVTickTimer = Random.Range(0f, 0.5f);

		droneCam = DroneController.DroneCam.gameObject.GetComponentInChildren<Camera>();
	}

	//private void OnDrawGizmos()
	//{
	//	Gizmos.color = Color.yellow;
	//	Gizmos.DrawSphere(topLeft.position, 0.5f);
	//	Gizmos.color = Color.red;
	//	Gizmos.DrawSphere(bottomRight.position, 0.5f);
	//}

	private void Update()
	{
		return;
		CVTickTimer += Time.deltaTime;

		if (CVTickTimer > 0.1)
		{
			CVTickTimer = 0f;

			bool leftHit = false;
			bool rightHit = false;
			bool onCamera = true;

			Vector3 screenPos = droneCam.WorldToScreenPoint(topLeft.position);

			var heading = topLeft.position - droneCam.transform.position;

			if (Vector3.Dot(droneCam.transform.forward, heading) < 0) 
			{
				savedTopLeft.x = screenPos.x;
				savedTopLeft.y = screenPos.y;

				//if (screenPos.x != 0)
				//{
				//	Debug.Log("(" + savedTopLeft.x + ", " + savedTopLeft.y + ")");
				//}

				onCamera = false;
			}

			screenPos = droneCam.WorldToScreenPoint(bottomRight.position);

			heading = bottomRight.position - droneCam.transform.position;

			if (Vector3.Dot(droneCam.transform.forward, heading) < 0)
			{
				savedBottomRight.x = screenPos.x;
				savedBottomRight.y = screenPos.y;

				//if (screenPos.x != 0)
				//{
				//	Debug.Log("(" + savedBottomRight.x + ", " + savedBottomRight.y + ")");
				//}

				onCamera = false;
			}

			if (savedTopLeft.x != 0)
				Debug.Log("(" + savedTopLeft.x + ", " + savedTopLeft.y + ") - (" + savedBottomRight.x + ", " + savedBottomRight.y + ")");

			if (onCamera)
			{ 
				RaycastHit hit;

				// Check left
				if 
				(
					Physics.Raycast
					(
						topLeft.position,
						topLeft.position - DroneController.DroneCam.position, 
						out hit, 
						Mathf.Infinity, 
						CVlayermask
					)
				)
				{
					leftHit = true;
				}

				// Check right
				if
				(
					Physics.Raycast
					(
						bottomRight.position,
						bottomRight.position - DroneController.DroneCam.position,
						out hit,
						Mathf.Infinity,
						CVlayermask
					)
				)
				{
					rightHit = true;
				}
			}

			if (leftHit && rightHit && onCamera)
			{
				CVTimer++;

				if (CVTimer > 4)
				{
					bool foundOne = false;
					int savedIndex = 0;

					for (int i = 0; i < activeRefs.Count; i++)
					{
						if (activeRefs[i].parent == this)
						{
							foundOne = true;
							savedIndex = i;

							break;
						}
					}

					if (!foundOne)
					{
						// Add it in.
						CVInfoBody.topLeft = savedTopLeft;
						CVInfoBody.bottomRight = savedBottomRight;

						activeRefs.Add(CVInfoBody);
						//activeRefs.Add(CVInfoHead); //TBA
					}
					else
					{
						activeRefs[savedIndex].topLeft = savedTopLeft;
						activeRefs[savedIndex].bottomRight = savedBottomRight;

						if (savedTopLeft.x != 0)
						{
							Debug.Log("(" + savedTopLeft.x + ", " + savedTopLeft.y + ") - (" + savedBottomRight.x + ", " + savedBottomRight.y + ")");
						}
					}
				}
			}
			else
			{
				CVTimer = 0;

				// Search of any existing CVRefs with this parent and remove them.
				for (int i = 0; i < activeRefs.Count; i++)
				{
					if (activeRefs[i].parent == this)
					{
						activeRefs[i].topLeft = Vector2.zero;
						activeRefs[i].bottomRight = Vector2.zero;
						Debug.Log("REMOVED");
					}
				}
			}
		}
	}

	private void LateUpdate()
	{
		if (!isMovingToTarget)
		{
			if (!FindNewTarget())
			{
				return;
			}
		}

		if (Vector3.Distance(transform.position, currentTarget) < 3)
		{
			timeNearTarget += Time.deltaTime;

			if (timeNearTarget >= 1f)
			{
				isMovingToTarget = false;
			}
		}
	}

	private bool FindNewTarget()
	{
		if (!refAgent) return false;

		currentTarget = PathPoint.GetRandomPoint();
		refAgent.SetDestination(currentTarget);
		isMovingToTarget = true;
		timeNearTarget = 0f;

		return true;
	}

	public void PutOnRandomHat()
	{
		int index = Random.Range(0, hats.Count);

		GameObject hat = GameObject.Instantiate<GameObject>(hats[index], hatSlot);

		hasHat = true;
		headBorder.color = Color.white;
		CVInfoHead.title = "Has hat";
	}

	public void PutOnHat(GameObject Hat)
	{
		audioSource.clip = successClip;
		audioSource.Play();

		GameObject hat = GameObject.Instantiate<GameObject>(Hat, hatSlot);
		hatless--;
		hasHat = true;
		headBorder.color = Color.white;
		CVInfoHead.title = "Given hat";
	}
}
