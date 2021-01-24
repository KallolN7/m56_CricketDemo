using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCoordinates
{

    //public static Dictionary<int, ContactPoint> contactPointsDictionary = new Dictionary<int, ContactPoint>();
    /*    public static Dictionary<int, float> bouncePointZDictionary = new Dictionary<int, float>();
        public static Dictionary<int, float> keeperPointYDictionary = new Dictionary<int, float>();*/
    public static void Initialize()
    {
        Debug.Log("Initializing AllCoordinates");
    }

    public static Dictionary<int, ReleasePointDetails> releasePointsDictionary = new Dictionary<int, ReleasePointDetails>
    {
        {0,new ReleasePointDetails(0,122f,204f,1326f)},
        {1,new ReleasePointDetails(1,48f,204f,1326f)},
        {2,new ReleasePointDetails(2,-48f,204f,1326f)},
        {3,new ReleasePointDetails(3,-122f,204f,1326f)},
    };

    public class ReleasePointDetails
    {
        private int index;
        private float startingPointX, startingPointY, startingPointZ;

        public ReleasePointDetails(int index, float startingPointX, float startingPointY, float startingPointZ)
        {
            this.index = index;
            this.startingPointX = startingPointX;
            this.startingPointY = startingPointY;
            this.startingPointZ = startingPointZ;
        }

        public int GetRpIndex()
        {
            return index;
        }

        public Vector3 GetReleasePoint()
        {
            int mayaFactor = 100;
            return new Vector3(this.startingPointX, this.startingPointY, this.startingPointZ) / mayaFactor;
        }

        public Vector3 GetGroundLevelRp()
        {
            int mayaFactor = 100;
            return new Vector3(this.startingPointX, 0, this.startingPointZ) / mayaFactor;
        }

        public float GetCoordinate_X()
        {
            int mayaFactor = 100;
            return startingPointX / mayaFactor;
        }
    }
}