using MorningBird.IO;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;



namespace MorningBird.SubMonoClass
{
    
    public enum ESubModuleRequestErrorMessage : sbyte
    {
        None = 0,
        Confirmed = 1,
        AlreadyExist = 10,
        PlayerNotYetInitialized = 11,
    }

    public abstract class SubMonoClassCore : SerializedMonoBehaviour
    {
        [FoldoutGroup("Debug")]
        [ShowInInspector] public bool IsAwakeInitialized { protected set; get; } = false;
        [FoldoutGroup("Debug")]
        [ShowInInspector] public bool IsStartActioned { protected set; get; } = false;
        [FoldoutGroup("Debug")]
        [ShowInInspector] List<SubMonoClassModule> _subClassModuleList = new List<SubMonoClassModule>(20);

        protected virtual void Awake()
        {
            // find Modules and Turned on
            List<SubMonoClassModule> tempModules = new List<SubMonoClassModule>(20);
            transform.GetComponents<SubMonoClassModule>(tempModules);

            foreach (var moudle in tempModules)
            {
                moudle.InitializeAction();
                _subClassModuleList.Add(moudle);
            }
        }
        protected abstract void Start();
        protected abstract void Update();
        protected abstract void OnEnable();
        protected abstract void OnDisable();
        protected abstract void OnDestroy();

        public bool RequestApproach(SubMonoClassModule mouduleClass, out ESubModuleRequestErrorMessage errorMessage)
        {
            if (IsAwakeInitialized == false)
            {
                errorMessage = ESubModuleRequestErrorMessage.PlayerNotYetInitialized;
                return false;
            }

            foreach (var moudule in _subClassModuleList)
            {
                if (moudule == mouduleClass)
                {
                    errorMessage = ESubModuleRequestErrorMessage.AlreadyExist;
                    return true;
                }
            }

            errorMessage = ESubModuleRequestErrorMessage.Confirmed;
            mouduleClass.InitializeAction();
            _subClassModuleList.Add(mouduleClass);
            return true;
        }

        public bool TryFindSubMoudle<T>(out T subModuleOrNull)where T : SubMonoClassModule
        {
            foreach (SubMonoClassModule module in _subClassModuleList)
            {
                if(module is T moudleClass)
                {
                    subModuleOrNull = moudleClass;
                    return true;
                }
            }

            subModuleOrNull = null;
            return false;
        }

        protected void SubModulesStart()
        {
            foreach (var module in _subClassModuleList)
            {
                module.StartAction();
            }
        }

        protected void SubModulesUpdate()
        {
            foreach (var module in _subClassModuleList)
            {
                module.UpdateAction();
            }
        }

        protected void SubModulesOnEnable()
        {
            foreach (var module in _subClassModuleList)
            {
                module.OnEnableAction();
            }
        }

        protected void SubModulesOnDisable()
        {
            foreach (var module in _subClassModuleList)
            {
                module.OnDisableAction();
            }
        }

        protected void SubModulesOnDestroy()
        {
            foreach (var module in _subClassModuleList)
            {
                module.OnDestroyAction();
            }
        }



    }

}
