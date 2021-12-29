using Commons;
using UnityEngine;

public class CubeDeploy : MonoBehaviour
{
    public GameObject Deploy( Transform currentCube,  Transform previousCube,  CubeMovement.Direction side)
    {
        // #### Parameter SetGet and Calculate ####

        float cubeDistance = CalcCubeDistance(currentCube.transform,previousCube.transform,side); //x 혹은 z 축을 기준로 거리를 잽니다.
        float cubeDistanceSide = cubeDistance > 0 ? 1 : -1; //큐브 간의 거리의 방향 normal;
        cubeDistance = Mathf.Abs(cubeDistance);
        float cubeMaxDistance = CalcCubeScale(currentCube.transform,previousCube.transform,side); //x 혹은 z 축을 기준의 cube의 scale을 반환합니다.


        if (cubeDistance > cubeMaxDistance)
        {
            GameManager.Instance.GameOver();
        }
        else if (cubeDistance < Constants.HANGMATCHDISTANCE)
        {
            currentCube.position = new Vector3(
                previousCube.position.x, 
                currentCube.position.y, 
                previousCube.position.z);
        }
        else
        {
            CutCube(
                currentCube.transform,
                previousCube.transform,
                cubeDistance,side,
                cubeDistanceSide);
        }

        return currentCube.gameObject;
    }

    private void CutCube(
        Transform currentCube, Transform previousCube,
        float cubeDistance, CubeMovement.Direction side,
        float cubeDistanceSide)
    {
        float positiveCubeScale = side == CubeMovement.Direction.X
            ? previousCube.localScale.x
            : previousCube.localScale.z - cubeDistance;
        float negativeCubeScale = side == CubeMovement.Direction.X 
            ? currentCube.localScale.x 
            :  currentCube.localScale.z - positiveCubeScale;
        
        float previousCubePosSide = side == CubeMovement.Direction.X 
            ? previousCube.position.x 
            : previousCube.position.z;
        float positiveCubePos = previousCubePosSide + (cubeDistance / 2f);
        
        currentCube.localScale = new Vector3(
            currentCube.localScale.x,
            currentCube.localScale.y,
            positiveCubeScale);
        currentCube.position = new Vector3( 
            currentCube.position.x,
            currentCube.position.y,
            positiveCubePos);

        float cubeEdge = currentCube.position.z + (positiveCubeScale / 2f * cubeDistanceSide);
        float negativeCubePos = cubeEdge + (negativeCubeScale / 2f * cubeDistanceSide);

        GameObject negativeCube = CutNegativeCube(currentCube, side, negativeCubePos, negativeCubeScale);
        StartCoroutine(Collection.WaitThenCallback(10f, () => { Destroy(negativeCube); }));
    }

    private float CalcCubeDistance(Transform currentCube, Transform previousCube, CubeMovement.Direction side)
    {
        if (side == CubeMovement.Direction.X)
            return currentCube.position.x - previousCube.position.x;
        else if (side == CubeMovement.Direction.Z)
            return currentCube.position.z - previousCube.position.z;
        return 0f; // if add direction change else 
    }

    private float CalcCubeScale(Transform currentCube, Transform previousCube,CubeMovement.Direction side )
    {
        if(side == CubeMovement.Direction.X)
            return currentCube.position.x - previousCube.position.x;
        else if (side == CubeMovement.Direction.Z)
            return currentCube.position.z - previousCube.position.z;
        return 0f; // if add direction change else 
    }
    
    private GameObject CutNegativeCube(Transform currentCube, CubeMovement.Direction side, float negativeCubePos, float negativeCubeScale)
    {
        GameObject negativeCube = Instantiate(currentCube.gameObject);

        if (side == CubeMovement.Direction.X)
        {
            negativeCube.transform.localScale = new Vector3(currentCube.localScale.x, currentCube.localScale.y, negativeCubeScale);
            negativeCube.transform.position = new Vector3(currentCube.position.x, currentCube.position.y, negativeCubePos);
        }
        else if (side == CubeMovement.Direction.Z)
        {
            negativeCube.transform.localScale = new Vector3(negativeCubeScale, currentCube.localScale.y, currentCube.localScale.z);
            negativeCube.transform.position = new Vector3(negativeCubePos, currentCube.position.y, currentCube.position.z);
        }
        negativeCube.AddComponent<Rigidbody>();
        return negativeCube;
    }
}