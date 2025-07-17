using UnityEngine;

public class CharacterController
{
    public PlayerMovement playerMovement;
    public float[,] spacing = new float[4, 2] { { 0.15f, 0.2f }, { 0.2f, 0.15f }, { 0.1f, 0.1f }, { 0.5f, 0.05f } };
    public float smoothFactor = 0.5f;

    public void Move(Vector3 moveDirection)
    {
        playerMovement.transform.position += moveDirection;
        playerMovement.transform.position = Vector3.Lerp(playerMovement.transform.position, playerMovement.transform.position + moveDirection, Time.deltaTime);
    }
    public void followKing(Character king)
    {
        if (king == null)
        {
            
        }
        else
        {
                        // ...existing code...
            if (!playerMovement._isKing)
            {
                // Move towards the player smoothly
                playerMovement.transform.position = Vector3.Lerp(
                    playerMovement.transform.position,
                    king.transform.position - new Vector3(getSpacing(playerMovement).x, getSpacing(playerMovement).y, 0),
                    smoothFactor * Time.deltaTime
                );
            
                // Xoay theo hướng của king
                playerMovement.transform.rotation = Quaternion.Euler(0, king.transform.rotation.eulerAngles.y, 0);
            }
            // ...existing code...
        }
        
    }
    public Vector2 getSpacing(PlayerMovement playerMovement)
    {
        return new Vector2(spacing[playerMovement.transform.GetSiblingIndex()-1, 0], spacing[playerMovement.transform.GetSiblingIndex()-1, 1]); 
    }
    public void MoveToPositionSmooth(Vector3 targetPosition)
    {
        playerMovement.transform.position = Vector3.Lerp(
            playerMovement.transform.position,
            targetPosition,
            smoothFactor * Time.deltaTime
        );
    }
}