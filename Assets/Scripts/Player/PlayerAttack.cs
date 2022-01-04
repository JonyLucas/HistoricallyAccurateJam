using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float _maxArrowCount = 4;

    [SerializeField]
    private float _fireRate = 1f;

    [SerializeField]
    private GameObject _arrowPrefab;

    [SerializeField]
    private GameObject _arrowParent;

    private List<GameObject> _playerArrows;

    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        InitializeArrows();
       
    }

    private void InitializeArrows()
    {
         _playerArrows = new List<GameObject>();
        for (int i = 0; i < _maxArrowCount; i++)
        {
            var arrowObj = Instantiate(_arrowPrefab);
            arrowObj.SetActive(false);
            _playerArrows.Add(arrowObj);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ShootArrows();
        }
    }

    private void ShootArrows()
    {
        var arrow = _playerArrows.FirstOrDefault(x => !x.activeInHierarchy);
        if (arrow != null)
        {
            arrow.transform.position = transform.position;
            arrow.GetComponent<ArrowMovement>().MoveDirection = _playerMovement.FacingRight ? Vector2.right : Vector2.left;
            arrow.SetActive(true);
        }
    }
}