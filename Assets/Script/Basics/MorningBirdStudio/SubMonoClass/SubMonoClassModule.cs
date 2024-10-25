using Cysharp.Threading.Tasks;
using MorningBird.IO;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorningBird.SubMonoClass
{
    public abstract class SubMonoClassModule : SerializedMonoBehaviour
    {
        [FoldoutGroup("Debug")]
        [FoldoutGroup("Debug/Module")]
        [ShowInInspector] SubMonoClassCore _coreClass;
        [FoldoutGroup("Debug/Module/Initialize")]
        [ShowInInspector] public bool IsInitialized { protected set; get; } = false;
        [FoldoutGroup("Debug/Module/Initialize")]
        [ShowInInspector] public bool IsStartActioned { protected set; get; } = false;
        [FoldoutGroup("Debug/Module/Condition")]
        [SerializeField] bool _canUpdate = true;
        public bool CanUpdate
        {
            set { _canUpdate = value; }
            get { return _canUpdate; }
        }

        protected virtual async void Awake()
        {
            if (IsInitialized == true)
            {
                return;
            }

            _coreClass = this.transform.GetComponent<SubMonoClassCore>();

            if (_coreClass == null)
            {
                LogManager.ReportLogAssertion(new LogFormet("Something Wrong Player is null", ELogLevel.Error));
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                await UniTask.WaitForSeconds(0.25f);

                if (_coreClass.RequestApproach(this, out ESubModuleRequestErrorMessage errorMessage) == true)
                {
                    break;
                }

                Debug.Assert(i >= 9, "player is not ready for regist this. the code is not gonna work" + errorMessage, this);
            }

        }

        #region Built In Methud

        public void InitializeAction()
        {
            if(IsInitialized == true)
            {
                return;
            }

            InitializeExcution();

            IsInitialized = true;
        }
        protected abstract void InitializeExcution();

        public void StartAction()
        {
            if (IsInitialized == false)
            {
                InitializeAction();
            }

            if(IsStartActioned == true)
            {
                return;
            }

            StartExcution();

            IsStartActioned = true;
        }
        protected abstract void StartExcution();

        public void UpdateAction()
        {
            if(CanUpdate == false)
            {
                return;
            }

            UpdateExcution();
        }
        protected abstract void UpdateExcution();

        public void OnEnableAction()
        {
            OnEnableExcution();
        }
        protected abstract void OnEnableExcution();

        public void OnDisableAction()
        {
            OnDisableExcution();
        }
        protected abstract void OnDisableExcution();

        public void OnDestroyAction()
        {
            OnDestroyExcution();
        }
        protected abstract void OnDestroyExcution();

        #endregion

        #region Module Controll


        #endregion

    }
}