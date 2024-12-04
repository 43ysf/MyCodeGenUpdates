using SqlLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinsLayer
{
    public class clsSaveMethod
    {
        //Business Layer..
        public static string GenerateSaveMethod()
        {
            StringBuilder methodBuilder = new StringBuilder();
            methodBuilder.AppendLine("        public async Task<bool> Save()");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            switch (Mode)");
            methodBuilder.AppendLine("            {");
            methodBuilder.AppendLine("                case enMode.addNew:");
            methodBuilder.AppendLine("                        Mode = enMode.update;");
            methodBuilder.AppendLine($"                     return await _AddNew{ClsGloble.GetTableName}();");
            methodBuilder.AppendLine("                case enMode.update:");
            methodBuilder.AppendLine($"                    return await _Update{ClsGloble.GetTableName}();");

            methodBuilder.AppendLine("            }");
            methodBuilder.AppendLine("            return false;");
            methodBuilder.AppendLine("        }");
            return methodBuilder.ToString();
        }


        //Project Is Not Async

        //Business Layer
        public static string GenerateSaveMethodIsNotAsync()
        {
            StringBuilder methodBuilder = new StringBuilder();
            methodBuilder.AppendLine("        public bool Save()");
            methodBuilder.AppendLine("        {");
            methodBuilder.AppendLine("            switch (Mode)");
            methodBuilder.AppendLine("            {");
            methodBuilder.AppendLine("                case enMode.addNew:");
            methodBuilder.AppendLine($"                    if (_AddNew{ClsGloble.GetTableName}())");
            methodBuilder.AppendLine("                    {");
            methodBuilder.AppendLine("                        Mode = enMode.update;");
            methodBuilder.AppendLine("                        return true;");
            methodBuilder.AppendLine("                    }");
            methodBuilder.AppendLine("                    else");
            methodBuilder.AppendLine("                    {");
            methodBuilder.AppendLine("                        return false;");
            methodBuilder.AppendLine("                    }");
            methodBuilder.AppendLine("                case enMode.update:");
            methodBuilder.AppendLine($"                    return _Update{ClsGloble.GetTableName}();");
            methodBuilder.AppendLine("            }");
            methodBuilder.AppendLine("            return false;");
            methodBuilder.AppendLine("        }");
            return methodBuilder.ToString();
        }


    }
}
