using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CodeGenAccess;
using CodeGenBuisness;
using Microsoft.SqlServer.Server;
using MyC_AndSQLLib;

namespace CodeGenBusiness
{
    public partial class clsTable
    {
        private async Task<string> BuildGetAllProcedureQuery()
        {
            StringBuilder queryBuilder = new StringBuilder();


            //Check Procedure Name
            if (await clsTables.IsProcedureExists($"Sp_GetAll{TableName}"))
                return null;
            else
            {



                queryBuilder.AppendLine($@"CREATE PROCEDURE Sp_GetAll{TableName}");

                queryBuilder.AppendLine();
                queryBuilder.AppendLine("AS");
                queryBuilder.AppendLine("BEGIN");
                queryBuilder.AppendLine("    BEGIN TRY");
                queryBuilder.AppendLine($"        Select * from {TableName}");

                queryBuilder.AppendLine("END TRY");

                //Handel Error
                queryBuilder.AppendLine("    BEGIN CATCH");
                queryBuilder.AppendLine("        DECLARE @ErrorMessage NVARCHAR(4000);");
                queryBuilder.AppendLine("        DECLARE @ErrorSeverity INT;");
                queryBuilder.AppendLine("        DECLARE @ErrorState INT;");
                queryBuilder.AppendLine("        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();");
                queryBuilder.AppendLine("        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);");
                queryBuilder.AppendLine("    END CATCH");
                queryBuilder.AppendLine("END");



                return queryBuilder.ToString();
            }
        }
        private async Task<string> BuildAddNewProcedureQuery()
        {

            // CREATE Procedure
            StringBuilder queryBuilder = new StringBuilder();

            //Check Procedure Name

            if (await clsTables.IsProcedureExists($"Sp_AddNew{TableName}"))
                return null;
            else
            {

                queryBuilder.AppendLine($@"CREATE PROCEDURE Sp_AddNew{TableName}");
                string PRIMETERName = "";
                foreach (clsColumn col in Columns)
                {

                    if (col.ColumnName.ToLower().Contains("id"))
                    {
                        continue;
                    }
                    queryBuilder.AppendLine($"    @{col.ColumnName} {clsSql.CsharbToSqlDataType(col.ColumnType)},");

                }

                queryBuilder.Length--;
                queryBuilder.AppendLine("    @NewAddClass INT OUTPUT");
                queryBuilder.AppendLine();
                queryBuilder.AppendLine("AS");
                queryBuilder.AppendLine("BEGIN");
                queryBuilder.AppendLine("    BEGIN TRY");
                queryBuilder.AppendLine($"        INSERT INTO {TableName} (");


                foreach (clsColumn col in Columns)
                {

                    if (col.ColumnName.ToLower().Contains("id"))
                    {
                        continue;
                    }
                    queryBuilder.Append($"{col.ColumnName}, ");
                    PRIMETERName += col.ColumnName + ",";

                }


                queryBuilder.Length -= 2;
                queryBuilder.Append(")");

                queryBuilder.AppendLine(" VALUES (");

                foreach (var c in PRIMETERName.Split(','))
                {

                    if (c != "")
                        queryBuilder.Append($"@{c},");


                }
                if (queryBuilder.Length > 0 && queryBuilder[queryBuilder.Length - 1] == ',')
                {
                    queryBuilder.Length--;
                }

                queryBuilder.AppendLine();

                queryBuilder.Append(");");

                queryBuilder.AppendLine("        SET @NewAddClass = SCOPE_IDENTITY();");
                queryBuilder.AppendLine("    END TRY");

                string sd = queryBuilder.ToString();

                queryBuilder.AppendLine("    BEGIN CATCH");
                queryBuilder.AppendLine("        DECLARE @ErrorMessage NVARCHAR(4000);");
                queryBuilder.AppendLine("        DECLARE @ErrorSeverity INT;");
                queryBuilder.AppendLine("        DECLARE @ErrorState INT;");
                queryBuilder.AppendLine("        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();");
                queryBuilder.AppendLine("        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);");
                queryBuilder.AppendLine("    END CATCH");
                queryBuilder.AppendLine("END");



                return queryBuilder.ToString();

            }
        }
        private async Task<string> BuiildUpdateProcedureQuery()
        {
            StringBuilder queryBuilder = new StringBuilder();

            //Check Procedure Name
            if (await clsTables.IsProcedureExists($"Sp_Update{TableName}"))
                return null;
            else
            {

                queryBuilder.AppendLine($@"CREATE PROCEDURE Sp_Update{TableName}");
                StringBuilder Primeter = new StringBuilder();
                clsColumn PrimCol = null;
                //Adding Prametear
                foreach (clsColumn col in Columns)
                {

                    if (col.IsPrimaryKey)
                        PrimCol = col;
                    else
                        Primeter.AppendLine($"{col.ColumnName} = @{col.ColumnName},");


                    queryBuilder.AppendLine($"     @{col.ColumnName} {clsSql.CsharbToSqlDataType(col.ColumnType)},");

                }


                //Remove Last Char ,
                if (queryBuilder.Length > 0 && queryBuilder.ToString().TrimEnd().EndsWith(","))
                {
                    queryBuilder.Length -= 3;
                }

                queryBuilder.AppendLine("\nAS ");
                queryBuilder.AppendLine("BEGIN");
                queryBuilder.AppendLine("    BEGIN TRY");
                queryBuilder.AppendLine($"\tUpdate {TableName} ");
                queryBuilder.AppendLine("SET");


                queryBuilder.AppendLine(Primeter.ToString());

                if (queryBuilder.Length > 0 && queryBuilder.ToString().TrimEnd().EndsWith(","))
                {
                    queryBuilder.Length -= 5;
                }


                queryBuilder.AppendLine($"\n Where {PrimCol.ColumnName} = @{PrimCol.ColumnName};\n");

                queryBuilder.AppendLine("IF @@ROWCOUNT=0");


                queryBuilder.AppendLine("BEGIN");
                queryBuilder.AppendLine("            RAISERROR('No record found with the provided ID.', 16, 1);");
                queryBuilder.AppendLine("END");


                queryBuilder.AppendLine("   END TRY");
                queryBuilder.AppendLine("   BEGIN CATCH");

                queryBuilder.AppendLine("        DECLARE @ErrorMessage NVARCHAR(4000);");
                queryBuilder.AppendLine("        DECLARE @ErrorSeverity INT;");
                queryBuilder.AppendLine("        DECLARE @ErrorState INT;");
                queryBuilder.AppendLine("        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();");
                queryBuilder.AppendLine("        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);");
                queryBuilder.AppendLine("    END CATCH");
                queryBuilder.AppendLine("END");




            }
            return queryBuilder.ToString();
        }
        //private async Task<string> BuildFindByNameProcedureQuery(string ColumnName = "")
        //{
        //    StringBuilder queryBuilder = new StringBuilder();

        //    //Check Procedure Name
        //    if (await clsTables.IsProcedureExists($"Sp_Get{TableName}ByName"))
        //        return null;
        //    else
        //    {
        //        queryBuilder.AppendLine($@"CREATE PROCEDURE Sp_Get{TableName}ByName");


        //        queryBuilder.AppendLine($"    @{ColumnName} NVARCHAR(100)");
        //        queryBuilder.AppendLine();
        //        queryBuilder.AppendLine("AS");
        //        queryBuilder.AppendLine("BEGIN");
        //        queryBuilder.AppendLine("    BEGIN TRY");
        //        queryBuilder.AppendLine($"        SELECT * from {TableName}");

        //        queryBuilder.AppendLine($"Where {ColumnName} = @{ColumnName};");

        //        queryBuilder.AppendLine("END TRY");

        //        //Handel Error
        //        queryBuilder.AppendLine("    BEGIN CATCH");
        //        queryBuilder.AppendLine("        DECLARE @ErrorMessage NVARCHAR(4000);");
        //        queryBuilder.AppendLine("        DECLARE @ErrorSeverity INT;");
        //        queryBuilder.AppendLine("        DECLARE @ErrorState INT;");
        //        queryBuilder.AppendLine("        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();");
        //        queryBuilder.AppendLine("        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);");
        //        queryBuilder.AppendLine("    END CATCH");
        //        queryBuilder.AppendLine("END");


        //        return queryBuilder.ToString();
        //    }
        //}
        private async Task<string> BuildFindByIDProcedureQuery()
        {
            StringBuilder queryBuilder = new StringBuilder();

            //Check Procedure Name
            if (await clsTables.IsProcedureExists($"Sp_Get{TableName}ByID"))
                return null;
            else
            {

                queryBuilder.AppendLine($@"CREATE PROCEDURE Sp_Get{TableName}ByID");


                queryBuilder.AppendLine("    @ID INT");
                queryBuilder.AppendLine();
                queryBuilder.AppendLine("AS");
                queryBuilder.AppendLine("BEGIN");
                queryBuilder.AppendLine("    BEGIN TRY");
                queryBuilder.AppendLine($"        SELECT * from {TableName}");



                queryBuilder.AppendLine($"Where {this._GetPrimaryKeyColumn().ColumnName}=@ID");

                queryBuilder.AppendLine("END TRY");

                //Handel Error
                queryBuilder.AppendLine("    BEGIN CATCH");
                queryBuilder.AppendLine("        DECLARE @ErrorMessage NVARCHAR(4000);");
                queryBuilder.AppendLine("        DECLARE @ErrorSeverity INT;");
                queryBuilder.AppendLine("        DECLARE @ErrorState INT;");
                queryBuilder.AppendLine("        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();");
                queryBuilder.AppendLine("        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);");
                queryBuilder.AppendLine("    END CATCH");
                queryBuilder.AppendLine("END");

                return queryBuilder.ToString();
            }
        }
        private async Task<string> BuildDeleteProcedureQuery()
        {
            StringBuilder queryBuilder = new StringBuilder();


            //Check Procedure Name

            if (await clsTables.IsProcedureExists($"Sp_Delete{TableName}"))
                return null;
            else
            {
                queryBuilder.AppendLine($@"CREATE PROCEDURE Sp_Delete{TableName}");
                queryBuilder.AppendLine("    @ID INT");
                queryBuilder.AppendLine();
                queryBuilder.AppendLine("AS");
                queryBuilder.AppendLine("BEGIN");
                queryBuilder.AppendLine("    BEGIN TRY");
                queryBuilder.AppendLine($"        Delete from {TableName} ");
                queryBuilder.AppendLine($"Where {this._GetPrimaryKeyColumn().ColumnName}=@ID");
                queryBuilder.AppendLine("IF @@ROWCOUNT=0");
                queryBuilder.AppendLine("BEGIN");
                queryBuilder.AppendLine("            RAISERROR('No records found to delete for the provided ID.', 16, 1);");
                queryBuilder.AppendLine("END");
                queryBuilder.AppendLine("END TRY");

                //Handel Error
                queryBuilder.AppendLine("    BEGIN CATCH");
                queryBuilder.AppendLine("        DECLARE @ErrorMessage NVARCHAR(4000);");
                queryBuilder.AppendLine("        DECLARE @ErrorSeverity INT;");
                queryBuilder.AppendLine("        DECLARE @ErrorState INT;");
                queryBuilder.AppendLine("        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();");
                queryBuilder.AppendLine("        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);");
                queryBuilder.AppendLine("    END CATCH");
                queryBuilder.AppendLine("END");

                string Mystr = queryBuilder.ToString();


                return queryBuilder.ToString();
            }
        }

        public static async Task<bool> ExecuteQuery(Task<string> TaskName)
        {

            return await clsTables.ExecuteQuery(TaskName);
        }

        public async Task<int> GenerateAllStoredProcedure(string ColumnName = "")
        {
            int Counter = 0;

            //if (!string.IsNullOrEmpty(ColumnName.Trim()))
            //{
            //    if (await ExecuteQuery(BuildFindByNameProcedureQuery())) ;
            //    Counter++;
            //}
            if (await ExecuteQuery(BuildAddNewProcedureQuery()))
                Counter++;
            if (await ExecuteQuery(BuildDeleteProcedureQuery()))
                Counter++;
            if (await ExecuteQuery(BuiildUpdateProcedureQuery()))
                Counter++;
            if (await ExecuteQuery(BuildGetAllProcedureQuery()))
                Counter++;
            if (await ExecuteQuery(BuildFindByIDProcedureQuery()))
                Counter++;
            return Counter;


        }


    }
}
