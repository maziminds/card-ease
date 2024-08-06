using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class V3CardZonManager : CardEase.CardZoneManager<V3CardModel>
    {
        [Header("--------------Prefabs----------------")]
        [Tooltip("use of prefab")]
        [SerializeField] GameObject somePreFab;

        [Header("--------------Data----------------")]
        [Tooltip("use or meaning of variable")]
        [SerializeField] bool someBoolean;
        [SerializeField] int someInt;


        // --------------------------MONO methods------------------------
        #region methods related to MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
        }

        void Start()
        {

        }

        void Update()
        {

        }
        #endregion



        // --------------------------HELPER METHODS------------------------
        #region it's mostly public and can be used by other classes as well
        public void SomeMethod()
        {

        }
        #endregion


        //---------------------------ENUMERATORs--------------------------
        #region all enumerators used in this class(could be private, public)
        private IEnumerator SomeEnumerator()
        {
            yield return new WaitForSecondsRealtime(1f);
        }
        #endregion
    }
}