using Commons;
using UnityEngine;

public class CubeDeploy : MonoBehaviour
{
    private Transform currentCube, previousCube;
    private CubeMovement.Direction side;
    private float hangDistance;
    private float hangSide;
    private float maxDistance;
    
    public GameObject Deploy(Transform currentCube, Transform previousCube, CubeMovement.Direction side)
    {
        // #### Parameter SetGet and Calculate ####
        this.currentCube = currentCube;
        this.previousCube = previousCube;
        this.side = side;
        hangDistance = CalcHangDistance();
        hangSide = hangDistance > 0 ? 1 : -1;
        hangDistance = Mathf.Abs(hangDistance); //좋은 방법은 아닌듯
        maxDistance = CalcMaxDistance();
        
        
        if (hangDistance > maxDistance ) //아예 안겹칠 때 실패
        {
            GameManager.Instance.GameOver();
        } else if (hangDistance < Constants.HANGMATCHDISTANCE) // 보정값을 포함한 완벽
        {
            // Upper Function;
            currentCube.position = new Vector3(
                previousCube.position.x, 
                currentCube.position.y, 
                previousCube.position.z);
        }
        else // 단면 커팅
        {
            //positiveCube : 절단 이후 계속 유지되는 큐브
            //negativeCube : 절단 이후 사라지는 큐브
            SplitCube();
        }

        return this.currentCube.gameObject;
    }

    private float CalcHangDistance()
    {
        if(side == CubeMovement.Direction.X)
            return currentCube.position.x - previousCube.position.x;
        else if (side == CubeMovement.Direction.Z)
            return currentCube.position.z - previousCube.position.z;
        return 0f; // if add direction change else 
    }

    private float CalcMaxDistance()
    {
        if (side == CubeMovement.Direction.X)
            return currentCube.localScale.x;
        else if (side == CubeMovement.Direction.Z)
            return currentCube.localScale.z;
        return currentCube.localScale.y; // if add direction change else 
    }

    private void SplitCube()
    {
        //if add side need to change this check
        float positiveCubeScale =
            (side == CubeMovement.Direction.X ? 
                previousCube.localScale.x : previousCube.localScale.z) - hangDistance;
        float negativeCubeScale = 
            (side == CubeMovement.Direction.X ? 
                currentCube.localScale.x :  currentCube.localScale. z ) - positiveCubeScale;
        float previousCubePosSide =
            (side == CubeMovement.Direction.X) ? previousCube.position.x : previousCube.position.z;
        float positiveCubePos = previousCubePosSide + (hangDistance / 2f);
        currentCube.localScale = new Vector3( currentCube.localScale.x, currentCube.localScale.y, positiveCubeScale);
        currentCube.position = new Vector3( currentCube.position.x, currentCube.position.y, positiveCubePos);

        float cubeEdge = currentCube.position.z + (positiveCubeScale / 2f * hangSide);
        float negativeCubePos = cubeEdge + (negativeCubeScale / 2f * hangSide);

        CreateNegativeCube(negativeCubePos, negativeCubeScale);
    }

    private GameObject CreateNegativeCube(float negativeCubePos, float negativeCubeScale)
    {
        GameObject cube = Instantiate(currentCube.gameObject);

        if (side == CubeMovement.Direction.X)
        {
            cube.transform.localScale = new Vector3(currentCube.localScale.x, currentCube.localScale.y, negativeCubeScale);
            cube.transform.position = new Vector3(currentCube.position.x, currentCube.position.y, negativeCubePos);
        }
        else if (side == CubeMovement.Direction.Z)
        {
            cube.transform.localScale = new Vector3(negativeCubeScale, currentCube.localScale.y, currentCube.localScale.z);
            cube.transform.position = new Vector3(negativeCubePos, currentCube.position.y, currentCube.position.z);
        }

        cube.AddComponent<Rigidbody>();
        return cube;
    }
}