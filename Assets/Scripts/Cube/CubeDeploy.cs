using Commons;
using UnityEngine;
public class CubeDeploy 
{ 
    public void Deploy(GameObject currentCube, GameObject previousCube, CubeMovement.Direction side)
    {
        float hangDistance = CalcHangDistance(currentCube.transform, previousCube.transform, side);
        float maxDistance = CalcMaxDistance(currentCube.transform, previousCube.transform, side);
        if (hangDistance > maxDistance ) //아예 안겹칠 때 실패
        {
            
            GameManager.Instance.GameOver();
        } else if (hangDistance < Constants.HANGMATCHDISTANCE) // 보정값을 포함한 완벽
        {
            // Upper Function;
            new Vector3(previousCube.transform.position.x, previousCube.transform.position.y, LastCube.transform.position.z);
        }
        else // 단면 커팅
        {
            
        }
    }

    private float CalcHangDistance(Transform currentCube, Transform previousCube, CubeMovement.Direction side)
    {
        if(side == CubeMovement.Direction.X)
            return Mathf.Abs(currentCube.position.x - previousCube.position.x);
        else if (side == CubeMovement.Direction.Z)
            return Mathf.Abs(currentCube.position.z - previousCube.position.z);
        return 0f; // if add direction change else 
    }

    private float CalcMaxDistance(Transform currentCube, Transform previousCube, CubeMovement.Direction side)
    {
        if (side == CubeMovement.Direction.X)
            return currentCube.localScale.x;
        else if (side == CubeMovement.Direction.Z)
            return currentCube.localScale.z;
        return currentCube.localScale.y; // if add direction change else 
    }

    private GameObject CreateNewCube()
    {
        
    }
}