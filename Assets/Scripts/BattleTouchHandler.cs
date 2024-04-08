using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTouchHandler : TouchHandler
{
    public BattleManager manager;
    private float enter;

    private GamePiece selected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TouchUpdateLoop();
    }

    public Vector3 ScreenToWorldPos(Vector2 screenPos)
    {
        Ray ray = manager.mainCamera.ScreenPointToRay(screenPos);
        if (manager.board.plane.Raycast(ray, out enter))
        {
            return ray.GetPoint(enter);
        }
        return Vector3.zero;
    }

    public void ClearSelection()
    {
        if (selected != null)
        {
            selected.meshRenderer.material.color = Color.white;
            manager.selectedAnimist.selectedAnimist = null;
            manager.selectedAnimist.gameObject.SetActive(false);
            manager.selectedSpirit.selectedSpirit = null;
            manager.selectedSpirit.gameObject.SetActive(false);
            selected = null;
        }
    }

    //Clears selection and returns false if gamePiece is null
    public bool TrySelectPiece(GamePiece gamePiece)
    {
        ClearSelection();

        selected = gamePiece;
        if (selected != null)
        {
            selected.meshRenderer.material.color = Color.blue;
            if (selected.pieceName != "Animist")
            {
                manager.selectedSpirit.selectedSpirit = (Spirit)gamePiece;
                manager.selectedSpirit.gameObject.SetActive(true);
            }
            else
            {
                manager.selectedAnimist.selectedAnimist = (Animist)gamePiece;
                manager.selectedAnimist.gameObject.SetActive(true);
            }
            return true;
        }
        return false;
    }

    public override void HandleTap()
    {
        Debug.Log(manager.board.GetClosestGrid(ScreenToWorldPos(touchStartPosition)));
        TrySelectPiece(manager.GetPieceFromCoords(manager.board.GetClosestGrid(ScreenToWorldPos(touchStartPosition))));
    }

    public override void HandleDrag()
    {
        (int x, int y) startCoord = manager.board.GetClosestGrid(ScreenToWorldPos(touchStartPosition));
        GamePiece gamePiece = manager.GetPieceFromCoords(startCoord);

        if (gamePiece != selected)
        {
            ClearSelection();
            if (gamePiece != null)
            {
                gamePiece.meshRenderer.material.color = Color.cyan;
            }
        }

        if (gamePiece == null || startCoord.y >= BattleManager.boardY / 2)
        {
            return;
        }

        if (!manager.isTurnOver)
        {
            (int x, int y) coords = manager.board.GetClosestGrid(ScreenToWorldPos(touchEndPosition));
            if (coords.y >= BattleManager.boardY / 2)
            {
                return;
            }
            manager.CreateMovementInput(startCoord, coords);
            //if (coords.y <= manager.board.length / 2.0f)
            {
                GamePiece replace = manager.GetPieceFromCoords(coords);
                if (replace == null)
                {
                    Debug.Log("replace null");
                    if (coords.x != -1 && coords.y != -1)
                    {
                        gamePiece.SetPosition(coords.x, coords.y);
                    }
                }
                else
                {
                    Debug.Log("swapped");
                    manager.SwapPosition(gamePiece, replace);
                    replace.meshRenderer.material.color = Color.cyan;

                }
            }
        }
    }
}
