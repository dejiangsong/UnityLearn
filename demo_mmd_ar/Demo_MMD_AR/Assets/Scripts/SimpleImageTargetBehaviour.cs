//=============================================================================================================================
//
// Copyright (c) 2015-2017 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
// EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
// and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
//
//=============================================================================================================================

using UnityEngine;

namespace EasyAR
{
    public class SimpleImageTargetBehaviour : ImageTargetBaseBehaviour
    {
        /* ×¢ÊÍ´úÂëÎªÇàÑÛ°×ÁúÓÃ */
        //public GameObject BEWD;
        //public float speed = 0.02f;

        //private Vector3 v_old = new Vector3();
        //private Vector3 v = new Vector3();

        protected override void Awake()
        {
            base.Awake();
            TargetFound += OnTargetFound;
            TargetLost += OnTargetLost;
            TargetLoad += OnTargetLoad;
            TargetUnload += OnTargetUnload;
        }


        void OnTargetFound(TargetAbstractBehaviour behaviour)
        {
            Debug.Log("Found:" + Target.Id);
        }

        
        void OnTargetLost(TargetAbstractBehaviour behaviour)
        {
            Debug.Log("Lost:" + Target.Id);
            //BEWD.transform.position = v_old;      //´æ´¢³õÊ¼Î»ÖÃ£¬±ãÓÚÇàÑÛ°×Áú¹éÎ»
        }


        void OnTargetLoad(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
        {
            Debug.Log("Load target (" + status + "):" + Target.Id + "(" + Target.Name + ")" + "->" + tracker);
        }


        void OnTargetUnload(ImageTargetBaseBehaviour behaviour, ImageTrackerBaseBehaviour tracker, bool status)
        {
            Debug.Log("UnLoad target (" + status + "):" + Target.Id + "(" + Target.Name + ")" + "->" + tracker);
        }


        //protected override void Start()
        //{
        //    base.Start();
        //    v.y = speed;
        //    v_old = BEWD.transform.position;
        //}


        //protected override void Update()
        //{
        //    base.Update();
        //    if (BEWD.transform.position.y < 1)
        //    {
        //        BEWD.transform.position += v;
        //    }
        //}

    }
}
