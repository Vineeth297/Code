using System.Collections;
using UnityEngine;

public class GuardBox : MonoBehaviour
{
	public static event System.Action OnGuardSpottedThePlayer;
	
	[SerializeField] private Transform pathHolder;
	[SerializeField] private float speed;
	[SerializeField] private float turnSpeed;

	[SerializeField] private Light spotLight;
	[SerializeField] private float viewDistance;
	private Color _startingSpotLightColor;
	
	private float _viewAngle;
	private Transform _player;

	[SerializeField] private LayerMask viewMask;

	[SerializeField] private float timeToSpotPlayer;
	private float _playerVisibilityTimer;
	private void Start()
	{
		_viewAngle = spotLight.spotAngle;
		_startingSpotLightColor = spotLight.color;
		_player = GameObject.FindGameObjectWithTag("Player").transform;
		
		Vector3[] wayPoints = new Vector3[pathHolder.childCount];
		for (int i = 0; i < wayPoints.Length; i++)
		{
			wayPoints[i] = pathHolder.GetChild(i).position;
			wayPoints[i] = new Vector3(wayPoints[i].x, transform.position.y, wayPoints[i].z);
		}

		StartCoroutine(MoveGuard(wayPoints));
	}

	private void Update()
	{
		if (CanSeePlayer())
			_playerVisibilityTimer += Time.deltaTime;
		else
			_playerVisibilityTimer -= Time.deltaTime;
		
		_playerVisibilityTimer = Mathf.Clamp(_playerVisibilityTimer, 0f, timeToSpotPlayer);
		spotLight.color = Color.Lerp(_startingSpotLightColor, Color.red, _playerVisibilityTimer / timeToSpotPlayer);

		if (_playerVisibilityTimer >= timeToSpotPlayer)
		{
			OnGuardSpottedThePlayer?.Invoke();   // if event is not null
		}
	}
	
	private bool CanSeePlayer()
	{
		if (Vector3.Distance(transform.position, _player.position) < viewDistance)
		{
			var playerDirection = _player.position - transform.position;
			var angleBetweenPlayerAndGuard = Vector3.Angle(transform.forward, playerDirection);
			if (angleBetweenPlayerAndGuard < _viewAngle / 2f)
			{
				if (!Physics.Linecast(transform.position, _player.position, viewMask))
					return true;
			}
		}
		return false;
	}
	
	private void OnDrawGizmos()
	{
		var startPosition = pathHolder.GetChild(0).position;
		var previousPosition = startPosition;

		foreach (Transform waypoint in pathHolder)
		{
			Gizmos.DrawSphere(waypoint.position,0.3f);
			Gizmos.DrawLine(previousPosition,waypoint.position);
			previousPosition = waypoint.position;
		}
		Gizmos.DrawLine(previousPosition,startPosition);
		
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position,transform.forward * viewDistance);
	}

	private IEnumerator MoveGuard(Vector3[] wayPoints)
	{
		transform.position = wayPoints[0];
		
		int waypPointIndex = 1;
		var targetWayPoint = wayPoints[waypPointIndex];
		transform.LookAt(targetWayPoint);
		
		while (true)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
			if (transform.position == targetWayPoint)
			{
				waypPointIndex = (waypPointIndex + 1) % wayPoints.Length;
				targetWayPoint = wayPoints[waypPointIndex];
				
				yield return new WaitForSeconds(0.3f);
				yield return StartCoroutine(RotateGuard(targetWayPoint));  // wait till the guard is rotating
			}
			yield return null; //yield for 1 frame reach iteration of while loop. => gives a bit smooth movement!!
		}
	}

	private IEnumerator RotateGuard(Vector3 lookTarget)
	{
		var lookTargetDirection = (lookTarget - transform.position).normalized;
		var targetAngle = 90 - Mathf.Atan2(lookTargetDirection.z, lookTargetDirection.x) * Mathf.Rad2Deg;

		while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f) // mathf.abs is to properly rotate in required direction
		{
			var angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
			transform.eulerAngles = Vector3.up * angle;
			yield return null;
		}
	}
}
