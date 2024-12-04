//using SqlLayer;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BussinsLayer
//{
//    public class ClsGenericAllProceses
//    {
//        public static async Task<int> ProsessAll(string SerachByName = "")
//        {
//            int Count = 0;

//            // Execute Query

//            if (SerachByName != "")
//                if (await ClsGloble.ExecuteQuery(ClssearchByName.BuildCreateProcedureQuery(SerachByName)))
//                    Count++;

//            if (await ClsGloble.ExecuteQuery(ClsCreat.BuildCreateProcedureQuery()))
//                Count++;

//            if (await ClsGloble.ExecuteQuery(ClsDelete.BuildCreateProcedureQuery()))
//                Count++;

//            if (await ClsGloble.ExecuteQuery(ClsGetAll.BuildCreateProcedureQuery()))
//                Count++;

//            if (await ClsGloble.ExecuteQuery(clsSreachByID.BuildCreateProcedureQuery()))
//                Count++;

//            if (await ClsGloble.ExecuteQuery(Clsupdate.BuildCreateProcedureQuery()))
//                Count++;

//            return Count;

//        }

//    }
//}
